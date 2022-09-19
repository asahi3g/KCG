using System;
using UnityEngine;
using KMath;
using Enums.Tile;
using static UnityEngine.GraphicsBuffer;

namespace Planet.Unity
{
    public class AimAITest : MonoBehaviour
    {
        [SerializeField] Material Material;
        public Planet.PlanetState Planet;
        AgentEntity Marine;
        AgentEntity Target;
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

        public void Initialize()
        {
            GameResources.Initialize();

            Vec2i mapSize = new Vec2i(32, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            Marine = Planet.AddAgent(new Vec2f(1.0f, 3.0f), Enums.AgentType.EnemyGunner);
            float x = UnityEngine.Random.Range(16.0f, 31.0f);
            float y = UnityEngine.Random.Range(2.0f, 15.0f);

            Target = Planet.AddAgent(new Vec2f(x, y), Enums.AgentType.FlyingSlime);
            GenerateMap();
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
    }
}
