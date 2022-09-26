using UnityEngine;
using Enums.Tile;
using KMath;
using Inventory;
using Item;

namespace Planet.Unity
{
    class ItemTest : MonoBehaviour
    {
        [SerializeField] Material   Material;

        public Planet.PlanetState       Planet;
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
            Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            Planet.DrawHUD(Player);
        }

        private void OnDrawGizmos()
        {
            Planet.DrawDebug();
        }

        // Create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            Player = Planet.AddPlayer(GameState.AnimationManager.CharacterSpriteId, 32, 48, new Vec2f(6.0f, 4.0f), 0, 100, 100, 100, 100, 100);
            int inventoryID = Player.agentInventory.InventoryID;

            GenerateMap();

            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.PulseWeapon, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SniperRifle, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Shotgun, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.LongRifle, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.RPG, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.SMG, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.GrenadeLauncher, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.GasBomb, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Sword, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.RiotShield, new Vec2f(2.0f, 4.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.StunBaton, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.AutoCannon, new Vec2f(3.0f, 3.0f));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Bow, new Vec2f(3.0f, 3.0f));

            var SpawnEnemyTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.SpawnEnemySlimeTool);
            var SpawnPistol = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.Pistol);
            //var SpawnPumpShotgun = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.Shotgun);
            //var SpawnWaterBottle = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.WaterBottle);
            //var SpawnPlanterTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.MajestyPalm);
            //var SpawnHarvestTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.HarvestTool);
            //var SpawnScannerTool = GameState.ItemSpawnSystem.SpawnInventoryItem(Planet.EntitasContext, Enums.ItemType.ScannerTool);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnEnemyTool, inventoryID);
            GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnPistol, inventoryID);
            //GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnPumpShotgun, inventoryID);
            //GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnWaterBottle, inventoryID);
            //GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnPlanterTool, inventoryID);
            //GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnHarvestTool, inventoryID);
            //GameState.InventoryManager.AddItem(Planet.EntitasContext, SpawnScannerTool, inventoryID);
        }

        void GenerateMap()
        {
            ref var tileMap = ref Planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    TileID frontTile;

                    if (i >= tileMap.MapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                        {
                            frontTile = TileID.Moon;
                        }
                        else
                        {
                            frontTile = TileID.Glass;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                        {
                            frontTile = TileID.Glass;
                        }
                        else
                        {
                            frontTile = TileID.Moon;
                        }
                    }

                    if (j is > 1 and < 6 || (j > 8 + i))
                    {
                        frontTile = TileID.Air;
                    }


                    tileMap.SetFrontTile(i, j, frontTile);
                }
            }
        }
    }
}
