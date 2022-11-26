using Item;
using UnityEngine;
using Utility;

namespace Inventory
{
    public class InventoryManager
    {
        static int uniqueID = 0;
        public InventoryEntity CreateInventory(int InventoryEntityTemplateID, Enums.InventoryEntityType type = 
                Enums.InventoryEntityType.Default)
        {
            var InventoryEntityTemplate = GameState.InventoryCreationApi.Get(InventoryEntityTemplateID);
            var inventoryEntity = GameState.Planet.EntitasContext.inventory.CreateEntity();
            inventoryEntity.AddInventoryID(uniqueID++);
            if (InventoryEntityTemplate.HasToolBar)
                inventoryEntity.hasInventoryToolBarDraw = true;
            
            int size = InventoryEntityTemplate.SlotCount;
            inventoryEntity.AddInventoryInventoryEntity(-1, InventoryEntityTemplateID, new Slot[size], -1, new BitSet((uint)size), type);

            int inventoryId = inventoryEntity.inventoryID.ID;
            int length = InventoryEntityTemplate.Slots.Length;

            for (int i = 0; i < length; i++)
            {
                int slotID = InventoryEntityTemplate.Slots[i].SlotID;
                if (slotID == -1)
                    continue;
                
                inventoryEntity.inventoryInventoryEntity.Slots[slotID] = new Slot(inventoryId, i, InventoryEntityTemplate.Slots[i].Restriction);
            }

            return inventoryEntity;
        }

        public InventoryEntity CreateDefaultInventory(Enums.InventoryEntityType type = Enums.InventoryEntityType.Default)
        {
            return CreateInventory(GameState.InventoryCreationApi.GetDefaultPlayerInventoryModelID(), type);
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
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            
            itemEntity.ReplaceItemInventory(inventoryID, slotID);

            // Check restriction.
            Enums.ItemGroups slotGroup = inventory.Slots[slotID].ItemGroups;
            Enums.ItemGroups group = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).Group;
            if (slotGroup > 0 && group != slotGroup)
                return false;

            if (inventory.SlotsMask[slotID])
            {
                ItemProperties proprieties = GameState.ItemCreationApi.Get(itemEntity.itemType.Type);
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
            inventory.Slots[slotID].SetItemId(itemEntity.itemID.ID);

            return true;
        }

        public bool AddItemAtFirstEmptySlot(ItemInventoryEntity itemEntity, int inventoryID)
        {
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            int fistEmptySlot = GetFirstEmptySlot(inventory.SlotsMask, inventory);

            if (fistEmptySlot < 0)
                return false;

            // Check restriction.
            Enums.ItemGroups slotGroup = inventory.Slots[fistEmptySlot].ItemGroups;
            Enums.ItemGroups group = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).Group;
            if (slotGroup > 0 && group != slotGroup)
                return false;

            itemEntity.ReplaceItemInventory(inventoryID, fistEmptySlot);
            inventory.SlotsMask.Set(fistEmptySlot);
            inventory.Slots[fistEmptySlot].SetItemId(itemEntity.itemID.ID);

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
            ItemProperties itemProperties = GameState.ItemCreationApi.Get(entity.itemType.Type);

            // If stackable check if there are any available stack in the inventory.
            if (itemProperties.IsStackable())
            {
                var Group = GameState.Planet.EntitasContext.itemInventory.GetEntitiesWithItemInventory(inventoryID); // Todo: Use multiple Entity Index. To narrow down the search with item type.

                foreach (ItemInventoryEntity entityIT in Group)
                {
                    if (TryAddToStack(entity, entityIT, itemProperties.MaxStackCount))
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
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            if (!inventory.SlotsMask[slotID])
                return;

            ItemInventoryEntity itemEntity = GetItemInSlot(inventoryID, slotID);
            inventory.SlotsMask.UnSet(slotID);
            inventory.Slots[slotID].ClearItemId();
            itemEntity.RemoveItemInventory();
        }
        
        public void ChangeSelectedSlot(int slotIndex, int inventoryID)
        {
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            inventory.SetSelectedSlotIndex(slotIndex);
        }
        
        public void ChangeSelectedSlot(Slot slot, int inventoryID)
        {
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            if (inventory.Index != inventoryID)
            {
                throw new UnityException("Slot doesnt belong to this inventory");
            }
            inventory.SetSelectedSlotIndex(slot.Index);
        }

        public bool IsFull(int inventoryID)
        {
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;

            if (inventory.SlotsMask.All()) // Test if all bits are set to one.
                return true;

            return false;
        }

        private bool IsFull(BitSet slots)
        {
            return slots.All(); // Inventory is Full?
        }

        // Update this.
        public ItemInventoryEntity GetItemInSlot(int inventoryID, int slotIndex)
        {
            if (slotIndex < 0) return null;
            InventoryEntityComponent inventory = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            if (slotIndex >= inventory.Size)
            {
                return null;
            }
            int itemID = inventory.Slots[slotIndex].ItemID;

            // Check if there is an item in the slot.
            if(itemID >= 0)
                return GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(itemID);
            return null;
        }

        public bool GetItemInSlot(int inventoryID, int slotIndex, out ItemInventoryEntity itemInventoryEntity)
        {
            itemInventoryEntity = GetItemInSlot(inventoryID, slotIndex);
            return itemInventoryEntity != null;
        }

        private int GetFirstEmptySlot(BitSet slots, InventoryEntityComponent inventory)
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
