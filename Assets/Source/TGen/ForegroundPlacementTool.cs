using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;
using KGUI.Elements;

namespace TGen
{
    public class ForegroundPlacementTool
    {

        private int initialX, initialY;

        private int cellSize = 32;

        private float interval = 4F;

        private Image[] PlaceBlockButtons;

        public void Initialize(ref Planet.PlanetState Planet)
        {
            var xInterval = cellSize * interval;

            var yInterval = -cellSize * interval;

            initialX = 0;

            initialY = 1;

            PlaceBlockButtons = new Image[GameResources.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameResources.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = Planet.AddUIImage(((BlockTypeAndRotation)i).ToString(),
                    GameObject.Find("Canvas").transform, GameResources.TGenIsotypeSprites[i],
                    new Vec2f(initialX + column * xInterval, initialY + row * yInterval), new Vec3f(1f, -1f, 1f), cellSize, cellSize).kGUIElementsImage.Image;

                Debug.Log("column: " + column.ToString());

                Debug.Log("row: " + row.ToString());

                column++;

                if(column == 4)
                {
                    column = 0;
                    row++;
                }
            }
        }

        public void UpdateToolGrid()
        {
            if(Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < PlaceBlockButtons.Length; i++)
                {
                    if(PlaceBlockButtons[i].IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        Debug.Log(string.Format("Place {0}", ((BlockTypeAndRotation)i).ToString()));
                    }
                }
            }
        }

    }

}