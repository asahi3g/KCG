using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;

namespace Planet.Unity
{
    public class TGenTest : MonoBehaviour
    {

        [SerializeField]
        private Material Material;

        public Planet.PlanetState Planet;

        private static bool Init = false;

        [SerializeField]
        private bool IntializeTGenGrid = true;

        [SerializeField]
        private bool EnableTileGrid = true;

        [SerializeField]
        private bool DrawMapBorder = true;

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

            Planet.Update(Time.deltaTime, Material, transform);
        }


        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            if (IntializeTGenGrid)
                GameState.TGenGrid.InitStage1(mapSize);

            if (EnableTileGrid)
                GameState.TGenRenderGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (DrawMapBorder)
                GameState.TGenRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);
            
        }
    }
}
