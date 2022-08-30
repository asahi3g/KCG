using UnityEngine;
using KMath;
using System.Collections.Generic;
using System.Collections;
using Collisions;
using Entitas;

namespace Projectile
{
    public class ProcessCollisionSystem
    {
        List<ProjectileEntity> ToRemoveList = new();
        List<ProjectileEntity> ToRemoveArrowList = new();
        List<ProjectileEntity> PopGasList = new();
        public void Update(ref PlanetTileMap.TileMap tileMap)
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = Contexts.sharedInstance.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var physicsState = entity.projectilePhysicsState;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y), entity.projectileSprite2D.Size);

                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        if(entity.projectileType.Type == Enums.ProjectileType.Arrow)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else
                        {
                            entity.Destroy();
                        }
                        return;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                    }
                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.angularVelocity))
                {
                    if(entity.projectileCollider.isFirstSolid)
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.Arrow)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else
                        {
                            entity.Destroy();
                        }
                        return;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                    }
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y), entity.projectileSprite2D.Size);

                // If is colliding left-right stop x movement
                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.Arrow)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else
                        {
                            entity.Destroy();
                        }
                        return;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                    }
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.Arrow)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                        else
                        {
                            entity.Destroy();
                        }
                        return;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.angularVelocity = Vec2f.Zero;
                        }
                    }
                }
            }
        }

        float elapsed = 0.0f;
        private float bounceValue = 0.4f;
        private bool deleteArrows;
        private bool PopGas;

        // new version of the update function
        // uses the planet state to remove the projectile
        public void UpdateEx(ref Planet.PlanetState planet)
        {
            ToRemoveList.Clear();

            // Get Delta Time
            float deltaTime = Time.deltaTime;
            ref PlanetTileMap.TileMap tileMap = ref planet.TileMap;

            // Get Vehicle Physics Entity
            var entities = planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var physicsState = entity.projectilePhysicsState;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y), entity.projectileSprite2D.Size);

                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        //entity.Destroy();
                        ToRemoveList.Add(entity);
                        continue;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                            PopGasList.Add(entity);
                            PopGas = true;
                        }
                    }
                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.angularVelocity))
                {
                    if(entity.projectileCollider.isFirstSolid)
                    {
                        //entity.Destroy();
                        ToRemoveList.Add(entity);
                         continue;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                            PopGasList.Add(entity);
                            PopGas = true;
                        }
                    }
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y), entity.projectileSprite2D.Size);

                // If is colliding left-right stop x movement
                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        //entity.Destroy();
                        ToRemoveList.Add(entity);
                         continue;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                            PopGasList.Add(entity);
                            PopGas = true;
                        }
                    }
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileCollider.isFirstSolid)
                    {
                        //entity.Destroy();
                        ToRemoveList.Add(entity);
                         continue;
                    }
                    else
                    {
                        if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                        {
                            entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                            PopGasList.Add(entity);
                            PopGas = true;
                        }
                    }
                }
            }

            foreach (var entityP in ToRemoveList)
            {
                if(entityP.projectileType.Type == Enums.ProjectileType.Grenade)
                {
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                    // Check if projectile has hit a enemy.
                    var entitiesA = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entity in entitiesA)
                    {   
                        float dist = Vec2f.Distance(new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y), new Vec2f(entityP.projectilePhysicsState.Position.X, entityP.projectilePhysicsState.Position.Y));

                        float radius = 2.0f;

                        if (dist < radius)
                        {
                            Vec2f entityPos = entity.agentPhysicsState.Position;
                            Vec2f bulletPos = entityP.projectilePhysicsState.Position;
                            Vec2f diff = bulletPos - entityPos;
                            diff.Y = 0;
                            diff.Normalize();

                            Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);

                            if (entity.hasAgentStats)
                            {
                                var stats = entity.agentStats;
                                stats.Health -= 25;
                            

                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + 25, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), 
                                    new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y + 0.35f));
                            }
                        }
                    }
                    planet.RemoveProjectile(entityP.projectileID.Index);
                }
                else if (entityP.projectileType.Type == Enums.ProjectileType.Rocket)
                {
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                    // Check if projectile has hit a enemy.
                    var entitiesA = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

                    // Todo: Create a agent colision system?
                    foreach (var entity in entitiesA)
                    {
                        float dist = Vec2f.Distance(new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y), new Vec2f(entityP.projectilePhysicsState.Position.X, entityP.projectilePhysicsState.Position.Y));

                        float radius = 4.0f;

                        if (dist < radius)
                        {
                            Vec2f entityPos = entity.agentPhysicsState.Position;
                            Vec2f bulletPos = entityP.projectilePhysicsState.Position;
                            Vec2f diff = bulletPos - entityPos;
                            diff.Y = 0;
                            diff.Normalize();

                            Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);

                            if (entity.hasAgentStats)
                            {
                                var stats = entity.agentStats;
                                stats.Health -= 100;
                               

                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + 100, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), 
                                    new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y + 0.35f));
                            }
                        }
                    }
                    planet.RemoveProjectile(entityP.projectileID.Index);
                }
                else if (entityP.projectileType.Type == Enums.ProjectileType.Arrow)
                {
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);

                    entityP.projectilePhysicsState.Velocity = Vec2f.Zero;

                    DeleteProjectile(entityP);

                    deleteArrows = true;
                }
                else if (entityP.projectileType.Type == Enums.ProjectileType.Bullet)
                {
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);

                    planet.RemoveProjectile(entityP.projectileID.Index);
                }
                else if (entityP.projectileType.Type == Enums.ProjectileType.GasGrenade)
                {
                    planet.AddParticleEmitter(entityP.projectilePhysicsState.Position, Particle.ParticleEmitterType.DustEmitter);
                }
            }

            // Arrow Deleting
            if (deleteArrows || PopGas)
                elapsed += Time.deltaTime;

            if(elapsed > 2.5f && elapsed < 2.53f)
            {
                for (int j = 0; j < PopGasList.Count; j++)
                {
                    if (PopGasList.Count < 1)
                        PopGas = false;

                    if (PopGasList[j].isEnabled)
                    {
                        planet.AddParticle(new Vec2f(PopGasList[j].projectilePhysicsState.Position.X - 2.0f, PopGasList[j].projectilePhysicsState.Position.Y - 1.5f), new Vec2f(0.0f, 0.5f), Particle.ParticleType.GasParticle);
                    }
                }
                PopGas = false;
            }

            if(elapsed > 8.0f)
            {
                for(int i = 0; i<  ToRemoveArrowList.Count; i++)
                {
                    if(ToRemoveArrowList[i].isEnabled)
                        ToRemoveArrowList[i].Destroy(); 
                }

                for (int j = 0; j < PopGasList.Count; j++)
                {
                    if (PopGasList[j].isEnabled)
                        PopGasList[j].Destroy();
                }
                deleteArrows = false;
                PopGas = false;
                elapsed = 0.0f;
            }
        }

        public void DeleteProjectile(ProjectileEntity arrow)
        {
            ToRemoveArrowList.Add(arrow);
        }
    }
}
