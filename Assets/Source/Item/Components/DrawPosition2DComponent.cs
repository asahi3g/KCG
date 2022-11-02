using Entitas;
using KMath;

namespace Item
{
    // Used by pickup actions.
    [ItemParticle]
    public class DrawPosition2DComponent : IComponent
    {
        public Vec2f Velocity;
        public Vec2f Position;
    }
}
