using Entitas;

namespace Projectile
{
    [Projectile]
    public class ExplosiveComponent : IComponent
    {
        // Explosive Properties.
        public float BlastRadius;
        // Damage at the origin
        public int MaxDamage;
        // Time in seconds it takes to explode after first hit.
        public float Elapse;
    }
}
