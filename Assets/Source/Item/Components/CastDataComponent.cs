using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums.Tile;

namespace Item
{
    [ItemInventory]
    public class CastDataComponent : IComponent
    {
        public Data data;
        public bool InputsActive;
    }
}

