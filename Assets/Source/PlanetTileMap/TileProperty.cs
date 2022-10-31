using Collisions;
using Enums.Tile;

//TODO: add material type for block
//TODO: per material coefficient of restitution, coefficient of static friction and coefficient of dynamic friction
//TODO: Want to use elliptical/capsule collider eventually too, not just box collider
//TODO: Each Tile type has as collision type enum, determining collision behavior/lines

namespace PlanetTileMap
{
    // Integer id for tile type, look up tile properties in TilePropertyManager by ID
    public struct TileProperty
    {
        
        public TileID TileID;
        public MaterialType MaterialType;
        public int BaseSpriteId;
        public TileDrawType DrawType;

        public Enums.LootTableType DropTableID;


        public byte Durability; //max health of tile
        
        // To map neighbour tiles or not
        public bool IsAutoMapping; 
        public bool CannotBeRemoved; // bedrock cannot be removed

        public SpriteRuleType SpriteRuleType;
        public CollisionType CollisionIsoType;
        public Enums.GeometryTileShape BlockShapeType;

        public bool IsSolid => CollisionIsoType == CollisionType.Solid;
        public bool IsAPlatform => CollisionIsoType == CollisionType.Platform;
        public TileProperty(TileID tileID, int baseSpriteId) : this()
        {
            TileID = tileID;
            BaseSpriteId = baseSpriteId;
        }
    }
}
