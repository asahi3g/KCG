namespace Utility
{
    public class Utils
    {
        public static void Assert(bool condition, string message = "")
        {
            UnityEngine.Application.Quit();
            UnityEngine.Assertions.Assert.IsTrue(condition, message);

            if (!condition)
            {
                UnityEngine.Application.Quit();
            }
        }
    }
}
