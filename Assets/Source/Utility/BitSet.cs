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
        public UInt32 Length;           // Length of Bits in Bits.
        private UInt64[] bits;          // 0 Is the least significant bit.
        private UInt64 bitMask;         // Bit mask for most significant UInt64.

        public BitSet(UInt32 bitLength)
        {
            Length = bitLength;
            UInt64 length = (UInt64)Length >> 6;

            if (bitLength % 64 != 0)
                length++;

            bitMask = 1ul << (int)(bitLength % 64);
            bitMask -= 1;
            bits = new UInt64[length];
        }

        public void Set(int pos)
        {
            int offset = pos >> 6;
            bits[offset] |= 1uL << pos;
        }

        public void UnSet(int pos)
        {
            int offset = pos >> 6;
            bits[offset] &= ~(1uL << pos);
        }

        public bool Get(int pos)
        {
            int offset = pos >> 6;
            if (offset >= bits.Length)
                return false;
            return (bits[offset] & (1uL << pos)) != 0;
        }

        public void SetAll()
        {
            Array.Fill<UInt64>(bits, UInt64.MaxValue);
        }

        /// <summary>
        /// Set all bist to 0.
        /// </summary>
        public void Clear()
        {
            Array.Fill<UInt64>(bits, 0ul);
        }

        /// <summary>
        /// Flip the value of all Bits.
        /// </summary>
        public void Flip()
        {
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = ~bits[i];
            }
        }

        // Test if any bit is 1.
        public bool Any()
        {
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != 0)
                    return false;
            }
            return true;
        }

        // Test if all Bits are set to 1.
        public bool All()
        {
            for (int i = 0; i < (bits.Length - 1); i++)
            {
                if (bits[i] != UInt64.MaxValue)
                    return false;
            }

            if (bits[bits.Length - 1] != bitMask)
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
            set { if (value) { Set(i); } else { UnSet(i); } }
        }

        public static BitSet operator &(BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < lhs.bits.Length; i++)
            {
                newBitSet.bits[i] = lhs.bits[i] & rhs.bits[i];
            }

            return newBitSet;
        }

        public static BitSet operator | (BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < (lhs.bits.Length - 1); i++)
            {
                newBitSet.bits[i] = lhs.bits[i] | rhs.bits[i];
            }
            newBitSet.bits[lhs.bits.Length - 1] = 
                (lhs.bits[lhs.bits.Length - 1] | rhs.bits[lhs.bits.Length - 1]) & lhs.bitMask;

            return newBitSet;
        }

        public static BitSet operator ^(BitSet lhs, BitSet rhs)
        {
#if         DEBUG
            IsOperationValid(lhs, rhs);
#endif
            BitSet newBitSet = new BitSet(lhs.Length);

            for (int i = 0; i < (lhs.bits.Length - 1); i++)
            {
                newBitSet.bits[i] = lhs.bits[i] ^ rhs.bits[i];
            }
            newBitSet.bits[lhs.bits.Length - 1] =
                (lhs.bits[lhs.bits.Length - 1] ^ rhs.bits[lhs.bits.Length - 1]) & lhs.bitMask;

            return newBitSet;
        }

        // Todo implement bit shifting.

        public static bool operator ==(BitSet lhs, BitSet rhs)
        {
#if DEBUG
            IsOperationValid(lhs, rhs);
#endif

            for (int i = 0; i < lhs.bits.Length; i++)
            {
                if (lhs.bits[i] != rhs.bits[i])
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
        
        protected bool Equals(BitSet other)
        {
            return Length == other.Length && Equals(bits, other.bits) && bitMask == other.bitMask;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BitSet) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Length, bits, bitMask);
        }
    }
}
