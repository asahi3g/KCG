//import UnityEngine

using System;

namespace KMath
{
    public static class KMath
    {
        public static float LengthSquared(UnityEngine.Vector2 vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y);
        }

        public static float MakePositive(float num)
        {
            if (num < 0)
            {
                num *= -1f;
            }

            return num;
        }

         public static float Clamp(float value, float min, float max) 
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static bool AlmostEquals(this float value1, float value2, float precision = 0.0000001f)
        {
            return (Math.Abs(value1 - value2) <= precision);
        }
    }
}