using Newtonsoft.Json;

namespace Tiled
{




    public class TiledTileset
    {

        public int tilecount;
        public int columns;
        public int rows;
        public TiledTile[] Tiles;

        public TiledProperty[] properties;



        public static TiledTileset FromJson(string filename)
        {
            string json = System.IO.File.ReadAllText(filename);

            TiledTileset newObject = JsonConvert.DeserializeObject<TiledTileset>(json);
            newObject.rows = newObject.tilecount / newObject.columns;

            TiledTile[] newTiles = new TiledTile[newObject.rows * newObject.columns];
            for(int i = 0; i < newTiles.Length; i++)
            {
                newTiles[i].id = (i + 1);
                newTiles[i].properties = new TiledProperty[] { new() {name="", type="", value=""} };
            }

            for(int i = 0; i < newObject.Tiles.Length; i++)
            {
                newTiles[newObject.Tiles[i].id].properties = newObject.Tiles[i].properties;
            }

            newObject.Tiles = newTiles;

            return newObject;
        }

    }
}