using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums.Tile;

namespace Item
{
    [ItemInventory]
    public class TileComponent : IComponent
    {
        public TileID TileID;
        public MapLayerType Layer;
        public bool InputsActive;
    }
}

