//imports UnityEngine

using Enums.PlanetTileMap;
using KMath;

namespace Planet.Unity
{
    class TileVariantTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;

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
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse1))
            {
                var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
                int x = (int)worldPosition.X;
                int y = (int)worldPosition.Y;
                UnityEngine.Debug.Log(x + " " + y);
                GameState.Planet.TileMap.RemoveFrontTile(x, y);                
            }

            GameState.TileMapRenderer.UpdateFrontLayerMesh();
            GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            var planet = GameState.Planet;
            var mapSize = new Vec2i(16, 16);
            AgentEntity player = new AgentEntity();

            var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
            foreach (var entity in entities)
            {
                if (entity.isAgentPlayer)
                    player = entity;

            }


            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);

            ref var tileMap = ref planet.TileMap;

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

                    planet.TileMap.SetFrontTile(i,j, frontTile);
                }
            }
        }
    }
}
