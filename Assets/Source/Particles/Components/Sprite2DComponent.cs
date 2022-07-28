using Entitas;
using KMath;
using UnityEngine;

namespace Particle
{
    [Particle]
    public class Sprite2DComponent : IComponent
    {
        public int SpriteId;

        public Vec2f[] Vertices; // uised for debris
        public Vec2f[] TextureCoords; // used for debris

        public Vec2f Size;
    }
}
