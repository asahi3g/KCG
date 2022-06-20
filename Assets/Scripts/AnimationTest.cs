using UnityEngine;
using System.Collections.Generic;
using Enums.Tile;

namespace Planet.Unity
{
    class AnimationTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        Planet.PlanetState Planet;

        Contexts EntitasContext;
        static bool Init = false;
  

        public void Start()
        {
            if (!Init)
            {
                Initialize();
                Init = true;
            }
        }


        public void Update()
        {
            Planet.TileMap TileMap = Planet.TileMap;
            Material material = Material;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                Planet.PlaceTile(x, y, 10, MapLayerType.Front);
                //TileMap.BuildLayerTexture(MapLayerType.Front);
                
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                TileMap.RemoveTile(x, y, MapLayerType.Front);
                //TileMap.BuildLayerTexture(MapLayerType.Front);
                
            }



                
            // unity rendering stuff
            // will be removed layer
            foreach(var mr in GetComponentsInChildren<MeshRenderer>())
            {
                if (Application.isPlaying)
                {
                    Destroy(mr.gameObject);
                }
                else
                {
                    DestroyImmediate(mr.gameObject);
                }
            }

            Planet.Update(Time.deltaTime, Material, transform);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            EntitasContext = Contexts.sharedInstance;



            int TilesMoon = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\tiles_moon\\Tiles_Moon.png", 16, 16);
            int OreTileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\ores\\gem_hexagon_1.png", 16, 16);
            int GunSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\gun-temp.png", 44, 25);

            int RockSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\rock1.png", 16, 16);
            int RockDustSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\rock1_dust.png", 16, 16);

            int DustSpriteSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\particles\\dust1.png", 16, 16);
            int SlimeSpriteSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\slime.png", 32, 32);

    
            int DustBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(DustSpriteSheet, 0, 0, 5, 0, Enums.AtlasType.Agent);

            int SlimeIdleBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SlimeSpriteSheet, 0, 0, 3, 0, Enums.AtlasType.Agent);

            int SlimeJumpBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SlimeSpriteSheet, 0, 2, 1, 3, Enums.AtlasType.Agent);


            GameState.TileCreationApi.CreateTile(8);
            GameState.TileCreationApi.SetTileName("ore_1");
            GameState.TileCreationApi.SetTileTexture16(OreTileSheet, 0, 0);
            GameState.TileCreationApi.EndTile();

            GameState.TileCreationApi.CreateTile(9);
            GameState.TileCreationApi.SetTileName("glass");
            GameState.TileCreationApi.SetTileSpriteSheet16(TilesMoon, 11, 10);
            GameState.TileCreationApi.EndTile();

            GameState.TileCreationApi.CreateTile(10);
            GameState.TileCreationApi.SetTileName("moon");
            GameState.TileCreationApi.SetTileSpriteSheet16(TilesMoon, 0, 0);
            GameState.TileCreationApi.EndTile();

            int particleAnimation = 0;
            int slimeIdle = 1;
            int slimeJump = 2;

            GameState.AnimationManager.CreateAnimation(particleAnimation);
            GameState.AnimationManager.SetName("particle");
            GameState.AnimationManager.SetTimePerFrame(0.15f);
            GameState.AnimationManager.SetBaseSpriteID(DustBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(6);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation(slimeIdle);
            GameState.AnimationManager.SetName("slime-idle");
            GameState.AnimationManager.SetTimePerFrame(0.35f);
            GameState.AnimationManager.SetBaseSpriteID(SlimeIdleBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(4);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation(slimeJump);
            GameState.AnimationManager.SetName("slime-jump");
            GameState.AnimationManager.SetTimePerFrame(0.35f);
            GameState.AnimationManager.SetBaseSpriteID(SlimeJumpBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(4);
            GameState.AnimationManager.EndAnimation();



            // Generating the map
            Vector2Int mapSize = new Vector2Int(16, 16);
            Planet = new Planet.PlanetState(mapSize);
            GenerateMap();

            Planet.AddAgent(Instantiate(Material), 0, 16, 16, new Vector2(6.0f, 3.0f), particleAnimation);
            Planet.AddAgent(Instantiate(Material), 0, 32, 32, new Vector2(2.0f, 3.0f), slimeIdle);
            Planet.AddAgent(Instantiate(Material), 0, 32, 32, new Vector2(4.0f, 3.0f), slimeJump);
        }




        void GenerateMap()
        {
            Planet.TileMap TileMap = Planet.TileMap;

           Vector2Int mapSize = TileMap.MapSize;

           for(int j = 0; j < mapSize.y; j++)
            {
                for(int i = 0; i < mapSize.x; i++)
                {
                    Tile.Tile frontTile = Tile.Tile.EmptyTile;
                    Tile.Tile oreTile = Tile.Tile.EmptyTile;

                    if (i >= mapSize.x / 2)
                    {
                        if (j % 2 == 0 && i == mapSize.x / 2)
                        {
                            frontTile.Type = 10;
                        }
                        else
                        {
                            frontTile.Type = 9;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == mapSize.x / 2 + 1)
                        {
                            frontTile.Type = 9;
                        }
                        else
                        {
                            frontTile.Type = 10;
                        }
                    }


                    if (i % 10 == 0)
                    {
                        oreTile.Type = 8;
                    }

                    if ((j > 1 && j < 6) || (j > (8 + i)))
                    {
                       frontTile.Type = -1; 
                       oreTile.Type = -1;
                    }

                    
                    TileMap.SetTile(i, j, frontTile, MapLayerType.Front);
                    TileMap.SetTile(i, j, oreTile, MapLayerType.Ore);
                }
            }

            TileMap.HeightMap.UpdateTopTilesMap(ref TileMap);

            TileMap.UpdateTileMapPositions(MapLayerType.Front);
            TileMap.UpdateTileMapPositions(MapLayerType.Ore);
            //TileMap.BuildLayerTexture(MapLayerType.Front);
            //TileMap.BuildLayerTexture(MapLayerType.Ore);
        
        }
        
    }
}


