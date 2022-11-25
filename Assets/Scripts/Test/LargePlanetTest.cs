//import UnityEngine

using Enums;
using Enums.PlanetTileMap;
using KMath;
using Item;

namespace Planet.Unity
{
    class LargePlanetTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        Inventory.InventoryManager inventoryManager;
        Inventory.DrawSystem inventoryDrawSystem;
        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

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
            if (!Init)
                return;
            
            int selectedSlot = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity.SelectedSlotIndex;

            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(inventoryID, selectedSlot);
            ItemProperties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);
            if (itemProperty.IsTool())
            {
                if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
                {
                    GameState.ActionCreationSystem.CreateAction(
                        itemProperty.ToolActionType, Player.agentID.ID);
                }
            }

            GameState.Planet.Update(UnityEngine.Time.deltaTime);
            Draw();
        }

        private void Draw()
        {
            if (!Init)
                return;

            if (UnityEngine.Event.current.type != UnityEngine.EventType.Repaint)
                return;

            inventoryDrawSystem.Draw();
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            inventoryManager = new Inventory.InventoryManager();
            inventoryDrawSystem = new Inventory.DrawSystem();

            GameResources.Initialize();

            // Generating the map
            ref var planet = ref GameState.Planet;
            Vec2i mapSize = new Vec2i(6400, 1600);

            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);

            GenerateMap();
            Player = planet.AddAgentAsPlayer(new Vec2f(3.0f, 1600));
            PlayerID = Player.agentID.ID;
            //SpawnStuff();

            inventoryID = Player.agentInventory.InventoryID;

            ItemInventoryEntity gun = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Pistol);
            ItemInventoryEntity ore = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Ore);
            ItemInventoryEntity placementTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.PlacementTool);
            ItemInventoryEntity removeTileTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.RemoveTileTool);
            ItemInventoryEntity spawnEnemySlimeTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.SpawnEnemySlimeTool);
            ItemInventoryEntity miningLaserTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.MiningLaserTool);
            ItemInventoryEntity particleEmitterPlacementTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.ParticleEmitterPlacementTool);


            inventoryManager.AddItem(placementTool, inventoryID);
            inventoryManager.AddItem(removeTileTool, inventoryID);
            inventoryManager.AddItem(spawnEnemySlimeTool, inventoryID);
            inventoryManager.AddItem(miningLaserTool, inventoryID);
            inventoryManager.AddItem(particleEmitterPlacementTool, inventoryID);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var planet = ref GameState.Planet;
            
            ref var tileMap = ref planet.TileMap;

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


                    tileMap.GetTile(i, j).FrontTileID = frontTileID;
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

            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            tileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

        }

        void SpawnStuff()
        {
            ref var planet = ref GameState.Planet;
            ref var tileMap = ref planet.TileMap;
            System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

            float spawnHeight = tileMap.MapSize.Y - 2;


            planet.AddAgent(new Vec2f(6.0f, spawnHeight), AgentType.Agent);
            planet.AddAgent(new Vec2f(1.0f, spawnHeight), AgentType.Agent);

            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                if (random.Next() % 5 == 0)
                {
                    planet.AddAgentAsEnemy(new Vec2f((float)i, spawnHeight));    
                }
            }
            
            planet.AddItemParticle(Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            planet.AddItemParticle(Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));
        }
        
    }
    
}
