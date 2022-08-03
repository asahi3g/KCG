using Item;
using Utility;

namespace Inventory
{
    public class InventoryManager
    {
        public void OpenInventory(int inventoryID)
        {
            GameState.InventoryCreationApi.Get(inventoryID).InventoryFlags |= InventoryModel.Flags.Draw;
        }

        public void CloseInventory(int inventoryID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);

            inventory.InventoryFlags |= InventoryModel.Flags.Draw;
        }

        public void AddItemAtSlot(Contexts contexts, ItemInventoryEntity itemEntity, int inventoryID, int slotID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
            itemEntity.ReplaceItemInventory(inventoryID, slotID);

            if (inventory.SlotsMask[slotID])
            {
                // Move to first empty slot:
                ItemInventoryEntity currentItem = GetItemInSlot(contexts, inventoryID, slotID);
                RemoveItem(contexts, inventoryID, slotID);
                AddItemAtFirstEmptySlot(currentItem, inventoryID);
            }

            inventory.SlotsMask.Set(slotID);
            inventory.Slots[slotID].ItemID = itemEntity.itemID.ID;
        }

        public void AddItemAtFirstEmptySlot(ItemInventoryEntity entity, int inventoryID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
            int fistEmptySlot = GetFirstEmptySlot(inventory.SlotsMask);

            entity.ReplaceItemInventory(inventoryID, fistEmptySlot);
            inventory.SlotsMask.Set(fistEmptySlot);
            inventory.Slots[fistEmptySlot].ItemID = entity.itemID.ID;
        }

        public void AddItem(Contexts contexts, ItemInventoryEntity entity, int inventoryID)
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

            AddItemAtFirstEmptySlot(entity, inventoryID);
        }

        public void PickUp(Contexts entitasContext, ItemParticleEntity entity, int inventoryID)
        {
            AddItem(entitasContext, GameState.ItemSpawnSystem.SpawnInventoryItem(entitasContext, entity), 
                inventoryID);
        }

        public void RemoveItem(Contexts contexts, int inventoryID, int slotID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);

            if (!inventory.SlotsMask[slotID])
                return;
            ItemInventoryEntity itemEntity = GetItemInSlot(contexts, inventoryID, slotID);
            inventory.SlotsMask.UnSet(slotID);
            inventory.Slots[slotID].ItemID = -1;
            itemEntity.RemoveItemInventory();
        }
        
        public void ChangeSlot( int newSelectedSlot, int inventoryID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
            inventory.SelectedSlotID = newSelectedSlot;
        }

        public bool IsFull( int inventoryID)
        {
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);

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
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
            int itemID = inventory.Slots[slot].ItemID;

            // Check if there is an item in the slot.
            if(itemID >= 0)
                return contexts.itemInventory.GetEntityWithItemID(itemID);
            return null;
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
