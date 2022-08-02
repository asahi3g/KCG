using System.Collections;
using Utility;
using Entitas;

namespace Inventory
{
    [Inventory]
    public class SlotsComponent : IComponent
    {
        /// <summary>
        /// Current Slots(inventory.Width * inventory.Heigh)
        /// </summary>
        public BitSet Values;
        /// <summary>
        /// Selected slot
        /// </summary>
        public int  Selected;
    }
}

