using UnityEngine;
using Enums.Tile;
using KMath;

namespace Planet.Unity
{
    class MediumPlanetTest : MonoBehaviour
    {
        [SerializeField] Material Material;
        public PlanetState Planet;
        Inventory.InventoryManager inventoryManager;
        Inventory.DrawSystem inventoryDrawSystem;
        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;
        int toolBarID;

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
            int toolBarID = Player.agentToolBar.ToolBarID;
            InventoryEntity Inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            int selectedSlot = Inventory.inventorySlots.Selected;

            ItemEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext.item, toolBarID, selectedSlot);
            ItemPropertiesEntity itemProperty = Planet.EntitasContext.itemProperties.GetEntityWithItemProperty(item.itemType.Type);
            if (itemProperty.hasItemPropertyAction)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameState.ActionSchedulerSystem.ScheduleAction(Player,
                        GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.itemPropertyAction.ActionTypeID, Player.agentID.ID));
                }
            }

            Planet.Update(Time.deltaTime, Material, transform);
            //   Vector2 playerPosition = Player.Entity.physicsPosition2D.Value;

            // transform.position = new Vector3(playerPosition.x - 6.0f, playerPosition.y - 6.0f, -10.0f);
        }
        
        private void OnRenderObject()
        {
            inventoryDrawSystem.Draw(Planet.EntitasContext, Material, transform);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            inventoryManager = new Inventory.InventoryManager();
            inventoryDrawSystem = new Inventory.DrawSystem();

            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(6400, 24);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            GameResources.CreateItems(Planet.EntitasContext);

            GenerateMap();
            Player = Planet.AddPlayer(new Vec2f(3.0f, 25));
            PlayerID = Player.agentID.ID;
            //SpawnStuff();

            var inventoryAttacher = Inventory.InventoryAttacher.Instance;

            inventoryID = Player.agentInventory.InventoryID;
            toolBarID = Player.agentToolBar.ToolBarID;

            ItemEntity gun = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.Gun);
            ItemEntity ore = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.Ore);
            ItemEntity placementTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.PlacementTool);
            ItemEntity removeTileTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.RemoveTileTool);
            ItemEntity spawnEnemySlimeTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.SpawnEnemySlimeTool);
            ItemEntity miningLaserTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.MiningLaserTool);
            ItemEntity pipePlacementTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.PipePlacementTool);
            ItemEntity particleEmitterPlacementTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext.item, Enums.ItemType.ParticleEmitterPlacementTool);


            inventoryManager.AddItem(Planet.EntitasContext, placementTool, toolBarID);
            inventoryManager.AddItem(Planet.EntitasContext, removeTileTool, toolBarID);
            inventoryManager.AddItem(Planet.EntitasContext, spawnEnemySlimeTool, toolBarID);
            inventoryManager.AddItem(Planet.EntitasContext, miningLaserTool, toolBarID);
            inventoryManager.AddItem(Planet.EntitasContext, pipePlacementTool, toolBarID);
            inventoryManager.AddItem(Planet.EntitasContext, particleEmitterPlacementTool, toolBarID);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var tileMap = ref Planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    var frontTileID = TileID.Air;

                    if (i >= tileMap.MapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                        {
                            frontTileID = TileID.Moon;
                        }
                        else
                        {
                            frontTileID = TileID.Glass;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                        {
                            frontTileID = TileID.Glass;
                        }
                        else
                        {
                            frontTileID = TileID.Moon;
                        }
                    }


                    tileMap.SetTile(i, j, frontTileID, MapLayerType.Front);
                }
            }

            /*for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                for (int j = tileMap.MapSize.Y - 10; j < tileMap.MapSize.Y; j++)
                {
                    tileMap.SetTile(i, j, TileID.Air, MapLayerType.Front);
                }
            }

            int carveHeight = tileMap.MapSize.Y - 10;

            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                int move = ((int) KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int) KMath.Random.Mt19937.genrand_int32() % 5) <= 3)
                {
                    move = 0;
                }

                carveHeight += move;
                if (carveHeight >= tileMap.MapSize.Y)
                {
                    carveHeight = tileMap.MapSize.Y - 1;
                }

                if (carveHeight < 0)
                {
                    carveHeight = 0;
                }
                for (int j = carveHeight; j < tileMap.MapSize.Y && j < carveHeight + 4; j++)
                {
                    tileMap.SetTile(i, j, TileID.Air, MapLayerType.Front);
                }
            }

            carveHeight = 5;

            for (int i = tileMap.MapSize.X - 1; i >= 0; i--)
            {
                int move = ((int) KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int) KMath.Random.Mt19937.genrand_int32() % 10) <= 3)
                {
                    move = 1;
                }

                carveHeight += move;
                if (carveHeight >= tileMap.MapSize.Y)
                {
                    carveHeight = tileMap.MapSize.Y - 1;
                }

                if (carveHeight < 0)
                {
                    carveHeight = 0;
                }

                for (int j = carveHeight; j < tileMap.MapSize.Y && j < carveHeight + 4; j++)
                {
                    tileMap.SetTile(i, j, TileID.Air, MapLayerType.Front);
                }
            }
*/

       //     tileMap.UpdateTileMapPositions(MapLayerType.Front);

        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;
            System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

            float spawnHeight = tileMap.MapSize.Y - 2;


            Planet.AddAgent(new Vec2f(6.0f, spawnHeight));
            Planet.AddAgent(new Vec2f(1.0f, spawnHeight));

            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                if (random.Next() % 5 == 0)
                {
                    Planet.AddEnemy(new Vec2f((float)i, spawnHeight));    
                }
            }
            
            GameState.ItemSpawnSystem.SpawnItem(Planet.EntitasContext, Enums.ItemType.Gun, new Vec2f(6.0f, spawnHeight));
            GameState.ItemSpawnSystem.SpawnItem(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));
        }
        
    }
    
}
