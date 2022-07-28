using Entitas;
using KMath;

namespace Particle
{
    [Particle]
    public class Box2DColliderComponent : IComponent
    {
        public Vec2f Size;
        public Vec2f Offset;
    }
} 
