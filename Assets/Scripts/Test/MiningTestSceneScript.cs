using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Particle;

namespace Planet.Unity
{
    class MiningTestSceneScript : MonoBehaviour
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
            Planet.Update(Time.deltaTime, Material, transform);
            Planet.DrawHUD(Player);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool.UpdateToolGrid();
            }

            MaterialBag.hasInventoryDraw = Planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).hasInventoryDraw;
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
            Planet.DrawDebug();

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
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementMaterialTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemySlimeTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ParticleEmitterPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SpawnEnemyGunnerTool, Planet.EntitasContext);
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

            tileMap.SetBackTile(5, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(5, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(4, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(4, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(3, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(3, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(2, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(2, 7, TileID.FP_R0_Rock);



            tileMap.SetFrontTile(5, 1, TileID.Moon);
            tileMap.SetFrontTile(5, 2, TileID.Moon);
            tileMap.SetFrontTile(5, 3, TileID.Moon);
            tileMap.SetFrontTile(5, 4, TileID.Moon);
            tileMap.SetFrontTile(5, 5, TileID.Moon);
            tileMap.SetFrontTile(5, 6, TileID.Moon);
            tileMap.SetFrontTile(5, 7, TileID.Moon);

            tileMap.SetFrontTile(4, 1, TileID.Moon);
            tileMap.SetFrontTile(4, 2, TileID.Moon);
            tileMap.SetFrontTile(4, 3, TileID.Moon);
            tileMap.SetFrontTile(4, 4, TileID.Moon);
            tileMap.SetFrontTile(4, 5, TileID.Moon);
            tileMap.SetFrontTile(4, 6, TileID.Moon);
            tileMap.SetFrontTile(4, 7, TileID.Moon);

            tileMap.SetFrontTile(3, 1, TileID.Moon);
            tileMap.SetFrontTile(3, 2, TileID.Moon);
            tileMap.SetFrontTile(3, 3, TileID.Moon);
            tileMap.SetFrontTile(3, 4, TileID.Moon);
            tileMap.SetFrontTile(3, 5, TileID.Moon);
            tileMap.SetFrontTile(3, 6, TileID.Moon);
            tileMap.SetFrontTile(3, 7, TileID.Moon);

            tileMap.SetFrontTile(2, 1, TileID.Moon);
            tileMap.SetFrontTile(2, 2, TileID.Moon);
            tileMap.SetFrontTile(2, 3, TileID.Moon);
            tileMap.SetFrontTile(2, 4, TileID.Moon);
            tileMap.SetFrontTile(2, 5, TileID.Moon);
            tileMap.SetFrontTile(2, 6, TileID.Moon);
            tileMap.SetFrontTile(2, 7, TileID.Moon);

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
