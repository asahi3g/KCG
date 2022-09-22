using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using KGUI.Elements;
using PlanetTileMap;
using System.IO;
using Particle;

namespace Planet.Unity
{
    class GeometryTestSceneScript : MonoBehaviour
    {
        [SerializeField] Material Material;

        [SerializeField]
        private bool enableGeometryPlacementTool;

        public PlanetState Planet;
        Inventory.InventoryManager inventoryManager;

        GeometryBlockPlacementTool geometryPlacementTool;

        AgentEntity Player;

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
            if (Init)
            {
                int selectedSlot = Planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).inventoryEntity.SelectedSlotID;

                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, InventoryID, selectedSlot);
                if (item != null)
                {
                    ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);
                    if (itemProperty.IsTool())
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (!Inventory.InventorySystemsState.MouseDown)
                                GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType,
                                Player.agentID.ID, item.itemID.ID);
                        }
                    }
                }

                Planet.Update(Time.deltaTime, Material, transform);
                Planet.DrawHUD(Player);

                if (enableGeometryPlacementTool)
                {
                    geometryPlacementTool.UpdateToolGrid();
                }

                MaterialBag.hasInventoryDraw = Planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).hasInventoryDraw;
            }
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            // Draw HUD
            Planet.DrawHUD(Player);

            if (Event.current.type != EventType.Repaint)
                return;

            // Draw Statistics
            KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);
        }

        private void OnDrawGizmos()
        {
            // Set the color of gizmos
            Gizmos.color = Color.green;

            // Draw a cube around the map
            if (Planet.TileMap != null)
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            Gizmos.color = Color.yellow;
            CircleSmoke.DrawGizmos();
            Gizmos.color = Color.red;

            // Draw lines around player if out of bounds
            if (Player != null)
                if (Player.agentPhysicsState.Position.X - 10.0f >= Planet.TileMap.MapSize.X)
                {
                    // Out of bounds

                    // X+
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X + 10.0f, Player.agentPhysicsState.Position.Y));

                    // X-
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X - 10.0f, Player.agentPhysicsState.Position.Y));

                    // Y+
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y + 10.0f));

                    // Y-
                    Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            Admin.AdminAPI.DrawChunkVisualizer(Planet.TileMap);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {

            Application.targetFrameRate = 60;

            inventoryManager = new Inventory.InventoryManager();

            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            /*var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
            Planet.TileMap = TileMapManager.Load("map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);*/

            GenerateMap();
            SpawnStuff();

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD(Player);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool = new GeometryBlockPlacementTool(true, true);
                geometryPlacementTool.Initialize(ref Planet, Material, transform);
            }

            //TileMapManager.Save(Planet.TileMap, "map.kmap");

            MaterialBag = Planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID(), "MaterialBag");

            InventoryID = Player.agentInventory.InventoryID;

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol, Planet.EntitasContext);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore, Planet.EntitasContext);

            // Admin API Add Items
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementToolBack, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemySlimeTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PipePlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ParticleEmitterPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemyGunnerTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementMaterialTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PotionTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GasBomb, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Dirt, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Bedrock, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Pipe, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Wire, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.HealthPositon, 64, Planet.EntitasContext);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);

            ref var tileMap = ref Planet.TileMap;

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 0, TileID.HSQNoSpecular_3);
                tileMap.SetFrontTile(i, 31, TileID.HSQNoSpecular_3);
            }

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(31, i, TileID.HSQNoSpecular_0);
                tileMap.SetFrontTile(0, i, TileID.HSQNoSpecular_0);
            }

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 1, TileID.SQNoSpecular_0);
            }

            for (int i = 0; i < 13; i++)
            {
                tileMap.SetFrontTile(i, 7, TileID.SQNoSpecular_0);
            }

            for (int i = 19; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 7, TileID.SQNoSpecular_0);
            }

            tileMap.SetFrontTile(24, 8, TileID.TO_3);
            tileMap.SetFrontTile(25, 9, TileID.TO_3);
            tileMap.SetFrontTile(26, 10, TileID.TO_3);
            tileMap.SetFrontTile(27, 11, TileID.TO_3);
            tileMap.SetFrontTile(28, 12, TileID.TO_3);


            tileMap.SetFrontTile(24, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(23, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(22, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(21, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(20, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(19, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(18, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(17, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(16, 12, TileID.HSQ_0);
            tileMap.SetFrontTile(15, 12, TileID.HSQ_0);

            tileMap.SetFrontTile(30, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(29, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(28, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(27, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(26, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(25, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(24, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(23, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(22, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(21, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(20, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(19, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(18, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(17, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(16, 16, TileID.HSQ_0);
            tileMap.SetFrontTile(15, 16, TileID.HSQ_0);


            tileMap.SetFrontTile(14, 12, TileID.RCSQ_3);
            tileMap.SetFrontTile(13, 11, TileID.RCSQ_3);
            tileMap.SetFrontTile(12, 10, TileID.RCSQ_3);
            tileMap.SetFrontTile(11, 9, TileID.RCSQ_3);
            tileMap.SetFrontTile(10, 8, TileID.RCSQ_3);

            tileMap.SetFrontTile(14, 11, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(13, 10, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(12, 9, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(11, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(12, 8, TileID.SQNoSpecular_0);

            tileMap.SetFrontTile(10, 13, TileID.TO_2);
            tileMap.SetFrontTile(9, 12, TileID.TO_2);




            tileMap.SetFrontTile(25, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(26, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(27, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(28, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(29, 8, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(30, 8, TileID.SQNoSpecular_0);

            tileMap.SetFrontTile(26, 9, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(27, 9, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(28, 9, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(29, 9, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(30, 9, TileID.SQNoSpecular_0);

            tileMap.SetFrontTile(27, 10, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(28, 10, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(29, 10, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(30, 10, TileID.SQNoSpecular_0);

            tileMap.SetFrontTile(28, 11, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(29, 11, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(30, 11, TileID.SQNoSpecular_0);

            tileMap.SetFrontTile(29, 12, TileID.SQNoSpecular_0);
            tileMap.SetFrontTile(30, 12, TileID.SQNoSpecular_0);



            for (int y = 0; y < 31; y++)
            {
                for (int x = 0; x < 31; x++)
                {
                    tileMap.SetBackTile(x, y, TileID.Glass);
                }
            }

            tileMap.SetFrontTile(15, 4, TileID.Platform);
            tileMap.SetFrontTile(14, 4, TileID.Platform);
            tileMap.SetFrontTile(16, 4, TileID.Platform);

            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            tileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = Planet.AddPlayer(new Vec2f(3.0f, spawnHeight));

            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));
        }
    }
}
