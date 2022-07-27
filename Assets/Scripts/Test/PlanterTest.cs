using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;

namespace Planet.Unity
{
    public class PlanterTest : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                PlacePlanter();
            }
        }

        private void PlacePlanter()
        {
            var planet = FindObjectOfType<ItemTest>().Planet;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            planet.AddMech(new Vec2f(x, y), MechType.Planter);
        }
    }
}
