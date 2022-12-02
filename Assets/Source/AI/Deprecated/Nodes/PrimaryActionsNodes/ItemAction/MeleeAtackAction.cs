//import UnityEngine

using KMath;
using Enums;
using Item;

namespace Node
{
    public class MeleeAtackAction : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.MeleeAttackAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            float damage = 15;
            float range = 2.0f;

            // Check if projectile has hit a enemy.
            var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            var player = agentEntity;
            if (player != null && player.IsStateFree())
            {
                var physicsState = player.agentPhysicsState;
                var box2dCollider = player.physicsBox2DCollider;
                var model3d = player.Agent3DModel;

                Vec2f playerCenterPosition = physicsState.Position + box2dCollider.Offset + box2dCollider.Size * 0.5f;

                player.SwordSlash(0.2f);

                foreach (var agent in agents)
                {
                    if (agent != player && agent.isAgentAlive)
                    {
                        var testPhysicsState = agent.agentPhysicsState;

                        //TODO(): not good we need collision checks
                        if (Vec2f.Distance(testPhysicsState.Position, physicsState.Position) <= range)
                        {
                            Vec2f direction = physicsState.Position - testPhysicsState.Position;
                            int KnockbackDir = 0;
                            if (direction.X > 0)
                            {
                                KnockbackDir = 1;
                            }
                            else if (direction.X < 0)
                            {
                                KnockbackDir = -1;
                            }
                            direction.Y = 0;
                            direction.Normalize();

                            agent.Knockback(7.0f, -KnockbackDir);

                            // spawns a debug floating text for damage 
                            planet.AddFloatingText("" + damage, 0.5f, new Vec2f(direction.X * 0.05f, direction.Y * 0.05f), 
                            new Vec2f(testPhysicsState.Position.X, testPhysicsState.Position.Y + 0.35f));

                            agent.agentStats.Health.Remove((int)damage);
                        }
                    }
                }
                
                var destructableMechs = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechDurability));

              // Todo: Move this code to mech system 

               //foreach (var mech in destructableMechs)
               //{
               //    var testMechPosition = mech.mechPosition2D;
               //
               //    if (Vec2f.Distance(testMechPosition.Value, physicsState.Position) <= WeaponProperty.Range)
               //    {
               //        mech.mechDurability.Durability -= 20;
               //
               //        if (mech.mechDurability.Durability <= 0)
               //        {
               //            ToRemoveMechs.Add(mech);
               //        }
               //    }
               //}
            }

            //foreach (var mech in ToRemoveMechs)
            //{
            //    planet.AddDebris(mech.mechPosition2D.Value, GameState.ItemCreationApi.ChestIconParticle, 1.5f, 1.0f);
            //    planet.RemoveMech(mech.mechID.Index);
            //}
            //
            //ToRemoveMechs.Clear();

            /*// Todo: Create a agent colision system?
            foreach (var entity in entities)
            {
                if (!entity.isAgentPlayer)
                {
                    if (IsInRange(new Vector2(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y)))
                    {
                        if(ActionPropertyEntity.actionPropertyShield.ShieldActive)
                        {
                            planet.AddFloatingText("Shield", 0.5f, Vec2f.Zero, new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y + 0.35f));
                        }
                        else
                        {
                            Vec2f entityPos = entity.agentPhysicsState.Position;
                            Vec2f bulletPos = new Vec2f(x, y);
                            Vec2f diff = bulletPos - entityPos;
                            diff.Y = 0;
                            diff.Normalize();

                            Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);

                            Enemies.Add(entity);

                            if (entity.hasAgentStats)
                            {
                                var stats = entity.agentStats;
                                entity.ReplaceAgentStats(stats.Health - (int)damage, stats.Food, stats.Water, stats.Oxygen,
                                    stats.Fuel, stats.AttackCooldown);

                                entity.agentPhysicsState.Velocity = Vec2f.Zero;
                                CanStun = true;

                                // spawns a debug floating text for damage 
                                planet.AddFloatingText("" + damage, 0.5f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(entityPos.X, entityPos.Y + 0.35f));
                                nodeEntity.nodeExecution.State = Enums.ActionState.Success;
                            }
                        }
                    }
                }
            }*/

            nodeEntity.nodeExecution.State = NodeState.Success;

            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, 0.0f);
        }
    }
}

