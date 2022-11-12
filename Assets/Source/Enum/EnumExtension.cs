using System;

public static class EnumExtension
{
    public static T Next<T>(this T src) where T : Enum 
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

        var arr = (T[])Enum.GetValues(src.GetType());

        int j = (Array.IndexOf(arr, src) + 1) % arr.Length; // <- Modulo % Arr.Length added

        return arr[j];
    }
}
