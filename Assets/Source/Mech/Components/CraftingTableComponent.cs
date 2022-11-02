using Entitas;

namespace Mech
{
    // Makes item destructable.
    [Mech]
    public class CraftingTableComponent : IComponent
    {
        public InventoryEntity InputInventory;
        public InventoryEntity OutputInventory;
    }
}
