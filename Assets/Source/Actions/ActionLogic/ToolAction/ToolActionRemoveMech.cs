using System;
using UnityEngine;
using PlanetTileMap;
using Mech;
using Enums.Tile;
using KMath;

namespace Action
{
    public class ToolActionRemoveMech : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionRemoveMech(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {

        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            var mech = planet.GetMechFromPosition(new KMath.Vec2f(x, y));

            if (mech != null)
            {
                planet.Player.UseTool(0.2f);

                planet.RemoveMech(mech.mechID.Index);
            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionRemoveMechCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionRemoveMech(entitasContext, actionID);
        }
    }
}
