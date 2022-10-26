using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;
    }
}
