using KMath;
using System;

namespace Collisions
{
    public class CircleLineCollision_
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


        public static float TestCollision(Vec2f point, float circleRadius, Vec2f velocity, Vec2f p1, Vec2f p2)
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

                


            // if the time is more than 1 or less than epsillon
            // that means we dont collide and the time should be 1
            if (timeX >= 1.0f)
            {
                timeX = 1.0f;
            }

            if (timeX <= -1.0f)
            {
                timeX = -1.0f;
            }


            
            Vec2f perp = new Vec2f(velocity.Y, -velocity.X).Normalize();
            Vec2f pointA = point + perp * circleRadius;
            Vec2f pointB = point - perp * circleRadius;

            float pointATime = (k - pointA.X) / velocity.X;
            float pointBTime = (k - pointB.X) / velocity.X;

            float circleLimitA = pointA.Y + velocity.Y * pointATime;
            float circleLimitB = pointB.Y + velocity.Y * pointBTime;

            float difference = MathF.Abs(MathF.Max(circleLimitB, MathF.Max(circleLimitA, MathF.Max(p1.Y, p2.Y))) - 
            MathF.Min(circleLimitB, MathF.Min(circleLimitA, MathF.Min(p1.Y, p2.Y))));

            Line2D line = new Line2D(p1, p2);
            Vec2f stopPoint = point + velocity * timeX;

            if (k >= 0)
            {
                stopPoint.X += circleRadius;
            }
            else
            {
                stopPoint.X -= circleRadius;
            }   

            UnityEngine.Debug.Log("onLine : " + line.OnLine(stopPoint));

            if (!line.OnLine(stopPoint))
            {
                timeX = 1.0f;

                 RayCastResult rs1 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p1, circleRadius);
                 RayCastResult rs2 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p2, circleRadius);

                 float time1 = rs1.Intersect ? (rs1.Point.X - point.X) / velocity.X : 1.0f;
                 float time2 = rs2.Intersect ? (rs2.Point.X - point.X) / velocity.X : 1.0f;

                 timeX = MathF.Min(time1, time2);


                /*float distanceToMove = MathF.Min(MathF.Abs(p1.Y - stopPoint.Y), MathF.Abs(p2.Y - stopPoint.Y));

                float mag1 = (p1 - stopPoint).Magnitude;
                float mag2 = (p2 - stopPoint).Magnitude;

                float x1;
                float y1;

                if (mag1 < mag2)
                {
                    x1 = p1.X;
                    y1 = p1.Y;
                }
                else
                {
                    x1 = p2.X;
                    y1 = p2.Y;
                }

                float a = velocity.Y / velocity.X;

                float A = 1 + (a * a);
                float B = -2 * x1 + -2 * y1 * a;
                float C = (x1 * x1) + (y1 * y1) - (circleRadius * circleRadius);

                float delta = (B * B) - 4 * A * C;

                if (delta < 0)
                {
                   // timeX = 1.0f;
                }
                else if (delta == 0)
                {
                    float x2 = (-B) / (2 * A);
                    float y2 = a * x2;

                    timeX = (x2) / velocity.X; 
                }
                else if (delta > 0)
                {
                    if (k >= 0)
                    {
                        float x2 = (-B - MathF.Sqrt(delta)) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }
                    else
                    {
                        float x2 = (-B + MathF.Sqrt(delta)) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }
                }*/
            }
            else if (difference > (MathF.Abs((circleLimitA - circleLimitB)) + MathF.Abs(p2.Y - p1.Y)))
            {
                timeX = 1.0f;
            }

           /* if (difference > (MathF.Abs((circleLimitA - circleLimitB)) + MathF.Abs(p2.Y - p1.Y)))
            {
                // no collision
                timeX = 1.0f;
            }
            else
            {

               
            }*/




            return timeX;
        }



        public static float TestCollision(Vec2f point, Vec2f velocity, Vec2f p1, Vec2f p2, float cos, float sin)
        {

            // step 1: p1 is the origin
            // offset everything by p1

            p2 -= p1;
            point -= p1;
            p1 -= p1;

            // step3: apply the rotation

            point = rotatePoint(point, cos, sin);
            p2 = rotatePoint(p2, cos, sin);
            velocity = RotateVector2d(velocity, cos, sin);

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


                

        static Vec2f RotateVector2d(Vec2f vector, float cos, float sin)
        {
            Vec2f result = new Vec2f();
            result.X = vector.X * cos - vector.Y * sin;
            result.Y = vector.X * sin + vector.Y * cos;

            return result;
        }

        static Vec2f RotateVector2d(Vec2f vector, float angle)
        {
            Vec2f result = new Vec2f();
            result.X = vector.X * MathF.Cos(angle) - vector.Y * MathF.Sin(angle);
            result.Y = vector.X * MathF.Sin(angle) + vector.Y * MathF.Cos(angle);

            return result;
        }

        public static Vec2f rotatePoint(Vec2f pos, float cos, float sin)
        {
            Vec2f newv;
            newv.X = pos.X * cos - pos.Y * sin;
            newv.Y = pos.X * sin + pos.Y * cos;

            return newv;
        }

        public static Vec2f rotatePoint(Vec2f pos, float angle) 
        {
            Vec2f newv;
            newv.X = pos.X * MathF.Cos(angle) - pos.Y * MathF.Sin(angle);
            newv.Y = pos.X * MathF.Sin(angle) + pos.Y * MathF.Cos(angle);

            return newv;
        }
    }
}