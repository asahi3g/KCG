using UnityEngine;

public static class ColorExtensions
{

    public static Color SetAlpha(this Color value, float a)
    {
        value.a = a;
        return value;
    }
}
