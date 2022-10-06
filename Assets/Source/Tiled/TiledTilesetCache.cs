using System.Collections.Generic;

namespace Tiled
{




    public class TiledTilesetCache
    {

        public Dictionary<string, TiledTileset> Tilesets;


        public TiledTilesetCache()
        {
            Tilesets = new Dictionary<string, TiledTileset>();
        }


        public void AddTileset(string name, TiledTileset tileset)
        {
            Tilesets.Add(name, tileset);
        }

        public TiledTileset GetTileset(string name)
        {
            if (Tilesets.ContainsKey(name))
            {      
                return Tilesets[name];
            }
            else
            {
                return null;
            }
        }
    }
}