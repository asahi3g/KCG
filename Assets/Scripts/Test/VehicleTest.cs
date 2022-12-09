//imports UnityEngine

using Enums.PlanetTileMap;
using KMath;
using Particle;
using PlanetTileMap;

namespace Planet.Unity
{
    class VehicleTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        [UnityEngine.SerializeField]
        private bool enableGeometryPlacementTool;


        Inventory.InventoryManager inventoryManager;

        GeometryBlockPlacementTool geometryPlacementTool;

        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int InventoryID;
        InventoryEntity MaterialBag;

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
            var planet = GameState.Planet;
            planet.Update(UnityEngine.Time.deltaTime);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool.UpdateToolGrid();
            }
        }

        private void OnDrawGizmos()
        {
            // Set the color of gizmos
            UnityEngine.Gizmos.color = UnityEngine. Color.green;

            var planet = GameState.Planet;
            // Draw a cube around the map
            if (planet.TileMap != null)
                UnityEngine.Gizmos.DrawWireCube(UnityEngine.Vector3.zero, new UnityEngine.Vector3(planet.TileMap.MapSize.X, planet.TileMap.MapSize.Y, 0.0f));

            UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
            CircleSmoke.DrawGizmos();
            UnityEngine.Gizmos.color = UnityEngine.Color.red;

            GameState.VehicleCollisionSystem.DrawGizmos();

            // Draw lines around player if out of bounds
            if (Player != null)
                if (Player.agentPhysicsState.Position.X - 10.0f >= planet.TileMap.MapSize.X)
                {
                    // Out of bounds

                    // X+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X + 10.0f, Player.agentPhysicsState.Position.Y));

                    // X-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X - 10.0f, Player.agentPhysicsState.Position.Y));

                    // Y+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y + 10.0f));

                    // Y-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            ChunkVisualizer.Draw(0.5f, 0.0f);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {

            UnityEngine.Application.targetFrameRate = 60;

            inventoryManager = new Inventory.InventoryManager();

            GameResources.Initialize();

            // Generating the map
            var planet = GameState.Planet;
            Vec2i mapSize = new Vec2i(32, 32);
            planet.Init(mapSize);

            /*var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
            planet.TileMap = TileMapManager.Load("map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);*/

            GenerateMap();
            SpawnStuff();

            planet.InitializeSystems(Material, transform);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool = new GeometryBlockPlacementTool(true, true);
                geometryPlacementTool.Initialize(Material, transform);
            }

            //TileMapManager.Save(planet.TileMap, "map.kmap");

            MaterialBag = planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID());

            InventoryID = Player.agentInventory.InventoryID;

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore);

            // Admin API Add Items
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemySlimeTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ParticleEmitterPlacementTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementMaterialTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PotionTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GasBomb);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Dirt, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Bedrock, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Pipe, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Wire, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.HealthPotion, 64);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);

            var planet = GameState.Planet;
            
            ref var tileMap = ref planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    var frontTileID = TileID.Air;
                    var backTileID = TileID.Air;

                    if (i >= tileMap.MapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                        {
                            frontTileID = TileID.Moon;
                            backTileID = TileID.Background;
                        }
                        else
                        {
                            frontTileID = TileID.Glass;
                            backTileID = TileID.Background;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                        {
                            frontTileID = TileID.Glass;
                            backTileID = TileID.Background;
                        }
                        else
                        {
                            frontTileID = TileID.Moon;
                            backTileID = TileID.Background;
                            if ((int)KMath.Random.Mt19937.genrand_int32() % 10 == 0)
                            {
                                int oreRandom = (int)KMath.Random.Mt19937.genrand_int32() % 3;
                                if (oreRandom == 0)
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameState.ItemCreationApi.OreSprite;
                                }
                                else if (oreRandom == 1)
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameState.ItemCreationApi.Ore2Sprite;
                                }
                                else
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameState.ItemCreationApi.Ore3Sprite;
                                }

                                tileMap.GetTile(i, j).DrawType = TileDrawType.Composited;
                            }
                        }
                    }

                    tileMap.SetFrontTile(i, j, frontTileID);
                    tileMap.SetBackTile(i, j, backTileID);
                }
            }



            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                for (int j = tileMap.MapSize.Y - 10; j < tileMap.MapSize.Y; j++)
                {
                    tileMap.SetFrontTile(i, j, TileID.Air);
                    tileMap.SetBackTile(i, j, TileID.Air);
                    tileMap.GetTile(i, j).DrawType = TileDrawType.Normal;
                }
            }

            int carveHeight = tileMap.MapSize.Y - 10;

            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                int move = ((int)KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int)KMath.Random.Mt19937.genrand_int32() % 5) <= 3)
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
                    tileMap.SetFrontTile(i, j, TileID.Air);
                    tileMap.SetBackTile(i, j, TileID.Air);
                    tileMap.SetMidTile(i, j, TileID.Wire);
                }
            }

            carveHeight = 5;

            for (int i = tileMap.MapSize.X - 1; i >= 0; i--)
            {
                int move = ((int)KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int)KMath.Random.Mt19937.genrand_int32() % 10) <= 3)
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
                    tileMap.GetTile(i, j).FrontTileID = TileID.Air;
                    tileMap.GetTile(i, j).MidTileID = TileID.Pipe;
                }
            }


            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }

            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            tileMap.SetFrontTile(4, 15, TileID.Platform);
            tileMap.SetFrontTile(5, 15, TileID.Platform);
            tileMap.SetFrontTile(6, 15, TileID.Platform);
            tileMap.SetFrontTile(7, 15, TileID.Platform);
            tileMap.SetFrontTile(8, 15, TileID.Platform);

            tileMap.SetFrontTile(4, 18, TileID.Platform);
            tileMap.SetFrontTile(5, 18, TileID.Platform);
            tileMap.SetFrontTile(6, 18, TileID.Platform);
            tileMap.SetFrontTile(7, 18, TileID.Platform);
            tileMap.SetFrontTile(8, 18, TileID.Platform);

            tileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
        }

        void SpawnStuff()
        {
            var planet = GameState.Planet;
            ref var tileMap = ref planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = planet.AddAgentAsPlayer(new Vec2f(3.0f, spawnHeight));
            PlayerID = Player.agentID.ID;

            planet.AddVehicle(Enums.VehicleType.Jet, new Vec2f(5.0f, 25.0f));

            planet.AddItemParticle(Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            planet.AddItemParticle(Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));
        }
    }
}
