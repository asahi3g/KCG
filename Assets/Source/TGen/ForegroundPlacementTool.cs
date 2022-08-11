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

        private Color backgroundColor = Color.grey;

        private int isotypeCount = 41;

        private float cellSize = 32F;

        private float interval = 4F;

        private float w, h, xInterval, yInterval;

        private Image[] PlaceBlockButtons;

        public void Initialize(ref Planet.PlanetState Planet)
        {
            w = cellSize / Screen.width;

            h = cellSize / Screen.height;

            xInterval = interval / Screen.width;

            yInterval = interval / Screen.height;

            PlaceBlockButtons = new Image[GameResources.TGenIsotypeSprites.Length];

            for (int i = 0; i < GameResources.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = Planet.AddUIImage(((BlockTypeAndRotation)i).ToString(),
                    GameObject.Find("Canvas").transform, GameResources.TGenIsotypeSprites[i],
                    new Vec2f(0.5f + i, 0.5f + i), new Vec3f(1f, 1f, 1f), 32, 32).kGUIElementsImage.Image;
            }
        }

    }

}