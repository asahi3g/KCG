//imports UnityEngine

using Enums.PlanetTileMap;
using KMath;

namespace Planet.Unity
{
    class TileVariantTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

        static bool Init = false;
        public PlanetState Planet;
        

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
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse1))
            {
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;
                UnityEngine.Debug.Log(x + " " + y);
                Planet.TileMap.RemoveFrontTile(x, y);                
            }

            GameState.TileMapRenderer.UpdateFrontLayerMesh(Planet.TileMap);
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            var mapSize = new Vec2i(16, 16);

            AgentEntity player = new AgentEntity();

            var entities = Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
            foreach (var entity in entities)
            {
                if (entity.isAgentPlayer)
                    player = entity;

            }

            Planet = new PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            ref var tileMap = ref Planet.TileMap;

            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for(int i = 0; i < tileMap.MapSize.X; i++)
                {
                    var frontTile = TileID.Air;

                    if (i >= mapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == mapSize.X / 2)
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
                        if (j % 3 == 0 && i == mapSize.X / 2 + 1)
                        {
                            frontTile = TileID.Glass;
                        }
                        else
                        {
                            frontTile = TileID.Moon;
                        }
                    }

                    if (j is > 1 and < 6 || (j > (8 + i)))
                    {
                       frontTile = TileID.Air;
                    }

                    Planet.TileMap.SetFrontTile(i,j, frontTile);
                }
            }
        }
    }
}
