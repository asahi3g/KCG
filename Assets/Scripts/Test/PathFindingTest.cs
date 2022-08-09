using Enums.Tile;
using Item;
using KMath;
using UnityEngine;
using PlanetTileMap;

namespace Planet.Unity
{
    class PathFindingTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        Planet.PlanetState Planet;
        AgentEntity Agent;

        static bool Init = false;

        byte[,] map = new byte[12, 12];

        public void Start()
        {
            Debug.Log("Click somewhere to set slime target gol.");
            if (!Init)
            {
                Initialize();
                GameState.Renderer.Initialize(Material);
                Init = true;
            }
        }

        public void Update()
        {  
         if (Input.GetKeyDown(KeyCode.Mouse0))
         {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vec2f goalPos = new Vec2f(worldPosition.x, worldPosition.y);
                GameState.ActionCreationSystem.CreateMovementAction(Planet.EntitasContext, Enums.ActionType.MoveAction,
                   Agent.agentID.ID, goalPos);
            }

            Planet.Update(Time.deltaTime, Material, transform);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            GameState.InventoryDrawSystem.Draw(Planet.EntitasContext);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Set map path.

            map = new byte[16, 16]
                {
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0},
                { 0 ,0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };

            // Generating the map
            Vec2i mapSize = new Vec2i(16, 16);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            GenerateMap();
        }

        void GenerateMap()
        {
            ref var tileMap = ref Planet.TileMap;

            for (int j = tileMap.MapSize.Y - 1; j >= 0; j--)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    TileID frontTile;

                    if (map[j,i] == 1)
                    {
                       frontTile = TileID.Moon;
                    }
                    else
                    {
                        frontTile = TileID.Air;
                    }

                    tileMap.SetFrontTile(i, tileMap.MapSize.Y - 1 - j, frontTile);
                }
            }
        }
    }
}
