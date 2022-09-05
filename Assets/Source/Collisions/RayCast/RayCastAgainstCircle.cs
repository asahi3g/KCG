using KMath;
using System;

namespace Collisions
{

    public static partial class Collisions
    {
        public static RayCastResult RayCastAgainstCircle(Line2D line, Vec2f circleCenter, float radius)
        {
            RayCastResult result = new RayCastResult();

            Vec2f dir = (line.B - line.A).Normalized;

            // offset starting point to circle coordinates
            Vec2f s = line.A - circleCenter;


            float b = Vec2f.Dot(dir, s);
            float c = Vec2f.Dot(s, s) - radius * radius;
            float h = b * b - c;
            float distance = 0.0f;
            if (h < 0.0f)
            {
                // no intersection
                distance = -1.0f;
            }
            else
            {
                h = (float)Math.Sqrt(h);
                float t = -b - h;

                distance = Math.Max(t, 0);
            }

            // starting point is not inside circle
            // and there is an intersection
            if (distance > 0)
            {
                float maxDistance = (line.B - line.A).Magnitude;
                Vec2f point = dir * distance + s + circleCenter;

                if (distance <= maxDistance)
                {
                    result.Intersect = true;
                    result.Point = point;
                }
            }

            return result;
        }
    }
}