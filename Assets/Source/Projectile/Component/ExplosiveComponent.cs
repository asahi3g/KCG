using Entitas;

namespace Projectile
{
    [Projectile]
    public class ExplosiveComponent : IComponent
    {
        // Explosive Properties.
        public float BlastRadius;
        /// <summary> Damage at the origin</summary>
        public int MaxDamage;
        /// <summary> Time in seconds it takes to explode after first hit.</summary>
        public float Elapse;
    }
}
