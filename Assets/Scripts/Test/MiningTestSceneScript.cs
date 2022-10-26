//imports UnityEngine


using Enums.PlanetTileMap;
using KMath;
using Particle;
using PlanetTileMap;

namespace Planet.Unity
{
    class MiningTestSceneScript : UnityEngine.MonoBehaviour
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
            planet.Update(UnityEngine.Time.deltaTime, Material, transform);
            planet.DrawHUD(Player);

            if (enableGeometryPlacementTool)
            {
                //geometryPlacementTool.UpdateToolGrid();
            }

            MaterialBag.hasInventoryDraw = planet.EntitasContext.inventory.GetEntityWithInventoryID(InventoryID).hasInventoryDraw;
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            // Draw HUD
            GameState.Planet.DrawHUD(Player);

            if (UnityEngine.Event.current.type != UnityEngine.EventType.Repaint)
                return;

            // Draw Statistics
            KGUI.Statistics.StatisticsDisplay.DrawStatistics();
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
            Vec2i mapSize = new Vec2i(64, 32);
            planet.Init(mapSize);

            GenerateMap();
            SpawnStuff();

            planet.InitializeSystems(Material, transform);
            planet.InitializeHUD();

            if (enableGeometryPlacementTool)
            {
                //geometryPlacementTool = new GeometryBlockPlacementTool(true, true);
                //geometryPlacementTool.Initialize(ref Planet, Material, transform);
            }

