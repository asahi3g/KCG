//import UnityEngine

using KMath;
using Enums;
using Item;

namespace Node
{
    public class MeleeAtackAction : NodeBase
    {
        public override ActionType Type => ActionType.MeleeAttackAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            float damage = 15;
            float range = 3.0f;

            // Check if projectile has hit a enemy.
            var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            var player = agentEntity;
            if (player != null && player.IsStateFree())
            {
                var physicsState = player.agentPhysicsState;
                var box2dCollider = player.physicsBox2DCollider;
                var model3d = player.agentAgent3DModel;


                var cursorWorldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                Vec2f playerCenterPosition = physicsState.Position + box2dCollider.Offset + box2dCollider.Size * 0.5f;

                Vec2f dir = cursorWorldPosition - playerCenterPosition;
                dir.Normalize();
                float angle = System.MathF.Atan2(dir.Y, dir.X);

                bool useSwordSlash = false;
                Vec2f attackPosition = new Vec2f();

                if (physicsState.FacingDirection == 1)
                {
                   /* if (angle >= System.MathF.PI * 0.25f)
                    {
                        attackPosition = physicsState.Position;   
                        attackPosition.Y += box2dCollider.Offset.Y + box2dCollider.Size.Y * 0.5f;
                        if (physicsState.FacingDirection == 1)
                        {
                            attackPosition.X += 1.5f;
                        }
                        else if (physicsState.FacingDirection == -1)
                        {
                            attackPosition.X -= 1.5f;
                        }    

                        useSwordSlash = player.SwordSlashUp(planet, attackPosition);
                    }
                    else
                    {
                        attackPosition = physicsState.Position;   
                        attackPosition.Y += box2dCollider.Offset.Y + box2dCollider.Size.Y * 0.5f;
                        attackPosition.Y -= 0.75f;
                        if (physicsState.FacingDirection == 1)
                        {
                            attackPosition.X += 1.5f;
                        }
                        else if (physicsState.FacingDirection == -1)
                        {
                            attackPosition.X -= 1.5f;
                        }    
                        useSwordSlash = player.SwordSlashDiagonal(planet, attackPosition);
                    }*/
                }

                attackPosition = physicsState.Position;   
                attackPosition.Y += box2dCollider.Offset.Y + box2dCollider.Size.Y * 0.5f;
               // attackPosition.Y -= 0.75f;
                if (physicsState.FacingDirection == 1)
                {
                    attackPosition.X += 1.5f;
                }
                else if (physicsState.FacingDirection == -1)
                {
                    attackPosition.X -= 1.5f;
                }    
                useSwordSlash = player.SwordSlashDiagonal(planet, attackPosition);

                 
                

                if (useSwordSlash)
                {

                    foreach (var agent in agents)
                    {
                        if (agent != player && agent.isAgentAlive)
                        {
                            var testPhysicsState = agent.agentPhysicsState;

                            //TODO(): not good we need collision checks
                            if (Vec2f.Distance(testPhysicsState.Position, attackPosition) <= range)
                            {
                                Vec2f direction = physicsState.Position - testPhysicsState.Position;
                                int KnockbackDir = 0;
                                if (direction.X > 0)
                                {
                                    KnockbackDir = 1;
                                }
                                else 
                                {
                                    KnockbackDir = -1;
                                }
                                direction.Y = 0;
                                direction.Normalize();

                                float KnockbackValue = 10.0f;
                                Enums.ParticleEffect bloodEffect = Enums.ParticleEffect.Blood_Small;

                                if (player.agentPhysicsState.MoveIndex == 2)
                                {
                                    KnockbackValue = 30.0f;
                                    bloodEffect = Enums.ParticleEffect.Blood_Medium;
                                }

                                agent.Knockback(KnockbackValue, -KnockbackDir);
                                agent.FlashFor(0.125f);

                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + damage, 0.5f, new Vec2f(direction.X * 0.05f, direction.Y * 0.05f), 
                                new Vec2f(testPhysicsState.Position.X, testPhysicsState.Position.Y + 0.35f));

                                planet.AddParticleEmitter(agent.agentPhysicsState.Position + agent.physicsBox2DCollider.Offset + agent.physicsBox2DCollider.Size * 0.5f, 
                                Particle.ParticleEmitterType.SwordAttack_Impact);

                                planet.AddParticleEffect(agent.agentPhysicsState.Position + agent.physicsBox2DCollider.Offset + agent.physicsBox2DCollider.Size * 0.5f,
                                bloodEffect);

                                agent.agentStats.Health.Remove((int)damage);
                            }
                        }
                    }
                }
                
                var destructableMechs = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechDurability));

            }


            nodeEntity.nodeExecution.State = NodeState.Success;

            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, 0.0f);
        }
    }
}

