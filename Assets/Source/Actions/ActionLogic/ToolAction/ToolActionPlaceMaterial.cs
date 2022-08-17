using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Action
{
    public class ToolActionPlaceMaterial : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        private Data data;

        public ToolActionPlaceMaterial(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            bool CanPlace = true;

            if (ItemEntity.hasItemCastData)
            {
                if (ItemEntity.itemCastData.data.TileID == TileID.Error)
                    ItemEntity.itemCastData.data = (Data)ActionPropertyEntity.actionPropertyData.Data;

                var entities = EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            int selectedSlot = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.SelectedSlotID;

                            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, entity.inventoryID.ID, selectedSlot);

                            if (item != null)
                            {
                                if (item.hasItemStack)
                                {
                                    switch(ItemEntity.itemCastData.data.TileID)
                                    {
                                        case TileID.Moon:
                                            if (item.itemType.Type == Enums.ItemType.Dirt)
                                            {
                                                item.itemStack.Count--;

                                                if (item.itemStack.Count < 1)
                                                {
                                                    CanPlace = false;
                                                    GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                    item.Destroy();
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }
                                            }

                                            break;
                                    }
                                }
                            }
                            else 
                            {
                                CanPlace = false;
                            }
                        }
                    }
                }

                if (ItemEntity.itemCastData.InputsActive && CanPlace)
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
                data = (Data)ActionPropertyEntity.actionPropertyData.Data;

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                        y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    switch (data.Layer)
                    {
                        case MapLayerType.Back:
                            planet.TileMap.SetBackTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Mid:
                            planet.TileMap.SetMidTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Front:
                            planet.TileMap.SetFrontTile(x, y, data.TileID);
                            break;
                    }
                }

            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionPlaceMaterialCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPlaceMaterial(entitasContext, actionID);
        }
    }
}
