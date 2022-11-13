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
        public int ID;
        public LootDrop[] ItemDrops;
    }

    // Used by loot drop system to specify where to drop and which table entry to use.
    public struct LootDropEntity
    {
        public int TableID;
        public Vec2f DropPos;       // Position to Drop items. 
        // If -1 create items in specified position if != -1 create items inside inventory.
        public int InventoryID;
    }
}
