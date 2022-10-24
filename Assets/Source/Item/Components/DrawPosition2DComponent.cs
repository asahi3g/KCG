using Entitas;
using KMath;

namespace Item
{
    /// <summary>
    /// Used by pickup actions.
    /// </summary>
    [ItemParticle]
    public class DrawPosition2DComponent : IComponent
    {
        public Vec2f Acceleration;
        public Vec2f Velocity;
        public Vec2f Position;
    }
}
