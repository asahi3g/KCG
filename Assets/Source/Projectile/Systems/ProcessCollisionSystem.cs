using UnityEngine;
using KMath;
using Particle;
using Utility;
using System.Collections.Generic;

namespace Projectile
{
    public class ProcessCollisionSystem
    {
        public void UpdateEx(Planet.PlanetState planet, float deltaTime)
        {
            const float THRESHOLD_VERTICAL_SPEED = 2.0f; // If slower than this stick to the ground.
            ref PlanetTileMap.TileMap tileMap = ref planet.TileMap;

            var entities = planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            List < AgentEntity > CollidedWith = new List< AgentEntity >();
            List < Vec2f > CollisionPosition = new List< Vec2f >();

            foreach (var entity in entities)
            {
               AgentEntity ownerEntity = planet.EntitasContext.agent.GetEntityWithAgentID(entity.projectileID.AgentOwnerID);
                float bounceValue = GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).BounceValue;
                bool bounce = 
                    GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).Flags.HasFlag(ProjectileProperties.ProjFlags.CanBounce);

                var physicsState = entity.projectilePhysicsState;
                var box2DCollider = entity.physicsBox2DCollider;

                AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

                // Collising with terrainr(raycasting)
                var rayCastingResult = Collisions.Collisions.RayCastAgainstTileMapBox2d(tileMap, new KMath.Line2D(
                    physicsState.PreviousPosition, physicsState.Position), box2DCollider.Size.X, box2DCollider.Size.Y);
                Vec2f oppositeDirection = (physicsState.PreviousPosition - physicsState.Position).Normalized;

                if (rayCastingResult.Intersect)
                {
                    if (bounce)
                    {
                        float r = (physicsState.Position.X - rayCastingResult.Point.X) / (physicsState.Position.X - physicsState.PreviousPosition.X);
                        float t = deltaTime * r; // Aproximation. Doesn't deal with acceleration.

                        if (KMath.KMath.AlmostEquals(rayCastingResult.Normal.X, 0.0f))
                        {
                            if (KMath.KMath.AlmostEquals(rayCastingResult.Normal.Y, 1.0f) && (Mathf.Abs(physicsState.Velocity.Y) < THRESHOLD_VERTICAL_SPEED))
                            {
                                physicsState.Velocity.Y = 0.0f;
                                physicsState.Position.Y = rayCastingResult.Point.Y;
                                physicsState.OnGrounded = true;
                            }
                            else
                            {
                                physicsState.Velocity.Y = -physicsState.Velocity.Y * bounceValue;
                                physicsState.Position.Y = rayCastingResult.Point.Y + physicsState.Velocity.Y * t;
                            }
                        }
                        else
                        {
                            physicsState.Velocity.X = -physicsState.Velocity.X * bounceValue;
                            physicsState.Position.X = rayCastingResult.Point.X + physicsState.Velocity.X * t;
                        }

                    }
                    else
                    {
                        physicsState.Position = rayCastingResult.Point;
                        physicsState.Velocity = Vec2f.Zero;
                    }

                    if (!entity.hasProjectileOnHit)
                    {
                        entity.AddProjectileOnHit(-1, Time.time, rayCastingResult.Point, Time.time, rayCastingResult.Point, false);
                    }
                    else 
                    {
                        entity.projectileOnHit.LastHitPos = rayCastingResult.Point;
                        entity.projectileOnHit.LastHitTime = Time.time;
                    }

                    if (entity.isProjectileFirstHIt)
                    {
                        physicsState.Velocity = new Vec2f();
                    }
                }

                // Collision with Agent.
                // Todo: Box2d uses center position. Change for leftmost button for consistency.
                Vec2f position = physicsState.Position + box2DCollider.Offset  + box2DCollider.Size / 2.0f;
                Collisions.Box2D entityBox = new Collisions.Box2D { x = position.X, y = position.Y, w = box2DCollider.Size.X, h = box2DCollider.Size.Y };
                Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
                for (int i = 0; i < planet.AgentList.Length; i++)
                {
                    AgentEntity agentEntity = planet.AgentList.Get(i);
                    if (agentEntity.agentID.Faction != ownerEntity.agentID.Faction && agentEntity.isAgentAlive)
                    {
                        var agentPhysicsState = agentEntity.agentPhysicsState;
                        var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                        Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset + agentBox2dCollider.Size/2;

                        Collisions.Box2D agentBox = new Collisions.Box2D{x = agentPosition.X, y = agentPosition.Y, w = agentBox2dCollider.Size.X, h = agentBox2dCollider.Size.Y};
                        if (Collisions.Collisions.SweptBox2dCollision(ref entityBox, delta, agentBox, false))
                        {
                            CollidedWith.Add(agentEntity);
                            CollisionPosition.Add(new Vec2f(entityBox.x, entityBox.y));
                        }
                    }
                }

                AgentEntity closestAgent = null;
                float closestDistance = 999999.0f;
                Vec2f collisionPosition = new Vec2f();

                if (CollidedWith.Count > 0)
                {
                    closestAgent = CollidedWith[0];
                    closestDistance = (CollidedWith[0].agentPhysicsState.PreviousPosition - entity.projectilePhysicsState.Position).SqrMagnitude;
                    collisionPosition = CollisionPosition[0];
                }


                for(int i = 0; i < CollidedWith.Count; i++)
                {
                    AgentEntity agentEntity = CollidedWith[i];
                    Vec2f testPosition = CollisionPosition[i];
                    float testDistance = (agentEntity.agentPhysicsState.PreviousPosition - entity.projectilePhysicsState.Position).SqrMagnitude;
                    if (testDistance < closestDistance)
                    {
                        closestAgent = agentEntity;
                        closestDistance = testDistance;
                        collisionPosition = testPosition;
                    }
                }


                if (closestAgent != null)
                {
                    physicsState.Position = new Vec2f(collisionPosition.X, collisionPosition.Y) - box2DCollider.Offset;
                    physicsState.Velocity = Vec2f.Zero;

                    // Todo: Deals with case: colliding with an object and an agent at the same frame.
                    if (!entity.hasProjectileOnHit)
                    {
                        entity.AddProjectileOnHit(closestAgent.agentID.ID, Time.time, collisionPosition, Time.time, collisionPosition, false);
                    }
                    else
                    {
                        entity.projectileOnHit.AgentID = closestAgent.agentID.ID;
                        entity.projectileOnHit.LastHitPos = collisionPosition;
                        entity.projectileOnHit.LastHitTime = Time.time;
                    }
                }

                CollidedWith.Clear();
                CollisionPosition.Clear();

                entityBoxBorders.DrawBox();
            }

            CircleSmoke.Update(ref planet.TileMap);
            GameState.ProjectileProcessOnHit.Update(ref planet);
        }
    }
}
