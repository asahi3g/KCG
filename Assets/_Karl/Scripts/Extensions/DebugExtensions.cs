using System;
using System.Globalization;
using UnityEngine;

public static class DebugExtensions
{

    public static readonly Color NumberColor = new Color(0.5f, 0.7f, 1f, 1f);
    public static readonly Color EnumColor = Color.Lerp(Color.red, Color.yellow, 0.5f);
    

    public static string ToStringPretty(this System.Object obj)
    {
        if (obj == null) return "null";
        
        if (obj is bool b)
        {
            return b.ToString().Color(b ? Color.green : Color.red);
        }

        if (obj is Enum e)
        {
            return $"[{e.ToString()}]".Color(EnumColor);
        }

        if (obj is int i)
        {
            return i.ToString().Color(NumberColor);
        }
        
        if (obj is float f)
        {
            return $"{f.ToString(CultureInfo.InvariantCulture)}f".Color(NumberColor);
        }
        
        if (obj is double d)
        {
            return $"{d.ToString(CultureInfo.InvariantCulture)}d".Color(NumberColor);
        }
        
        return obj.ToString();
    }
}
