using TMPro;

public static class TMPExtensions
{

    public static void Clear(this TMP_Text value)
    {
        if (value == null) return;
        value.SetText(string.Empty);
    }
    
}
