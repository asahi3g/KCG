using KMath;

namespace LootDrop
{
    public struct LootDrop
    {
        public Enums.ItemType Type;
        public int[] DropNum;
        public int[] DropProbability;
    }

    public struct LootDropEntry
    {
        public Enums.LootTableType ID;
        public LootDrop[] ItemDrops;
    }

    /// <summary>
    /// Used by loot drop system to specify where to drop and which table entry to use.
    /// </summary>
    public struct LootDropEntity
    {
        public Enums.LootTableType TableID;
        public Vec2f DropPos;       // Position to Drop items. 
        // If -1 create items in specified position if != -1 create items inside inventory.
        public int InventoryID;
    }
}
