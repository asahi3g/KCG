using System;

namespace KMath
{
    /*
     xmin           xmax
     ymax           ymax
        O----------O
        |box_center|
        |    O     |
        |          |
        O----------O
     xmin         xmax
     ymin         ymin
    */
    /// <summary>
    /// Axis-aligned Bounding Box 2D
    /// </summary>
    public struct AABox2D
    {
        public float xmin;
        public float xmax;
        public float ymin;
        public float ymax;

        public AABox2D(Vec2f position, Vec2f size)
        {
            xmin = position.X;
            xmax = position.X + size.X;
            ymin = position.Y;
            ymax = position.Y + size.Y;
        }

        public AABox2D(int x, int y)
        {
            xmin = x;
            xmax = x + 1f;
            ymin = y;
            ymax = y + 1f;
        }

        public bool OverlapPoint(Vec2f point)
        {
            // is the point inside the rectangle's bounds?
            return point.X >= xmin &&     // right of the left edge AND
                   point.X <= xmax &&     // left of the right edge AND
                   point.Y >= ymin &&     // below the top AND
                   point.Y <= ymax;       // above the bottom
        }
    }
}

