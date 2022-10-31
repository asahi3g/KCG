using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KMath
{
    // Vector 2D Integer
    public struct Vec2i
    {
        private static readonly Vec2i zeroVector = new(0, 0);
        private static readonly Vec2i oneVector = new(1, 1);
        
        public int X;
        public int Y;

        public Vector2Int GetVector2() => new Vector2Int(X, Y);

        public Vec2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Methods

        // Make X and Y positive
        [MethodImpl((MethodImplOptions) 256)]
        public void Abs()
        {
            X = Math.Abs(X);
            Y = Math.Abs(Y);
        }
        
        public bool Equals(Vec2i other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vec2i other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion

        #region Properties

        // Returns the squared length of this vector (Read Only).
        public int sqrMagnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => X * X + Y * Y;
        }
        
        // Returns the length of this vector (Read Only).
        public float magnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => Mathf.Sqrt(X * X + Y * Y);
        }

        // Shorthand for writing Vec2f(0, 0).
        public static Vec2i zero
        {
            [MethodImpl((MethodImplOptions) 256)] get => zeroVector;
        }


        // Shorthand for writing Vec2f(1, 1).
        public static Vec2i one
        {
            [MethodImpl((MethodImplOptions)256)]
            get => oneVector;
        }

        #endregion

        #region Operators

        [MethodImpl((MethodImplOptions) 256)]
        public static explicit operator Vec2f(Vec2i obj)
        {
            var output = new Vec2f(obj.X, obj.Y);
            return output;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator *(Vec2i a, int d) => new(a.X * d, a.Y * d);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator *(int d, Vec2i a) => new(a.X * d, a.Y * d);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator <<(Vec2i a, int d) => new(a.X << d, a.Y << d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator /(Vec2i a, int d) => new(a.X / d, a.Y / d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator -(Vec2i a, Vec2i b) => new(a.X - b.X, a.Y - b.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator -(Vec2i a, int b) => new(a.X - b, a.Y - b);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2i operator +(Vec2i a, Vec2i b) => new(a.X + b.X, a.Y + b.Y);
        [MethodImpl((MethodImplOptions)256)]
        public static bool operator ==(Vec2i lhs, Vec2i rhs)
        {
            return (rhs.X == lhs.X && rhs.Y == lhs.Y) ? true : false;
        }

        [MethodImpl((MethodImplOptions)256)]
        public static bool operator !=(Vec2i lhs, Vec2i rhs) => !(lhs == rhs);

        #endregion

    }
}

