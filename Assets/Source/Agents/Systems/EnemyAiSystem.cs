using System;
using KMath;

namespace Agent
{
    // Deprecated, it's going to be removed soon.
    public class EnemyAiSystem
    {
        public void Update(float deltaTime)
        {
            ref var planet = ref GameState.Planet;
            var players = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentEnemy));

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
                        float YDiff = Math.Abs(targetPhysicsState.Position.Y - physicsState.Position.Y);
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
                                UnityEngine.Vector2 oppositeDirection = new UnityEngine.Vector2(-direction.X, -direction.Y);
                                var stats = entity.agentStats;
                                float damage = 20.0f;
                                stats.Health -= (int)damage;


                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(physicsState.Position.X, physicsState.Position.Y + 0.35f));


                                // knockback test
                                entity.Knockback(10.0f, -KnockbackDir);
                                closestPlayer.Knockback(10.0f, KnockbackDir);
                            }


                            // if the enemy is close to the player
                            // the enemy will move towards the player
                            if ((Len <= enemyComponent.DetectionRadius && Len >= 0.5f) || entity.agentAction.Action == AgentAlertState.Alert)
                            {
                                entity.agentAction.Action = AgentAlertState.Alert;

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
                                    entity.Jump();
                                }

                            }
                            else
                            {
                                //Idle
                                physicsState.Acceleration = new Vec2f();

                                entity.agentAction.Action = AgentAlertState.UnAlert;
                            }
                        }
                    }
                }
            }
        }
    }
}
