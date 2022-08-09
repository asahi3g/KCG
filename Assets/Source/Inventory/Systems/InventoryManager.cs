﻿using Item;
using Utility;

namespace Inventory
{
    public class InventoryManager
    {
        public InventoryEntity CreateInventory(Contexts context, int inventoryModelID, int inventoryID)
        {
            Inventory.InventoryModel inventoryModel = GameState.InventoryCreationApi.Get(inventoryModelID);
            InventoryEntity invetoryEntity = context.inventory.CreateEntity();
            invetoryEntity.AddInventoryID(inventoryID);
            if (inventoryModel.HasToolBar)
                invetoryEntity.hasInventoryToolBarDraw = true;
            
            int size = inventoryModel.SlotCount;
            invetoryEntity.AddInventoryEntity(inventoryModelID, new Slot[size], 0, size, new BitSet((uint)size));

            for (int i = 0; i < inventoryModel.Slots.Length; i++)
            {
                int slotID = inventoryModel.Slots[i].SlotID;
                if (slotID == -1)
                    continue;

                invetoryEntity.inventoryEntity.Slots[slotID] = new Slot
                {
                    ItemID = -1,
                    GridSlotID = i,
                    Restriction = inventoryModel.Slots[i].Restriction
                };
            }

            return invetoryEntity;
        }

        public InventoryEntity CreateDefaultInventory(Contexts context, int inventoryID)
        {
            return CreateInventory(context, GameState.InventoryCreationApi.GetDefaultPlayerInventoryModelID(), inventoryID);
        }

        public void OpenInventory(Contexts contexts, int inventoryID)
            => contexts.inventory.GetEntityWithInventoryID(inventoryID).hasInventoryDraw = true;

        public void CloseInventory(Contexts contexts, int inventoryID)
            => contexts.inventory.GetEntityWithInventoryID(inventoryID).hasInventoryDraw = false;

        public bool AddItemAtSlot(Contexts contexts, ItemInventoryEntity itemEntity, int inventoryID, int slotID)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            
            itemEntity.ReplaceItemInventory(inventoryID, slotID);

            // Check restriction.
            Enums.ItemGroups slotGroup = inventory.Slots[slotID].Restriction;
            Enums.ItemGroups group = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).Group;
            if (slotGroup > 0 && group != slotGroup)
                return false;

            if (inventory.SlotsMask[slotID])
            {
                // Move to first empty slot:
                ItemInventoryEntity currentItem = GetItemInSlot(contexts, inventoryID, slotID);
                RemoveItem(contexts, inventoryID, slotID);
                if (!AddItemAtFirstEmptySlot(contexts, currentItem, inventoryID))
                    return false;
            }
            inventory.SlotsMask.Set(slotID);
            inventory.Slots[slotID].ItemID = itemEntity.itemID.ID;

            return true;
        }

        public bool AddItemAtFirstEmptySlot(Contexts contexts, ItemInventoryEntity itemEntity, int inventoryID)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
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

        public bool AddItem(Contexts contexts, ItemInventoryEntity entity, int inventoryID)
        {
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
                        return true;
                    }
                    
                    if (NewEntityCount + EntityITCount <= MaxStackSize)
                    {
                        entityIT.ReplaceItemStack(NewEntityCount + EntityITCount);
                        entity.Destroy();
                        return true;
                    }
                }
            }

            return AddItemAtFirstEmptySlot(contexts, entity, inventoryID);
        }

        public void PickUp(Contexts entitasContext, ItemParticleEntity entity, int inventoryID)
        {
            AddItem(entitasContext, GameState.ItemSpawnSystem.SpawnInventoryItem(entitasContext, entity), 
                inventoryID);
        }

        public void RemoveItem(Contexts contexts, int inventoryID, int slotID)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;

            if (!inventory.SlotsMask[slotID])
                return;
            ItemInventoryEntity itemEntity = GetItemInSlot(contexts, inventoryID, slotID);
            inventory.SlotsMask.UnSet(slotID);
            inventory.Slots[slotID].ItemID = -1;
            itemEntity.RemoveItemInventory();
        }
        
        public void ChangeSlot(Contexts contexts, int newSelectedSlot, int inventoryID)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            inventory.SelectedSlotID = newSelectedSlot;
        }

        public bool IsFull(Contexts contexts, int inventoryID)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;

            if (inventory.SlotsMask.All()) // Test if all bits are set to one.
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
        public ItemInventoryEntity GetItemInSlot(Contexts contexts, int inventoryID, int slot)
        {
            Inventory.EntityComponent inventory = contexts.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            int itemID = inventory.Slots[slot].ItemID;

            // Check if there is an item in the slot.
            if(itemID >= 0)
                return contexts.itemInventory.GetEntityWithItemID(itemID);
            return null;
        }

        private int GetFirstEmptySlot(BitSet Slots, Inventory.EntityComponent inventory)
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
