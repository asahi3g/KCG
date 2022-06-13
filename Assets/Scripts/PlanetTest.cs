using UnityEngine;
using System.Collections.Generic;
using Enums.Tile;
using KMath;

namespace Planet.Unity
{
    class PlanetTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        Planet.PlanetState Planet;
        Inventory.ManagerSystem inventoryManagerSystem;
        Inventory.DrawSystem    inventoryDrawSystem;

        Contexts EntitasContext;


        Agent.AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;

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
                TileMap.BuildLayerTexture(MapLayerType.Front);
                
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                TileMap.RemoveTile(x, y, MapLayerType.Front);
                TileMap.RemoveTile(x, y, MapLayerType.Ore);
                TileMap.BuildLayerTexture(MapLayerType.Front);
                TileMap.BuildLayerTexture(MapLayerType.Ore);
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

            inventoryDrawSystem.Draw(Instantiate(Material), transform, 1000);
            Planet.Update(Time.deltaTime, Material, transform);

            Vector2 playerPosition = Player.Entity.physicsPosition2D.Value;

           // transform.position = new Vector3(playerPosition.x - 6.0f, playerPosition.y - 6.0f, -10.0f);
        }

        void DrawSpriteAtlas()
        {
            ref Sprites.SpriteAtlas atlas = ref GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Agent);
            Sprites.Sprite sprite = new Sprites.Sprite
            {
                Texture = atlas.Texture,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };
            Utility.Render.DrawSprite(-3, -1, atlas.Width / 32.0f, atlas.Height / 32.0f, sprite, Instantiate(Material), transform);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            EntitasContext = Contexts.sharedInstance;
        
            inventoryManagerSystem = new Inventory.ManagerSystem(EntitasContext);
            inventoryDrawSystem = new Inventory.DrawSystem(EntitasContext);



            int TilesMoon = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\tiles_moon\\Tiles_Moon.png", 16, 16);
            int OreTileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\ores\\gem_hexagon_1.png", 16, 16);
            int Ore2TileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\ores\\ore_copper_1.png", 16, 16);
            int Ore3TileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\luis\\ores\\ore_adamantine_1.png", 16, 16);

            int GunSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\gun-temp.png", 44, 25);

            int RockSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\rock1.png", 16, 16);
            int RockDustSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\item\\rock1_dust.png", 16, 16);

            int SlimeSpriteSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\assets\\slime.png", 32, 32);

            int CharacterSpriteSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Moonbunker\\Tilesets\\Sprites\\character\\character.png", 32, 48);

            int SlimeMoveLeftBaseSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 0, 0, Enums.AtlasType.Agent);
            GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 1, 0, Enums.AtlasType.Agent);
            GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 2, 0, Enums.AtlasType.Agent);
            GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 3, 0, Enums.AtlasType.Agent);

            CharacterSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(CharacterSpriteSheet, 0, 0, Enums.AtlasType.Agent);

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

            GameState.TileCreationApi.CreateTile(11);
            GameState.TileCreationApi.SetTileName("ore_2");
            GameState.TileCreationApi.SetTileTexture16(Ore2TileSheet, 0, 0);
            GameState.TileCreationApi.EndTile();

            GameState.TileCreationApi.CreateTile(12);
            GameState.TileCreationApi.SetTileName("ore_3");
            GameState.TileCreationApi.SetTileTexture16(Ore3TileSheet, 0, 0);
            GameState.TileCreationApi.EndTile();


            GameState.AnimationManager.CreateAnimation(0);
            GameState.AnimationManager.SetName("character-move-left");
            GameState.AnimationManager.SetTimePerFrame(0.15f);
            GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
            GameState.AnimationManager.SetFrameCount(1);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation(1);
            GameState.AnimationManager.SetName("character-move-right");
            GameState.AnimationManager.SetTimePerFrame(0.15f);
            GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
            GameState.AnimationManager.SetFrameCount(1);
            GameState.AnimationManager.EndAnimation();

            GameState.AnimationManager.CreateAnimation(2);
            GameState.AnimationManager.SetName("slime-move-left");
            GameState.AnimationManager.SetTimePerFrame(0.35f);
            GameState.AnimationManager.SetBaseSpriteID(SlimeMoveLeftBaseSpriteId);
            GameState.AnimationManager.SetFrameCount(4);
            GameState.AnimationManager.EndAnimation();


            Item.CreationApi.Instance.CreateItem(Enums.ItemType.Gun, "Gun");
            Item.CreationApi.Instance.SetTexture(GunSpriteSheet);
            Item.CreationApi.Instance.SetInventoryTexture(GunSpriteSheet);
            Item.CreationApi.Instance.SetSize(new Vector2(0.5f, 0.5f));
            Item.CreationApi.Instance.EndItem();

            Item.CreationApi.Instance.CreateItem(Enums.ItemType.Ore, "Ore");
            Item.CreationApi.Instance.SetTexture(OreTileSheet);
            Item.CreationApi.Instance.SetInventoryTexture(OreTileSheet);
            Item.CreationApi.Instance.SetSize(new Vector2(0.5f, 0.5f));
            Item.CreationApi.Instance.SetStackable(99);
            Item.CreationApi.Instance.EndItem();

            Item.CreationApi.Instance.CreateItem(Enums.ItemType.PlacementTool, "PlacementTool");
            Item.CreationApi.Instance.SetTexture(RockSpriteSheet);
            Item.CreationApi.Instance.SetInventoryTexture(RockSpriteSheet);
            Item.CreationApi.Instance.SetSize(new Vector2(0.5f, 0.5f));
            Item.CreationApi.Instance.EndItem();

            Item.CreationApi.Instance.CreateItem(Enums.ItemType.RemoveTileTool, "RemoveTileTool");
            Item.CreationApi.Instance.SetTexture(RockDustSpriteSheet);
            Item.CreationApi.Instance.SetInventoryTexture(RockDustSpriteSheet);
            Item.CreationApi.Instance.SetSize(new Vector2(0.5f, 0.5f));
            Item.CreationApi.Instance.EndItem();

            // Generating the map
            Vector2Int mapSize = new Vector2Int(32, 16);
            Planet = new Planet.PlanetState(mapSize);
            GenerateMap();
            SpawnStuff();

            var inventoryAttacher = Inventory.InventoryAttacher.Instance;

            int inventoryID = Player.Entity.agentInventory.InventoryID;
            int toolBarID = Player.Entity.agentToolBar.ToolBarID;

            GameEntity gun = GameState.ItemSpawnSystem.SpawnIventoryItem(Enums.ItemType.Gun);
            GameEntity ore = GameState.ItemSpawnSystem.SpawnIventoryItem(Enums.ItemType.Ore);
            GameEntity placementTool = GameState.ItemSpawnSystem.SpawnIventoryItem(Enums.ItemType.PlacementTool);
            GameEntity removeTileTool = GameState.ItemSpawnSystem.SpawnIventoryItem(Enums.ItemType.RemoveTileTool);

            inventoryManagerSystem.AddItem(gun, toolBarID);
            inventoryManagerSystem.AddItem(ore, toolBarID);
            inventoryManagerSystem.AddItem(placementTool, toolBarID);
            inventoryManagerSystem.AddItem(removeTileTool, toolBarID);
        }



        void GenerateOre()
        {
            Planet.TileMap TileMap = Planet.TileMap;

            Tile.Tile oreTile = Tile.Tile.EmptyTile;
            for(int j = 0; j < TileMap.MapSize.y; j++)
            {
                for(int i = 0; i < TileMap.MapSize.x; i++)
                {
                    ref Tile.Tile tile = ref TileMap.GetTileRef(i, j, MapLayerType.Front);

                    if (tile.Type == 10 && (((int)KMath.Random.Mt19937.genrand_int32() % 30) == 0))
                    {
                        int type = ((int)KMath.Random.Mt19937.genrand_int32() % 6);
                        if (type == 0)
                        {
                            oreTile.Type = 8;
                        }
                        else if (type == 1 || type == 2)
                        {
                            oreTile.Type = 11;
                        }
                        else if (type == 3 || type == 4)
                        {
                            oreTile.Type = 12;
                        }

                        TileMap.SetTile(i, j, oreTile, MapLayerType.Ore);
                    }
                }
            }
        }
        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);
            Planet.TileMap TileMap = Planet.TileMap;

            Vec2i mapSize = TileMap.MapSize;

           for(int j = 0; j < mapSize.Y; j++)
            {
                for(int i = 0; i < mapSize.X; i++)
                {
                    Tile.Tile frontTile = Tile.Tile.EmptyTile;

                    if (i >= mapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == mapSize.X / 2)
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
                        if (j % 3 == 0 && i == mapSize.X / 2 + 1)
                        {
                            frontTile.Type = 9;
                        }
                        else
                        {
                            frontTile.Type = 10;
                        }
                    }

                    
                    TileMap.SetTile(i, j, frontTile, MapLayerType.Front);
                }
            }

            int carveHeight = TileMap.MapSize.y;

            for(int i = 0; i < TileMap.MapSize.x; i++)
            {
                int move = ((int)KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int)KMath.Random.Mt19937.genrand_int32() % 5) <= 3)
                {
                    move = 0;
                }
                carveHeight += move;
                if (carveHeight >= TileMap.MapSize.y)
                {
                    carveHeight = TileMap.MapSize.y - 1;
                }

                for(int j = carveHeight; j < TileMap.MapSize.y && j < carveHeight + 4; j++)
                {
                    TileMap.SetTile(i, j, Tile.Tile.EmptyTile, MapLayerType.Front);
                }
            }

            carveHeight = 5;

            for(int i = TileMap.MapSize.x - 1; i >=0; i--)
            {
                int move = ((int)KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int)KMath.Random.Mt19937.genrand_int32() % 10) <= 3)
                {
                    move = 1;
                }
                carveHeight += move;
                if (carveHeight >= TileMap.MapSize.y)
                {
                    carveHeight = TileMap.MapSize.y - 1;
                }

                for(int j = carveHeight; j < TileMap.MapSize.y && j < carveHeight + 4; j++)
                {
                    TileMap.SetTile(i, j, Tile.Tile.EmptyTile, MapLayerType.Front);
                }
            }


            GenerateOre();

            TileMap.HeightMap.UpdateTopTilesMap(ref TileMap);

            TileMap.UpdateTileMapPositions(MapLayerType.Front);
            TileMap.UpdateTileMapPositions(MapLayerType.Ore);
            TileMap.BuildLayerTexture(MapLayerType.Front);
            TileMap.BuildLayerTexture(MapLayerType.Ore);
        
        }

        void SpawnStuff()
        {
            Planet.TileMap TileMap = Planet.TileMap;
            System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

            float spawnHeight = TileMap.MapSize.y + 2.0f;

            Player = Planet.AddPlayer(Instantiate(Material), CharacterSpriteId, 32, 48, new Vector2(3.0f, spawnHeight), 0);
            PlayerID = Player.Entity.agentID.ID;

            Planet.AddAgent(Instantiate(Material), CharacterSpriteId, 32, 48, new Vector2(6.0f, spawnHeight), 0);
            Planet.AddAgent(Instantiate(Material), CharacterSpriteId, 32, 48, new Vector2(1.0f, spawnHeight), 0);

            for(int i = 0; i < TileMap.MapSize.x; i++)
            {
                if (random.Next() % 5 == 0)
                {
                    Planet.AddEnemy(Instantiate(Material), CharacterSpriteId, 32, 32, new Vector2((float)i, spawnHeight), 2);    
                }
            }


            
            GameState.ItemSpawnSystem.SpawnItem(Enums.ItemType.Gun, new Vector2(6.0f, spawnHeight));
            GameState.ItemSpawnSystem.SpawnItem(Enums.ItemType.Ore, new Vector2(3.0f, spawnHeight));
        }
        
    }
}


