//imports UnityEngine

using KMath;

namespace Planet.Unity
{
    public class AimAITest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        bool Init = false;
        float LastSpawn = 0;

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

            const float SPAWN_DELAY = 2.0f;
            if ((UnityEngine.Time.realtimeSinceStartup - LastSpawn) > SPAWN_DELAY)
            {
                SpawnTarget();
                LastSpawn = UnityEngine.Time.realtimeSinceStartup;
            }
        }

        public void Initialize()
        {
            GameResources.Initialize();

            Vec2i mapSize = new Vec2i(32, 16);
            ref var planet = ref GameState.Planet;

            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);
            planet.AddAgent(new Vec2f(16.0f, 2.0f), Enums.AgentType.EnemyMarine);
            
            GenerateMap();
            LastSpawn = UnityEngine.Time.realtimeSinceStartup;
        }

        private void GenerateMap()
        {
            ref var planet = ref GameState.Planet;
            ref var tileMap = ref planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    if (j == 0)
                        tileMap.SetFrontTile(i, j, Enums.PlanetTileMap.TileID.Moon);
                }
            }
        }

        private void SpawnTarget()
        {
            float x = UnityEngine.Random.Range(1.0f, 31.0f);
            GameState.Planet.AddAgent(new Vec2f(x, 2.0f), Enums.AgentType.Slime);
        }
    }
}
