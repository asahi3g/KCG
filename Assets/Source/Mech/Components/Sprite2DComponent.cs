using Entitas;
using KMath;
using UnityEngine;

namespace Mech
{
    [Mech]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;
    }
}
