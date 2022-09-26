using KMath;
using Particle;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Projectile
{
    public class ProcessState
    {
        public void Update(ref Planet.PlanetState planet)
        {
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                if (entityP.hasProjectileRange)
                {
                    if ((entityP.projectilePhysicsState.Position - entityP.projectileStart.StarPos).Magnitude > entityP.projectileRange.Range)
                    {
                        entityP.isProjectileDelete = true;
                        planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
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
                }
            }
        }
    }
}
