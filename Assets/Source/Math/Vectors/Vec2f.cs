using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KMath
{
    // Vector 2D Floating-Point
    [Serializable]
    public struct Vec2f
    {
        // Shorthand for writing Vec2f(0, 0).
        public static Vec2f Zero { [MethodImpl((MethodImplOptions) 256)] get; } = new Vec2f(0f, 0f);

        // Shorthand for writing Vector2(1, 1).
        public static Vec2f One { [MethodImpl((MethodImplOptions) 256)] get; } = new Vec2f(1f, 1f);
        
        public float X;
        public float Y;

        public Vector2 GetVector2() => new Vector2(X, Y);

        public Vec2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "{" + X + " " + Y + "}";
        }
        

        #region Properties

        // Returns the squared length of this vector (Read Only).
        public float SqrMagnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => X * X + Y * Y;
        }
        
        // Returns the length of this vector (Read Only).
        public float Magnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => MathF.Sqrt(X * X + Y * Y);
        }
        

        public Vec2f Perpendicular()
        {
            return new Vec2f(Y, -X);
        }
        
        // Returns this vector with a magnitude of 1 (Read Only).
        public Vec2f Normalized
        {
            [MethodImpl((MethodImplOptions) 256)] get
            {
                Vec2f normalized = new Vec2f(X, Y);
                normalized.Normalize();
                return normalized;
            }
        }

        #endregion

        #region Methods

        [MethodImpl((MethodImplOptions)256)]
        public static float Cross(Vec2f lhs, Vec2f rhs)
        {
            return lhs.X * rhs.Y - lhs.Y * rhs.X;
        }

        // Dot Product of two vectors.
        [MethodImpl((MethodImplOptions) 256)]
        public static float Dot(Vec2f lhs, Vec2f rhs) => lhs.X * rhs.X + lhs.Y * rhs.Y;

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(Vec2f other) => X * other.X + Y * other.Y;

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(float x, float y) => X * x + Y * y;


        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f Clamp(Vec2f value, Vec2f minValue, Vec2f maxValue) =>
            new Vec2f(Math.Clamp(value.X, minValue.X, maxValue.X), Math.Clamp(value.Y, minValue.Y, maxValue.Y));

        // Makes this vector have a magnitude of 1
        [MethodImpl((MethodImplOptions) 256)]
        public Vec2f Normalize()
        {
            var magnitude = Magnitude;
            if (magnitude > 9.99999974737875E-06)
                this /= magnitude;
            else
                this = Zero;
            
            return this;
        }

        // Projecting current vector onto other vector.
        [MethodImpl((MethodImplOptions) 256)]
        public Vec2f Project(Vec2f other)
        {
            // dot product
            var dp = Dot(this, other);

            var projectionX = (dp / other.SqrMagnitude) * other.X;
            var projectionY = (dp / other.SqrMagnitude) * other.Y;

            return new Vec2f(projectionX, projectionY);
        }
        
        // Returns the distance between a and b.
        [MethodImpl((MethodImplOptions) 256)]
        public static float Distance(Vec2f a, Vec2f b)
        {
            float num1 = a.X - b.X;
            float num2 = a.Y - b.Y;
            return MathF.Sqrt(num1 * num1 + num2 * num2);
        }
        
        // Returns the 2D vector perpendicular to this 2D vector. The result is always rotated 90-degrees in a counter-clockwise direction for a
        // 2D coordinate system where the positive Y axis goes up.
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f Perpendicular(Vec2f inDirection) => new Vec2f(-inDirection.Y, inDirection.X);

        // Angle in rad
        [MethodImpl((MethodImplOptions)256)]
        public Vec2f Rotate(float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            float px = X * cos - Y * sin;
            Y = X * sin + Y * cos;
            X = px;

            return this;
        }

        // param Angle in rad
        [MethodImpl((MethodImplOptions)256)]
        public static Vec2f Rotate(Vec2f vec, float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            float px = vec.X * cos - vec.Y * sin;
            vec.Y = vec.X * sin + vec.Y * cos;
            vec.X = px;

            return vec;
        }
        // Returns a vector off the vector defined by a normal
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Reflect(Vec2f inDirection, Vec2f inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vec2f(num * inNormal.X + inDirection.X, num * inNormal.Y + inDirection.Y);
        }
        
        public bool Equals(Vec2f other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vec2f other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public float GetAngle(Vec2f other) => MathF.Atan2(other.Y, other.X) - MathF.Atan2(Y, X);

        #endregion

        #region Operators

        [MethodImpl((MethodImplOptions) 256)]
        public static explicit operator Vec2i(Vec2f obj)
        {
            Vec2i output = new Vec2i((int)obj.X, (int)obj.Y);
            return output;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator *(Vec2f a, float d) => new Vec2f(a.X * d, a.Y * d);


        public static Vec2f operator *(Vec2f a, Vec2f b) => new Vec2f(a.X * b.X, a.Y * b.Y);

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator *(float d, Vec2f a) => new Vec2f(a.X * d, a.Y * d);

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator /(Vec2f a, float d) => new Vec2f(a.X / d, a.Y / d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a, Vec2f b) => new Vec2f(a.X - b.X, a.Y - b.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a, float b) => new Vec2f(a.X - b, a.Y - b);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a) => new Vec2f(-a.X, -a.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator +(Vec2f a, Vec2f b) => new Vec2f(a.X + b.X, a.Y + b.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator +(Vec2f a, float b) => new Vec2f(a.X + b, a.Y + b);
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Vec2f lhs, Vec2f rhs)
        {
            float num1 = lhs.X - rhs.X;
            float num2 = lhs.Y - rhs.Y;
            return num1 * num1 + num2 * num2 < 9.99999943962493E-11;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Vec2f lhs, Vec2f rhs) => !(lhs == rhs);

        #endregion

    }
}

