﻿using Item;
using Utility;

namespace Inventory
{
    public class InventoryManager
    {
        static int uniqueID = 0;
        public InventoryEntity CreateInventory(int inventoryModelID, string name = "")
        {
            var inventoryModel = GameState.InventoryCreationApi.Get(inventoryModelID);
            var inventoryEntity = GameState.Planet.EntitasContext.inventory.CreateEntity();
            inventoryEntity.AddInventoryID(uniqueID++);
            if (inventoryModel.HasToolBar)
                inventoryEntity.hasInventoryToolBarDraw = true;
            
            int size = inventoryModel.SlotCount;
            inventoryEntity.AddInventoryInventory(-1, inventoryModelID, new Slot[size], 0, size, new BitSet((uint)size));

            for (int i = 0; i < inventoryModel.Slots.Length; i++)
            {
                int slotID = inventoryModel.Slots[i].SlotID;
                if (slotID == -1)
                    continue;

                inventoryEntity.inventoryInventory.Slots[slotID] = new Slot
                {
                    ItemID = -1,
                    GridSlotID = i,
                    Restriction = inventoryModel.Slots[i].Restriction
                };
            }

            if (name != "")
            {
                inventoryEntity.AddInventoryName(name);
            }

            return inventoryEntity;
        }

        public InventoryEntity CreateDefaultInventory(string name = "")
        {
            return CreateInventory(GameState.InventoryCreationApi.GetDefaultPlayerInventoryModelID(), name);
        }

        public void OpenInventory(InventoryEntity inventoryEntity)
        {
            inventoryEntity.hasInventoryDraw = true;
            GameState.InventoryWindowScaleSystem.OnOpenWindow(inventoryEntity);
        }

        public void CloseInventory(InventoryList inventoryList, InventoryEntity inventoryEntity)
        {
            inventoryEntity.hasInventoryDraw = false;
            GameState.InventoryWindowScaleSystem.OnCloseWindow(inventoryList);
        }

        public bool AddItemAtSlot(ItemInventoryEntity itemEntity, int inventoryID, int slotID)
        {
            InventoryComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;
            
            itemEntity.ReplaceItemInventory(inventoryID, slotID);

            // Check restriction.
            Enums.ItemGroups slotGroup = inventory.Slots[slotID].Restriction;
            Enums.ItemGroups group = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).Group;
            if (slotGroup > 0 && group != slotGroup)
                return false;

            if (inventory.SlotsMask[slotID])
            {
                ItemProprieties proprieties = GameState.ItemCreationApi.Get(itemEntity.itemType.Type);
                ItemInventoryEntity currentItem = GetItemInSlot(inventoryID, slotID);

                // If stackable check if there are any available stack in the inventory.
                if (proprieties.IsStackable())
                {
                    if (TryAddToStack(itemEntity, currentItem, proprieties.MaxStackCount))
                        return true;
                }

                // Move to first empty slot:
                if (!AddItemAtFirstEmptySlot(currentItem, inventoryID))
                    return false;
            }
            inventory.SlotsMask.Set(slotID);
            inventory.Slots[slotID].ItemID = itemEntity.itemID.ID;

            return true;
        }

        public bool AddItemAtFirstEmptySlot(ItemInventoryEntity itemEntity, int inventoryID)
        {
            InventoryComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;
            int fistEmptySlot = GetFirstEmptySlot(inventory.SlotsMask, inventory);

            if (fistEmptySlot < 0)
                return false;

            // Check restriction.
            Enums.ItemGroups slotGroup = inventory.Slots[fistEmptySlot].Restriction;
            Enums.ItemGroups group = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).Group;
            if (slotGroup > 0 && group != slotGroup)
                return false;

            itemEntity.ReplaceItemInventory(inventoryID, fistEmptySlot);
            inventory.SlotsMask.Set(fistEmptySlot);
            inventory.Slots[fistEmptySlot].ItemID = itemEntity.itemID.ID;

            return true;
        }

        public bool TryAddToStack(ItemInventoryEntity newEntity, ItemInventoryEntity inventoryEntity, int maxStackCount)
        {

            if (inventoryEntity.itemType.Type != newEntity.itemType.Type)
                return false;

            int count = newEntity.hasItemStack ? newEntity.itemStack.Count : 1;

            if (inventoryEntity.hasItemStack)
            {
                if (count == maxStackCount)
                    return false;

                if (count + inventoryEntity.itemStack.Count <= maxStackCount)
                {
                    inventoryEntity.itemStack.Count += count;
                    newEntity.Destroy();
                    return true;
                }
            }
            else
            {
                inventoryEntity.AddItemStack(count + 1);
                newEntity.Destroy();
                return true;
            }

            return false;
        }

        public bool AddItem(ItemInventoryEntity entity, int inventoryID)
        {
            ItemProprieties proprieties = GameState.ItemCreationApi.Get(entity.itemType.Type);

            // If stackable check if there are any available stack in the inventory.
            if (proprieties.IsStackable())
            {
                var Group = GameState.Planet.EntitasContext.itemInventory.GetEntitiesWithItemInventory(inventoryID); // Todo: Use multiple Entity Index. To narrow down the search with item type.

                foreach (ItemInventoryEntity entityIT in Group)
                {
                    if (TryAddToStack(entity, entityIT, proprieties.MaxStackCount))
                        return true;
                }
            }

            return AddItemAtFirstEmptySlot(entity, inventoryID);
        }

        public void PickUp(ItemParticleEntity entity, int inventoryID)
        {
            AddItem(GameState.ItemSpawnSystem.SpawnInventoryItem(entity), inventoryID);
        }

        public void RemoveItem(int inventoryID, int slotID)
        {
            InventoryComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;
            if (!inventory.SlotsMask[slotID])
                return;

            ItemInventoryEntity itemEntity = GetItemInSlot(inventoryID, slotID);
            inventory.SlotsMask.UnSet(slotID);
            inventory.Slots[slotID].ItemID = -1;
            itemEntity.RemoveItemInventory();
        }
        
        public void ChangeSlot(int newSelectedSlot, int inventoryID)
        {
            InventoryComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;
            inventory.SelectedSlotID = newSelectedSlot;
        }

        public bool IsFull(int inventoryID)
        {
            InventoryComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;

            if (inventory.SlotsMask.All()) // Test if all bits are set to one.
                return true;

            return false;
        }

        private bool IsFull(BitSet slots)
        {
            return slots.All(); // Inventory is Full?
        }

        // Update this.
        public ItemInventoryEntity GetItemInSlot(int inventoryID, int slot)
        {
            ref var planet = ref GameState.Planet;
            InventoryComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventory;
            int itemID = inventory.Slots[slot].ItemID;

            // Check if there is an item in the slot.
            if(itemID >= 0)
                return planet.EntitasContext.itemInventory.GetEntityWithItemID(itemID);
            return null;
        }

        private int GetFirstEmptySlot(BitSet slots, InventoryComponent inventory)
        {
            if (IsFull(slots))
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i])
                    return i;
            }

            return -1;  
        }
    }
}
