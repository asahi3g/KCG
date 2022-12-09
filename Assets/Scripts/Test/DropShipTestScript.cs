//imports UnityEngine


using Enums.PlanetTileMap;
using KMath;
using Particle;
using PlanetTileMap;

namespace Planet.Unity
{
    class DropShipTestScript : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        [UnityEngine.SerializeField]
        private bool enableGeometryPlacementTool;


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
            ref var planet = ref GameState.Planet;
            
            planet.Update(UnityEngine.Time.deltaTime);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool.UpdateToolGrid();
            }
        }

        private void OnDrawGizmos()
        {
            ref var planet = ref GameState.Planet;
            
            planet.DrawDebug();

            // Set the color of gizmos
            UnityEngine.Gizmos.color = UnityEngine.Color.green;

            // Draw a cube around the map
            if (planet.TileMap != null)
                UnityEngine.Gizmos.DrawWireCube(UnityEngine.Vector3.zero, new UnityEngine.Vector3(planet.TileMap.MapSize.X, planet.TileMap.MapSize.Y, 0.0f));

            UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
            CircleSmoke.DrawGizmos();
            UnityEngine.Gizmos.color = UnityEngine.Color.red;

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
            ref var planet = ref GameState.Planet;
            Vec2i mapSize = new Vec2i(32, 32);
            planet.Init(mapSize);

            GenerateMap();
            SpawnStuff();

            planet.InitializeSystems(Material, transform);

            if (enableGeometryPlacementTool)
            {
                geometryPlacementTool = new GeometryBlockPlacementTool(true, true);
                geometryPlacementTool.Initialize(Material, transform);
            }

            //TileMapManager.Save(GameState.Planet.TileMap, "map.kmap");

            MaterialBag = planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID());

            InventoryID = Player.agentInventory.InventoryID;

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore);

            // Admin API Add s
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.Flare);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Dirt, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Bedrock, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Pipe, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Wire, 64);
            Admin.AdminAPI.AddItem(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.HealthPotion, 64);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);

            ref var planet = ref GameState.Planet;

            ref var tileMap = ref planet.TileMap;

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
            ref var planet = ref GameState.Planet;
            
            ref var tileMap = ref planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = planet.AddAgentAsPlayer(new Vec2f(3.0f, spawnHeight));

            planet.AddItemParticle(Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            planet.AddItemParticle(Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));

            planet.AddVehicle(Enums.VehicleType.DropShip, new Vec2f(25, 32));
        }
    }
}
