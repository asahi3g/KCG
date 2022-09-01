using KMath;
using System;

namespace Collisions
{



    public static partial class Collisions
    {


        public struct RayCastResult
        {
            public Vec2f Point;
            public bool Intersect;
        }

        public static RayCastResult RayCastAgainstCircle(Line2D line, Vec2f position, float radius)
        {
            

            RayCastResult result = new RayCastResult();

            return result;

        }
    }
}