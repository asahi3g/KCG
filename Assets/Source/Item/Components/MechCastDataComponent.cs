using Entitas;
using Entitas.CodeGeneration.Attributes;
using Mech;

namespace Item
{
    [ItemInventory]
    public class MechCastDataComponent : IComponent
    {
        public Data data;
        public bool InputsActive;
    }
}

