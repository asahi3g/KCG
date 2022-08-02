using Entitas;
using UnityEngine;
using KMath;
using Enums;

namespace Action
{
    public class DropAction : ActionBase
    {
        private ItemParticleEntity ItemParticle;

        public DropAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            if (!AgentEntity.hasAgentInventory)
                return;

            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(AgentEntity.agentInventory.InventoryID);

            // Todo: start playing some animation
            if (GameState.InventoryCreationApi.Get(inventoryEntity.inventoryID.TypeID).HasTooBar())
            {
                int selected = inventoryEntity.inventoryEntity.SelectedID;


                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext.itemInventory,
                    AgentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                {
                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                    return;
                }

                GameState.InventoryManager.RemoveItem(planet.EntitasContext, itemInventory, selected);

                // Create item particle from item inventory.
                Vec2f pos = AgentEntity.agentPosition2D.Value + AgentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticle = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, itemInventory, pos);
                ItemParticle.itemMovement.Velocity = new Vec2f(-8.0f, 8.0f);
                ItemParticle.isItemUnpickable = true;

                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Running);
                return;
            }
            // ToolBar is non existent. 
            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            // Action is active untill itemParticle becomes pickable again.
            ActionEntity.ReplaceActionTime(ActionEntity.actionTime.StartTime + deltaTime);
            if (ActionEntity.actionTime.StartTime < ActionPropertyEntity.actionPropertyTime.Duration)
            {
                return;
            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            if (ActionEntity.actionExecution.State == Enums.ActionState.Success)
                ItemParticle.isItemUnpickable = false;
            base.OnExit(ref planet);
        }
    }

    // Factory Method
    public class DropActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new DropAction(entitasContext, actionID);
        }
    }
}
