using KMath;
using UnityEngine;

public static class KMathExtensions
{

    public static Vector3 GetVector3(this Vec3f value)
    {
        return new Vector3(value.X, value.Y, value.Z);
    }
}
