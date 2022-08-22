using System;
using UnityEngine;
using System.Collections.Generic;
using KMath;

namespace Agent
{
    public class EnemyAiSystem
    {
        List<AgentEntity> ToRemoveAgents = new List<AgentEntity>();
        public void Update(ref Planet.PlanetState planetState, float deltaTime)
        {
            var players = planetState.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            var entities = planetState.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentEnemy));


            if (players.count > 0)
            {
                AgentEntity closestPlayer = players.GetEntities()[0];

                foreach (var entity in entities)
                {
                    var state = entity.agentState;
                    var targetPhysicsState = closestPlayer.agentPhysicsState;
                    var enemyComponent = entity.agentEnemy;
                    var physicsState = entity.agentPhysicsState;

                    if (state.State == AgentState.Alive)
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

                                enemyComponent.EnemyCooldown = 2.0f;

                            }
                        }


                        if (entity.hasAgentStats && entity.agentStats.Health <= 0.0f)
                        {
                            ToRemoveAgents.Add(entity);
                        }
                    }

                    
                }
            }

            foreach (var entity in ToRemoveAgents)
            {
                planetState.KillAgent(entity.agentID.Index);             
            }
            ToRemoveAgents.Clear();
        }
    }
}
