using System;

namespace KMath
{
    public struct CircleSector
    {
        public float Radius;
        public float Fov;
        public Vec2f StartPos;
        public Vec2f Dir;

        public bool Intersect(Vec2f point)
        {
            // Broad phase check if point is inside circle
            Vec2f dir = point - StartPos;
            if (dir.Magnitude > Radius)
                return false;
           
            dir.Normalize();
            float theta = MathF.Atan2(dir.Y, dir.X);
            theta -= MathF.Atan2(Dir.Y, Dir.X);
            theta = MathF.Abs(theta);

            if (theta <= Fov/2.0f)
                return true;

            return false;
        }
    }
}
