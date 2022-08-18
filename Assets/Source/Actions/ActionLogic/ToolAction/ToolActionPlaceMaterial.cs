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

            // Set Data Layer To Front
            ItemEntity.itemCastData.data.Layer = MapLayerType.Front;

            // Can Place Boolean
            bool CanPlace = true;

            // Check Component Available
            if (ItemEntity.hasItemCastData)
            {
                // Get All Inventory Entities
                var entities = EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

                // Itreate All Entities
                foreach (var entity in entities)
                {
                    // Check Component Available
                    if (entity.hasInventoryName)
                    {
                        // Check  If Component Is Available
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            // Get Selected Slot
                            int selectedSlot = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.SelectedSlotID;

                            // Get Item From Selected Slot
                            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, entity.inventoryID.ID, selectedSlot);

                            // Check Item Is Available
                            if (item != null)
                            {
                                // Check If Component Is Available
                                if (item.hasItemStack)
                                {
                                    // Check Tile ID Equals To Any Material
                                    if (ItemEntity.itemCastData.data.TileID == TileID.Error ||
                                                    ItemEntity.itemCastData.data.TileID == TileID.Air ||
                                                    ItemEntity.itemCastData.data.Layer != MapLayerType.Front)
                                    {
                                        // Return True
                                        ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                        return;
                                    }

                                    // Switch TileID
                                    switch (ItemEntity.itemCastData.data.TileID)
                                    {
                                        // If Case Is Dirt
                                        case TileID.Moon:
                                            if (item.itemType.Type == Enums.ItemType.Dirt)
                                            {
                                                // Get Cursor Position
                                                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                int x = (int)worldPosition.x;
                                                int y = (int)worldPosition.y;

                                                // If Selected Tile Already Have Same Tile
                                                if (planet.TileMap.GetFrontTileID(x, y) == TileID.Moon)
                                                {
                                                    // Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }

                                                // Decrease 1 Count Of Item
                                                item.itemStack.Count--;

                                                // If Item Stack Less Than 1
                                                if (item.itemStack.Count < 1)
                                                {
                                                    // Can Place False
                                                    CanPlace = false;

                                                    // Remove Item From Material Bag
                                                    GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);

                                                    // Destroy Item Entity
                                                    item.Destroy();

                                                    // Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }
                                            }
                                            break;

                                        // If Case Is Bedrock
                                        case TileID.Bedrock:
                                            if (item.itemType.Type == Enums.ItemType.Bedrock)
                                            {
                                                // Get Cursor Position        
                                                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                int x = (int)worldPosition.x;
                                                int y = (int)worldPosition.y;

                                                // If Selected Tile Already Have Same Tile
                                                if (planet.TileMap.GetFrontTileID(x, y) == TileID.Bedrock)
                                                {
                                                    //Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }

                                                // Decrease 1 Count Of Item
                                                item.itemStack.Count--;

                                                // If Item Stack Less Than 1
                                                if (item.itemStack.Count < 1)
                                                {
                                                    // Can Place False
                                                    CanPlace = false;

                                                    // Remove Item From Material Bag
                                                    GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);

                                                    // Destroy Item
                                                    item.Destroy();

                                                    // Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }
                                            }
                                            break;

                                        // If Case Is Pipe
                                        case TileID.Pipe:
                                            if (item.itemType.Type == Enums.ItemType.Pipe)
                                            {
                                                // Get Cursor Position  
                                                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                int x = (int)worldPosition.x;
                                                int y = (int)worldPosition.y;

                                                // If Selected Tile Already Have Same Tile
                                                if (planet.TileMap.GetFrontTileID(x, y) == TileID.Pipe)
                                                {
                                                    //Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }

                                                // Decrease 1 Count Of Item
                                                item.itemStack.Count--;

                                                // If Item Stack Less Than 1
                                                if (item.itemStack.Count < 1)
                                                {
                                                    // Can Place False
                                                    CanPlace = false;

                                                    // Remove Item From Material Bag
                                                    GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);

                                                    // Destroy Item
                                                    item.Destroy();

                                                    // Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }
                                            }
                                            break;

                                        // If Case Is Wire
                                        case TileID.Wire:
                                            if (item.itemType.Type == Enums.ItemType.Wire)
                                            {
                                                // Get Cursor Position   
                                                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                int x = (int)worldPosition.x;
                                                int y = (int)worldPosition.y;

                                                // If Selected Tile Already Have Same Tile
                                                if (planet.TileMap.GetFrontTileID(x, y) == TileID.Wire)
                                                {
                                                    //Return;
                                                    ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                                    return;
                                                }

                                                // Decrease 1 Count Of Item
                                                item.itemStack.Count--;

                                                // If Item Stack Less Than 1
                                                if (item.itemStack.Count < 1)
                                                {
                                                    // Can Place False
                                                    CanPlace = false;

                                                    // Remove Item From Material Bag
                                                    GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);

                                                    // Destroy Item
                                                    item.Destroy();

                                                    // Return;
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
                                // Set Can Place False
                                CanPlace = false;
                            }
                        }
                    }
                }


                // If Inputs Active
                if (ItemEntity.itemCastData.InputsActive && CanPlace)
                {
                    // Get Cursor Position
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    // Check Map Size
                    if (x >= 0 && x < planet.TileMap.MapSize.X &&
                            y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        // Set Tile To Do Cursor Position
                        planet.TileMap.SetFrontTile(x, y, ItemEntity.itemCastData.data.TileID);
                    }
                }
            }
            else
            {
                // Initialize Data
                data = (Data)ActionPropertyEntity.actionPropertyData.Data;

                // Get Cursor Position  
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                // Check Map Size
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                        y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    // Set Tile To Do Cursor Position
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
