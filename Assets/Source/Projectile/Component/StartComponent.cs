using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class StartComponent : IComponent
    {
        public Vec2f StarPos; 
        public float StartTime;
    }
}
