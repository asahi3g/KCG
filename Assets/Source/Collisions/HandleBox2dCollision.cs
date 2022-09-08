
using Entitas;
using KMath;
using System;

namespace Collisions
{


   

    public static partial class Collisions
    {

        public static Vec2f HandleBox2dCollision(ref AABox2D a, ref AABox2D b)
        {
            Vec2f centerB = new Vec2f(b.xmin + (b.xmax - b.xmin) * 0.5f, b.ymin + (b.ymax - b.ymin) * 0.5f);
            Vec2f centerA = new Vec2f(a.xmin + (a.xmax - a.xmin) * 0.5f, a.ymin + (a.ymax - a.ymin) * 0.5f);

            Vec2f directionVector = (centerB - centerA).Normalized;

            float aWidth = (a.xmax - a.xmin);
            float bWidth = (b.xmax - b.xmin);

            float aHeight = (a.ymax - a.ymin);
            float bHeight = (b.ymax - b.ymin);

            Vec2f magnitude = new Vec2f(Math.Abs((aWidth + bWidth)) - Math.Abs((b.xmax - a.xmin)), Math.Abs((aHeight - bHeight)) - Math.Abs((b.ymax - b.ymin)));

            return magnitude * directionVector;

        }


    }
}