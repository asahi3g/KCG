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
        Stagger,        // Stagger
        SlidingLeft,    // Wall sliding down
        SlidingRight,    // Wall sliding down
        SwordSlash,
        FireGun,
        KnockedDownFront, // the death animation front version
        LyingFront, // dead state front version
    }
}