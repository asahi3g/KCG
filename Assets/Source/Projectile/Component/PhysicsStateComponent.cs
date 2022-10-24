using System;
using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class PhysicsStateComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;
        public float Rotation;

        public Vec2f Velocity;
        public Vec2f Acceleration; // Instantaneous, reset to zero at the end of the frame.

        public bool OnGrounded = true;
    }
}

