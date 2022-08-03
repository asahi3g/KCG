using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums.Tile;

namespace Item
{
    [ItemParticle, ItemInventory]
    public class CastDataComponent : IComponent
    {
        public Data data;
    }
}

