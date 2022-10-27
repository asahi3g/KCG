using KMath;
using Particle;
using UnityEngine;

namespace Projectile
{
    public class ProcessState
    {
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                if (entityP.hasProjectileRange)
                {
                    if ((entityP.projectilePhysicsState.Position - entityP.projectileStart.StarPos).Magnitude > entityP.projectileRange.Range)
                    {
                        entityP.isProjectileDelete = true;
                        planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, ParticleEmitterType.DustEmitter);
                    }
                }

                if (entityP.projectileType.Type == Enums.ProjectileType.GasGrenade)
                {
                    float elapsed = Time.realtimeSinceStartup - entityP.projectileStart.StartTime;

                    if (elapsed > 3.0f)
                    {
                        CircleSmoke.Spawn(1, entityP.projectilePhysicsState.Position, new Vec2f(0.1f, 0.2f), new Vec2f(0.1f, 0.2f));
                    }

                    if (elapsed > 8.0f)
                    {
                        entityP.isProjectileDelete = true;
                    }
                }
                else if(entityP.projectileType.Type == Enums.ProjectileType.Flare)
                {
                    float elapsed = Time.realtimeSinceStartup - entityP.projectileStart.StartTime;

                    if (elapsed > 1.0f)
                    {
                        CircleSmoke.SpawnFlare(1, entityP.projectilePhysicsState.Position, new Vec2f(0.05f, 10.0f), new Vec2f(0.01f, 1.0f));
                    }

                    if (elapsed > 8.0f)
                    {
                        entityP.isProjectileDelete = true;
                    }
                }
            }
        }
    }
}
