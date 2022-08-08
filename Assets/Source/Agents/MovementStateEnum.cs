namespace Agent
{

    public enum MovementState
    {
        None,
        Move,
        Idle,
        Falling,
        Flying, // using the jetpack
        Sliding, // sliding down a wall
        Dashing // dashing
    }
}