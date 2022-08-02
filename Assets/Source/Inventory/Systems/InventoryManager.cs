using Item;
using System.Collections;
using Utility;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager
    {
        public InventoryManager()
        {
        }

        public void OpenInventory(Contexts contexts, int inventoryID)
        {
            var inventory = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
            inventory.isInventoryDrawable = true;
        }

        public void CloseInventory(Contexts entitasContext, int inventoryID)
        {
            var inventory = entitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);

            inventory.isInventoryDrawable = false;
        }

        public void AddItem(Contexts contexts, ItemInventoryEntity entity, int inventoryID)
        {
            var inventory = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);

            ItemProprieties proprieties = GameState.ItemCreationApi.Get(entity.itemType.Type);

            // If stackable check if there are any available stack in the inventory.
            if (proprieties.IsStackable())
            {
                var Group = contexts.itemInventory.GetEntitiesWithItemInventory(inventoryID); // Todo: Use multiple Entity Index. To narrow down the search with item type.

                int NewEntityCount = 1;
                if (entity.hasItemStack)
                    NewEntityCount = entity.itemStack.Count;

                foreach (ItemInventoryEntity entityIT in Group)
                {
                    if (entityIT.itemType.Type != entity.itemType.Type)
                    {
                        continue;
                    }
                    
                    int EntityITCount = 1;
                    int MaxStackSize = 64;
                    if (entityIT.hasItemStack)
                    {
                        EntityITCount = entityIT.itemStack.Count;
                        if (EntityITCount == MaxStackSize)
                            continue;
                    }
                    else
                    {
                        entityIT.AddItemStack(NewEntityCount + EntityITCount);
                        entity.Destroy();
                        return;
                    }
                    
                    if (NewEntityCount + EntityITCount <= MaxStackSize)
                    {
                        entityIT.ReplaceItemStack(NewEntityCount + EntityITCount);
                        entity.Destroy();
                        return;
                    }
                }
            }

            int fistEmptySlot = GetFirstEmptySlot(inventory.inventoryEntity.SlotsMask);
            entity.AddItemInventory(inventoryID, fistEmptySlot);
            inventory.inventoryEntity.SlotsMask.Set(fistEmptySlot);
            inventory.inventoryEntity.Slots[fistEmptySlot].ItemID = entity.itemID.ID;
        }

        public void PickUp(Contexts entitasContext, ItemParticleEntity entity, int inventoryID)
        {
            AddItem(entitasContext, GameState.ItemSpawnSystem.SpawnInventoryItem(entitasContext, entity), 
                inventoryID);
        }

        public void RemoveItem(Contexts contexts, ItemInventoryEntity entity, int slot)
        {
            var inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(entity.itemInventory.InventoryID);
            inventoryEntity.inventoryEntity.SlotsMask.UnSet(slot);
            inventoryEntity.inventoryEntity.Slots[slot].ItemID = -1;
            entity.RemoveItemInventory();
        }
        
        public void ChangeSlot(Contexts contexts, int newSelectedSlot, int inventoryID)
        {
            var inventory = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
            inventory.inventoryEntity.SelectedID = newSelectedSlot;
        }

        public bool IsFull(Contexts contexts, int inventoryID)
        {
            InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
            if (inventoryEntity.inventoryEntity.SlotsMask.All()) // Test if all bits are set to one.
                return true;

            return false;
        }

        private bool IsFull(BitSet Slots)
        {
            if (Slots.All())
            {
                return true; // Inventory is full.
            }
            return false;
        }

        // Update this.
        public ItemInventoryEntity GetItemInSlot(ItemInventoryContext itemContext, int inventoryID, int slot)
        {
            var items = itemContext.GetEntitiesWithItemInventory(inventoryID);

            foreach (var item in items)
            {
                if (item.itemInventory.SlotID == slot)
                {
                    return item;
                }
            }

            return null; // No item in selected slot.
        }

        private int GetFirstEmptySlot(BitSet Slots)
        {
            if (IsFull(Slots))
            {
                return -1;
            }

            for (int i = 0; i < Slots.Length; i++)
            {
                if (!Slots[i])
                    return i;
            }

            return -1;  
        }
    }
}
