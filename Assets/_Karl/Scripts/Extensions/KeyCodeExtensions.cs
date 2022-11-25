using UnityEngine;

public static class KeyCodeExtensions
{

    public static string ToStringPretty(this KeyCode keyCode){
        switch(keyCode){
            case KeyCode.Alpha0: return 0.ToString();
            case KeyCode.Alpha1: return 1.ToString();
            case KeyCode.Alpha2: return 2.ToString();
            case KeyCode.Alpha3: return 3.ToString();
            case KeyCode.Alpha4: return 4.ToString();
            case KeyCode.Alpha5: return 5.ToString();
            case KeyCode.Alpha6: return 6.ToString();
            case KeyCode.Alpha7: return 7.ToString();
            case KeyCode.Alpha8: return 8.ToString();
            case KeyCode.Alpha9: return 9.ToString();
            
            case KeyCode.UpArrow: return "Up";
            case KeyCode.DownArrow: return "Down";
            case KeyCode.LeftArrow: return "Left";
            case KeyCode.RightArrow: return "Right";
            
            case KeyCode.Mouse0: return "Right Mouse Button";
            case KeyCode.Mouse1: return "Left Mouse Button";
            case KeyCode.Mouse2: return "Middle Mouse Button";   
        }
        return keyCode.ToString();
    }
}
