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
    public class MechTest : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                PlaceMech();
            }
        }

        private void PlaceMech()
        {
            Debug.Log("PLACE MECH");

            var planet = FindObjectOfType<MovementSceneScript>().Planet;

            planet.AddMech(new Vec2f(15F, 15F), MechType.Storage);
        }
    } 
}
