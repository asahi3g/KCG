using Entitas;
using System.Collections.Generic;

namespace Action.Property
{
    [ActionProperties]
    public class TilePlacementComponent : IComponent
    {
        public List<PlanetTileMap.Tile> Tiles;
        public List<KMath.Vec2i> TilesPos;
    }
}
