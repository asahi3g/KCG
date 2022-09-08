using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections;
using Particle;

namespace Action
{
    public class ToolActionGasBomb : ActionBase
    {
        private Item.FireWeaponPropreties WeaponProperty;
        private ProjectileEntity ProjectileEntity;
        private ItemInventoryEntity ItemEntity;
        private Vec2f StartPos;
        private float elapsed = 0.0f;

        public ToolActionGasBomb(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);
            WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            StartPos = AgentEntity.agentPhysicsState.Position;
            StartPos.X += 0.5f;
            StartPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(planet.EntitasContext, AgentEntity.agentInventory.InventoryID, ItemEntity.itemInventory.SlotID);
            ItemEntity.Destroy();

            ProjectileEntity = planet.AddProjectile(StartPos, new Vec2f(x - StartPos.X, y - StartPos.Y).Normalized, Enums.ProjectileType.GasGrenade, false);

            ActionEntity.actionExecution.State = Enums.ActionState.Running;

            GameState.ActionCoolDownSystem.SetCoolDown(EntitasContext, ActionEntity.actionID.TypeID, AgentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            float damage = 0.1f;

            // Check if projectile has hit something and was destroyed.
            if (!ProjectileEntity.isEnabled)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                return;
            }

            // Start position
            StartPos = AgentEntity.agentPhysicsState.Position;
            StartPos.X += 0.5f;
            StartPos.Y += 0.5f;

            elapsed += Time.deltaTime;

            if(elapsed > 3.0f)
            {
                CircleSmoke.Spawn(1, ProjectileEntity.projectilePhysicsState.Position, new Vec2f(0.1f, 0.2f), new Vec2f(0.1f, 0.2f));
            }

            if(elapsed > 8.0f)
            {
                ProjectileEntity.Destroy();
                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                return;
            }

            // Check if projectile has hit a enemy.
            var entities = EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            // Todo: Create a agent colision system?
            foreach (var entity in entities)
            {
                float dist = Vec2f.Distance(new Vec2f(AgentEntity.agentPhysicsState.Position.X, AgentEntity.agentPhysicsState.Position.Y), new Vec2f(ProjectileEntity.projectilePhysicsState.Position.X, ProjectileEntity.projectilePhysicsState.Position.Y));

                if (dist < 2.0f)
                {
                   Vec2f entityPos = entity.agentPhysicsState.Position;
                   Vec2f bulletPos = ProjectileEntity.projectilePhysicsState.Position;
                   Vec2f diff = bulletPos - entityPos;
                   diff.Y = 0;
                   diff.Normalize();

                   Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);

                    if (AgentEntity.hasAgentStats)
                    {
                        var stats = entity.agentStats;
                        stats.Health -= (int)damage;

                        planet.AddFloatingText("SMOKE!", 0.001f, Vec2f.Zero, AgentEntity.agentPhysicsState.Position);

                        // spawns a debug floating text for damage 
                        planet.AddFloatingText("" + damage, 0.001f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(entityPos.X, entityPos.Y + 0.85f));
                    }
                }
            }
        }

        public override void OnExit(ref PlanetState planet)
        {   
            base.OnExit(ref planet);
        }
    }


    public class ToolActionGasBombCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionGasBomb(entitasContext, actionID);
        }
    }
}

