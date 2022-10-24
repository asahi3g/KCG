using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Particle;
using PlanetTileMap;

namespace Planet.Unity
{
    class DropShipTestScript : MonoBehaviour
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
            ChunkVisualizer.Draw(Planet.TileMap, 0.5f, 0.0f);
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

            GenerateMap();
            SpawnStuff();

            Planet.InitializeSystems(Material, transform);
            Planet.InitializeHUD();

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
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.Flare, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
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

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    tileMap.SetFrontTile(x, y / 2, TileID.Moon);
                }
            }

            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            tileMap.SetFrontTile(1, 7, TileID.Moon);
            tileMap.SetFrontTile(1, 8, TileID.Moon);
            tileMap.SetFrontTile(1, 9, TileID.Moon);
            tileMap.SetFrontTile(1, 10, TileID.Moon);
            tileMap.SetFrontTile(1, 11, TileID.Moon);
            tileMap.SetFrontTile(1, 12, TileID.Moon);
            tileMap.SetFrontTile(1, 13, TileID.Moon);
            tileMap.SetFrontTile(1, 14, TileID.Moon);
            tileMap.SetFrontTile(1, 15, TileID.Moon);
            tileMap.SetFrontTile(1, 16, TileID.Moon);

            tileMap.SetFrontTile(2, 16, TileID.Moon);
            tileMap.SetFrontTile(3, 16, TileID.Moon);
            tileMap.SetFrontTile(4, 16, TileID.Moon);
            tileMap.SetFrontTile(5, 16, TileID.Moon);
            tileMap.SetFrontTile(6, 16, TileID.Moon);
            tileMap.SetFrontTile(7, 16, TileID.Moon);


            tileMap.SetFrontTile(10, 16, TileID.Moon);
            tileMap.SetFrontTile(11, 16, TileID.Moon);
            tileMap.SetFrontTile(12, 16, TileID.Moon);
            tileMap.SetFrontTile(13, 16, TileID.Moon);

            tileMap.SetFrontTile(13, 16, TileID.Moon);
            tileMap.SetFrontTile(13, 15, TileID.Moon);
            tileMap.SetFrontTile(13, 14, TileID.Moon);
            tileMap.SetFrontTile(13, 13, TileID.Moon);
            tileMap.SetFrontTile(13, 12, TileID.Moon);
            tileMap.SetFrontTile(13, 11, TileID.Moon);
            tileMap.SetFrontTile(13, 10, TileID.Moon);
            tileMap.SetFrontTile(13, 9, TileID.Moon);
            tileMap.SetFrontTile(13, 8, TileID.Moon);
            tileMap.SetFrontTile(13, 7, TileID.Moon);

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }
        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = Planet.AddPlayer(new Vec2f(3.0f, spawnHeight));

            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));

            Planet.AddVehicle(Enums.VehicleType.DropShip, new Vec2f(25, 32));
        }
    }
}
