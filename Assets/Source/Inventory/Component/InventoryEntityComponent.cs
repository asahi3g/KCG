using Entitas;
using Utility;
using Enums;

namespace Inventory
{
    [Inventory]
    public class InventoryEntityComponent : IComponent
    {
        public int Index;               // Index in inventory list.
        public int InventoryEntityTemplateID;
        public Slot[] Slots;
        public int SelectedSlotID;
        public int Size;                // Total number of slots in the inventory.
        public BitSet SlotsMask;        // Free slots set to 0/ Fill slots to 1.
        public InventoryEntityType InventoryType;   // Type of inventory (agentEquipment, agent, vehilce, mechStorage);
    }
}
