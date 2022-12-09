﻿//imports UnityEngine

using Enums.Tile;
using Enums.PlanetTileMap;
using Item;
using KMath;

namespace Planet.Unity
{
    class PathFindingTest : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;
        
        AgentEntity Slime;
        AgentEntity FlyingSlime;
        AgentEntity Agent;

        AgentEntity SelectedAgent;

        static bool Init = false;

        byte[,] map;

        public void Start()
        {
            UnityEngine.Debug.Log("Click somewhere to set slime target gol.");
            UnityEngine.Debug.Log("Click 1 to select slime, 2 to select flying slime, and 3 to select agent");
            if (!Init)
            {
                Initialize();
                GameState.Renderer.Initialize(Material);
                Init = true;
            }
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha1))
            {
                UnityEngine.Debug.Log("Slime selected.");
                SelectedAgent = Slime;
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha2))
            {
                UnityEngine.Debug.Log("Flying Slime selected.");
                SelectedAgent = FlyingSlime;
            }
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha3))
            {
                UnityEngine.Debug.Log("Agent selected.");
                SelectedAgent = Agent;
            }


            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
            {
                var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
                Vec2f goalPos = new Vec2f(worldPosition.X, worldPosition.Y);
                GameState.ActionCreationSystem.CreateMovementAction(Enums.ActionType.MoveToAction,
                   SelectedAgent.agentID.ID, goalPos);
            }
            
            GameState.Planet.Update(UnityEngine.Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            GameState.PathFindingDebugSystem.Draw();
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Set map path.
            // 0 -> air
            // 1 -> solid
            // 2 -> one way plataform

            map = new byte[32, 32]
                {
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 2, 2, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1 ,1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 2, 2, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 1 ,1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 1 ,1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2 ,2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1 ,1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };

            // Generating the map
            var planet = GameState.Planet;
            Vec2i mapSize = new Vec2i(32, 32);
            planet.Init(mapSize);
            planet.InitializeSystems(Material, transform);

            Slime = planet.AddAgent(new Vec2f(0f, 22f), Enums.AgentType.Slime);
            FlyingSlime = planet.AddAgent(new Vec2f(0f, 18f), Enums.AgentType.FlyingSlime);
            Agent = planet.AddAgent(new Vec2f(1, 22f), Enums.AgentType.Agent);

            SelectedAgent = Slime;

            GenerateMap();
        }

        void GenerateMap()
        {
            var planet = GameState.Planet;
            ref var tileMap = ref planet.TileMap;

            for (int j = tileMap.MapSize.Y - 1; j >= 0; j--)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    TileID frontTile;

                    if (map[j, i] == 1)
                    {
                        frontTile = TileID.Moon;
                    }
                    else if (map[j, i] == 2)
                    {
                        frontTile = TileID.Platform;
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
