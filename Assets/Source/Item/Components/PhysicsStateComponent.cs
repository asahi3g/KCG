using Entitas;
using KMath;

namespace Item
{
    [ItemParticle]
    public class PhysicsStateComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;

        public Vec2f Velocity;
        public Vec2f Acceleration;  // Instantaneous, reset to zero at the end of the frame.

        public bool OnGrounded;
    }
}
