#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class Screenshot
{
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        ScreenCapture.CaptureScreenshot("Screenshots/Screenshot" + GetCh() + GetCh() + GetCh() + "_" + Screen.width + "x" + Screen.height + ".png", 1);
    }


    [MenuItem("Screenshot/Freeze")]
    public static void Frees()
    {
        CameraFollow.STOP = !CameraFollow.STOP;
    }

    [MenuItem("Screenshot/Zoom")]
    public static void Zoom()
    {
        GameState.InputProcessSystem.scale += 1;
    }

    public static char GetCh()
    {
        return (char)UnityEngine.Random.Range('A', 'Z');
    }
}
#endif
