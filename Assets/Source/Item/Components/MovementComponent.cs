using System;
using Entitas;
using KMath;

namespace Item
{
    [ItemParticle]
    public class MovementComponent : IComponent
    {
        public Vec2f Velocity;
        public Vec2f Acceleration;

        public bool OnGrounded;
    }
}
