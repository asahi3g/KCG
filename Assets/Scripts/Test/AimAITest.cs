using UnityEngine;
using KMath;
using Enums.Tile;

namespace Planet.Unity
{
    public class AimAITest : MonoBehaviour
    {
        [SerializeField] Material Material;
        public Planet.PlanetState Planet;
        AgentEntity Marine;
        AgentEntity Target;
        static bool Init = false;
        const float SHOOT_COOL_DOWN = 1f;
        float lastShootTime;

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
            float timeSinceStart = Time.realtimeSinceStartup;
            
            int inventoryID = Marine.agentInventory.InventoryID;
            int selectedSlot = Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity.SelectedSlotID;
            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, inventoryID, selectedSlot);

            if ((timeSinceStart - lastShootTime) >= SHOOT_COOL_DOWN)
            {
                GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, Enums.NodeType.ShootFireWeaponAction, Marine.agentID.ID, item.itemID.ID);
                lastShootTime = Time.realtimeSinceStartup;
            }
            
            Planet.Update(Time.deltaTime, Material, transform);

            if (!Target.isAgentAlive)
            {
                Target = SpawnTarget();
            }
        }

        public void Initialize()
        {
            GameResources.Initialize();

            Vec2i mapSize = new Vec2i(32, 16);
            Planet = new PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);
            Marine = Planet.AddAgent(new Vec2f(1.0f, 3.0f), Enums.AgentType.EnemyGunner);
            Target = SpawnTarget();
            GenerateMap();
            lastShootTime = Time.realtimeSinceStartup;
        }

        private void GenerateMap()
        {
            ref var tileMap = ref Planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    if (j == 0)
                        tileMap.SetFrontTile(i, j, TileID.Moon);
                }
            }
        }

        private AgentEntity SpawnTarget()
        {
            float x = Random.Range(16.0f, 31.0f);
            float y = Random.Range(2.0f, 15.0f);

            return Planet.AddAgent(new Vec2f(x, y), Enums.AgentType.FlyingSlime);
        }
    }
}
