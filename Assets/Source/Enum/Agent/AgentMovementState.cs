namespace Enums
{
    public enum AgentMovementState
    {
        None = 0,
        Idle,
        IdleAfterShooting,
        Move, // moving left or right
        MoveBackward,
        Falling, // falling from the sky
        JetPackFlying,  // Using the jetpack
        Dashing,        // Dashing
        Rolling, //Rolling
        Crouch,
        Jump,
        Flip,
        Limp,
        Drink,
        Crouch_Move,
        Crouch_MoveBackward,
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
        MonsterAttack,
        PickaxeHit,
        ChoppingTree,
    }
}
