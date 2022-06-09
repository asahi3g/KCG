using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Planet.Unity
{
    //Note: TileMap should be mostly controlled by GameManager


    //Note(Mahdi): we are just testing and making sure everything is working
    // before we move things out of here
    // there will be things like rendering, collision, TileMap
    // that are not supposed to be here.

    class MapLoaderTestScript : MonoBehaviour
    {
        //public string TileMap = "Moonbunker/Moon Bunker.tmx";
        [SerializeField] Material Material;

        public static string BaseDir => Application.streamingAssetsPath;

        List<int> triangles = new();
        List<Vector2> uvs = new();
        List<Vector3> verticies = new();
        
        public Planet.TileMap TileMap;

        int SortingOrder = 0;

        int PlayerSpriteID;
        int PlayerSprite2ID;
        const float TileSize = 1.0f;

        readonly Vector2 MapOffset = new(-3.0f, 4.0f);

        static bool InitTiles;


        ECSInput.ProcessSystem InputProcessSystems;
        Agent.SpawnerSystem AgentSpawnerSystem;
        Agent.MovableSystem AgentMovableSystem;
        Agent.DrawSystem AgentDrawSystem;
        Agent.ProcessCollisionSystem AgentProcessCollisionSystem;

        public void Start()
        {
            if (!InitTiles)
            {
                InitializeSystems();
                CreateDefaultTiles();

                InitTiles = true;
            }
        }   

        void InitializeSystems()
        {
            InputProcessSystems = new ECSInput.ProcessSystem();
            AgentSpawnerSystem = new Agent.SpawnerSystem();
            AgentMovableSystem = new Agent.MovableSystem();
            AgentDrawSystem = new Agent.DrawSystem();
            AgentProcessCollisionSystem = new Agent.ProcessCollisionSystem();

            AgentSpawnerSystem.SpawnPlayer(Material, new Vector2(3.0f, 2.0f));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                
                var chunkIndex = TileMap.Chunks.GetChunkIndex(x, y);
                var tileIndex = Planet.Chunk.GetTileIndex(x, y);
                
                Debug.Log($"{x} {y} ChunkIndex: {chunkIndex} TileIndex: {tileIndex}");
            }
        
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                Debug.Log(x + " " + y);
                TileMap.RemoveTile(x, y, Enums.Tile.MapLayerType.Front);
                TileMap.Layers.BuildLayerTexture(TileMap, Enums.Tile.MapLayerType.Front);
                
            }

            foreach(var mr in GetComponentsInChildren<MeshRenderer>())
                if (Application.isPlaying)
                    Destroy(mr.gameObject);
                else
                    DestroyImmediate(mr.gameObject);

            InputProcessSystems.Update();
            AgentMovableSystem.Update();
            AgentProcessCollisionSystem.Update(TileMap);
            TileMap.Layers.DrawLayer(Enums.Tile.MapLayerType.Front, Instantiate(Material), transform, 10);
            TileMap.Layers.DrawLayer(Enums.Tile.MapLayerType.Ore, Instantiate(Material), transform, 11);
            AgentDrawSystem.Draw(Instantiate(Material), transform, 12);
        }


        public void CreateDefaultTiles()
        {
            int metalSlabsTileSheet = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\Tiles_metal_slabs\\Tiles_metal_slabs.png");
            int stoneBulkheads = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\tile_wallbase\\Tiles_stone_bulkheads.png");
            int tilesMoon = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\tiles_moon\\Tiles_Moon.png");
            int oreTileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\ores\\gem_hexagon_1.png");


            GameState.TileCreationApi.CreateTile(8);
            GameState.TileCreationApi.SetTileName("ore_1");
            GameState.TileCreationApi.SetTileTexture16(oreTileSheet, 0, 0);
            GameState.TileCreationApi.EndTile();

            GameState.TileCreationApi.CreateTile(9);
            GameState.TileCreationApi.SetTileName("glass");
            GameState.TileCreationApi.SetTileSpriteSheet16(tilesMoon, 11, 10);
            GameState.TileCreationApi.EndTile();



            // Generating the map
            Vector2Int mapSize = new Vector2Int(16, 16);

            TileMap = new Planet.TileMap(mapSize);

            for(int j = 0; j < mapSize.y; j++)
            {
                for(int i = 0; i < mapSize.x; i++)
                {
                    Tile.Tile frontTile = Tile.Tile.EmptyTile;
                    Tile.Tile oreTile = Tile.Tile.EmptyTile;

                    frontTile.Type = 9;


                    if (i % 10 == 0)
                    {
                        oreTile.Type = 8;
                    }

                    if ((j > 1 && j < 6) || (j > (8 + i)))
                    {
                       frontTile.Type = -1; 
                       oreTile.Type = -1;
                    }

                    
                    TileMap.SetTile(i, j, frontTile, Enums.Tile.MapLayerType.Front);
                    TileMap.SetTile(i, j, oreTile, Enums.Tile.MapLayerType.Ore);
                }
            }

            TileMap.HeightMap.UpdateTopTilesMap(ref TileMap);
            TileMap.UpdateTileMapPositions(Enums.Tile.MapLayerType.Front);
            TileMap.UpdateTileMapPositions(Enums.Tile.MapLayerType.Ore);
            TileMap.Layers.BuildLayerTexture(TileMap, Enums.Tile.MapLayerType.Front);
            TileMap.Layers.BuildLayerTexture(TileMap, Enums.Tile.MapLayerType.Ore);
        }
        
        

        public struct R
        {
            public float X;
            public float Y;
            public float W;
            public float H;

            public R(float x, float y, float w, float h)
            {
                X = x;
                Y = y;
                W = w;
                H = h;
            }
        }
        private static R CalcVisibleRect()
        {
            var cam = Camera.main;
            var pos = cam.transform.position;
            var height = 2f * cam.orthographicSize;
            var width = height * cam.aspect;
            var visibleRect = new R(pos.x - width / 2, pos.y - height / 2, width, height);
            return visibleRect;
        }

        private Texture2D CreateTextureFromRGBA(byte[] rgba, int w, int h)
        {

            var res = new Texture2D(w, h, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };

            var pixels = new Color32[w * h];
            for (int x = 0 ; x < w; x++)
            for (int y = 0 ; y < h; y++)
            { 
                int index = (x + y * w) * 4;
                var r = rgba[index];
                var g = rgba[index + 1];
                var b = rgba[index + 2];
                var a = rgba[index + 3];

                pixels[x + y * w] = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
            }
            res.SetPixels32(pixels);
            res.Apply();

            return res;
        }

    }
}