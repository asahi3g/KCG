using Entitas;
using KMath;
using UnityEngine;

namespace Vehicle.Pod
{
    [Pod]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;
        public Vec2f Size;
    }
}
