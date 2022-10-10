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

            p2 -= p1;
            circleCenter -= p1;
            p1 -= p1;

            float O = MathF.Atan(p2.Y / p2.X);
            float diff = MathF.PI * 0.5f - O;

            circleCenter = rotatePoint(circleCenter, diff);
            p2 = rotatePoint(p2, diff);
            velocity = RotateVector2d(velocity, diff);


            p1 -= circleCenter;
            p2 -= circleCenter;
            circleCenter -= circleCenter;

            float k = p1.X;

            if (k >= 0)
            {

                float timeX = (k-circleRadius) / velocity.X;
                float impactTimeX = (k) / velocity.X;

                if (timeX >= 1.0f || timeX < 0.0f)
                {
                    timeX = 1.0f;
                }

                Vec2f stopPoint = circleCenter + velocity * timeX;
                stopPoint.X += circleRadius;

                Line2D line = new Line2D(p1, p2);
                if (!line.OnLine(stopPoint) && timeX < 1.0f)
                {

                    Vec2f circleA = circleCenter + (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;
                    Vec2f circleB = circleCenter - (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;
                    Line2D line1 = new Line2D(circleA, circleA + velocity);
                    Line2D line2 = new Line2D(circleB, circleB + velocity);

                    if (!(line1.Intersects(line) || line2.Intersects(line)))
                    {
                        timeX = 1.0f;
                    }






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
                        timeX = 1.0f;
                    }
                    else if (delta == 0)
                    {
                        float x2 = (-B) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }
                    else if (delta > 0)
                    {
                        float x2 = (-B - MathF.Sqrt(delta)) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }*/
                              
                }
                 return timeX;

            }
            else
            {
                float timeX = (k+circleRadius) / velocity.X;
                float impactTimeX = (k) / velocity.X;

                if (timeX >= 1.0f || timeX < 0.0f)
                {
                    timeX = 1.0f;
                }

                Vec2f stopPoint = circleCenter + velocity * timeX;
                stopPoint.X -= circleRadius;


                Line2D line = new Line2D(p1, p2);
                if (!line.OnLine(stopPoint) && timeX < 1.0f)
                {
                    Vec2f circleA = circleCenter + (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;
                    Vec2f circleB = circleCenter - (new Vec2f(velocity.Y, -velocity.X)).Normalize() * circleRadius;
                    Line2D line1 = new Line2D(circleA, circleA + velocity);
                    Line2D line2 = new Line2D(circleB, circleB + velocity);

                    if (!(line1.Intersects(line) || line2.Intersects(line)))
                    {
                        timeX = 1.0f;
                    }






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
                        timeX = 1.0f;
                    }
                    else if (delta == 0)
                    {
                        float x2 = (-B) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }
                    else if (delta > 0)
                    {
                        float x2 = (-B + MathF.Sqrt(delta)) / (2 * A);
                        float y2 = a * x2;

                        timeX = (x2) / velocity.X; 
                    }*/

                }


                return timeX;
            }
        }
    }
}