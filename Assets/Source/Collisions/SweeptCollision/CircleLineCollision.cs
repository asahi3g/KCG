using KMath;
using System;

namespace Collisions
{
    public class CircleLineCollision
    {

        //collision between moving 2d circle and stationary 2d line segment
        
        // input:
        // - Vec2f point: vector that represents 1 points (x, y)
        // - float circleRadius: represents the radius of the circle
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

        public struct Result
        {
            public Vec2f Normal;
            public float Time;
        }


        public static Result TestCollision(Vec2f point, float circleRadius, Vec2f velocity, Vec2f p1, Vec2f p2)
        {

            Result result = new Result();

            // step 1: p1 is the origin
            // offset everything by p1

            // keep the original values
            Vec2f tmpP1 = p1;
            Vec2f originalPoint = point;

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

            // step 4: circle center is the origin
            // offset everything by the circle center

            // keep the temporary values
            Vec2f tmpPoint = point;

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

                


            // used to remove very small numbers that are supposed to be 0
            float epsilon = 0.001f;

            // if the time is more than 1 or less than epsillon
            // that means we dont collide and the time should be 1
            if (timeX >= 1.0f || timeX <= epsilon)
            {
                timeX = 1.0f;
            }


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



            if (!line.OnLine(stopPoint))
            {
                // testing collision against p1 and p2
                // raycasting the circle against p1 and p2

                RayCastResult rs1 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p1, circleRadius);
                RayCastResult rs2 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p2, circleRadius);

                 // get time of collision against p1
                 float time1 = rs1.Intersect ? (rs1.Point.X - point.X) / velocity.X : 1.0f;
                 // get time of collision against p2
                 float time2 = rs2.Intersect ? (rs2.Point.X - point.X) / velocity.X : 1.0f; 

                 if (time1 < time2)
                 {
                    stopPoint = p1;
                    timeX = time1;
                 }
                 else
                 {
                    stopPoint = p2;
                    timeX = time2;
                 }

            }

           

            result.Time = timeX;

            Vec2f collisionPoint = stopPoint;
            collisionPoint += tmpPoint;
            collisionPoint = rotatePoint(collisionPoint, -diff);
            collisionPoint += tmpP1;


            result.Normal = (collisionPoint - originalPoint).Normalize();


            return result;
        }



       public static float TestCollision(Vec2f point, float circleRadius, Vec2f velocity, Vec2f p1, Vec2f p2, float cos, float sin)
        {

            // step 1: p1 is the origin
            // offset everything by p1

            p2 -= p1;
            point -= p1;
            p1 -= p1;

            // step2: apply the rotation

            point = rotatePoint(point, cos, sin);
            p2 = rotatePoint(p2, cos, sin);
            velocity = RotateVector2d(velocity, cos, sin);

            // step 3: point center is the origin
            // offset everything by the point

            p1 -= point;
            p2 -= point;
            point -= point;

            // step 4: compute the time of collision
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


            if (!line.OnLine(stopPoint))
            {
                // testing collision against p1 and p2
                // raycasting the circle against p1 and p2

                RayCastResult rs1 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p1, circleRadius);
                RayCastResult rs2 = Collisions.RayCastAgainstCircle(new Line2D(point, point + velocity), p2, circleRadius);

                 // get time of collision against p1
                 float time1 = rs1.Intersect ? (rs1.Point.X - point.X) / velocity.X : 1.0f;
                 // get time of collision against p2
                 float time2 = rs2.Intersect ? (rs2.Point.X - point.X) / velocity.X : 1.0f; 

                 timeX = MathF.Min(time1, time2);
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
            newv.X = pos.X * System.MathF.Cos(angle) - pos.Y * System.MathF.Sin(angle);
            newv.Y = pos.X * System.MathF.Sin(angle) + pos.Y * System.MathF.Cos(angle);

            return newv;
        }
    }
}