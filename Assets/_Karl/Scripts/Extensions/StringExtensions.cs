using UnityEngine;

public static class StringExtensions
{

    public static string Color(this string value, Color color)
    {
        if (value == null) return null;
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{value}</color>";
    }
    
    public static string Color(this object value, Color color)
    {
        if (value == null) return null;
        return Color(value.ToString(), color);
    }
    
}
