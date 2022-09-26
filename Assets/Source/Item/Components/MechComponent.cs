using Entitas;
using Entitas.CodeGeneration.Attributes;
using Mech;

namespace Item
{
    [ItemInventory]
    public class MechComponent : IComponent
    {
        public Mech.MechType MechID;
        public bool InputsActive;
    }
}

