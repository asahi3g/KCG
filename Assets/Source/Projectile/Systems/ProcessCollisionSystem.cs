using UnityEngine;
using KMath;
using Collisions;
using Particle;
using UnityEngine.UIElements;
using Utility;

namespace Projectile
{
    public class ProcessCollisionSystem
    {
        // new version of the update function
        // uses the planet state to remove the projectile
        public void UpdateEx(ref Planet.PlanetState planet)
        {
            ref PlanetTileMap.TileMap tileMap = ref planet.TileMap;

            var entities = planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            foreach (var entity in entities)
            {
                float bounceValue = GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).BounceValue;
                bool bounce = 
                    GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).Flags.HasFlag(ProjectileProperties.ProjFlags.CanBounce);

                var physicsState = entity.projectilePhysicsState;
                var box2DCollider = entity.physicsBox2DCollider;

                AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

                if ((physicsState.Position - physicsState.PreviousPosition).Magnitude < 0.0001f)
                    continue;

                // Collising with terrainr(raycasting)
                var rayCastingResult =
                Collisions.Collisions.RayCastAgainstTileMapBox2d(tileMap, new KMath.Line2D(
                    physicsState.PreviousPosition, physicsState.Position), box2DCollider.Size.X, box2DCollider.Size.Y);
                Vec2f oppositeDirection = (physicsState.PreviousPosition - physicsState.Position).Normalized;

                if (rayCastingResult.Intersect)
                {
                    if (!entity.hasProjectileOnHit)
                        entity.AddProjectileOnHit(-1, Time.time, rayCastingResult.Point, Time.time, rayCastingResult.Point);
                    else 
                    {
                        entity.projectileOnHit.LastHitPos = rayCastingResult.Point;
                        entity.projectileOnHit.LastHitTime = Time.time;
                    }
                    if (entity.isProjectileFirstHIt)
                    {
                        physicsState.Velocity = new Vec2f();
                        physicsState.Position = rayCastingResult.Point + oppositeDirection * entity.projectileSprite2D.Size * 0.5f;
                    }
                }

                // Collision with Agent.
                Vec2f position = physicsState.Position + box2DCollider.Offset;
                Collisions.Box2D entityBox = new Collisions.Box2D { x = position.X, y = position.Y, w = box2DCollider.Size.X, h = box2DCollider.Size.Y };
                Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
                for (int i = 0; i < planet.AgentList.Length; i++)
                {
                    AgentEntity agentEntity = planet.AgentList.Get(i);
                    if (!agentEntity.isAgentPlayer && agentEntity.agentState.State == Agent.AgentState.Alive)
                    {
                        var agentPhysicsState = agentEntity.agentPhysicsState;
                        var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                        Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;

                        Collisions.Box2D agentBox = new Collisions.Box2D{x = agentPosition.X, y = agentPosition.Y, w = agentBox2dCollider.Size.X, h = agentBox2dCollider.Size.Y};
                        if (Collisions.Collisions.SweptBox2dCollision(ref entityBox, delta, agentBox, false))
                        {
                            if (entity.isProjectileFirstHIt)
                                physicsState.Position = new Vec2f(entityBox.x, entityBox.y) - box2DCollider.Offset;

                            // Todo: Deals with case: colliding with an object and an agent at the same frame.
                            if (!entity.hasProjectileOnHit)
                                entity.AddProjectileOnHit(agentEntity.agentID.ID, Time.time, rayCastingResult.Point, Time.time, rayCastingResult.Point);
                            else
                            {
                                entity.projectileOnHit.AgentID = agentEntity.agentID.ID;
                                entity.projectileOnHit.LastHitPos = rayCastingResult.Point;
                                entity.projectileOnHit.LastHitTime = Time.time;
                            }
                        }
                    }
                }

                // Todo: Use only new collision system.
                if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.Velocity))
                {
                    if (bounce)
                        entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.Velocity))
                {
                    
                    if (bounce)
                        entity.projectilePhysicsState.Velocity.Y = -entity.projectilePhysicsState.Velocity.Y * bounceValue;
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y), entity.projectileSprite2D.Size);

                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.Velocity))
                {
                    if (bounce)
                        entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                    
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.Velocity))
                {
                    if (bounce)
                        entity.projectilePhysicsState.Velocity.X = -entity.projectilePhysicsState.Velocity.X * (bounceValue - 0.1f);
                }

                entityBoxBorders.DrawBox();
            }

            CircleSmoke.Update(ref planet.TileMap);
            GameState.ProjectileProcessOnHit.Update(ref planet);
        }
    }
}
