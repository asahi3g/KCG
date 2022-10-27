//import UnityEngine

using Enums.PlanetTileMap;
using KMath;
using Item;
using Particle;
using PlanetTileMap;

namespace Planet.Unity
{
    class GeometryTestSceneScript : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        [UnityEngine.SerializeField]
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
                        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
                        {
                            if (!Inventory.InventorySystemsState.MouseDown)
                                GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType,
                                Player.agentID.ID, item.itemID.ID);
                        }
                    }
                }

                Planet.Update(UnityEngine.Time.deltaTime, Material, transform);
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

            if (UnityEngine.Event.current.type != UnityEngine.EventType.Repaint)
                return;

            // Draw Statistics
            KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);
        }

        private void OnDrawGizmos()
        {
            // Set the color of gizmos
            UnityEngine.Gizmos.color = UnityEngine.Color.green;

            // Draw a cube around the map
            if (Planet.TileMap != null)
                UnityEngine.Gizmos.DrawWireCube(UnityEngine.Vector3.zero, new UnityEngine.Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
            CircleSmoke.DrawGizmos();
            UnityEngine.Gizmos.color = UnityEngine.Color.red;

            // Draw lines around player if out of bounds
            if (Player != null)
                if (Player.agentPhysicsState.Position.X - 10.0f >= Planet.TileMap.MapSize.X)
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
            ChunkVisualizer.Draw(Planet.TileMap, 0.5f, 0.0f);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {

            UnityEngine.Application.targetFrameRate = 60;

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
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.Pistol, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GasBomb, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PotionTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.GeometryPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong)System.DateTime.Now.Ticks);

            ref var tileMap = ref Planet.TileMap;

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 0, TileID.HB_R3_Metal);
                tileMap.SetFrontTile(i, 31, TileID.HB_R3_Metal);
            }

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(31, i, TileID.HB_R0_Metal);
                tileMap.SetFrontTile(0, i, TileID.HB_R0_Metal);
            }

            for (int i = 0; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 1, TileID.SB_R0_Metal);
            }

            for (int i = 0; i < 13; i++)
            {
                tileMap.SetFrontTile(i, 7, TileID.SB_R0_Metal);
            }

            for (int i = 19; i < 31; i++)
            {
                tileMap.SetFrontTile(i, 7, TileID.SB_R0_Metal);
            }

            tileMap.SetFrontTile(24, 8, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(25, 9, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(26, 10, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(27, 11, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(28, 12, TileID.TB_R3_Metal);


            tileMap.SetFrontTile(24, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(23, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(22, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(21, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(20, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(19, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(18, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(17, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(16, 12, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(15, 12, TileID.HP_R0_Metal);

            tileMap.SetFrontTile(30, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(29, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(28, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(27, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(26, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(25, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(24, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(23, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(22, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(21, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(20, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(19, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(18, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(17, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(16, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(15, 16, TileID.HP_R0_Metal);

            tileMap.SetFrontTile(10, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(9, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(8, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(7, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(6, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(5, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(4, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(3, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(2, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(1, 16, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(0, 16, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(23, 17, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(24, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(25, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(26, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(27, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 17, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 17, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(24, 18, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(25, 18, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(26, 18, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(27, 18, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 18, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 18, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 18, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(25, 19, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(26, 19, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(27, 19, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 19, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 19, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 19, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(26, 20, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(27, 20, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 20, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 20, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 20, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(27, 21, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(28, 21, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 21, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 21, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(28, 22, TileID.TB_R3_Metal);
            tileMap.SetFrontTile(29, 22, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 22, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(24, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(23, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(22, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(21, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(20, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(19, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(18, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(17, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(16, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(15, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(14, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(13, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(12, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(11, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(10, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(9, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(8, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(7, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(6, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(5, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(4, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(3, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(2, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(1, 23, TileID.HP_R0_Metal);
            tileMap.SetFrontTile(0, 23, TileID.SB_R0_Metal);


            tileMap.SetFrontTile(14, 12, TileID.L2_R7_Metal);
            tileMap.SetFrontTile(13, 11, TileID.L2_R7_Metal);
            tileMap.SetFrontTile(12, 10, TileID.L2_R7_Metal);
            tileMap.SetFrontTile(11, 9, TileID.L2_R7_Metal);
            tileMap.SetFrontTile(10, 8, TileID.L2_R7_Metal);

            tileMap.SetFrontTile(14, 11, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(13, 10, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(12, 9, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(11, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(12, 8, TileID.SB_R0_Metal);


            tileMap.SetFrontTile(25, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(26, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(27, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 8, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 8, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(26, 9, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(27, 9, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 9, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 9, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 9, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(27, 10, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(28, 10, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 10, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 10, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(28, 11, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(29, 11, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 11, TileID.SB_R0_Metal);

            tileMap.SetFrontTile(29, 12, TileID.SB_R0_Metal);
            tileMap.SetFrontTile(30, 12, TileID.SB_R0_Metal);

            Planet.AddEnemy(new Vec2f(20, 10));
            Planet.AddEnemy(new Vec2f(20, 15));
            Planet.AddEnemy(new Vec2f(15, 15));

            Planet.AddAgent(new Vec2f(10, 22), Enums.AgentType.EnemyInsect);
            Planet.AddAgent(new Vec2f(20, 22), Enums.AgentType.EnemyInsect);
            Planet.AddAgent(new Vec2f(5, 12), Enums.AgentType.EnemyInsect);

            Planet.AddAgent(new Vec2f(5, 28), Enums.AgentType.EnemyGunner);
            Planet.AddAgent(new Vec2f(10, 28), Enums.AgentType.EnemyGunner);

            Planet.AddMech(new Vec2f(10, 2), Enums.MechType.SmashableBox);

            Planet.AddMech(new Vec2f(11, 6.1f), Enums.MechType.SurveillanceCamera);

            Planet.AddMech(new Vec2f(19, 6.1f), Enums.MechType.RoofScreen);

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

            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            tileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = Planet.AddPlayer(new Vec2f(2,3));
        }
    }
}
