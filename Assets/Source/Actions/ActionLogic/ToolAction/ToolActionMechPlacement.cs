using System;
using UnityEngine;
using PlanetTileMap;
using Mech;
using Enums.Tile;
using KMath;

namespace Action
{
    public class ToolActionMechPlacement : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionMechPlacement(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {

        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            planet.AddMech(new Vec2f(x, y), ItemEntity.itemMechCastData.data.MechID);

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            if (ItemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(planet.EntitasContext, AgentEntity.agentInventory.InventoryID, ItemEntity.itemInventory.SlotID);
                ItemEntity.Destroy();
            }
            base.OnExit(ref planet);
        }
    }


    // Factory Method
    public class ToolActionMechPlacementCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionMechPlacement(entitasContext, actionID);
        }
    }
}
