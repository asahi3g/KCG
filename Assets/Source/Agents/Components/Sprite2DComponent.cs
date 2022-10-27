using Entitas;
using KMath;

namespace Agent
{
    [Agent]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;
    }
}
