using System;

public static class NumericExtensions
{
    public static string ToStringBinary(this int value)
    {
        int[] b = new int[8];
        int pos  = 7;
        int i  = 0;
        while (i < 8)
        {
            if ((value & (1 << i)) != 0)
            {
                b[pos] = 1;
            }
            else
            {
                b[pos] = 0;
            }
            pos-=1;
            i+=1;
        }
        return $"{b[7]}{b[6]}{b[5]}{b[4]}{b[3]}{b[2]}{b[1]}{b[0]}{value}";
    }
    
    public static string ToStringBinary(this UInt64 value)
    {
        return Convert.ToString((long) value, 2);
    }
}
