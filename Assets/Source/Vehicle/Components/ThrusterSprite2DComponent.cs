using Entitas;
using KMath;

namespace Vehicle
{
    [Vehicle]
    public class ThrusterSprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;

        public Vec2f Position1;
        public Vec2f Position2;
    }
}
