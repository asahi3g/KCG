namespace Tiled
{


    public struct TiledTileProperty
    {
        public string name;
        public string type;
        public string value;
    }
    public struct TiledTile
    {
        public int id;
        public TiledTileProperty[] properties;
    }
}