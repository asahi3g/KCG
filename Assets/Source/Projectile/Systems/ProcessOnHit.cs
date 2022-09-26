using Entitas;
using KMath;
using Particle;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

                if (projectileEntity.projectileOnHit.AgentID != -1)
                {
                    AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(projectileEntity.projectileOnHit.AgentID);
                    OnHitAgent(projectileEntity, agent, ref planet);
                }

                switch (projectileEntity.projectileType.Type)
                {
                    case Enums.ProjectileType.FragGrenade:
                        FragGrenadeExplosive(ref planet, projectileEntity);
                        break;
                    case Enums.ProjectileType.Grenade:
                        Explosive(ref planet, projectileEntity);
                        break;
                    case Enums.ProjectileType.Rocket:
                        Explosive(ref planet, projectileEntity);
                        break;
                    case Enums.ProjectileType.Arrow:
                        Arrow(ref planet, projectileEntity);
                        break;
                    case Enums.ProjectileType.GasGrenade:
                        GasGrenade(ref planet, projectileEntity);
                        break;
                    default:
                        Default(ref planet, projectileEntity);
                        break;
                }
            }
        }

        public void OnHitAgent(ProjectileEntity projectileEntity, AgentEntity agentEntity, ref Planet.PlanetState planet)
        {
            if (!agentEntity.hasAgentStats)
                return;

            var stats = agentEntity.agentStats;
            if (projectileEntity.hasProjectileDamage)
            {
                int damage = projectileEntity.projectileDamage.Damage;
                stats.Health -= damage;

                // Debug floating Text
                //planet.AddFloatingText(damage.ToString(), 1.5f, new Vec2f(0.0f, 0.1f), agentEntity.agentPhysicsState.Position, new Color(1.0f, 0, 0, 1.0f), 12);

                planet.AddParticleEmitter(projectileEntity.projectilePhysicsState.Position, ParticleEmitterType.Blood);
            }
        }

        public void Default(ref Planet.PlanetState planet, ProjectileEntity pEntity)
        {
            planet.AddParticleEmitter(
                pEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
            planet.RemoveProjectile(pEntity.projectileID.Index);
        }

        public void Explosive(ref Planet.PlanetState planet, ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse <= 0.05f)
                planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);

            if (elapse - pEntity.projectileExplosive.Elapse <= 0.05f)
                return;

            Vec2f pos = pEntity.projectileOnHit.LastHitPos;
            float radius = pEntity.projectileExplosive.BlastRadius;
            int damage = pEntity.projectileExplosive.MaxDamage;

            Circle2D explosionCircle = new Circle2D { Center = pos, Radius = radius };

            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agentEntity = planet.AgentList.Get(i);
                if (!agentEntity.isAgentPlayer && agentEntity.isAgentAlive)
                {
                    var agentPhysicsState = agentEntity.agentPhysicsState;
                    var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                    Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                    AABox2D agentBox = new AABox2D(new Vec2f(agentPhysicsState.PreviousPosition.X, agentPhysicsState.Position.Y), agentBox2dCollider.Size);

                    if (explosionCircle.InterSectionAABB(ref agentBox))
                    {
                        // Todo: Deals with case: colliding with an object and an agent at the same frame.
                        planet.AddFloatingText(damage.ToString(), 2.5f, new Vec2f(0.0f, 0.1f), agentEntity.agentPhysicsState.Position);
                    }
                }
            }
                // Todo: Do a circle collision test.
                planet.RemoveProjectile(pEntity.projectileID.Index);
        }


        public void FragGrenadeExplosive(ref Planet.PlanetState planet, ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

                planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.ExplosionEmitter);
                planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.ShrapnelEmitter);

            Vec2f pos = pEntity.projectileOnHit.LastHitPos;
                float radius = pEntity.projectileExplosive.BlastRadius;
                int damage = pEntity.projectileExplosive.MaxDamage;

                Circle2D explosionCircle = new Circle2D { Center = pos, Radius = radius };

                for (int i = 0; i < planet.AgentList.Length; i++)
                {
                    AgentEntity agentEntity = planet.AgentList.Get(i);
                    if (!agentEntity.isAgentPlayer && agentEntity.isAgentAlive)
                    {
                        var agentPhysicsState = agentEntity.agentPhysicsState;
                        var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                        Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                        AABox2D agentBox = new AABox2D(new Vec2f(agentPhysicsState.PreviousPosition.X, agentPhysicsState.Position.Y), agentBox2dCollider.Size);

                        if (explosionCircle.InterSectionAABB(ref agentBox))
                        {
                            // Todo: Deals with case: colliding with an object and an agent at the same frame.
                            planet.AddFloatingText(damage.ToString(), 2.5f, new Vec2f(0.0f, 0.1f), agentEntity.agentPhysicsState.Position);
                        }
                    }
                }
                // Todo: Do a circle collision test.
                planet.RemoveProjectile(pEntity.projectileID.Index);
        }

        public void Arrow(ref Planet.PlanetState planet, ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse <= 0.05f)
            {
                planet.AddParticleEmitter(
                    pEntity.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                pEntity.projectilePhysicsState.Velocity = Vec2f.Zero;
                return;
            }

            if (elapse >= 12.0f)
                planet.RemoveProjectile(pEntity.projectileID.Index);
        }

        public void GasGrenade(ref Planet.PlanetState planet, ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse >= 12.0f)
                planet.RemoveProjectile(pEntity.projectileID.Index);
        }
    }
}
