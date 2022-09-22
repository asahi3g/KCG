using System;
using UnityEngine;
using Enums.Tile;

namespace Action
{
    public class ToolActionGeometryPlacement : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionGeometryPlacement(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            if (ItemEntity.hasItemCastData)
            {
                ItemEntity.itemCastData.data.Layer = MapLayerType.Front;

                if (ItemEntity.itemCastData.data.TileID == TileID.Error)
                    ItemEntity.itemCastData.data.TileID = TileID.TI_1;

                if (ItemEntity.itemCastData.InputsActive)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    if (x >= 0 && x < planet.TileMap.MapSize.X &&
                            y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        switch (ItemEntity.itemCastData.data.Layer)
                        {
                            case MapLayerType.Back:
                                planet.TileMap.SetBackTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Mid:
                                planet.TileMap.SetMidTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                            case MapLayerType.Front:
                                planet.TileMap.SetFrontTile(x, y, ItemEntity.itemCastData.data.TileID);
                                break;
                        }
                    }
                }
            }
            else
            {
                ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionGeometryPlacementCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionGeometryPlacement(entitasContext, actionID);
        }
    }
}
