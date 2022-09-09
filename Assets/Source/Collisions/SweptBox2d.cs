
using Entitas;
using KMath;
using System;

namespace Collisions
{


   

    public static partial class Collisions
    {

        public static bool TestLine(float lineX, float x, float y, float deltaX, float deltaY,
        ref float tMin, float minY, float maxY)
        {
            bool hit = false;
            float tEpsilon = 0.01f;
            if(deltaX != 0.0f)
            {
                float tResult = (lineX - x) / deltaX;
                float Y = y + tResult*deltaY;
                if((tResult >= 0.0f) && (tMin > tResult))
                {
                    if((Y >= minY) && (Y <= maxY))
                    {
                        tMin = Math.Max(0.0f, tResult - tEpsilon);
                        hit = true;
                    }
                }
            }
            
            return hit;
        }

        public static bool SweptBox2dCollision(ref Box2D b1, Vec2f delta, Box2D b2, bool slide)
        {
            
           /* bool collided = false;
            Vec2f d = delta;
            float stepSize = 0.1f;
            while(d.X > 0 && d.Y > 0)
            {
                Vec2f thisDelta = new Vec2f(Math.Max(stepSize, d.X), Math.Max(stepSize, d.Y));
                AABox2D b1AABB = new AABox2D(new Vec2f(b1.x, b1.y) + thisDelta, new Vec2f(b1.w, b1.h));
                AABox2D b2AABB = new AABox2D(new Vec2f(b2.x, b2.y), new Vec2f(b2.w - 0.01f, b2.h - 0.01f));

                if (Collisions.RectOverlapRect(b1AABB.xmin, b1AABB.xmax, b1AABB.ymin, b1AABB.ymax,
                        b2AABB.xmin, b2AABB.xmax, b2AABB.ymin, b2AABB.ymax))
                {
                    Vec2f centerB2 = new Vec2f(b2AABB.xmin + b2.w * 0.5f, b2AABB.ymin + b2.h * 0.5f);
                    Vec2f centerB1 = new Vec2f(b1AABB.xmin + b1.w * 0.5f, b1AABB.ymax + b1.h * 0.5f);

                    Vec2f dir = (-thisDelta).Normalized;
                    Vec2f magnitude = new Vec2f((b1.w + b2.w) * 0.5f - Math.Abs((centerB2.X - centerB1.X)), (b1.h + b2.h) * 0.5f - Math.Abs((centerB2.Y - centerB1.Y)));
                    b1.x = b1.x + dir.X * magnitude.X;
                    b1.y = b1.y + dir.Y * magnitude.Y;
                    collided = true;
                }

                d -= stepSize;

            }

            if (collided)
            {
                return true;
            }*/

            // Swept collision  detection using Minkowski sum

            Vec2f b1Center = new Vec2f(b1.x - b2.x, b1.y - b2.y);

            // the sum of b2 and b1 (b1 swept across b2)
            Vec2f dimensions = new Vec2f(b2.w + b1.w, b2.h + b1.h);
            Vec2f minCorner = -0.5f * dimensions;
            Vec2f maxCorner = 0.5f * dimensions;


            Vec2f normal = new Vec2f();
            float timeRemaining = 1.0f;
            bool hit = false;

            int numberOfIterations = 1;

            if (slide)
            {
                numberOfIterations = 4;
            }

            for(int iteration = 0; iteration < numberOfIterations && timeRemaining > 0.0f; iteration++)
            {
                float tMin = 1.0f;

                // raycasting the point across all box2d lines
                if (TestLine(minCorner.X, b1Center.X, b1Center.Y, delta.X, delta.Y, ref tMin, minCorner.Y, maxCorner.Y))
                {
                    normal = new Vec2f(-1.0f, 0.0f);
                    hit = true;
                }

                if (TestLine(maxCorner.X, b1Center.X, b1Center.Y, delta.X, delta.Y, ref tMin, minCorner.Y, maxCorner.Y))
                {
                    normal = new Vec2f(1.0f, 0.0f);
                    hit = true;
                }

                if (TestLine(minCorner.Y, b1Center.Y, b1Center.X, delta.Y, delta.X, ref tMin, minCorner.X, maxCorner.X))
                {
                    normal = new Vec2f(0.0f, -1.0f);
                    hit = true;
                }

                if (TestLine(maxCorner.Y, b1Center.Y, b1Center.X, delta.Y, delta.X, ref tMin, minCorner.X, maxCorner.X))
                {
                    normal = new Vec2f(0.0f, 1.0f);
                    hit = true;
                }

                b1.x += delta.X * tMin * timeRemaining;
                b1.y += delta.Y * tMin * timeRemaining;
                delta = delta - 1.0f * Vec2f.Dot(delta, normal) * normal;
                timeRemaining -= tMin * timeRemaining;
                
            }

            return hit;
        }


    }
}