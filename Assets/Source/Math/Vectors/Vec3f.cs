using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KMath
{
    // Vector 3D Floating-Point
    public struct Vec3f
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3 GetVector3() => new Vector3(X, Y, Z);

        public Vec3f(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0.0f;
        }
        
        public Vec3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3f(Vec2f xy)
        {
            X = xy.X;
            Y = xy.Y;
            Z = 0f;
        }

        public Vec3f(Vec2f xy, float z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        //  <para>Returns the length of this vector (Read Only).</para>
        public float Magnitude
        {
            [MethodImpl((MethodImplOptions) 256)] get => (float) Math.Sqrt((double) X * (double) X + (double) Y * (double) Y + (double) Z * (double) Z);
        }

        public Vec3f Normalize()
        {
            var magnitude = Magnitude;
            if (magnitude > 9.99999974737875E-06)
                this /= magnitude;
            else
                this = new Vec3f(0f, 0f, 0f);

            return this;
        }

        [MethodImpl((MethodImplOptions)256)]
        public static Vec3f Cross(Vec3f first, Vec3f second)
        {
            float x = first.Y * second.Z - first.Z * second.Y;
            float y = first.Z * second.X - first.X * second.Z;
            float z = first.X * second.Y - first.Y * second.X;
            return new Vec3f(x, y, z);
        }

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(Vec3f other) => X * other.X + Y * other.Y + Z * other.Z; 

        [MethodImpl((MethodImplOptions)256)]
        public float Dot(float x, float y, float z) => X * x + Y * y + Z * z;


        [MethodImpl((MethodImplOptions) 256)]
        public static explicit operator Vec2f(Vec3f obj)
        {
            Vec2f output = new Vec2f(obj.X, obj.Y);
            return output;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Vec3f operator+(Vec3f a, Vec3f b) => new Vec3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec3f operator-(Vec3f a, Vec3f b) => new Vec3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec3f operator*(Vec3f a, Vec3f b) => new Vec3f(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec3f operator/(Vec3f a, Vec3f b) => new Vec3f(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        [MethodImpl((MethodImplOptions) 256)]
        public static Vec3f operator/(Vec3f a, float b) => new Vec3f(a.X / b, a.Y / b, a.Z / b);
        
    }
}

