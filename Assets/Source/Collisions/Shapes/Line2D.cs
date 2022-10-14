using System;
using System.Runtime.CompilerServices;
using Enums.Tile;

namespace KMath
{
    [Serializable]
    public struct Line2D
    {
        public Vec2f A;
        public Vec2f B;

        public Line2D(Vec2f a, Vec2f b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// LINE/LINE collision check
        /// https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
        /// </summary>
        public bool Intersects(Line2D other)
        {
            int o1 = CheckOrientation(A, B, other.A);
            int o2 = CheckOrientation(A, B, other.B);
            int o3 = CheckOrientation(other.A, other.B, A);
            int o4 = CheckOrientation(other.A, other.B, B);

            // General case.
            if (o1 != o2 && o3 != o4)
                return true;

            // Lines are colinear and lies on segment
            if (o1 == 0 && OnSegment(A, other.A, B))
                return true;
            if (o2 == 0 && OnSegment(A, other.B, B))
                return true;
            if (o3 == 0 && OnSegment(other.A, other.B, other.B))
                return true;
            if (o4 == 0 && OnSegment(other.A, B, other.B))
                return true;

            return false;
        }

        /// <summary>
        ///  https://bryceboe.com/2006/10/23/line-segment-intersection-algorithm/
        ///  
        /// </summary>
        /// <returns>
        /// 0 - p1, p2 and p3 are collinear
        /// 1 - Clockwise
        /// 2 - Counterclockwise
        /// </returns>
        [MethodImpl((MethodImplOptions)256)]
        private static int CheckOrientation(Vec2f p1, Vec2f p2, Vec2f p3)
        {
            float temp = (p2.Y - p1.Y) * (p3.X - p2.X) - (p2.X - p1.X) * (p3.Y - p2.Y);

            if (temp == 0) return 0;

            return (temp > 0) ? 1 : 2;
        }

        /// <summary>
        /// Given tree colinear points check if point lines in the line point1 to point3
        /// </summary>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions)256)]
        public static bool OnSegment(Vec2f point1, Vec2f point2, Vec2f point3)
        {
            float epsilon = 0.00001f;

            if (point2.X - epsilon <= MathF.Max(point1.X, point3.X) && point2.X + epsilon >= MathF.Min(point1.X, point3.X) &&
                 point2.Y - epsilon <= MathF.Max(point1.Y, point3.Y) && point2.Y + epsilon >= MathF.Min(point1.Y, point3.Y))
                return true;

            return false;
        }


        /// <summary>
        /// Check if point in on the line.
        /// </summary>
        [MethodImpl((MethodImplOptions)256)]
        public bool OnLine(Vec2f point)
        {
            return OnSegment(A, point, B);
        }
    }
}
