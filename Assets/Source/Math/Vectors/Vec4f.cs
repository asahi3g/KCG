using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KMath
{
    // Vector 2D Floating-Point
    [Serializable]
    public struct Vec4f
    {
        // Shorthand for writing Vec4f(0, 0).
        public static Vec4f Zero { [MethodImpl((MethodImplOptions) 256)] get; } = new Vec4f(0f, 0f, 0.0f, 0.0f);

        // Shorthand for writing Vector2(1, 1).
        public static Vec4f One { [MethodImpl((MethodImplOptions) 256)] get; } = new Vec4f(1f, 1f, 1.0f, 1.0f);
        
        public float X;
        public float Y;
        public float Z;
        public float W;

        public float R {get{ return X; } set {X = value;}}
        public float G {get{ return Y; } set {Y = value;}}
        public float B {get{ return Z; } set {Z = value;}}
        public float A {get{ return W; } set {W = value;}}


        public Vector4 GetVector4() => new Vector4(X, Y, Z, W);

        public Vec4f(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public override string ToString()
        {
            return "{" + X + " " + Y + " " + Z + " " + W + "}";
        }
        

        #region Properties

    
        
        public bool Equals(Vec4f other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is Vec4f other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion

        #region Operators

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator *(Vec4f a, float d) => new Vec4f(a.X * d, a.Y * d, a.Z * d, a.W * d);


        public static Vec4f operator *(Vec4f a, Vec4f b) => new Vec4f(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator *(float d, Vec4f a) => new Vec4f(a.X * d, a.Y * d, a.Z * d, a.W * d);

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator /(Vec4f a, float d) => new Vec4f(a.X / d, a.Y / d, a.Z / d, a.W / d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator -(Vec4f a, Vec4f b) => new Vec4f(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator -(Vec4f a, float b) => new Vec4f(a.X - b, a.Y - b, a.Z * b, a.W - b);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator -(Vec4f a) => new Vec4f(-a.X, -a.Y, -a.Z, -a.W);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator +(Vec4f a, Vec4f b) => new Vec4f(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec4f operator +(Vec4f a, float b) => new Vec4f(a.X + b, a.Y + b, a.Z + b, a.W + b);
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Vec4f lhs, Vec4f rhs)
        {
            float num1 = lhs.X - rhs.X;
            float num2 = lhs.Y - rhs.Y;
            float num3 = lhs.Z - rhs.Z;
            float num4 = lhs.W - rhs.W;
            return num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4 < 9.99999943962493E-11;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Vec4f lhs, Vec4f rhs) => !(lhs == rhs);

        #endregion

    }
}

