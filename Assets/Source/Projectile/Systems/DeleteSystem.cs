using Entitas;
using Unity.VisualScripting;

namespace Projectile
{
    public class DeleteSystem
    {
        // Remove projectile only at the end of the frame.

        /// <summary> Should be called at the end of the frame.</summary>
        public void Update(ref Planet.PlanetState planet)
        {
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                if (entityP.isProjectileDelete)
                {
                    planet.RemoveProjectile(entityP.projectileID.ID);
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                }
            }
        }
    }
}