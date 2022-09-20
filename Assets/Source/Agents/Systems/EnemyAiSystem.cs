using System;
using UnityEngine;
using System.Collections.Generic;
using KMath;

namespace Agent
{
    public class EnemyAiSystem
    {
        public void Update(ref Planet.PlanetState planetState, float deltaTime)
        {
            var players = planetState.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            var entities = planetState.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentEnemy));


            if (players.count > 0)
            {
                AgentEntity closestPlayer = players.GetEntities()[0];

                foreach (var entity in entities)
                {
                    var targetPhysicsState = closestPlayer.agentPhysicsState;
                    var enemyComponent = entity.agentEnemy;
                    var physicsState = entity.agentPhysicsState;
                    var box2DComponent = entity.physicsBox2DCollider;

                    if (entity.isAgentAlive)
                    {
                        enemyComponent.EnemyCooldown -= deltaTime;

                        Vec2f direction = targetPhysicsState.Position - physicsState.Position;
                        float YDiff = System.Math.Abs(targetPhysicsState.Position.Y - physicsState.Position.Y);
                        int KnockbackDir = 0;
                        if (direction.X > 0)
                        {
                            KnockbackDir = 1;
                        }
                        else if (direction.X < 0)
                        {
                            KnockbackDir = -1;
                        }
                        float Len = direction.Magnitude;
                        direction.Y = 0;
                        direction.Normalize();

                        if (enemyComponent.Behaviour == EnemyBehaviour.Slime)
                        {
                            // enemy bumps into the player and takes damage
                            // and gets pushed back in the opposite direction
                            // a floating text with the amount of damage dealt on it
                            // will be spawned at that position
                            if (entity.hasAgentStats && Len <= 0.6f && !targetPhysicsState.Invulnerable)
                            {
                                Vector2 oppositeDirection = new Vector2(-direction.X, -direction.Y);
                                var stats = entity.agentStats;
                                float damage = 20.0f;
                                stats.Health -= (int)damage;
                                

                                // spawns a debug floating text for damage 
                                planetState.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(physicsState.Position.X, physicsState.Position.Y + 0.35f));

                                
                                // knockback test
                                GameState.AgentProcessPhysicalState.Knockback(entity, 10.0f, -KnockbackDir);
                                GameState.AgentProcessPhysicalState.Knockback(closestPlayer, 10.0f, KnockbackDir);
                            }


                            // if the enemy is close to the player
                            // the enemy will move towards the player
                            if (Len <= enemyComponent.DetectionRadius && Len >= 0.5f)
                            {
                                // if the enemy is stuck
                                // trigger the jump
                                bool jump = Math.Abs(physicsState.Velocity.X) <= 0.05f && 
                                                physicsState.Velocity.Y <= 0.05f && 
                                                physicsState.Velocity.Y >= -0.05f &&
                                                YDiff >= 0.9f;

                                // to move the enemy we have to add acceleration 
                                // towards the player (Equal two time the drag.)
                                physicsState.Acceleration = direction * 2f * physicsState.Speed / Physics.Constants.TimeToMax;

                                // jumping is just an increase in velocity
                                if (jump)
                                {
                                    GameState.AgentProcessPhysicalState.Jump(entity);
                                }

                            }
                            else
                            {
                                //Idle
                                physicsState.Acceleration = new Vec2f();
                            }
                        }

                        else if (enemyComponent.Behaviour == EnemyBehaviour.Gunner)
                        {
                            // enemy bumps into the player and takes damage
                            // and gets pushed back in the opposite direction
                            // a floating text with the amount of damage dealt on it
                            // will be spawned at that position
                            if (entity.hasAgentStats && Len <= 0.6f && !targetPhysicsState.Invulnerable)
                            {
                                Vector2 oppositeDirection = new Vector2(-direction.X, -direction.Y);
                                var stats = entity.agentStats;
                                float damage = 20.0f;
                                stats.Health -= (int)damage;

                                // spawns a debug floating text for damage 
                                planetState.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(physicsState.Position.X, physicsState.Position.Y + 0.35f));

                                // knockback test
                                float sumOfVelocity = System.Math.Abs(physicsState.Velocity.X) + System.Math.Abs(targetPhysicsState.Velocity.X);
                                GameState.AgentProcessPhysicalState.Knockback(entity, sumOfVelocity * 1.2f, -KnockbackDir);
                                GameState.AgentProcessPhysicalState.Knockback(closestPlayer, sumOfVelocity * 1.2f, KnockbackDir);
                            }

                            
                            float shootingRange = 6.0f;
                            // if the enemy is close to the player
                            // the enemy will move towards the player
                            if (Len <= enemyComponent.DetectionRadius && Len >= shootingRange)
                            {
                                // if the enemy is stuck
                                // trigger the jump
                                bool jump = Math.Abs(physicsState.Velocity.X) <= 0.05f && 
                                                physicsState.Velocity.Y <= 0.05f && 
                                                physicsState.Velocity.Y >= -0.05f &&
                                                YDiff >= 0.9f;

                                // to move the enemy we have to add acceleration 
                                // towards the player (Equal two time the drag.)

                                entity.Walk((int)direction.X);
                                //physicsState.Acceleration = direction * 2f * physicsState.Speed / Physics.Constants.TimeToMax;

                                // jumping is just an increase in velocity
                                if (jump)
                                {
                                    GameState.AgentProcessPhysicalState.Jump(entity);
                                }

                            }
                            else
                            {
                                //Idle
                                physicsState.Acceleration = new Vec2f();
                            }
                        }


                        else if (enemyComponent.Behaviour == EnemyBehaviour.Swordman)
                        {
                            /*// enemy bumps into the player and takes damage
                            // and gets pushed back in the opposite direction
                            // a floating text with the amount of damage dealt on it
                            // will be spawned at that position
                            if (entity.hasAgentStats && Len <= 0.6f && !targetPhysicsState.Invulnerable)
                            {
                                Vector2 oppositeDirection = new Vector2(-direction.X, -direction.Y);
                                var stats = entity.agentStats;
                                float damage = 20.0f;
                                entity.ReplaceAgentStats(stats.Health - (int)damage, stats.Food, stats.Water, stats.Oxygen,
                                    stats.Fuel, stats.AttackCooldown);

                                // spawns a debug floating text for damage 
                                planetState.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(physicsState.Position.X, physicsState.Position.Y + 0.35f));

                                // knockback test
                                GameState.AgentProcessPhysicalState.Knockback(entity, 20.0f, (int)(oppositeDirection.x + 0.5f));
                                GameState.AgentProcessPhysicalState.Knockback(closestPlayer, 20.0f, (int)(direction.X - 0.5f));
                            }*/

                            float swordRange = 1.5f;
                            // if the enemy is close to the player
                            // the enemy will move towards the player
                            if (Len <= enemyComponent.DetectionRadius && Len >= swordRange && 
                            enemyComponent.EnemyCooldown <= 0 && entity.IsStateFree())
                            {
                                // if the enemy is stuck
                                // trigger the jump
                                bool jump = Math.Abs(physicsState.Velocity.X) <= 0.05f && 
                                                physicsState.Velocity.Y <= 0.05f && 
                                                physicsState.Velocity.Y >= -0.05f &&
                                                YDiff >= 0.9f;

                                // to move the enemy we have to add acceleration 
                                // towards the player (Equal two time the drag.)
                                entity.Walk((int)direction.X);
                                //physicsState.Acceleration = direction * 2f * physicsState.Speed / Physics.Constants.TimeToMax;

                                // jumping is just an increase in velocity
                                if (jump)
                                {
                                    GameState.AgentProcessPhysicalState.Jump(entity);
                                }

                            }
                            else
                            {
                                //Idle
                                physicsState.Acceleration = new Vec2f();
                            }

                            if (Len <= swordRange && enemyComponent.EnemyCooldown <= 0 && entity.IsStateFree())
                            {
                                GameState.AgentProcessPhysicalState.SwordSlash(entity);
                                GameState.AgentProcessPhysicalState.Knockback(closestPlayer, 7.0f, KnockbackDir);

                                float damage = 25.0f;
                                // spawns a debug floating text for damage 
                                planetState.AddFloatingText("" + damage, 0.5f, new Vec2f(direction.X * 0.05f, direction.Y * 0.05f), new Vec2f(physicsState.Position.X, physicsState.Position.Y + 0.35f));

                                enemyComponent.EnemyCooldown = 1.0f;

                            }
                        }
                        else if (enemyComponent.Behaviour == EnemyBehaviour.Insect)
                        {

                            float swordRange = 1.5f;
                            // if the enemy is close to the player
                            // the enemy will move towards the player
                            if (Len <= enemyComponent.DetectionRadius && Len >= swordRange && 
                            enemyComponent.EnemyCooldown <= 0 && entity.IsStateFree())
                            {
                                // if the enemy is stuck
                                // trigger the jump
                                bool jump = Math.Abs(physicsState.Velocity.X) <= 0.05f && 
                                                physicsState.Velocity.Y <= 0.05f && 
                                                physicsState.Velocity.Y >= -0.05f &&
                                                YDiff >= 0.9f;

                                // to move the enemy we have to add acceleration 
                                // towards the player (Equal two time the drag.)
                                entity.Walk((int)direction.X);
                                //physicsState.Acceleration = direction * 2f * physicsState.Speed / Physics.Constants.TimeToMax;

                                // jumping is just an increase in velocity
                                if (jump)
                                {
                                    GameState.AgentProcessPhysicalState.Jump(entity);
                                }

                            }
                            else
                            {
                                //Idle
                                physicsState.Acceleration = new Vec2f();
                            }

                            if (Len <= swordRange && enemyComponent.EnemyCooldown <= 0 && entity.IsStateFree())
                            {
                                entity.MonsterAttack(2.0f);
                            }


                            if (physicsState.ActionJustEnded)
                            {
                                Vec2f swordPosition = new Vec2f(physicsState.Position.X + box2DComponent.Offset.X + physicsState.MovingDirection * box2DComponent.Size.X,
                                physicsState.Position.Y + box2DComponent.Offset.Y);

                                
                                int [] agentIds = Collisions.Collisions.BroadphaseAgentCircleTest(planetState, swordPosition, 1.0f);
                                for(int agentIndex = 0; agentIndex < agentIds.Length; agentIndex++)
                                {
                                    int agentID = agentIds[agentIndex];
                                    AgentEntity thisAgent = planetState.AgentList.Get(agentID);
                                    if (thisAgent != entity)
                                    {
                                        var thisAgentPhysicsState = thisAgent.agentPhysicsState;

                                        Vec2f thisDirection = thisAgentPhysicsState.Position - physicsState.Position;
                                        int thisKnockbackDir = 0;
                                        if (thisDirection.X > 0)
                                        {
                                            thisKnockbackDir = 1;
                                        }
                                        else if (thisDirection.X < 0)
                                        {
                                            thisKnockbackDir = -1;
                                        }

                                        GameState.AgentProcessPhysicalState.Knockback(thisAgent, 7.0f, thisKnockbackDir);

                                        float damage = 25.0f;
                                        // spawns a debug floating text for damage 
                                        planetState.AddFloatingText("" + damage, 0.5f, new Vec2f(thisDirection.X * 0.05f, thisDirection.Y * 0.05f), 
                                    new Vec2f(thisAgentPhysicsState.Position.X, thisAgentPhysicsState.Position.Y + 0.35f));
                                    }

                                }
                                
                                physicsState.ActionJustEnded = false;
                                enemyComponent.EnemyCooldown = 1.0f;
                            }
                        }
                    }

                    
                }
            }
        }
    }
}
