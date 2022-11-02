using KMath;
using System;

namespace Collisions
{

    public class LineLineSweepTest
    {


        public static float TestCollision(Line2D line, Vec2f velocity, Vec2f p1, Vec2f p2)
        {
            // check if the 2 points forming the moving segment collide with the line [p1, p2]
            float distance1 = PointLineCollision.TestCollision(line.A, velocity, p1, p2);
            float distance2 = PointLineCollision.TestCollision(line.B, velocity, p1, p2);

            // check if the 2 points forming the static segment collide with the moving line
            // we are using -velocity instead of velocity here
            float distance3 = PointLineCollision.TestCollision(p1, -velocity, line.A, line.B);
            float distance4 = PointLineCollision.TestCollision(p2, -velocity, line.A, line.B);

            // return the minimum collision time
            return MathF.Min(MathF.Min(distance3, MathF.Min(distance1, distance2)), distance4);
        }
    }
}