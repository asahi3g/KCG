using UnityEngine;

public class Utils
{

    public static void Assert(bool condition, string message = "")
    {
        UnityEngine.Assertions.Assert.IsTrue(condition);

        if (!condition)
        {
            Debug.LogError(message);
            Application.Quit();
        }
    }
}
