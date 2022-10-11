using KMath;
using System;

namespace Collisions
{
    public class CircleLineCollision
    {



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


        public static float TestCollision(Vec2f circleCenter, float circleRadius, Vec2f velocity, Vec2f p1, Vec2f p2)
        {

            // step 1: p1 is the origin
            // offset everything by p1

            p2 -= p1;
            circleCenter -= p1;
            p1 -= p1;

            // step 2: compute the angle of rotation
            // so that segment [p1, p2] is vertical

            float O = MathF.Atan(p2.Y / p2.X);
            float diff = MathF.PI * 0.5f - O;

            // step3: apply the rotation

            circleCenter = rotatePoint(circleCenter, diff);
            p2 = rotatePoint(p2, diff);
            velocity = RotateVector2d(velocity, diff);

            // step 4: circle center is the origin
            // offset everything by the circle center

            p1 -= circleCenter;
            p2 -= circleCenter;
            circleCenter -= circleCenter;

            float k = p1.X;



            // step 5: check if the line is on the right or on the left of the circle

            float timeX = 1.0f;
            if (k >= 0)
            {
                // if the line is on the right 
                // the time is (k-circleRadius) / velocity.X

                timeX = (k-circleRadius) / velocity.X;
            }
            else
            {
                // if the line is on the left 
                // the time is (k+circleRadius) / velocity.X

                timeX = (k+circleRadius) / velocity.X;
            }
                

            // used to remove very small numbers that are supposed to be 0
            float epsilon = 0.001f;

            // if the time is more than 1 or less than epsillon
            // that means we dont collide and the time should be 1
            if (timeX >= 1.0f || timeX <= epsilon)
            {
                timeX = 1.0f;
            }

            // compute the point of collision with the static line [p1, p2]
            Vec2f stopPoint = circleCenter + velocity * timeX;
            stopPoint.X += circleRadius;

            

            // if the circle intersects the line we also need to see if 
            // it managed to hit the segment [p1, p2]
            // the line is infinite but the segment is not
            if (timeX < 1.0f)
            {

                // segment [p1, p2]
                Line2D line = new Line2D(p1, p2);
                // 2 points that form the limits of the moving circle
                // used to check if one of them intersect the static line
                Vec2f circleA = circleCenter + (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;
                Vec2f circleB = circleCenter - (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;

                // the lines are just the 2 points + velocity
                Line2D line1 = new Line2D(circleA, circleA + velocity);
                Line2D line2 = new Line2D(circleB, circleB + velocity);

                // check if the line is between the acceptable range of the moving circle
                if (!(line1.Intersects(line) || line2.Intersects(line)))
                {
                    // does not collide
                    timeX = 1.0f;
                }
                            
            }

            return timeX;
        }
    }
}