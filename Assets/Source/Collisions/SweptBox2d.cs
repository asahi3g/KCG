
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

        public static void SweptBox2dCollision(ref Box2D b1, Vec2f delta, Box2D b2)
        {
            // Swept collision  detection using Minkowski sum


            Vec2f b1Center = new Vec2f(b1.x - b2.x, b1.y - b2.y);

            // the sum of b2 and b1 (b1 swept across b2)
            Vec2f dimensions = new Vec2f(b2.w + b1.w, b2.h + b1.h);
            Vec2f minCorner = -0.5f * dimensions;
            Vec2f maxCorner = 0.5f * dimensions;


            Vec2f normal = new Vec2f();
            float timeRemaining = 1.0f;
            bool hit = false;

            for(int iteration = 0; iteration < 4 && timeRemaining > 0.0f; iteration++)
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

        }
  
        public static float SweptBox2d(Box2D b1, Vec2f b1Velocity, Box2D b2)
        {
            float xInvEntry, yInvEntry; 
            float xInvExit, yInvExit; 

            // find the distance between the objects on the near and far sides for both x and y 
            if (b1Velocity.X > 0.0f) 
            { 
                xInvEntry = b2.x - (b1.x + b1.w);  
                xInvExit = (b2.x + b2.w) - b1.x;
            }
            else 
            { 
                xInvEntry = (b2.x + b2.w) - b1.x;  
                xInvExit = b2.x - (b1.x + b1.w);  
            } 

            if (b1Velocity.Y > 0.0f) 
            { 
                yInvEntry = b2.y - (b1.y + b1.h);  
                UnityEngine.Debug.Log("b2 " + b2.y + " " + (b1.y + b1.h));
                 yInvExit = (b2.y + b2.h) - b1.y;  
            }
            else 
            { 
                yInvEntry = (b2.y + b2.h) - b1.y;  
                yInvExit = b2.y - (b1.y + b1.h);  
            }


            // find time of collision and time of leaving for each axis (if statement is to prevent divide by zero) 
            float xEntry, yEntry; 
            float xExit, yExit; 

            if (b1Velocity.X == 0.0f) 
            { 
                xEntry = -float.PositiveInfinity; 
                xExit = float.PositiveInfinity; 
            } 
            else 
            { 
                xEntry = xInvEntry / b1Velocity.X; 
                xExit = xInvExit / b1Velocity.X; 
            } 

            if (b1Velocity.Y == 0.0f) 
            { 
                yEntry = -float.PositiveInfinity; 
                yExit = float.PositiveInfinity; 
            } 
            else 
            { 
                yEntry = yInvEntry / b1Velocity.Y; 
                yExit = yInvExit / b1Velocity.Y; 
            }

            // find the earliest/latest times of collisionfloat
            float entryTime = Math.Max(xEntry, yEntry); 
            float exitTime = Math.Max(xExit, yExit);


            // if there was no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f) 
            {  
                return 1.0f; 
            }


            return entryTime;
        }


    }
}