            MaterialBag = planet.AddInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID(), "MaterialBag");

            InventoryID = Player.agentInventory.InventoryID;

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore);

            // Admin API Add Items
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.Pickaxe);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);
            
            ref var planet = ref GameState.Planet;

            ref var tileMap = ref planet.TileMap;

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


            tileMap.SetBackTile(6, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(6, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(7, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(7, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(8, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(8, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(9, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(9, 7, TileID.FP_R0_Rock);




            tileMap.SetFrontTile(6, 1, TileID.GoldBlock_1);
            tileMap.SetFrontTile(6, 2, TileID.GoldBlock_3);
            tileMap.SetFrontTile(6, 3, TileID.GoldBlock_0);
            tileMap.SetFrontTile(6, 4, TileID.GoldBlock_6);
            tileMap.SetFrontTile(6, 5, TileID.GoldBlock_7);
            tileMap.SetFrontTile(6, 6, TileID.GoldBlock_4);
            tileMap.SetFrontTile(6, 7, TileID.GoldBlock_2);

            tileMap.SetFrontTile(7, 1, TileID.GoldBlock_1);
            tileMap.SetFrontTile(7, 2, TileID.GoldBlock_3);
            tileMap.SetFrontTile(7, 3, TileID.GoldBlock_0);
            tileMap.SetFrontTile(7, 4, TileID.GoldBlock_6);
            tileMap.SetFrontTile(7, 5, TileID.GoldBlock_7);
            tileMap.SetFrontTile(7, 6, TileID.GoldBlock_4);
            tileMap.SetFrontTile(7, 7, TileID.GoldBlock_2);

            tileMap.SetFrontTile(8, 1, TileID.GoldBlock_1);
            tileMap.SetFrontTile(8, 2, TileID.GoldBlock_3);
            tileMap.SetFrontTile(8, 3, TileID.GoldBlock_0);
            tileMap.SetFrontTile(8, 4, TileID.GoldBlock_6);
            tileMap.SetFrontTile(8, 5, TileID.GoldBlock_7);
            tileMap.SetFrontTile(8, 6, TileID.GoldBlock_4);
            tileMap.SetFrontTile(8, 7, TileID.GoldBlock_2);

            tileMap.SetFrontTile(9, 1, TileID.GoldBlock_1);
            tileMap.SetFrontTile(9, 2, TileID.GoldBlock_3);
            tileMap.SetFrontTile(9, 3, TileID.GoldBlock_0);
            tileMap.SetFrontTile(9, 4, TileID.GoldBlock_6);
            tileMap.SetFrontTile(9, 5, TileID.GoldBlock_7);
            tileMap.SetFrontTile(9, 6, TileID.GoldBlock_4);
            tileMap.SetFrontTile(9, 7, TileID.GoldBlock_2);



            tileMap.SetBackTile(10, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(10, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(11, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(11, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(12, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(12, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(13, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(13, 7, TileID.FP_R0_Rock);



            tileMap.SetFrontTile(10, 1, TileID.DiamondBlock_0);
            tileMap.SetFrontTile(10, 2, TileID.DiamondBlock_4);
            tileMap.SetFrontTile(10, 3, TileID.DiamondBlock_7);
            tileMap.SetFrontTile(10, 4, TileID.DiamondBlock_6);
            tileMap.SetFrontTile(10, 5, TileID.DiamondBlock_2);
            tileMap.SetFrontTile(10, 6, TileID.DiamondBlock_1);
            tileMap.SetFrontTile(10, 7, TileID.DiamondBlock_5);

            tileMap.SetFrontTile(11, 1, TileID.DiamondBlock_0);
            tileMap.SetFrontTile(11, 2, TileID.DiamondBlock_4);
            tileMap.SetFrontTile(11, 3, TileID.DiamondBlock_7);
            tileMap.SetFrontTile(11, 4, TileID.DiamondBlock_6);
            tileMap.SetFrontTile(11, 5, TileID.DiamondBlock_2);
            tileMap.SetFrontTile(11, 6, TileID.DiamondBlock_1);
            tileMap.SetFrontTile(11, 7, TileID.DiamondBlock_5);

            tileMap.SetFrontTile(12, 1, TileID.DiamondBlock_0);
            tileMap.SetFrontTile(12, 2, TileID.DiamondBlock_4);
            tileMap.SetFrontTile(12, 3, TileID.DiamondBlock_7);
            tileMap.SetFrontTile(12, 4, TileID.DiamondBlock_6);
            tileMap.SetFrontTile(12, 5, TileID.DiamondBlock_2);
            tileMap.SetFrontTile(12, 6, TileID.DiamondBlock_1);
            tileMap.SetFrontTile(12, 7, TileID.DiamondBlock_5);

            tileMap.SetFrontTile(13, 1, TileID.DiamondBlock_0);
            tileMap.SetFrontTile(13, 2, TileID.DiamondBlock_4);
            tileMap.SetFrontTile(13, 3, TileID.DiamondBlock_7);
            tileMap.SetFrontTile(13, 4, TileID.DiamondBlock_6);
            tileMap.SetFrontTile(13, 5, TileID.DiamondBlock_2);
            tileMap.SetFrontTile(13, 6, TileID.DiamondBlock_1);
            tileMap.SetFrontTile(13, 7, TileID.DiamondBlock_5);


            tileMap.SetBackTile(14, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(14, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(15, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(15, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(16, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(16, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(17, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(17, 7, TileID.FP_R0_Rock);



            tileMap.SetFrontTile(14, 1, TileID.LapisBlock_0);
            tileMap.SetFrontTile(14, 2, TileID.LapisBlock_4);
            tileMap.SetFrontTile(14, 3, TileID.LapisBlock_7);
            tileMap.SetFrontTile(14, 4, TileID.LapisBlock_6);
            tileMap.SetFrontTile(14, 5, TileID.LapisBlock_2);
            tileMap.SetFrontTile(14, 6, TileID.LapisBlock_1);
            tileMap.SetFrontTile(14, 7, TileID.LapisBlock_5);

            tileMap.SetFrontTile(15, 1, TileID.LapisBlock_0);
            tileMap.SetFrontTile(15, 2, TileID.LapisBlock_4);
            tileMap.SetFrontTile(15, 3, TileID.LapisBlock_7);
            tileMap.SetFrontTile(15, 4, TileID.LapisBlock_6);
            tileMap.SetFrontTile(15, 5, TileID.LapisBlock_2);
            tileMap.SetFrontTile(15, 6, TileID.LapisBlock_1);
            tileMap.SetFrontTile(15, 7, TileID.LapisBlock_5);

            tileMap.SetFrontTile(16, 1, TileID.LapisBlock_0);
            tileMap.SetFrontTile(16, 2, TileID.LapisBlock_4);
            tileMap.SetFrontTile(16, 3, TileID.LapisBlock_7);
            tileMap.SetFrontTile(16, 4, TileID.LapisBlock_6);
            tileMap.SetFrontTile(16, 5, TileID.LapisBlock_2);
            tileMap.SetFrontTile(16, 6, TileID.LapisBlock_1);
            tileMap.SetFrontTile(16, 7, TileID.LapisBlock_5);

            tileMap.SetFrontTile(17, 1, TileID.LapisBlock_0);
            tileMap.SetFrontTile(17, 2, TileID.LapisBlock_4);
            tileMap.SetFrontTile(17, 3, TileID.LapisBlock_7);
            tileMap.SetFrontTile(17, 4, TileID.LapisBlock_6);
            tileMap.SetFrontTile(17, 5, TileID.LapisBlock_2);
            tileMap.SetFrontTile(17, 6, TileID.LapisBlock_1);
            tileMap.SetFrontTile(17, 7, TileID.LapisBlock_5);



            tileMap.SetBackTile(18, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(18, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(19, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(19, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(20, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(20, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(21, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(21, 7, TileID.FP_R0_Rock);



            tileMap.SetFrontTile(18, 1, TileID.EmeraldBlock_0);
            tileMap.SetFrontTile(18, 2, TileID.EmeraldBlock_4);
            tileMap.SetFrontTile(18, 3, TileID.EmeraldBlock_7);
            tileMap.SetFrontTile(18, 4, TileID.EmeraldBlock_6);
            tileMap.SetFrontTile(18, 5, TileID.EmeraldBlock_2);
            tileMap.SetFrontTile(18, 6, TileID.EmeraldBlock_1);
            tileMap.SetFrontTile(18, 7, TileID.EmeraldBlock_5);

            tileMap.SetFrontTile(19, 1, TileID.EmeraldBlock_0);
            tileMap.SetFrontTile(19, 2, TileID.EmeraldBlock_4);
            tileMap.SetFrontTile(19, 3, TileID.EmeraldBlock_7);
            tileMap.SetFrontTile(19, 4, TileID.EmeraldBlock_6);
            tileMap.SetFrontTile(19, 5, TileID.EmeraldBlock_2);
            tileMap.SetFrontTile(19, 6, TileID.EmeraldBlock_1);
            tileMap.SetFrontTile(19, 7, TileID.EmeraldBlock_5);

            tileMap.SetFrontTile(20, 1, TileID.EmeraldBlock_0);
            tileMap.SetFrontTile(20, 2, TileID.EmeraldBlock_4);
            tileMap.SetFrontTile(20, 3, TileID.EmeraldBlock_7);
            tileMap.SetFrontTile(20, 4, TileID.EmeraldBlock_6);
            tileMap.SetFrontTile(20, 5, TileID.EmeraldBlock_2);
            tileMap.SetFrontTile(20, 6, TileID.EmeraldBlock_1);
            tileMap.SetFrontTile(20, 7, TileID.EmeraldBlock_5);

            tileMap.SetFrontTile(21, 1, TileID.EmeraldBlock_0);
            tileMap.SetFrontTile(21, 2, TileID.EmeraldBlock_4);
            tileMap.SetFrontTile(21, 3, TileID.EmeraldBlock_7);
            tileMap.SetFrontTile(21, 4, TileID.EmeraldBlock_6);
            tileMap.SetFrontTile(21, 5, TileID.EmeraldBlock_2);
            tileMap.SetFrontTile(21, 6, TileID.EmeraldBlock_1);
            tileMap.SetFrontTile(21, 7, TileID.EmeraldBlock_5);


            tileMap.SetBackTile(22, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(22, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(23, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(23, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(24, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(24, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(25, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(25, 7, TileID.FP_R0_Rock);



            tileMap.SetFrontTile(22, 1, TileID.RedStoneBlock_0);
            tileMap.SetFrontTile(22, 2, TileID.RedStoneBlock_4);
            tileMap.SetFrontTile(22, 3, TileID.RedStoneBlock_7);
            tileMap.SetFrontTile(22, 4, TileID.RedStoneBlock_6);
            tileMap.SetFrontTile(22, 5, TileID.RedStoneBlock_2);
            tileMap.SetFrontTile(22, 6, TileID.RedStoneBlock_1);
            tileMap.SetFrontTile(22, 7, TileID.RedStoneBlock_5);

            tileMap.SetFrontTile(23, 1, TileID.RedStoneBlock_0);
            tileMap.SetFrontTile(23, 2, TileID.RedStoneBlock_4);
            tileMap.SetFrontTile(23, 3, TileID.RedStoneBlock_7);
            tileMap.SetFrontTile(23, 4, TileID.RedStoneBlock_6);
            tileMap.SetFrontTile(23, 5, TileID.RedStoneBlock_2);
            tileMap.SetFrontTile(23, 6, TileID.RedStoneBlock_1);
            tileMap.SetFrontTile(23, 7, TileID.RedStoneBlock_5);

            tileMap.SetFrontTile(24, 1, TileID.RedStoneBlock_0);
            tileMap.SetFrontTile(24, 2, TileID.RedStoneBlock_4);
            tileMap.SetFrontTile(24, 3, TileID.RedStoneBlock_7);
            tileMap.SetFrontTile(24, 4, TileID.RedStoneBlock_6);
            tileMap.SetFrontTile(24, 5, TileID.RedStoneBlock_2);
            tileMap.SetFrontTile(24, 6, TileID.RedStoneBlock_1);
            tileMap.SetFrontTile(24, 7, TileID.RedStoneBlock_5);

            tileMap.SetFrontTile(25, 1, TileID.RedStoneBlock_0);
            tileMap.SetFrontTile(25, 2, TileID.RedStoneBlock_4);
            tileMap.SetFrontTile(25, 3, TileID.RedStoneBlock_7);
            tileMap.SetFrontTile(25, 4, TileID.RedStoneBlock_6);
            tileMap.SetFrontTile(25, 5, TileID.RedStoneBlock_2);
            tileMap.SetFrontTile(25, 6, TileID.RedStoneBlock_1);
            tileMap.SetFrontTile(25, 7, TileID.RedStoneBlock_5);



            tileMap.SetBackTile(26, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(26, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(27, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(27, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(28, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(28, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(29, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(29, 7, TileID.FP_R0_Rock);


            tileMap.SetFrontTile(26, 1, TileID.IronBlock_0);
            tileMap.SetFrontTile(26, 2, TileID.IronBlock_4);
            tileMap.SetFrontTile(26, 3, TileID.IronBlock_7);
            tileMap.SetFrontTile(26, 4, TileID.IronBlock_6);
            tileMap.SetFrontTile(26, 5, TileID.IronBlock_2);
            tileMap.SetFrontTile(26, 6, TileID.IronBlock_1);
            tileMap.SetFrontTile(26, 7, TileID.IronBlock_5);

            tileMap.SetFrontTile(27, 1, TileID.IronBlock_0);
            tileMap.SetFrontTile(27, 2, TileID.IronBlock_4);
            tileMap.SetFrontTile(27, 3, TileID.IronBlock_7);
            tileMap.SetFrontTile(27, 4, TileID.IronBlock_6);
            tileMap.SetFrontTile(27, 5, TileID.IronBlock_2);
            tileMap.SetFrontTile(27, 6, TileID.IronBlock_1);
            tileMap.SetFrontTile(27, 7, TileID.IronBlock_5);

            tileMap.SetFrontTile(28, 1, TileID.IronBlock_0);
            tileMap.SetFrontTile(28, 2, TileID.IronBlock_4);
            tileMap.SetFrontTile(28, 3, TileID.IronBlock_7);
            tileMap.SetFrontTile(28, 4, TileID.IronBlock_6);
            tileMap.SetFrontTile(28, 5, TileID.IronBlock_2);
            tileMap.SetFrontTile(28, 6, TileID.IronBlock_1);
            tileMap.SetFrontTile(28, 7, TileID.IronBlock_5);

            tileMap.SetFrontTile(29, 1, TileID.IronBlock_0);
            tileMap.SetFrontTile(29, 2, TileID.IronBlock_4);
            tileMap.SetFrontTile(29, 3, TileID.IronBlock_7);
            tileMap.SetFrontTile(29, 4, TileID.IronBlock_6);
            tileMap.SetFrontTile(29, 5, TileID.IronBlock_2);
            tileMap.SetFrontTile(29, 6, TileID.IronBlock_1);
            tileMap.SetFrontTile(29, 7, TileID.IronBlock_5);

            tileMap.SetBackTile(30, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(30, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(31, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(31, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(32, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(32, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(33, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(33, 7, TileID.FP_R0_Rock);


            tileMap.SetFrontTile(30, 1, TileID.CoalBlock_0);
            tileMap.SetFrontTile(30, 2, TileID.CoalBlock_4);
            tileMap.SetFrontTile(30, 3, TileID.CoalBlock_7);
            tileMap.SetFrontTile(30, 4, TileID.CoalBlock_6);
            tileMap.SetFrontTile(30, 5, TileID.CoalBlock_2);
            tileMap.SetFrontTile(30, 6, TileID.CoalBlock_1);
            tileMap.SetFrontTile(30, 7, TileID.CoalBlock_5);

            tileMap.SetFrontTile(31, 1, TileID.CoalBlock_0);
            tileMap.SetFrontTile(31, 2, TileID.CoalBlock_4);
            tileMap.SetFrontTile(31, 3, TileID.CoalBlock_7);
            tileMap.SetFrontTile(31, 4, TileID.CoalBlock_6);
            tileMap.SetFrontTile(31, 5, TileID.CoalBlock_2);
            tileMap.SetFrontTile(31, 6, TileID.CoalBlock_1);
            tileMap.SetFrontTile(31, 7, TileID.CoalBlock_5);

            tileMap.SetFrontTile(32, 1, TileID.CoalBlock_0);
            tileMap.SetFrontTile(32, 2, TileID.CoalBlock_4);
            tileMap.SetFrontTile(32, 3, TileID.CoalBlock_7);
            tileMap.SetFrontTile(32, 4, TileID.CoalBlock_6);
            tileMap.SetFrontTile(32, 5, TileID.CoalBlock_2);
            tileMap.SetFrontTile(32, 6, TileID.CoalBlock_1);
            tileMap.SetFrontTile(32, 7, TileID.CoalBlock_5);

            tileMap.SetFrontTile(33, 1, TileID.CoalBlock_0);
            tileMap.SetFrontTile(33, 2, TileID.CoalBlock_4);
            tileMap.SetFrontTile(33, 3, TileID.CoalBlock_7);
            tileMap.SetFrontTile(33, 4, TileID.CoalBlock_6);
            tileMap.SetFrontTile(33, 5, TileID.CoalBlock_2);
            tileMap.SetFrontTile(33, 6, TileID.CoalBlock_1);
            tileMap.SetFrontTile(33, 7, TileID.CoalBlock_5);


            tileMap.SetBackTile(34, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(34, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(35, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(35, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(36, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(36, 7, TileID.FP_R0_Rock);

            tileMap.SetBackTile(37, 1, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 2, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 3, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 4, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 5, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 6, TileID.FP_R0_Rock);
            tileMap.SetBackTile(37, 7, TileID.FP_R0_Rock);


            tileMap.SetFrontTile(34, 1, TileID.PinkDiaBlock_0);
            tileMap.SetFrontTile(34, 2, TileID.PinkDiaBlock_4);
            tileMap.SetFrontTile(34, 3, TileID.PinkDiaBlock_7);
            tileMap.SetFrontTile(34, 4, TileID.PinkDiaBlock_6);
            tileMap.SetFrontTile(34, 5, TileID.PinkDiaBlock_2);
            tileMap.SetFrontTile(34, 6, TileID.PinkDiaBlock_1);
            tileMap.SetFrontTile(34, 7, TileID.PinkDiaBlock_5);

            tileMap.SetFrontTile(35, 1, TileID.PinkDiaBlock_0);
            tileMap.SetFrontTile(35, 2, TileID.PinkDiaBlock_4);
            tileMap.SetFrontTile(35, 3, TileID.PinkDiaBlock_7);
            tileMap.SetFrontTile(35, 4, TileID.PinkDiaBlock_6);
            tileMap.SetFrontTile(35, 5, TileID.PinkDiaBlock_2);
            tileMap.SetFrontTile(35, 6, TileID.PinkDiaBlock_1);
            tileMap.SetFrontTile(35, 7, TileID.PinkDiaBlock_5);

            tileMap.SetFrontTile(36, 1, TileID.PinkDiaBlock_0);
            tileMap.SetFrontTile(36, 2, TileID.PinkDiaBlock_4);
            tileMap.SetFrontTile(36, 3, TileID.PinkDiaBlock_7);
            tileMap.SetFrontTile(36, 4, TileID.PinkDiaBlock_6);
            tileMap.SetFrontTile(36, 5, TileID.PinkDiaBlock_2);
            tileMap.SetFrontTile(36, 6, TileID.PinkDiaBlock_1);
            tileMap.SetFrontTile(36, 7, TileID.PinkDiaBlock_5);

            tileMap.SetFrontTile(37, 1, TileID.PinkDiaBlock_0);
            tileMap.SetFrontTile(37, 2, TileID.PinkDiaBlock_4);
            tileMap.SetFrontTile(37, 3, TileID.PinkDiaBlock_7);
            tileMap.SetFrontTile(37, 4, TileID.PinkDiaBlock_6);
            tileMap.SetFrontTile(37, 5, TileID.PinkDiaBlock_2);
            tileMap.SetFrontTile(37, 6, TileID.PinkDiaBlock_1);
            tileMap.SetFrontTile(37, 7, TileID.PinkDiaBlock_5);

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

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = planet.AddPlayer(new Vec2f(3.0f, spawnHeight));
        }
    }
}
