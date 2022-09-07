using System;
using UnityEngine;
using Enums.Tile;
using PlanetTileMap;

namespace Action
{
    public class ToolActionPotion : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionPotion(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            if (ItemEntity.itemPotionCastData.potionType == Enums.PotionType.Error)
                ItemEntity.itemPotionCastData.potionType = Enums.PotionType.HealthPotion;

            // Can Place Boolean
            bool CanPlace = true;

            var player = planet.Player;

            // Check Component Available
            if (ItemEntity.hasItemPotionCastData)
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
                            var Slots = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                            for (int i = 0; i < Slots.Length; i++)
                            {
                                // Get Item From Selected Slot
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, entity.inventoryID.ID, i);

                                // Check Item Is Available
                                if (item != null)
                                {
                                    // Check If Component Is Available
                                    if (item.hasItemStack)
                                    {
                                        // Check Tile ID Equals To Any Material
                                        if (ItemEntity.itemPotionCastData.potionType == Enums.PotionType.Error)
                                        {
                                            // Return True
                                            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
                                            return;
                                        }

                                        // Switch TileID
                                        switch (ItemEntity.itemPotionCastData.potionType)
                                        {
                                            // If Case Is Dirt
                                            case Enums.PotionType.HealthPotion:
                                                if (item.itemType.Type == Enums.ItemType.HealthPositon)
                                                {
                                                    // Get Cursor Position
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    player.UsePotion(2.0f);

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
                                        }
                                    }
                                }
                            }
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
    public class ToolActionPotionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPotion(entitasContext, actionID);
        }
    }
}
