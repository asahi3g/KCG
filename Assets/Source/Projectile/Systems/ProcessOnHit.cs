using KMath;
using Particle;
using UnityEngine;

namespace Projectile
{
    public class ProcessOnHit
    {
        public void Update()
        {
            for (int i = 0; i < GameState.Planet.ProjectileList.Length; i++)
            {
                ProjectileEntity projectileEntity = GameState.Planet.ProjectileList.Get(i);

                if (!projectileEntity.hasProjectileOnHit)
                    continue;

                if (projectileEntity.projectileOnHit.AgentID != -1)
                {
                    AgentEntity agent = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(projectileEntity.projectileOnHit.AgentID);
                    OnHitAgent(projectileEntity, agent);
                }

                switch (projectileEntity.projectileType.Type)
                {
                    case Enums.ProjectileType.FragGrenade:
                        FragGrenadeExplosive(projectileEntity);
                        break;
                    case Enums.ProjectileType.Grenade:
                        Explosive(projectileEntity);
                        break;
                    case Enums.ProjectileType.Rocket:
                        Explosive(projectileEntity);
                        break;
                    case Enums.ProjectileType.Arrow:
                        Arrow(projectileEntity);
                        break;
                    case Enums.ProjectileType.GasGrenade:
                        GasGrenade(projectileEntity);
                        break;
                    case Enums.ProjectileType.Flare:
                        FlareGrenade(projectileEntity);
                        break;
                    default:
                        Default(projectileEntity);
                        break;
                }
            }
        }

        public void OnHitAgent(ProjectileEntity projectileEntity, AgentEntity agentEntity)
        {
            if (!agentEntity.hasAgentStats)
                return;

            var stats = agentEntity.agentStats;
            if (projectileEntity.hasProjectileDamage)
            {
                int damage = projectileEntity.projectileDamage.Damage;

                GameState.Planet.AddParticleEmitter(projectileEntity.projectilePhysicsState.Position, ParticleEmitterType.Blood);
            }
        }

        public void Default(ProjectileEntity pEntity)
        {
            GameState.Planet.AddParticleEmitter(
                pEntity.projectilePhysicsState.Position, ParticleEmitterType.DustEmitter);
            pEntity.isProjectileDelete = true;
        }

        public void Explosive(ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse <= 0.05f)
                GameState.Planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, ParticleEmitterType.DustEmitter);

            if (elapse - pEntity.projectileExplosive.Elapse <= 0.05f)
                return;

            Vec2f pos = pEntity.projectileOnHit.LastHitPos;
            float radius = pEntity.projectileExplosive.BlastRadius;
            int damage = pEntity.projectileExplosive.MaxDamage;

            Circle2D explosionCircle = new Circle2D { Center = pos, Radius = radius };

            for (int i = 0; i < GameState.Planet.AgentList.Length; i++)
            {
                AgentEntity agentEntity = GameState.Planet.AgentList.Get(i);
                if (!agentEntity.isAgentPlayer && agentEntity.isAgentAlive)
                {
                    var agentPhysicsState = agentEntity.agentPhysicsState;
                    var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                    Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                    AABox2D agentBox = new AABox2D(new Vec2f(agentPhysicsState.PreviousPosition.X, agentPhysicsState.Position.Y), agentBox2dCollider.Size);

                    if (explosionCircle.InterSectionAABB(ref agentBox))
                    {
                        // Todo: Deals with case: colliding with an object and an agent at the same frame.
                        GameState.Planet.AddFloatingText(damage.ToString(), 2.5f, new Vec2f(0.0f, 0.1f), agentEntity.agentPhysicsState.Position);
                    }
                }
            }
            // Todo: Do a circle collision test.
            pEntity.isProjectileDelete = true;
        }


        public void FragGrenadeExplosive(ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            GameState.Planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, ParticleEmitterType.ExplosionEmitter);
            GameState.Planet.AddParticleEmitter(pEntity.projectilePhysicsState.Position, ParticleEmitterType.ShrapnelEmitter);

            Vec2f pos = pEntity.projectileOnHit.LastHitPos;
            float radius = pEntity.projectileExplosive.BlastRadius;
            int damage = pEntity.projectileExplosive.MaxDamage;

            Circle2D explosionCircle = new Circle2D { Center = pos, Radius = radius };

            for (int i = 0; i < GameState.Planet.AgentList.Length; i++)
            {
                AgentEntity agentEntity = GameState.Planet.AgentList.Get(i);
                if (!agentEntity.isAgentPlayer && agentEntity.isAgentAlive)
                {
                    var agentPhysicsState = agentEntity.agentPhysicsState;
                    var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                    Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                    AABox2D agentBox = new AABox2D(new Vec2f(agentPhysicsState.PreviousPosition.X, agentPhysicsState.Position.Y), agentBox2dCollider.Size);

                    if (explosionCircle.InterSectionAABB(ref agentBox))
                    {
                        // Todo: Deals with case: colliding with an object and an agent at the same frame.
                        GameState.Planet.AddFloatingText(damage.ToString(), 2.5f, new Vec2f(0.0f, 0.1f), agentEntity.agentPhysicsState.Position);
                    }
                }
            }
            // Todo: Do a circle collision test.
            pEntity.isProjectileDelete = true;
        }

        public void Arrow(ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse <= 0.05f)
            {
                GameState.Planet.AddParticleEmitter(
                    pEntity.projectilePhysicsState.Position, ParticleEmitterType.DustEmitter);
                pEntity.projectilePhysicsState.Velocity = Vec2f.Zero;
                return;
            }

            if (elapse >= 12.0f)
                pEntity.isProjectileDelete = true;
        }

        public void GasGrenade(ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            if (elapse >= 12.0f)
                pEntity.isProjectileDelete = true;
        }

        public void FlareGrenade(ProjectileEntity pEntity)
        {
            float elapse = Time.time - pEntity.projectileOnHit.FirstHitTime;

            // Spawn drop ship after throwing flare

            if(!pEntity.projectileOnHit.ParticleSpawned)
            {
                pEntity.projectileOnHit.ParticleSpawned = true;
                GameState.Planet.AddVehicle(Enums.VehicleType.DropShip, new Vec2f(0,0));
            }

            if (elapse >= 7.0f)
                pEntity.isProjectileDelete = true;
        }
    }
}
