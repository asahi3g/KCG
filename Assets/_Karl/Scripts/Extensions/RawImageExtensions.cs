using UnityEngine;
using UnityEngine.UI;

public static class RawImageExtensions
{

    public static void SetUvRect(this RawImage rawImage, Vector4 vector4)
    {
        if (rawImage == null) return;
        rawImage.uvRect = new Rect(vector4.x, vector4.y, vector4.z, vector4.w);
    }
}
