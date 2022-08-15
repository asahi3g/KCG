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

        private float interval = 2F;

        private Image[] PlaceBlockButtons;

        private int selectedTileIsotype;

        public void Initialize(ref Planet.PlanetState Planet)
        {
            var xInterval = cellSize * interval;

            var yInterval = -cellSize * interval;

            var size = 0.6F;

            initialX = 600;

            initialY = 300;

            PlaceBlockButtons = new Image[GameResources.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameResources.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = Planet.AddUIImage(((BlockTypeAndRotation)i).ToString(),
                    GameObject.Find("Canvas").transform, GameResources.TGenIsotypeSprites[i],
                    new Vec2f(initialX + column * xInterval, initialY + row * yInterval), new Vec3f(size, -size, size), cellSize, cellSize).kGUIElementsImage.Image;

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
                        selectedTileIsotype = i + 1;

                        var blockIsotype = (BlockTypeAndRotation)(selectedTileIsotype);

                        Debug.Log(string.Format("Select {0}", blockIsotype.ToString()));
                    }
                }
            }
        }

    }

}