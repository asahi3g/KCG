using Entitas;

namespace Mech
{
    /// <summary>
    /// Makes item destructable.
    /// </summary>
    [Mech]
    public class CraftingTableComponent : IComponent
    {
        public InventoryEntity InputInventory;
        public InventoryEntity OutputInventory;
    }
}
