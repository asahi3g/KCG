using System;
using System.Runtime.CompilerServices;

namespace KMath
{
    // Vector 2D Floating-Point
    [Serializable]
    public struct Vec2f
    {
        /// <summary>
        ///   <para>Shorthand for writing Vec2f(0, 0).</para>
        /// </summary>
        public static Vec2f Zero { [MethodImpl((MethodImplOptions) 256)] get; } = new(0f, 0f);

        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 1).</para>
        /// </summary>
        public static Vec2f One { [MethodImpl((MethodImplOptions) 256)] get; } = new(1f, 1f);
        
        public float X;
        public float Y;

        public Vec2f(float x, float y)
        {
            X = x;
            Y = y;
        }
        

        #region Properties

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float SqrMagnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => X * X + Y * Y;
        }
        
        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float Magnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => MathF.Sqrt(X * X + Y * Y);
        }
        
        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
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
        
        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        [MethodImpl((MethodImplOptions) 256)]
        public static float Dot(Vec2f lhs, Vec2f rhs) => lhs.X * rhs.X + lhs.Y * rhs.Y;

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(Vec2f other) => this.X * other.X + this.Y * other.Y;

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(float x, float y) => this.X * x + this.Y * y;


        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f Clamp(Vec2f value, Vec2f minValue, Vec2f maxValue) => new(Math.Clamp(value.X, minValue.X, maxValue.X), Math.Clamp(value.Y, minValue.Y, maxValue.Y));

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public Vec2f Normalize()
        {
            var magnitude = this.Magnitude;
            if (magnitude > 9.99999974737875E-06)
                this /= magnitude;
            else
                this = Zero;
            
            return this;
        }

        /// <summary>
        ///   <para>Projecting current vector onto other vector</para>
        /// </summary>
        /// <param name="other"></param>
        [MethodImpl((MethodImplOptions) 256)]
        public Vec2f Project(Vec2f other)
        {
            // dot product
            var dp = Dot(this, other);

            var projectionX = (dp / other.SqrMagnitude) * other.X;
            var projectionY = (dp / other.SqrMagnitude) * other.Y;

            return new Vec2f(projectionX, projectionY);
        }
        
        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [MethodImpl((MethodImplOptions) 256)]
        public static float Distance(Vec2f a, Vec2f b)
        {
            float num1 = a.X - b.X;
            float num2 = a.Y - b.Y;
            return MathF.Sqrt(num1 * num1 + num2 * num2);
        }
        
        /// <summary>
        ///   <para>Returns the 2D vector perpendicular to this 2D vector. The result is always rotated 90-degrees in a counter-clockwise direction for a 2D coordinate system where the positive Y axis goes up.</para>
        /// </summary>
        /// <param name="inDirection">The input direction.</param>
        /// <returns>
        ///   <para>The perpendicular direction.</para>
        /// </returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f Perpendicular(Vec2f inDirection) => new(-inDirection.Y, inDirection.X);

        #endregion

        #region Operators
        
        [MethodImpl((MethodImplOptions) 256)]
        public static explicit operator Vec2i(Vec2f obj)
        {
            Vec2i output = new Vec2i((int)obj.X, (int)obj.Y);
            return output;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator *(Vec2f a, float d) => new(a.X * d, a.Y * d);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator *(float d, Vec2f a) => new(a.X * d, a.Y * d);

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator /(Vec2f a, float d) => new(a.X / d, a.Y / d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a, Vec2f b) => new(a.X - b.X, a.Y - b.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a, float b) => new(a.X - b, a.Y - b);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator -(Vec2f a) => new(-a.X, -a.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator +(Vec2f a, Vec2f b) => new(a.X + b.X, a.Y + b.Y);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec2f operator +(Vec2f a, float b) => new(a.X + b, a.Y + b);
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

