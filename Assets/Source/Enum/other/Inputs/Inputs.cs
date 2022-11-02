namespace Enums
{
    // Enum for detecting player's input device
    public enum InputDeviceType
    {
        InvalidInputDeviceType,
        KeyboardMouse,
        Controller
    };

    // Enum for detecting key event (Pressed, Released, or NULL)
    public enum eKeyEvent
    {
        Invalid,
        Press,
        Release
    }


    // Enum for specifing player state to assign inputs
    public enum PlayerState
    {
        Pedestrian,
        Vehicle
    }
}
