namespace Admin
{
    // Admin API
    public static class AdminAPI
    {
        // Spawn Item Function
        public static ItemInventoryEntity SpawnItem(Enums.ItemType itemID)
        {
            // Spawn Item
            ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(itemID);

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
    }
}
