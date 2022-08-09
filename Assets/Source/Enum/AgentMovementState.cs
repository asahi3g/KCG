namespace Enums
{
    public enum AgentMovementState
    {
        None,
        Idle,
        Move, // moving left or right
        Falling, // falling from the sky
        JetPackFlying,  // Using the jetpack
        Dashing,        // Dashing
        SlidingLeft,    // Wall sliding down
        SlidingRight,    // Wall sliding down
        SwordSlash,
    }
}