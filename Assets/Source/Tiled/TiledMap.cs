using Newtonsoft.Json;

namespace Tiled
{



    public class TiledMap
    {

        public int height;
        public int width;

        public TileMapTileset[] tilesets;
        public TiledMapLayer[] layers;

         public static TiledMap FromJson(string filename)
        {
            string json = System.IO.File.ReadAllText(filename);

            TiledMap newObject = JsonConvert.DeserializeObject<TiledMap>(json);

            return newObject;
        }

    }
}