using System;
using Entitas;
using KMath;

namespace Agent
{
    public static class MovableExtenstion
    {
        public static void CheckPrecision(this ref float num)
        {
            if (Math.Abs(num - 0f) <= 0.01f)
            {
                num = 0f;
            }
        }
    }

    [Agent]
    public class MovableComponent : IComponent
    {
        public float Speed;
        public Vec2f Velocity;
        public Vec2f Acceleration;

        public bool AffectedByGravity;          // is used to know whether an object is affected by the gravity
        public bool AffectedByGroundFriction;   // used to determine whether the
                                                // friction type is ground friction or air friction

        public bool Invulnerable; // used for dashing
        public bool OnGrounded;   // are we standing on a block or not
        public bool SlidingRight; // Wall sliding down
        public bool SlidingLeft;  // Wall sliding down

        public bool Droping;      // dropping
        public bool WantToDrop;   // DropKey is pressed or not
    }
}