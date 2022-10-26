//import UnityEngine

using Enums.PlanetTileMap;
using KMath;

namespace Planet.Unity
{
    class ItemTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material   Material;
        
        AgentEntity                     Player;

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
            GameState.Planet.Update(UnityEngine.Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            GameState.Planet.DrawHUD(Player);
        }

        private void OnDrawGizmos()
        {
            GameState.Planet.DrawDebug();
        }

        // Create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 64);

            GameState.Planet.Init(mapSize);
            GameState.Planet.InitializeSystems(Material, transform);
            Player = GameState.Planet.AddPlayer(new Vec2f(6.0f, 4.0f));
            GameState.Planet.InitializeHUD();

            int inventoryID = Player.agentInventory.InventoryID;

            GenerateMap();

            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.PulseWeapon, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.SniperRifle, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.Shotgun, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.LongRifle, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.RPG, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.SMG, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.GrenadeLauncher, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.Sword, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.SpawnEnemySlimeTool);
            var SpawnPistol = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.Pistol);
            var SpawnPumpShotgun = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.Shotgun);
            var SpawnWaterBottle = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.WaterBottle);
            var SpawnPlanterTool = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.MajestyPalm);
            var SpawnHarvestTool = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.HarvestTool);
            var SpawnScannerTool = GameState.ItemSpawnSystem.SpawnInventoryItem(GameState.Planet.EntitasContext, Enums.ItemType.ScannerTool);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnEnemyTool, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnPistol, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnPumpShotgun, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnWaterBottle, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnPlanterTool, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnHarvestTool, inventoryID);
            GameState.InventoryManager.AddItem(GameState.Planet.EntitasContext, SpawnScannerTool, inventoryID);
        }

        void GenerateMap()
        {
            ref var tileMap = ref GameState.Planet.TileMap;

             for (int i = 0; i < tileMap.MapSize.X; i++)
             {
                 TileID frontTile = TileID.Moon;
                 tileMap.SetFrontTile(i, 0, frontTile);
            }
        }
    }
}
