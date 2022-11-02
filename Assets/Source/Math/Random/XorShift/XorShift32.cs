using System;

namespace KMath.Random
{
    // Very fast but fails Big Crush test.
    public class XorShift32
    {
        private UInt32 Seed;

        public XorShift32(UInt32 seed)
        {
            if (seed == 0) return; // seed cannot be 0
            Seed = seed;
        }

        public UInt32 Next()
        {
            Seed ^= Seed << 13;
            Seed ^= Seed >> 17;
            Seed ^= Seed << 15;
            return Seed;
        }
    }
}
