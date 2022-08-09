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

        public float Speed;             // Runing speed in tiles/seconds.
        public Vec2f Velocity;
        public Vec2f Acceleration;      // Instantaneous, reset to zero at the end of the frame.
        public int Direction; // 1 or -1 last direction the player was looking at

        public Enums.AgentMovementState MovementState;

        //public Flags MovementFlags; 
        public bool AffectedByGravity;
        public bool Invulnerable;
        public bool OnGrounded;
        public bool Droping;
        public bool WantToDrop;

        public int JumpCounter;
        public float DashCooldown;
        public float SlashCooldown;

        //[Flags]
        //public enum Flags : byte
        //{
        //    Running = 1 << 0,
        //    AffectedByGravity = 1 << 1,     // is used to know whether the agent is affected by the gravity
        //    Invulnerable = 1 << 2,          // used for dashing
        //    OnGrounded = 1 << 3,            // are we standing on a block or not // Updated by collision system.
        //    Droping = 1 << 4,               // dropping
        //    WantToDrop = 1 << 5,            // dropKey is pressed or not
        //}
    }
}
