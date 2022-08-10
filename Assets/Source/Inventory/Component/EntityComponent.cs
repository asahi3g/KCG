using Entitas;
using Utility;

namespace Inventory
{
    [Inventory]
    public class EntityComponent : IComponent
    {
        public int Index;               // Index in inventory list.
        public int InventoryModelID;
        public Slot[] Slots;
        public int SelectedSlotID;
        public int Size;                // Total number of slots in the inventory.
        public BitSet SlotsMask;        // Free slots set to 0/ Fill slots to 1.
    }
}
