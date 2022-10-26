using Newtonsoft.Json;
using System;

namespace Tiled
{



    public class TiledMap
    {

        public int height;
        public int width;

        public TileMapTileset[] tilesets;
        public TiledMapLayer[] layers;

        TiledTilesetCache tilesetCache;

        string RootFolder;

        public TiledMap(string rootFolder)
        {
             tilesetCache = new TiledTilesetCache();
             RootFolder = rootFolder;
        }

        public TiledMaterialAndShape GetTile(int index)
        {
            if (tilesets.Length > 0)
            {
                int tilesetId = 0;
                for(int i = 0; i < tilesets.Length; i++)
                {
                    ref TileMapTileset tileset = ref tilesets[i];

                    if ((tileset.firstgid - 1) > index)
                    {
                        break;
                    }
                    else
                    {
                        tilesetId = i;
                    }
                }

                string tilesetName = tilesets[tilesetId].source;
                int tileIndex = index - (tilesets[tilesetId].firstgid - 1);
                

                TiledTileset foundTileset = tilesetCache.GetTileset(tilesetName);

                if (foundTileset == null)
                {
                    foundTileset = TiledTileset.FromJson(RootFolder + tilesetName);

                    tilesetCache.AddTileset(tilesetName, foundTileset);
                }

                PlanetTileMap.MaterialType material = PlanetTileMap.MaterialType.Metal;
                Enum.TryParse<PlanetTileMap.MaterialType>(foundTileset.properties[0].value, out material);

                Enums.GeometryTileShape shape = Enums.GeometryTileShape.SB_R0;
                Enum.TryParse<Enums.GeometryTileShape>(foundTileset.Tiles[tileIndex].properties[0].value, out shape);

                return new TiledMaterialAndShape{Shape=shape, Material=material};

            }
            else
            {
                return new TiledMaterialAndShape();
            }
        }

         public static TiledMap FromJson(string filename, string rootFolder)
        {
            string json = System.IO.File.ReadAllText(filename);

            TiledMap newObject = JsonConvert.DeserializeObject<TiledMap>(json);
            newObject.RootFolder = rootFolder;

            return newObject;
        }

        

    }
}