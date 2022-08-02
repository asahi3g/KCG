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

        [SerializeField] Material Material;

        public Planet.PlanetState Planet;

        static bool Init = false;

        static bool IntializeTGenGrid = true;

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

        private void OnRenderObject()
        {
            //CHANGE TO DRAW MAP
            GameState.InventoryDrawSystem.Draw(Planet.EntitasContext, transform);
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);
            Planet.InitializeSystems(Material, transform);

            if(IntializeTGenGrid)
            {
                GameState.TGenGrid.InitStage1(mapSize);
            }
        }
    }
}
