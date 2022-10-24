namespace Admin
{
    // Admin API
    public static class AdminAPI
    {
        // Spawn Item Function
        public static ItemInventoryEntity SpawnItem(Enums.ItemType itemID, Contexts contexts)
        {
            if(contexts == null)
                return null;

            // Spawn Item
            ItemInventoryEntity item = GameState.ItemSpawnSystem.SpawnInventoryItem(contexts, itemID);

            // Return Item
            return item;
        }

        // Give Item to Active Agent Function
        public static void AddItem(Inventory.InventoryManager manager, int inventoryID, Enums.ItemType itemID, Contexts contexts)
        {
            if (contexts == null)
                return;

            manager.AddItem(contexts, SpawnItem(itemID, contexts), inventoryID);
        }

        public static void AddItemStackable(Inventory.InventoryManager manager, int inventoryID, Enums.ItemType itemID, int count, Contexts contexts)
        {
            if (contexts == null)
                return;

            // Testing stackable items.
            for (uint i = 0; i < count; i++)
            {
                manager.AddItem(contexts, GameState.ItemSpawnSystem.SpawnInventoryItem(contexts, itemID), inventoryID);
            }

        }

        // Give Item to Agent Function
        public static void AddItem(Inventory.InventoryManager manager, AgentEntity agentID, Enums.ItemType itemID, Contexts contexts)
        {
            if (contexts == null)
                return;

            manager.AddItem(contexts, SpawnItem(itemID, contexts), agentID.agentInventory.InventoryID);
        }
    }
}
