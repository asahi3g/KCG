using UnityEngine;

public static class StringExtensions
{

    public static string Color(this string value, Color color)
    {
        if (value == null) return null;
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{value}</color>";
    }
    
}
