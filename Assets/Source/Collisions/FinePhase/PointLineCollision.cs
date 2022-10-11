using KMath;
using System;

namespace Collisions
{
    public class PointLineCollision
    {

        //collision between moving 2d point and stationary 2d line segment
        
        // input:
        // - Vec2f point: vector that represents 1 points (x, y)
        // - Vec2f velocity: vector that represents the point velocity
        // - Vec2f p1 : vector that represent the first point of the segment [p1, p2]
        // - Vec2f p2: vector that represents the 2nd point of the segment [p1, p2]

        // output:
        // time of collision [0, 1]


        // steps

        // step 1: p1 is the origin
        // offset everything by p1

        // step 2: compute the angle of rotation
        // so that segment [p1, p2] is vertical

        // step3: apply the rotation

        // step 4: point center is the origin
        // offset everything by the point

        // step 5: compute the time of collision

        // step 6: see if the collision is valid and return the time of collision


        public static float TestCollision(Vec2f point, Vec2f velocity, Vec2f p1, Vec2f p2)
        {

            // step 1: p1 is the origin
            // offset everything by p1

            p2 -= p1;
            point -= p1;
            p1 -= p1;

            // step 2: compute the angle of rotation
            // so that segment [p1, p2] is vertical

            float O = MathF.Atan(p2.Y / p2.X);
            float diff = MathF.PI * 0.5f - O;

            // step3: apply the rotation

            point = rotatePoint(point, diff);
            p2 = rotatePoint(p2, diff);
            velocity = RotateVector2d(velocity, diff);

            // step 4: point center is the origin
            // offset everything by the point

            p1 -= point;
            p2 -= point;
            point -= point;

            // step 5: compute the time of collision
            float k = p1.X;
            float timeX = timeX = (k) / velocity.X;
                

            // used to remove very small numbers that are supposed to be 0
            float epsilon = 0.001f;

            // if the time is more than 1 or less than epsillon
            // that means we dont collide and the time should be 1
            if (timeX >= 1.0f || timeX <= epsilon)
            {
                timeX = 1.0f;
            }

            // step 6: see if the collision is valid

            // if the point intersects the line we also need to see if 
            // it managed to hit the segment [p1, p2]
            // the line is infinite but the segment is not
            if (timeX < 1.0f)
            {

                // segment [p1, p2]
                Line2D staticLine = new Line2D(p1, p2);

                // the line is just the point and the point + velocity
                Line2D movingLine = new Line2D(point, point + velocity);

                // check if the line intersects the segment [p1, p2]
                if (!(movingLine.Intersects(staticLine)))
                {
                    // does not collide
                    timeX = 1.0f;
                }
                            
            }

            return timeX;
        }


                

        static Vec2f RotateVector2d(Vec2f vector, float angle)
        {
            Vec2f result = new Vec2f();
            result.X = vector.X * MathF.Cos(angle) - vector.Y * MathF.Sin(angle);
            result.Y = vector.X * MathF.Sin(angle) + vector.Y * MathF.Cos(angle);

            return result;
        }

        public static Vec2f rotatePoint(Vec2f pos, float angle) 
        {
            Vec2f newv;
            newv.X = pos.X * System.MathF.Cos(angle) - pos.Y * System.MathF.Sin(angle);
            newv.Y = pos.X * System.MathF.Sin(angle) + pos.Y * System.MathF.Cos(angle);

            return newv;
        }
    }
}