using Entitas;
using KMath;
using Particle;
using System;
using UnityEngine;
namespace Projectile
{
    public class ProcessOnHit
    {
        public void Update(ref Planet.PlanetState planet)
        {
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity projectileEntity = planet.ProjectileList.Get(i);

                if (!projectileEntity.hasProjectileOnHit)
                    continue;

                switch (projectileEntity.projectileType.Type)
                {
                    case Enums.ProjectileType.Grenade:
                        planet.AddParticleEmitter(
                            projectileEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                        // Todo: Do a circle collision test.
                        break;
                    case Enums.ProjectileType.Rocket:
                        planet.AddParticleEmitter(
                            projectileEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                        // Todo: Do a circle collision test.
                        break;
                    case Enums.ProjectileType.Arrow:
                        planet.AddParticleEmitter(
                            projectileEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                        projectileEntity.projectilePhysicsState.Velocity = Vec2f.Zero;
                        // Delete projectile.
                        break;
                    case Enums.ProjectileType.Bullet:
                        planet.AddParticleEmitter(
                            projectileEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);

                        break;
                    case Enums.ProjectileType.GasGrenade:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void GasGrenade(ProjectileEntity pEntity)
    {
        float elapse = Time.deltaTime - pEntity.projectileOnHit.HitTime;
        if (elapse > 3.0f)
            CircleSmoke.Spawn(1, pEntity.projectilePhysicsState.Position, new Vec2f(0.1f, 0.2f), new Vec2f(0.1f, 0.2f));

        if (elapse > 8.0f)

}
