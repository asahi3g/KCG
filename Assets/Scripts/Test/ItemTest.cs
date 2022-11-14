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
            ref var planet = ref GameState.Planet;
            Vec2i mapSize = new Vec2i(16, 64);

            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);
            Player = planet.AddPlayer(new Vec2f(6.0f, 4.0f));

            int inventoryID = Player.agentInventory.InventoryID;

            GenerateMap();

            planet.AddItemParticle(Enums.ItemType.PulseWeapon, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(Enums.ItemType.SniperRifle, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(Enums.ItemType.Shotgun, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.LongRifle, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.RPG, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.SMG, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.GrenadeLauncher, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.Sword, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            planet.AddItemParticle(Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            planet.AddItemParticle(Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.SpawnEnemySlimeTool);
            var SpawnPistol = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Pistol);
            var SpawnPumpShotgun = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Shotgun);
            var SpawnWaterBottle = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.WaterBottle);
            var SpawnPlanterTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.MajestyPalm);
            var SpawnHarvestTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.HarvestTool);
            var SpawnScannerTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Enums.ItemType.ScannerTool);
            GameState.InventoryManager.AddItem(SpawnEnemyTool, inventoryID);
            GameState.InventoryManager.AddItem(SpawnPistol, inventoryID);
            GameState.InventoryManager.AddItem(SpawnPumpShotgun, inventoryID);
            GameState.InventoryManager.AddItem(SpawnWaterBottle, inventoryID);
            GameState.InventoryManager.AddItem(SpawnPlanterTool, inventoryID);
            GameState.InventoryManager.AddItem(SpawnHarvestTool, inventoryID);
            GameState.InventoryManager.AddItem(SpawnScannerTool, inventoryID);
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
