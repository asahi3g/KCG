using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMath
{
    // Vector 3D Floating-Point
    public struct Vec3f
    {
        float X;
        float Y;
        float Z;

        public Vec3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vec3f operator+(Vec3f a, Vec3f b)
        {
            return new Vec3f(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vec3f operator-(Vec3f a, Vec3f b)
        {
            return new Vec3f(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vec3f operator*(Vec3f a, Vec3f b)
        {
            return new Vec3f(a.x * b.x, a.y * b.y, a.z * b.z);
        }
    }
}

