using UnityEngine;

namespace Utility
{
    public class Utils
    {
        public static void Assert(bool condition, string message = "")
        {
            UnityEngine.Assertions.Assert.IsTrue(condition, message);

            if (!condition)
            {
                Application.Quit();
            }
        }
    }
}
