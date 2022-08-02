using System;
using Entitas;
using Utility;

namespace Inventory
{
    /// <summary>
    /// Inventory basic component.
    /// </summary>
    [Inventory]
    public class EntityComponent : IComponent
    {
        public int Width;
        public int Height;
        public Slot[] Slots;
        public BitSet SlotsMask;    // Free slots set to 0/ Fill slots to 1.   
        public int SelectedID;      // Selected slot.
    }
}
