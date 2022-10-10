using KMath;
using System;

namespace Collisions
{

    public class LineLineSweepTest
    {


        public static float TestCollision(Line2D line, Vec2f velocity, Vec2f p1, Vec2f p2)
        {
            float distance1 = CircleLineCollision.TestCollision(line.A, 0.0f, velocity, p1, p2);
            float distance2 = CircleLineCollision.TestCollision(line.B, 0.0f, velocity, p1, p2);


            return MathF.Min(distance1, distance2);
        }
    }
}