using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;

namespace Planet.Unity
{
    class MovementSceneScript : MonoBehaviour
    {
        [SerializeField] Material Material;

        public PlanetState Planet;
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
            if (Input.GetKeyDown(KeyCode.F1))
            {
                TileMapManager.Save(Planet.TileMap, "generated-maps/movement-map.kmap");
                Debug.Log("saved!");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                var camera = Camera.main;
                Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

                Planet.TileMap = TileMapManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

                Debug.Log("loaded!");
            }

            ref Inventory.InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
            int selectedSlot = inventory.SelectedSlotID;

            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, inventoryID, selectedSlot);
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);
            if (itemProperty.IsTool())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType, 
                       Player.agentID.ID, item.itemID.ID);
                }
            }

            Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            // Draw HUD UI
            HUDManager.Update(Player);

            // Draw Statistics
            KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);

            inventoryDrawSystem.Draw(Planet.EntitasContext);
        }
        

        private void OnDrawGizmos()
        {
            // Set the color of gizmos
            Gizmos.color = Color.green;
            
            // Draw a cube around the map
            if(Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPosition2D.Value.X -10.0f >= Planet.TileMap.MapSize.X)
                {
                    // Out of bounds
                
                    // X+
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X + 10.0f, Player.agentPosition2D.Value.Y));

                    // X-
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X - 10.0f, Player.agentPosition2D.Value.Y));

                    // Y+
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y + 10.0f));

                    // Y-
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y - 10.0f));
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
            Vec2i mapSize = new Vec2i(128, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            Planet.InitializeSystems(Material, transform);
            //GenerateMap();
            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            /*Planet.TileMap = TileMapManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                Debug.Log("loaded!");*/

            GenerateMap();

            Planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            Planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            Player = Planet.AddPlayer(new Vec2f(3.0f, 20));
            PlayerID = Player.agentID.ID;

            inventoryID = Player.agentInventory.InventoryID;

            // Player Status UI Init
             HUDManager.Initialize(Planet, Player);

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol, Planet.EntitasContext);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore, Planet.EntitasContext);

            // Admin API Add Items
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.SpawnEnemySlimeTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.MiningLaserTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.PipePlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.ParticleEmitterPlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, inventoryID, Enums.ItemType.ChestPlacementTool, Planet.EntitasContext);

            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(3.0f, 25.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PumpShotgun, new Vec2f(4.0f, 25.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PulseWeapon, new Vec2f(5.0f, 25.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SniperRifle, new Vec2f(6.0f, 25.0f));
            //GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Sword, new Vec2f(7.0f, 25.0f));

        }



        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var tileMap = ref Planet.TileMap;

            
            for (int j = 0; j < tileMap.MapSize.Y / 2; j++)
            {
                for(int i = 0; i < tileMap.MapSize.X; i++)
                {
                    tileMap.GetTile(i, j).FrontTileID = TileID.Moon;
                    tileMap.GetTile(i, j).BackTileID = TileID.Background;
                }
            }

            

            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID =  TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }

            tileMap.GetTile(8, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(9, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(10, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(11, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(12, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(13, 18).FrontTileID = TileID.Platform;

            tileMap.GetTile(12, 21).FrontTileID = TileID.Platform;
            tileMap.GetTile(13, 21).FrontTileID = TileID.Platform;
            tileMap.GetTile(14, 21).FrontTileID = TileID.Platform;

            tileMap.GetTile(14, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(15, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(16, 24).FrontTileID = TileID.Platform;


            tileMap.GetTile(19, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(20, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(21, 24).FrontTileID = TileID.Platform;



            tileMap.GetTile(26, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(29, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(32, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(36, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(40, 26).FrontTileID = TileID.Platform;


            tileMap.GetTile(16, 26).FrontTileID = TileID.Platform;


            tileMap.GetTile(12, 27).FrontTileID = TileID.Platform;

            tileMap.GetTile(8, 27).FrontTileID = TileID.Platform;
            tileMap.GetTile(7, 27).FrontTileID = TileID.Platform;


            for(int i = 0; i < 5; i++)
            {
                tileMap.GetTile(20, i + 16).FrontTileID = TileID.Moon;
            }

            for(int i = 0; i < 10; i++)
            {
                tileMap.GetTile(24, i + 16).FrontTileID = TileID.Moon;
            }


            tileMap.GetTile(26, 21).FrontTileID = TileID.Moon;
            tileMap.GetTile(27, 21).FrontTileID = TileID.Moon;
            tileMap.GetTile(26, 22).FrontTileID = TileID.Moon;
            tileMap.GetTile(27, 22).FrontTileID = TileID.Moon;
        }

    }
}
