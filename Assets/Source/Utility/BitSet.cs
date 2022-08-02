using System;

namespace Utility
{
    // Port c++ BitSet container. 

    // Todo:
    //      Method for first set bit.(Use logarithm method for this)
    //      Method for last set bit.
    //      Method for last unset bit.
    //      Method for first unset bit.
    //      Set all bits in range.
    //      Unset All bits in range.
    //      Return number of set bits.

    public class BitSet
    {
        public UInt32 Length;   // Length of Bits in Bits.
        UInt64[] Bits;          // 0 Is the least significant bit.
        UInt64 BitMask;         // Bit mask for most significant UInt64.

        public BitSet(UInt32 bitLength)
        {
            Length = bitLength;

            UInt64 bitMask = 0x3ul;
            UInt64 length = (UInt64)Length >> 6;

            if ((bitLength & bitMask) == 0)
            {
                length++;
            }

            BitMask = 1ul << (int)(bitLength % 64);
            BitMask -= 1;
            Bits = new UInt64[length + 1];
        }

        public void Set(int pos)
        {
            int offset = pos >> 6;
            Bits[offset] |= 1uL << pos;
        }

        public void UnSet(int pos)
        {
            int offset = pos >> 6;
            Bits[offset] &= ~ 1uL << pos;
        }

        public bool Get(int pos)
        {
            int offset = pos >> 6;
            if (offset >= Bits.Length)
                return false;
            return (Bits[offset] & (1uL << pos)) != 0;
        }

        /// <summary>
        /// Set all bist to 0.
        /// </summary>
        public void Clear()
        {
            Array.Fill<UInt64>(Bits, 0ul);
        }

        /// <summary>
        /// Flip the value of all Bits.
        /// </summary>
        public void Flip()
        {
            for (int i = 0; i < Bits.Length; i++)
            {
                Bits[i] = ~Bits[i];
            }
        }

        // Test if any bit is 1.
        public bool Any()
        {
            for (int i = 0; i < Bits.Length; i++)
            {
                if (Bits[i] != 0)
                    return false;
            }
            return true;
        }

        // Test if all Bits are set to 1.
        public bool All()
        {
            for (int i = 0; i < (Bits.Length - 1); i++)
            {
                if (Bits[i] != UInt64.MaxValue)
                    return false;
            }

            if (Bits[Bits.Length - 1] != BitMask)
            {
                return false;
            }

            return true;
        }

        // Test if all Bits are set to 0.
        public bool None()
        {
            return !Any();
        }

        public bool this[int i]
        {
            get => Get(i);
            set => Set(i);
        }

        public static BitSet operator &(BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < lhs.Bits.Length; i++)
            {
                newBitSet.Bits[i] = lhs.Bits[i] & rhs.Bits[i];
            }

            return newBitSet;
        }

        public static BitSet operator | (BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < (lhs.Bits.Length - 1); i++)
            {
                newBitSet.Bits[i] = lhs.Bits[i] | rhs.Bits[i];
            }
            newBitSet.Bits[lhs.Bits.Length - 1] = 
                (lhs.Bits[lhs.Bits.Length - 1] | rhs.Bits[lhs.Bits.Length - 1]) & lhs.BitMask;

            return newBitSet;
        }

        public static BitSet operator ^(BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < (lhs.Bits.Length - 1); i++)
            {
                newBitSet.Bits[i] = lhs.Bits[i] ^ rhs.Bits[i];
            }
            newBitSet.Bits[lhs.Bits.Length - 1] =
                (lhs.Bits[lhs.Bits.Length - 1] ^ rhs.Bits[lhs.Bits.Length - 1]) & lhs.BitMask;

            return newBitSet;
        }

        // Todo implement bit shifting.

        public static bool operator ==(BitSet lhs, BitSet rhs)
        {
#if DEBUG
            IsOperationValid(lhs, rhs);
#endif

            for (int i = 0; i < lhs.Bits.Length; i++)
            {
                if (lhs.Bits[i] != rhs.Bits[i])
                    return false;
            }

            return true;
        }
        public static bool operator != (BitSet lhs, BitSet rhs) => !(lhs == rhs);

        private static void IsOperationValid(BitSet lhs, BitSet rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                throw new ArgumentOutOfRangeException("Can't do BitSets operation with different lengths.");
            }
        }
    }
}
