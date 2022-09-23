using Entitas;
using KMath;
using System;

namespace Agent
{
    [Agent]
    public class PhysicsStateComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;

        public float Speed;                     // Runing speed in tiles/seconds.
        public float InitialJumpVelocity;       // Velocity at the beginning of the first jump
        public Vec2f Velocity;
        public Vec2f Acceleration;      // Instantaneous, reset to zero at the end of the frame.
        public int MovingDirection;           // 1 or -1 last direction the player was looking at
        public int FacingDirection;

        public Enums.AgentMovementState MovementState;
        public AgentAnimation LastAgentAnimation;
        public bool SetMovementState;

        //public Flags MovementFlags; 
        public bool AffectedByGravity;  // is used to know whether the agent is affected by the gravity
        public bool AffectedByFriction;
        public bool Invulnerable;       // used for dashing
        public bool OnGrounded;         // are we standing on a block or not // Updated by collision system.
        public bool Droping;            // dropping

        public int   JumpCounter;

        public float IdleAfterShootingTime;
        public float SlidingTime;
        public float DyingDuration;
        public bool ActionInProgress;
        public bool ActionJustEnded;
        public float ActionDuration;
        public float ActionCooldown;
        public float DashCooldown;
        public float SlashCooldown;
        public float StaggerDuration;         
        public float RollCooldown;            // cooldown for rolling
        public float RollImpactDuration;      // after the roll you take time to get up again
    }
}
