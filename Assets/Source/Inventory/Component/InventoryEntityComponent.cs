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
        private int _selectedSlotIndex;
        public int Size => Slots?.Length ?? 0;
        public BitSet SlotsMask;        // Free slots set to 0/ Fill slots to 1.
        public InventoryEntityType InventoryType;   // Type of inventory (agentEquipment, agent, vehilce, mechStorage);

        public int SelectedSlotIndex => _selectedSlotIndex;
        
        public InventoryEntityComponent()
        {
            ClearSelectedSlotIndex();
        }

        public void ClearSelectedSlotIndex()
        {
            _selectedSlotIndex = -1;
        }

        public void SetSelectedSlotIndex(int value)
        {
            _selectedSlotIndex = value;
        }
    }
}
