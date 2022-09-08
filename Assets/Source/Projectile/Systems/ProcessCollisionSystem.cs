using UnityEngine;
using KMath;
using Collisions;
using Particle;

namespace Projectile
{
    public class ProcessCollisionSystem
    {
        // new version of the update function
        // uses the planet state to remove the projectile
        public void UpdateEx(ref Planet.PlanetState planet)
        {
            float deltaTime = Time.deltaTime;
            ref PlanetTileMap.TileMap tileMap = ref planet.TileMap;

            var entities = planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            foreach (var entity in entities)
            {
                var physicsState = entity.projectilePhysicsState;
                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y), entity.projectileSprite2D.Size);
                var box2dCollider = entity.physicsBox2DCollider;

                Vec2f position = physicsState.PreviousPosition + box2dCollider.Offset;



                // Collising with terrain with raycasting
                var rayCastingResult =
                Collisions.Collisions.RayCastAgainstTileMapBox2d(tileMap, 
                new KMath.Line2D(physicsState.PreviousPosition, physicsState.Position), entity.projectileSprite2D.Size.X, entity.projectileSprite2D.Size.Y);
                Vec2f oppositeDirection = (physicsState.PreviousPosition - physicsState.Position).Normalized;

                if (rayCastingResult.Intersect)
                {                  
                    physicsState.Position = rayCastingResult.Point + oppositeDirection * entity.projectileSprite2D.Size * 0.5f;
                    physicsState.Velocity = new Vec2f();
                    entity.AddProjectileOnHit(-1);
                    continue;
                }

                
            
                var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));
                 // collision with agents
                 bool removeThisEntity = false;
                 Vec2f collidedPosition = position;
                 foreach (var agent in agents)
                 {
                    Collisions.Box2D entityBox = new Collisions.Box2D{x = position.X, y = position.Y, w = box2dCollider.Size.X, h = box2dCollider.Size.Y};
                Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
                    if (!agent.isAgentPlayer && agent.agentState.State == Agent.AgentState.Alive)
                    {
                        var agentPhysicsState = agent.agentPhysicsState;
                        var agentBox2dCollider = agent.physicsBox2DCollider;

                        Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                        Collisions.Box2D agentBox = new Collisions.Box2D{x = agentPosition.X, y = agentPosition.Y, w = agentBox2dCollider.Size.X, h = agentBox2dCollider.Size.Y};
                        bool collided = Collisions.Collisions.SweptBox2dCollision(ref entityBox, delta, agentBox, false);
                        
                        if (collided)
                        {
                            collidedPosition = new Vec2f(entityBox.x, entityBox.y) - box2dCollider.Offset;
                            if (agent.hasAgentStats)
                            {
                                var stats = agent.agentStats;
                                stats.Health -= 10;
                            

                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + 10, 2.5f, new Vec2f(0.0f, 0.1f), agentPosition);
                            }

                            removeThisEntity = true;
                            break;
                        }
                    }
                 }

                 if (removeThisEntity)
                 {
                    physicsState.Position = collidedPosition;
                    ToRemoveList.Add(entity);
                 }


                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                    {
                        entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                        PopGasList.Add(entity);
                    }
                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.angularVelocity))
                {
                    
                    if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                    {
                        entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                        PopGasList.Add(entity);
                    }
                    
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y), entity.projectileSprite2D.Size);

                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                    {
                        entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                        PopGasList.Add(entity);
                    }
                    
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.angularVelocity))
                {
                    if (entity.projectileType.Type == Enums.ProjectileType.GasGrenade)
                    {
                        entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                        PopGasList.Add(entity);
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
            if (deleteArrows)
                elapsed += Time.deltaTime;

            if(elapsed > 12.0f)
            {
                for(int i = 0; i<  ToRemoveArrowList.Count; i++)
                {
                    if(ToRemoveArrowList[i].isEnabled)
                        planet.RemoveProjectile(ToRemoveArrowList[i].projectileID.Index); 
                }

                for (int i = 0; i < PopGasList.Count; i++)
                {
                    if (PopGasList[i].isEnabled)
                        planet.RemoveProjectile(PopGasList[i].projectileID.Index);
                }
                deleteArrows = false;
                elapsed = 0.0f;
            }

            CircleSmoke.Update(ref planet.TileMap);
        }

        public void DeleteProjectile(ProjectileEntity arrow)
        {
            ToRemoveArrowList.Add(arrow);
        }
    }
}
