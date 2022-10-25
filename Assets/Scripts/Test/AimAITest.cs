using UnityEngine;
using KMath;
using Enums.Tile;

namespace Planet.Unity
{
    public class AimAITest : MonoBehaviour
    {
        [SerializeField] Material Material;
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
            ref PlanetState planet = ref GameState.CurrentPlanet;
            planet.Update(Time.deltaTime, Material, transform);

            const float SPAWN_DELAY = 2.0f;
            if ((Time.realtimeSinceStartup - LastSpawn) > SPAWN_DELAY)
            {
                SpawnTarget();
                LastSpawn = Time.realtimeSinceStartup;
            }
        }

        public void Initialize()
        {
            GameResources.Initialize();

            Vec2i mapSize = new Vec2i(32, 32);
            GameState.CurrentPlanet = new PlanetState();
            ref PlanetState planet = ref GameState.CurrentPlanet;
            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);
            planet.AddAgent(new Vec2f(16.0f, 2.0f), Enums.AgentType.EnemyMarine);
            
            GenerateMap();
            LastSpawn = Time.realtimeSinceStartup;
        }

        private void GenerateMap()
        {
            ref PlanetState planet = ref GameState.CurrentPlanet;
            ref var tileMap = ref planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    if (j == 0)
                        tileMap.SetFrontTile(i, j, TileID.Moon);
                }
            }
        }

        private void SpawnTarget()
        {
            float x = Random.Range(1.0f, 31.0f);
            GameState.CurrentPlanet.AddAgent(new Vec2f(x, 2.0f), Enums.AgentType.Slime, 1);
        }
    }
}
