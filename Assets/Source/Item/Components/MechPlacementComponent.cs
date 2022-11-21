using Entitas;
using Enums;

namespace Item
{
    [ItemInventory]
    public class MechPlacementComponent : IComponent
    {
        public MechType MechID;
        public bool InputsActive;
    }
}

