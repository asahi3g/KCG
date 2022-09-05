using CollisionsTest;
using KMath;
using System;
using System.Runtime.CompilerServices;

namespace Collisions
{
    public static class LineOfSightTest
    {
        [Flags]
        enum IsBehindFlag : byte
        {
            BottonLeft = 1 << 0, 
            TopLeft = 1 << 1,
            BottonRight = 1 << 2,
            TopRight = 1 << 3
        }

        /// <summary>
        /// If true it's inside field of view.
        /// https://legends2k.github.io/2d-fov/design.html#references
        /// </summary>
        /// <param name="box"></param>
        /// <param name="radious">Max sight distance</param>
        /// <param name="fov">Field of view/Sector angle in rad</param>
        /// <param name="startPos"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool AABBIntersectSector(ref AABox2D box, float radious, float fov, Vec2f startPos, Vec2f dir)
        {

            // Broad phase check if box overlap with sector. (Same code of AABBOverlapsSphere)
            if (!CircleTileMapStaticCollision.AABBOverlapsSphere(ref box, radious, startPos))
                return false;

            Vec2f[] points = new Vec2f[4];
            points[0] = new Vec2f(box.xmin, box.ymin);  // Botton left
            points[1] = new Vec2f(box.xmin, box.ymax);  // Top left
            points[2] = new Vec2f(box.xmax, box.ymin);  // Botton Right
            points[3] = new Vec2f(box.xmax, box.ymax);  // Top Right



            IsBehindFlag isBehindFlag = new IsBehindFlag();

            Vec2f leftEdge = Vec2f.Rotate(dir, fov / 2);   // Edge counter clock wise rotation
            Vec2f rightEdge = Vec2f.Rotate(dir, -fov / 2);   // Edge  clock wise rotation

            // Update point state.
            for (int i = 0; i < points.Length; i++)
            {
                Vec2f pointDir = points[i] - startPos;
                if (Vec2f.Dot(pointDir, dir) <= 0)
                {
                    isBehindFlag |= (IsBehindFlag)(1 << i);
                    continue;
                }
                if (pointDir.Magnitude > radious)
                    continue;
                
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                    return true;
            }

            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonLeft) || !isBehindFlag.HasFlag(IsBehindFlag.TopLeft))
            {
                if (EdgeIntersectSector(points[0], points[1], radious, leftEdge, rightEdge, startPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonLeft) || !isBehindFlag.HasFlag(IsBehindFlag.BottonRight))
            {
                if (EdgeIntersectSector(points[0], points[2], radious, leftEdge, rightEdge, startPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.TopLeft) || !isBehindFlag.HasFlag(IsBehindFlag.TopRight))
            {
                if (EdgeIntersectSector(points[1], points[3], radious, leftEdge, rightEdge, startPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonRight) || !isBehindFlag.HasFlag(IsBehindFlag.TopRight))
            {
                if (EdgeIntersectSector(points[2], points[3], radious, leftEdge, rightEdge, startPos))
                    return true;
            }

            return false;
        }

        [MethodImpl((MethodImplOptions)256)]
        private static bool EdgeIntersectSector(Vec2f start, Vec2f end, float radious, Vec2f leftEdge, Vec2f rightEdge, Vec2f startPos)
        {
            // https://www.geometrictools.com/Documentation/IntersectionLine2Circle2.pdf
            // Sector Arc Intersection
            Vec2f delta = start - startPos;
            Vec2f diff = end - start;
            float dLen = delta.Magnitude;
            float diffLen = diff.Magnitude;

            float t1 = Vec2f.Dot(end, delta);
            float t2 = t1 * t1 - (diffLen * diffLen * (dLen * dLen - radious * radious));

            float c1 = -1, c2 = -1;
            if (t2 >= 0)
            {
                c1 = (-t1 + MathF.Sqrt(t2))/ (diffLen * diffLen);
            }
            if (t1 > 0)
            {
                c2 = (-t1 - MathF.Sqrt(t2)) / (diffLen * diffLen);
            }

            // If intersects with circle
            if (c1 >= 0 && c1 <= 1)
            {
                // Check if intersection point is inside arc.
                Vec2f intersectPoint = start + c1 * diff;
                Vec2f pointDir = intersectPoint - startPos;
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                   return true;
            }

            if (c2 >= 0 && c2 <= 1)
            {
                // Check if intersection point is inside arc.
                Vec2f intersectPoint = start + c2 * diff;
                Vec2f pointDir = intersectPoint - startPos;
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                   return true;
            }

            // Sector Edge Intersection
            leftEdge *= radious;
            rightEdge *= radious;

            if (TwoLinesInteresctionTest(startPos, startPos + leftEdge, start, end))
                return true;
            if (TwoLinesInteresctionTest(startPos, startPos + rightEdge, start, end))
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
        private static bool onSegment(Vec2f point1, Vec2f point2, Vec2f point3)
        {
            if (point2.X <= MathF.Max(point1.X, point3.X) && point2.X >= MathF.Min(point1.X, point3.X) &&
                 point2.Y <= MathF.Max(point1.Y, point3.Y) && point2.Y >= MathF.Min(point1.Y, point3.Y))
                return true;

            return false;
        }

        /// <summary>
        /// https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
        /// </summary>
        private static bool TwoLinesInteresctionTest(Vec2f start1, Vec2f end1, Vec2f start2, Vec2f end2)
        {
            int o1 = CheckOrientation(start1, end1, start2);
            int o2 = CheckOrientation(start1, end1, end2);
            int o3 = CheckOrientation(start2, end2, start1);
            int o4 = CheckOrientation(start2, end2, end1);

            // General case.
            if (o1 != o2 && o3 != o4)
                return true;

            // Lines are colinear and lies on segment
            if (o1 == 0 && onSegment(start1, start2, end1)) 
                return true;
            if (o2 == 0 && onSegment(start1, end2, end1))
                return true;
            if (o3 == 0 && onSegment(start2, start2, end2)) 
                return true;
            if (o4 == 0 && onSegment(start2, end1, end2)) 
                return true;

            return false;
        }
    }
}
