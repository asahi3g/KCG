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

            switch (mech.mechType.mechType)
            {
                case MechType.Storage:
                    var item = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.Chest, mech.mechPosition2D.Value);
                    item.itemMechCastData.data.MechID = MechType.Storage;
                    break;
                case MechType.Planter:
                    item = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.Planter, mech.mechPosition2D.Value);
                    item.itemMechCastData.data.MechID = MechType.Planter;

                    break;
                case MechType.Light:
                    item = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.Light, mech.mechPosition2D.Value);
                    item.itemMechCastData.data.MechID = MechType.Light;

                    break;
                case MechType.SmashableBox:
                    item = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.SmashableBox, mech.mechPosition2D.Value);
                    item.itemMechCastData.data.MechID = MechType.SmashableBox;

                    break;
            }

            planet.RemoveMech(mech.mechID.ID);



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
