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

        private float initialX = 0.5F, initialY = 0.8F;

        private int cellSize = 32;

        private float interval = 4F;

        private Image[] PlaceBlockButtons;

        public void Initialize(ref Planet.PlanetState Planet)
        {
            var xInterval = interval / Screen.width;

            var yInterval = -interval / Screen.height;

            PlaceBlockButtons = new Image[GameResources.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameResources.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = Planet.AddUIImage(((BlockTypeAndRotation)i).ToString(),
                    GameObject.Find("Canvas").transform, GameResources.TGenIsotypeSprites[i],
                    new Vec2f(initialX + column * xInterval, initialY + row * yInterval), new Vec3f(1f, 1f, 1f), cellSize, cellSize).kGUIElementsImage.Image;

                column++;

                if(column == 4)
                {
                    column = 0;
                    row++;
                }
            }
        }

    }

}