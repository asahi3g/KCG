using Enums.Tile;

namespace PlanetTileMap
{
    public struct Tile
    {
        public TileDrawType DrawType;
        public int CompositeTileSpriteID;
        
        public TileID BackTileID;
        public int BackTileSpriteID;
        
        public TileID MidTileID;
        public int MidTileSpriteID;
        
        public TileID FrontTileID;
        public int FrontTileSpriteID;
        
        public TileShapeAndRotation CollisionIsoType1;
        public TileAdjacencyType CollisionIsoType2;
    }
}
