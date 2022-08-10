﻿using Entitas;
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

            int inventoryID = AgentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                inventoryEntity.inventoryEntity.InventoryModelID);

            // Todo: start playing some animation
            if (inventoryModel.HasToolBar)
            {
                int selected = inventoryEntity.inventoryEntity.SelectedSlotID;


                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                    AgentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                {
                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Fail);
                    return;
                }

                GameState.InventoryManager.RemoveItem(planet.EntitasContext, inventoryID, selected);

                // Create item particle from item inventory.
                Vec2f pos = AgentEntity.agentPhysicsState.Position + AgentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticle = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, itemInventory, pos);
                ItemParticle.itemPhysicsState.Velocity = new Vec2f(-8.0f, 8.0f);
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
