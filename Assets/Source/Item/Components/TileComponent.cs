using Entitas;
using Enums.PlanetTileMap;

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

