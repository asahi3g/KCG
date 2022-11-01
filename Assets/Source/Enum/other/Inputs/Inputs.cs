namespace Enums
{
    /// <summary>
    /// Enum for detecting player's input device
    /// </summary>
    public enum InputDeviceType
    {
        InvalidInputDeviceType,
        KeyboardMouse,
        Controller
    };

    /// <summary>
    /// Enum for detecting key event (Pressed, Released, or NULL)
    /// </summary>
    public enum eKeyEvent
    {
        Invalid,
        Press,
        Release
    }


    /// <summary>
    /// Enum for specifing player state to assign inputs
    /// </summary>
    public enum PlayerState
    {
        Pedestrian,
        Vehicle
    }
}
