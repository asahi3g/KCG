namespace Enums
{
    /// <summary>
    /// Enum for detecting player's input device
    /// </summary>
    public enum eInputDevice
    {
        KeyboardMouse,
        Controller,
        Invalid
    };

    /// <summary>
    /// Enum for detecting key event (Pressed, Released, or NULL)
    /// </summary>
    public enum eKeyEvent
    {
        Press,
        Release,
        Invalid
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
