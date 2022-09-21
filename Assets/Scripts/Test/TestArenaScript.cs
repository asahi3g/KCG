using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Particle;

namespace Planet.Unity
{
    class TestArenaScript : MonoBehaviour
    {
        [SerializeField] Material Material;

        [SerializeField]
        private bool enableGeometryPlacementTool;

        public PlanetState Planet;
        Inventory.InventoryManager inventoryManager;
        Inventory.DrawSystem inventoryDrawSystem;

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

        private void OnGUI()
        {
            if (!Init)
                return;

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
            inventoryDrawSystem = new Inventory.DrawSystem();

            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(256, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

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
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Dirt, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Bedrock, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Pipe, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.Wire, 64, Planet.EntitasContext);
            Admin.AdminAPI.AddItemStackable(inventoryManager, MaterialBag.inventoryID.ID, Enums.ItemType.HealthPositon, 64, Planet.EntitasContext);
        }

        void GenerateMap()
        {
            ref var tileMap = ref Planet.TileMap;

            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for (int j = 0; j < tileMap.MapSize.Y ; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }

            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = Planet.AddPlayer(new Vec2f(3.0f, spawnHeight));
            PlayerID = Player.agentID.ID;

            Planet.AddFixedFloatingText("BASIC MOTIONS\n1>Use arrow keys to move aronud and jump.", new Vec2f(10, 5), Color.white, 20);

            Planet.AddFixedFloatingText("Use Space Bar To Dash.", new Vec2f(30, 5), Color.white, 20);

            Planet.AddFixedFloatingText("Use 'F' To Start Jetpack", new Vec2f(45, 5), Color.white, 20);

            Planet.AddFixedFloatingText("Use 'Control' To Crouch\nPlayer also can move while crouching.", new Vec2f(60, 5), Color.white, 20);

            Planet.AddFixedFloatingText("Use 'K' To Roll", new Vec2f(75, 5), Color.white, 20);

            Planet.AddFixedFloatingText("TILE PLACEMENT TOOL\n1>Select tile placement tool from inventory.\n2>Select a tile from the menu.\n3>Place it anywhere with pressing 'LMB'.", new Vec2f(85, 5), Color.white, 20);
            Planet.AddItemParticle(new Vec2f(85, 7), Enums.ItemType.PlacementTool);

            Planet.AddFixedFloatingText("BACK TILE PLACEMENT TOOL\n1>Select back tile placement tool from inventory.\n2>Place it anywhere with pressing 'LMB'.", new Vec2f(105, 5), Color.white, 20);
            Planet.AddItemParticle(new Vec2f(105, 7), Enums.ItemType.PlacementToolBack);

            Planet.AddFixedFloatingText("REMOVE TILE TOOL\n1>Select remove tile tool from inventory.\n2>Click a tile to remove.\n3>Tile will drop as item when destroyed.", new Vec2f(125, 5), Color.white, 20);
            Planet.AddItemParticle(new Vec2f(125, 7), Enums.ItemType.RemoveTileTool);

            Planet.AddFixedFloatingText("Climb To Platforms Using Up Arrow", new Vec2f(140, 5), Color.white, 20);
            tileMap.SetFrontTile(140, 2, TileID.Platform);
            tileMap.SetFrontTile(141, 2, TileID.Platform);
            tileMap.SetFrontTile(142, 2, TileID.Platform);
            tileMap.SetFrontTile(143, 2, TileID.Platform);
            tileMap.SetFrontTile(144, 2, TileID.Platform);

            tileMap.SetFrontTile(145, 5, TileID.Platform);
            tileMap.SetFrontTile(146, 5, TileID.Platform);
            tileMap.SetFrontTile(147, 5, TileID.Platform);
            tileMap.SetFrontTile(148, 5, TileID.Platform);
            tileMap.SetFrontTile(149, 5, TileID.Platform);

            tileMap.SetFrontTile(140, 7, TileID.Platform);
            tileMap.SetFrontTile(141, 7, TileID.Platform);
            tileMap.SetFrontTile(142, 7, TileID.Platform);
            tileMap.SetFrontTile(143, 7, TileID.Platform);
            tileMap.SetFrontTile(144, 7, TileID.Platform);

            Planet.AddFixedFloatingText("PISTOL\n1>Take the gun.\n2>Shoot it using 'LMB'.\nPress 'R' to reload the clip.", new Vec2f(155, 5), Color.white, 20);
            Planet.AddItemParticle(new Vec2f(155, 7), Enums.ItemType.Pistol);


            Planet.AddFixedFloatingText("Eliminate the enemies.", new Vec2f(180, 5), Color.white, 20);
            Planet.AddAgent(new Vec2f(180, 7), Enums.AgentType.EnemyGunner);
            Planet.AddAgent(new Vec2f(185, 7), Enums.AgentType.EnemyHeavy);

            tileMap.SetFrontTile(180, 3, TileID.Platform);
            tileMap.SetFrontTile(181, 3, TileID.Platform);
            tileMap.SetFrontTile(182, 3, TileID.Platform);
            tileMap.SetFrontTile(183, 3, TileID.Platform);
            tileMap.SetFrontTile(184, 3, TileID.Platform);
                                 
            tileMap.SetFrontTile(185, 5, TileID.Platform);
            tileMap.SetFrontTile(186, 5, TileID.Platform);
            tileMap.SetFrontTile(187, 5, TileID.Platform);
            tileMap.SetFrontTile(188, 5, TileID.Platform);
            tileMap.SetFrontTile(189, 5, TileID.Platform);
                                 
            tileMap.SetFrontTile(180, 7, TileID.Platform);
            tileMap.SetFrontTile(181, 7, TileID.Platform);
            tileMap.SetFrontTile(182, 7, TileID.Platform);
            tileMap.SetFrontTile(183, 7, TileID.Platform);
            tileMap.SetFrontTile(184, 7, TileID.Platform);

            Planet.AddFixedFloatingText("ENEMY PLACEMENT TOOL.", new Vec2f(200, 5), Color.white, 20);
            Planet.AddItemParticle(new Vec2f(200, 5), Enums.ItemType.SpawnEnemySwordmanTool);
        }
    }
}
