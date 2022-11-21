//imports UnityEngine

using KMath;
using System;

namespace Planet.Unity
{
    public class AimAITest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        bool Init = false;

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
            GameState.Planet.Update(UnityEngine.Time.deltaTime);

            if (GetNumOfEnemiesAlive() < 2)
            {
                SpawnTarget();
            }
        }

        public void Initialize()
        {
            GameResources.Initialize();

            Vec2i mapSize = new Vec2i(128, 16);
            ref var planet = ref GameState.Planet;

            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);
            planet.AddAgent(new Vec2f(64.0f, 5.0f), Enums.AgentType.EnemyMarine);
            
            GenerateMap();
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
                    {
                        tileMap.SetFrontTile(i, j, Enums.PlanetTileMap.TileID.Moon);
                        continue;
                    }
                    if (i - tileMap.MapSize.X / 2f == 0)
                    {
                        if (j == 1 || j == 2 || j== 3)
                            tileMap.SetFrontTile(i, j, Enums.PlanetTileMap.TileID.Moon);
                        continue;
                    }
                    if (Math.Abs(i - tileMap.MapSize.X / 2f) <= 1)
                    {
                        if (j == 1)
                            tileMap.SetFrontTile(i, j, Enums.PlanetTileMap.TileID.Moon);
                    }
                }
            }
        }

        private void SpawnTarget()
        {
            float x = UnityEngine.Random.Range(1.0f, 127.0f);
            GameState.Planet.AddAgent(new Vec2f(x, 5.0f), Enums.AgentType.Slime, 1);
        }

        public int GetNumOfEnemiesAlive()
        {
            ref var planet = ref GameState.Planet;
            int numOfEnemies = 0;
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agent = planet.AgentList.Get(i);
                if (!agent.isAgentAlive)
                    continue;

                if (agent.isAgentPlayer)
                    continue;

                numOfEnemies++;
            }
            return numOfEnemies;
        }
    }
}
