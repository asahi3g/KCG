using Entitas;
using KMath;

namespace Vehicle
{
    [Vehicle]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;
    }
}
