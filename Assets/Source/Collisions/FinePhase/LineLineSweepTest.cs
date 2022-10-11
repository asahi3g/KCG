using KMath;
using System;

namespace Collisions
{

    public class LineLineSweepTest
    {


        public static float TestCollision(Line2D line, Vec2f velocity, Vec2f p1, Vec2f p2)
        {
            // check if the 2 points forming the moving segment collide with the line [p1, p2]
            float distance1 = CircleLineCollision.TestCollision(line.A, 0.0f, velocity, p1, p2);
            float distance2 = CircleLineCollision.TestCollision(line.B, 0.0f, velocity, p1, p2);

            // check if the 2 points forming the static segment collide with the moving line
            // we are using -velocity instead of velocity here
            float distance3 = CircleLineCollision.TestCollision(p1, 0.0f, -velocity, line.A, line.B);
            float distance4 = CircleLineCollision.TestCollision(p2, 0.0f, -velocity, line.A, line.B);

            // return the minimum collision time
            return MathF.Min(MathF.Min(distance3, MathF.Min(distance1, distance2)), distance4);
        }
    }
}