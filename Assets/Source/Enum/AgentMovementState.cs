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
        Rolling, //Rolling
        Crouch,
        Limp,
        Drink,
        Crouch_Move,
        StandingUpAfterRolling,  // standing up after rolling
        Stagger,        // Stagger
        SlidingLeft,    // Wall sliding down
        SlidingRight,    // Wall sliding down
        SwordSlash,
        FireGun,
        UseTool,
        KnockedDownFront, // the death animation front version
        LyingFront, // dead state front version
        KnockedDownBack, // the death animation back version
        LyingBack, // dead state back version
    }
}