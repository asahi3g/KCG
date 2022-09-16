using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections.Generic;
using Agent;
using Enums;

namespace Action
{
    public class ToolActionMeleeAttack : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private List<AgentEntity> Enemies = new();
        private List<MechEntity> ToRemoveMechs = new();
        private ItemInventoryEntity ItemEntity;
        private bool CanStun;
        private float elapsed;

        public ToolActionMeleeAttack(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public bool IsInRange(Vec2f currentTarget)
        {
            Vec2f yourPos = new Vec2f(AgentEntity.agentPhysicsState.Position.X, AgentEntity.agentPhysicsState.Position.Y);
            return WeaponProperty.Range >= Vec2f.Distance(yourPos, currentTarget);
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            float damage = WeaponProperty.BasicDemage;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            // Check if projectile has hit a enemy.
            var agents = EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            planet.AddFloatingText(WeaponProperty.MeleeAttackFlags.ToString(), 1.0f, new Vec2f(0, 0), new Vec2f(AgentEntity.agentPhysicsState.Position.X + 0.2f, AgentEntity.agentPhysicsState.Position.Y));
            var player = planet.Player;
            if (player != null)
            {
                var physicsState = player.agentPhysicsState;
                GameState.AgentProcessPhysicalState.SwordSlash(player);
                foreach (var agent in agents)
                {
                    var testState = agent.agentState;
                    if (agent != player && testState.State == AgentState.Alive)
                    {
                        var testPhysicsState = agent.agentPhysicsState;

                        //TODO(): not good we need collision checks
                        if (Vec2f.Distance(testPhysicsState.Position, physicsState.Position) <= WeaponProperty.Range)
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

                            GameState.AgentProcessPhysicalState.Knockback(agent, 7.0f, -KnockbackDir);

                            // spawns a debug floating text for damage 
                            planet.AddFloatingText("" + damage, 0.5f, new Vec2f(direction.X * 0.05f, direction.Y * 0.05f), 
                            new Vec2f(testPhysicsState.Position.X, testPhysicsState.Position.Y + 0.35f));

                            agent.agentStats.Health -= (int)damage;
                        }
                    }
                }
                
                var destructableMechs = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechDurability));

                foreach (var mech in destructableMechs)
                {
                    var testMechPosition = mech.mechPosition2D;

                    if (Vec2f.Distance(testMechPosition.Value, physicsState.Position) <= WeaponProperty.Range)
                    {
                        mech.mechDurability.Durability -= 20;

                        if (mech.mechDurability.Durability <= 0)
                        {
                            ToRemoveMechs.Add(mech);
                        }
                    }
                }
            }

            foreach (var mech in ToRemoveMechs)
            {
                planet.AddDebris(mech.mechPosition2D.Value, GameState.ItemCreationApi.ChestIconParticle, 1.5f, 1.0f);
                planet.RemoveMech(mech.mechID.Index);
            }
            
            ToRemoveMechs.Clear();

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
                                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                            }
                        }
                    }
                }
            }*/

            ActionEntity.actionExecution.State = Enums.ActionState.Running;

            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            /*if (CanStun)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].agentPhysicsState.Velocity = Vec2f.Zero;  
                }
                elapsed += Time.deltaTime;
                
                if(elapsed > (WeaponProperty.StaggerTime * WeaponProperty.StaggerRate))
                {
                    elapsed = 0;
                    CanStun = false;
                }
            }

            if(!CanStun)*/
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
        }

        public override void OnExit(ref PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    /// <summary>
    /// Factory Method
    /// </summary>
    public class ToolActionMeleeAttackCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionMeleeAttack(entitasContext, actionID);
        }
    }
}

