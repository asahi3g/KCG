using UnityEngine;
using KMath;
using Particle;
using Utility;
using System.Collections.Generic;


//TODO(Mahdi):
// we should be using Bresenham's line algorithm
// for test of projectile against terrain
// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm




namespace Projectile
{
    public class ProcessCollisionSystem
    {
        public void UpdateEx(float deltaTime)
        {
            ref var planet = ref GameState.Planet;
            const float THRESHOLD_VERTICAL_SPEED = 2.0f; // If slower than this stick to the ground.
            ref PlanetTileMap.TileMap tileMap = ref planet.TileMap;

            var entities = planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.PhysicsBox2DCollider, ProjectileMatcher.ProjectilePhysicsState));

            AgentEntity[] CollidedWithArray = new AgentEntity[1024];
            int CollidedWithArrayCount = 0;

            foreach (var entity in entities)
            {
               AgentEntity ownerEntity = planet.EntitasContext.agent.GetEntityWithAgentID(entity.projectileID.AgentOwnerID);
                float bounceValue = GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).BounceValue;
                bool bounce = 
                    GameState.ProjectileCreationApi.Get((int)entity.projectileType.Type).Flags.HasFlag(ProjectileProperties.ProjFlags.CanBounce);

                var physicsState = entity.projectilePhysicsState;
                var box2DCollider = entity.physicsBox2DCollider;

                AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);


                Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
                

                float minTime = 1.0f;
                Vec2f minNormal = new Vec2f();
                Enums.TileGeometryAndRotation minShape = 0;
                Enums.MaterialType minMaterial = Enums.MaterialType.Error;

                //TODO(Mahdi):
                // 1- do not iterate over all the lines in the tile map
                // instead get only the closest lines
                // 2- velocity quadrants
                for(int i = 0; i < planet.TileMap.GeometryArrayCount; i++)
                {
                    
                    Line2D line = planet.TileMap.GeometryArray[i].Line;
                    Vec2f normal = planet.TileMap.GeometryArray[i].Normal;
                    Enums.TileGeometryAndRotation shape = planet.TileMap.GeometryArray[i].Shape;
                    Enums.MaterialType material = planet.TileMap.GeometryArray[i].Material;

                    if (!(shape == Enums.TileGeometryAndRotation.QP_R0 || shape == Enums.TileGeometryAndRotation.QP_R1 || 
                    shape == Enums.TileGeometryAndRotation.QP_R2 || shape == Enums.TileGeometryAndRotation.QP_R3))
                    {

                        // circle line sweep test
                        var collisionResult = 
                        Collisions.CircleLineCollision.TestCollision(physicsState.PreviousPosition + box2DCollider.Size.X / 2.0f, box2DCollider.Size.X / 2.0f, delta, line.A, line.B);

                        if (collisionResult.Time < minTime)
                        {
                            minTime = collisionResult.Time;
                            minNormal = collisionResult.Normal;
                            minShape = shape;
                            minMaterial = material;
                        }
                    }
                }


                for (int i = 0; i < planet.AgentList.Length; i++)
                {
                    AgentEntity agentEntity = planet.AgentList.Get(i);
                    if (agentEntity.agentID.Faction != ownerEntity.agentID.Faction && agentEntity.isAgentAlive)
                    {
                        var agentPhysicsState = agentEntity.agentPhysicsState;
                        var agentBox2dCollider = agentEntity.physicsBox2DCollider;

                        Vec2f agentPosition = agentPhysicsState.Position + agentBox2dCollider.Offset;
                        bool collided = false;

                        // static check first

                      /*  // first check if the rectangles overlap at the target position
                        if (Collisions.Collisions.RectOverlapRect(agentPosition.X, agentPosition.X + agentBox2dCollider.Size.X,
                         agentPosition.Y, agentPosition.Y + agentBox2dCollider.Size.Y, 
                         physicsState.Position.X, physicsState.Position.X + box2DCollider.Size.X,
                          physicsState.Position.Y, physicsState.Position.Y + box2DCollider.Size.Y))
                        {
                            minTime = 0.0f;
                            minNormal = new Vec2f();
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }*/


                        // check if the rectangles overlap at the starting position
                        if (Collisions.Collisions.RectOverlapRect(agentPosition.X, agentPosition.X + agentBox2dCollider.Size.X,
                         agentPosition.Y, agentPosition.Y + agentBox2dCollider.Size.Y, 
                         physicsState.PreviousPosition.X, physicsState.PreviousPosition.X + box2DCollider.Size.X,
                          physicsState.PreviousPosition.Y, physicsState.PreviousPosition.Y + box2DCollider.Size.Y))
                        {
                            minTime = 0.0f;
                            minNormal = new Vec2f();
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }


                        // early escape
                        if (minTime > 0.0f)
                        {
                        


                        // agent hit box is just 4 lines (bottom, right top, left)

                        Line2D bottomLine = new Line2D(agentPosition, agentPosition + new Vec2f(agentBox2dCollider.Size.X, 0.0f));
                        Line2D rightLine = new Line2D(agentPosition + new Vec2f(agentBox2dCollider.Size.X, 0.0f), agentPosition + agentBox2dCollider.Size);
                        Line2D topLine = new Line2D(agentPosition + agentBox2dCollider.Size, agentPosition + new Vec2f(0.0f, agentBox2dCollider.Size.Y));
                        Line2D leftLine = new Line2D(agentPosition + new Vec2f(0.0f, agentBox2dCollider.Size.Y), agentPosition);

                        planet.AddDebugLine(bottomLine, Color.red);
                        planet.AddDebugLine(rightLine, Color.red);
                        planet.AddDebugLine(topLine, Color.red);
                        planet.AddDebugLine(leftLine, Color.red);


                        // sweep test against 4 lines
                        // and only save the smallest

                        // bottom line
                        var collisionResult = 
                        Collisions.CircleLineCollision.TestCollision(physicsState.PreviousPosition + box2DCollider.Size.X / 2.0f, box2DCollider.Size.X / 2.0f, delta, bottomLine.A, bottomLine.B);

                        if (collisionResult.Time < minTime)
                        {
                            minTime = collisionResult.Time;
                            minNormal = collisionResult.Normal;
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }

                        // top line
                        collisionResult = 
                        Collisions.CircleLineCollision.TestCollision(physicsState.PreviousPosition + box2DCollider.Size.X / 2.0f, box2DCollider.Size.X / 2.0f, delta, topLine.A, topLine.B);

                        if (collisionResult.Time < minTime)
                        {
                            minTime = collisionResult.Time;
                            minNormal = collisionResult.Normal;
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }

                        // right line
                        collisionResult = 
                        Collisions.CircleLineCollision.TestCollision(physicsState.PreviousPosition + box2DCollider.Size.X / 2.0f, box2DCollider.Size.X / 2.0f, delta, rightLine.A, rightLine.B);

                        if (collisionResult.Time < minTime)
                        {
                            minTime = collisionResult.Time;
                            minNormal = collisionResult.Normal;
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }

                        //left line
                        collisionResult = 
                        Collisions.CircleLineCollision.TestCollision(physicsState.PreviousPosition + box2DCollider.Size.X / 2.0f, box2DCollider.Size.X / 2.0f, delta, leftLine.A, leftLine.B);

                        if (collisionResult.Time < minTime)
                        {
                            minTime = collisionResult.Time;
                            minNormal = collisionResult.Normal;
                            minShape = Enums.TileGeometryAndRotation.Error;
                            collided = true;
                        }
                        }


                        if (collided)
                        {
                            if (CollidedWithArrayCount + 1 <= CollidedWithArray.Length)
                            {
                                System.Array.Resize(ref CollidedWithArray, CollidedWithArray.Length +  1024);
                            }

                            CollidedWithArray[CollidedWithArrayCount++] = agentEntity;
                        }

                    }
                }


                 AgentEntity closestAgent = null;
                float closestDistance = 999999.0f;

                if (CollidedWithArrayCount > 0)
                {
                    closestAgent = CollidedWithArray[0];
                    closestDistance = (CollidedWithArray[0].agentPhysicsState.PreviousPosition - entity.projectilePhysicsState.Position).SqrMagnitude;
                }


                for(int i = 0; i < CollidedWithArrayCount; i++)
                {
                    AgentEntity agentEntity = CollidedWithArray[i];
                    float testDistance = (agentEntity.agentPhysicsState.PreviousPosition - entity.projectilePhysicsState.Position).SqrMagnitude;
                    if (testDistance < closestDistance)
                    {
                        closestAgent = agentEntity;
                        closestDistance = testDistance;
                    }
                }



                if (minTime < 1.0f)
                {
                    float epsilon = 0.001f;

                    physicsState.Position = physicsState.PreviousPosition + delta * (minTime - epsilon);

                    // bouncing does not work at the moment
                    // fix later
                    // not priority right now

                   /* if (bounce)
                    {
                        float r = (physicsState.Position.X - physicsState.Position.X) / (physicsState.Position.X - physicsState.PreviousPosition.X);
                        float t = deltaTime * r; // Aproximation. Doesn't deal with acceleration.

                        if (KMath.KMath.AlmostEquals(minNormal.X, 0.0f))
                        {
                            if (KMath.KMath.AlmostEquals(minNormal.Y, 1.0f) && (Mathf.Abs(physicsState.Velocity.Y) < THRESHOLD_VERTICAL_SPEED))
                            {
                                physicsState.Velocity.Y = 0.0f;
                                physicsState.OnGrounded = true;
                            }
                            else
                            {
                                physicsState.Velocity.Y = -physicsState.Velocity.Y * bounceValue;
                                physicsState.Position.Y = physicsState.Position.Y + physicsState.Velocity.Y * t;
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
                    }*/


                    if (closestAgent == null)
                    {
                        // we collided with terrain
                          if (!entity.hasProjectileOnHit)
                        {
                            entity.AddProjectileOnHit(-1, Time.time, physicsState.Position, Time.time, physicsState.Position, false, minMaterial);
                        }
                        else 
                        {
                            entity.projectileOnHit.LastHitPos = physicsState.Position;
                            entity.projectileOnHit.LastHitTime = Time.time;
                        }

                        if (entity.isProjectileFirstHIt)
                        {
                            physicsState.Velocity = new Vec2f();
                        }
                    }
                    else
                    {
                        // we collided with an agent
                        // Todo: Deals with case: colliding with an object and an agent at the same frame.
                        if (!entity.hasProjectileOnHit)
                        {
                            entity.AddProjectileOnHit(closestAgent.agentID.ID, Time.time, physicsState.Position, Time.time, physicsState.Position, false, Enums.MaterialType.Flesh);
                        }
                        else
                        {
                            entity.projectileOnHit.AgentID = closestAgent.agentID.ID;
                            entity.projectileOnHit.LastHitPos = physicsState.Position;
                            entity.projectileOnHit.LastHitTime = Time.time;
                        }
                    }

                  
                }

                CollidedWithArrayCount = 0;
                entityBoxBorders.DrawBox();
            }

            CircleSmoke.Update();
            GameState.ProjectileProcessOnHit.Update();
        }
    }
}
