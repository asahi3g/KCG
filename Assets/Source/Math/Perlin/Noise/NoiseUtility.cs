namespace KMath.Noise
{
    internal static class NoiseUtility
    {
        static public int FastFloor(float value) => (int)(value >= 0 ? (int)value : (int)value - 1);

        static public float Lerp(float a, float b, float t) => a + t * (b - a);

        static public float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);

        public const float SQUARE_ROOT_2 = 1.4247691104677813f;

    }
}
