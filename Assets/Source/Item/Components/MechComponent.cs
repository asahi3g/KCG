using Entitas;
using Enums;

namespace Item
{
    [ItemInventory]
    public class MechComponent : IComponent
    {
        public MechType MechID;
        public bool InputsActive;
    }
}

