using System.Collections.Generic;
using System.Linq;
using Enums;
using Inventory;
using Item;
using UnityEngine;

namespace Admin
{
    // Admin API
    public static class AdminAPI
    {
        // Spawn Item Function
        public static ItemInventoryEntity SpawnItem(Enums.ItemType itemType)
        {
            // Spawn Item
            ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(itemType);

            // Return Item
            return item;
        }

        // Give Item to Active Agent Function
        public static void AddItem(Inventory.InventoryManager manager, int inventoryID, Enums.ItemType itemID)
        {
            manager.AddItem(SpawnItem(itemID), inventoryID);
        }

        public static void AddItem(Inventory.InventoryManager manager, int inventoryID, Enums.ItemType itemID, int quantity)
        {
            for (uint i = 0; i < quantity; i++) AddItem(manager, inventoryID, itemID);
        }

        // Give Item to Agent Function
        public static void AddItem(Inventory.InventoryManager manager, AgentEntity agentID, Enums.ItemType itemID)
        {
            manager.AddItem(SpawnItem(itemID), agentID.agentInventory.InventoryID);
        }
        
        public static void AddItems(AgentEntity agentEntity, ItemGroupType[] itemGroups, int quantity)
        {
            if (quantity <= 0) return;
            if (itemGroups == null || itemGroups.Length <= 0) return;
            if (agentEntity == null) return;
            if (!agentEntity.hasAgentInventory) return;
        
            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntityComponent inventoryEntityComponent = GameState.Planet.GetInventoryEntityComponent(inventoryID);
        
            // In case agent inventory is smaller than requested quantity
            quantity = Mathf.Min(quantity, inventoryEntityComponent.Size);

            List<ItemProperties> itemProperties = GameState.ItemCreationApi.GetAllByItemGroups(itemGroups);

            // Shuffle available items
            itemProperties.Sort((a, b)=> 1 - 2 * Random.Range(0, 1));

            // In case returned items count is smaller than current quantity
            quantity = Mathf.Min(quantity, itemProperties.Count);

            // Truncate list
            itemProperties = itemProperties.GetRange(0, quantity);

            // Sort items by items groups so they are placed in somewhat order in inventory
            itemProperties = itemProperties.OrderBy(x => (int) (x.Group)).ToList();

            quantity = itemProperties.Count;
            for (int i = 0; i < quantity; i++)
            {
                ItemProperties properties = itemProperties[i];
                AddItem(GameState.InventoryManager, inventoryID, properties.ItemType);
            }
        }
    }
}
