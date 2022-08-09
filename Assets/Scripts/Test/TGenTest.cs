using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;
using TGen;

namespace Planet.Unity
{
    public class TGenTest : MonoBehaviour
    {

        [SerializeField]
        private Material Material;

        public Planet.PlanetState Planet;

        private static bool Init = false;

        [SerializeField]
        private bool intializeTGenGrid = true;

        [SerializeField]
        private bool enableTileGrid = true;

        [SerializeField]
        private bool drawMapBorder = true;

        [SerializeField]
        private bool foregroundToolEnabled = true;

        private ForegroundPlacementTool placementTool;

        AgentEntity player;

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
            ref var tileMap = ref Planet.TileMap;
            Material material = Material;

            Planet.Update(Time.deltaTime, Material, transform, player);
        }
        
        public void Initialize()
        {
            GameResources.Initialize();

            player = new AgentEntity();

            var entities = Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPosition2D));
            foreach (var entity in entities)
            {
                if (entity.isAgentPlayer)
                    player = entity;

            }

            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform, player);

            if (intializeTGenGrid)
                GameState.TGenGrid.InitStage1(mapSize);

            if (enableTileGrid)
                GameState.TGenRenderGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (drawMapBorder)
                GameState.TGenRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);

            if(foregroundToolEnabled)
            {
                placementTool = new ForegroundPlacementTool();
                placementTool.Initialize();
            }
        }

        private void OnGUI()
        {
            if(placementTool != null && foregroundToolEnabled)
                placementTool.DrawGrid();
        }

    }
}
