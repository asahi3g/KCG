using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Mech;
using System.Linq;

namespace TGen
{
    public class ForegroundPlacementTool
    {

        private Color backgroundColor = Color.grey;

        private float cellSize = 32F;

        private float interval = 4F;

        private float w, h, xInterval, yInterval;

        public void Initialize()
        {
            w = cellSize / Screen.width;

            h = cellSize / Screen.height;

            xInterval = interval / Screen.width;

            yInterval = interval / Screen.height;
        }

        public void DrawGrid()
        {
            var rowIndex = 0;

            //Tile Number
            for (int i = 0; i < 4; i++)
            {
                var currentX = 0.5F + (w + xInterval) * i;

                var currentY = 0.8F + (h + yInterval) * rowIndex;

                GameState.Renderer.DrawQuadColorGui(currentX, currentY, w, h, backgroundColor);

                GameState.Renderer.DrawStringGui(currentX, currentY, w, h, (i + 1).ToString(), 24, TextAnchor.UpperCenter, Color.white);
            }

            rowIndex--;

            //SQUARE
            Sprites.Sprite sprite = GameState.TileSpriteAtlasManager.GetSprite(GameResources.TGenBlock_0);

            GameState.Renderer.DrawSpriteGui(0.5F, 0.8F + (h + yInterval) * rowIndex, w, h, sprite);
            //GameState.Renderer.DrawQuadColorGui(0.5F, 0.8F + (h + yInterval) * rowIndex, w, h, backgroundColor);

            //EMPTY
            GameState.Renderer.DrawQuadColorGui(0.5F + w + xInterval, 0.8F + (h + yInterval) * rowIndex, w, h, backgroundColor);

            rowIndex--;

            //HALF
            for (int i = 0; i < 4; i++)
            {
                var blockTypeAndRotation = (BlockTypeAndRotation)(i + 3);

                var currentX = 0.5F + (w + xInterval) * i;

                var currentY = 0.8F + (h + yInterval) * rowIndex;

                GameState.Renderer.DrawQuadColorGui(currentX, currentY, w, h, backgroundColor);

                GameState.Renderer.DrawStringGui(currentX, currentY, w, h, (blockTypeAndRotation).ToString(), 24, TextAnchor.UpperCenter, Color.white);
            }

            rowIndex--;

            //TRIANGLE 1
            for (int i = 0; i < 4; i++)
            {
                var blockTypeAndRotation = (BlockTypeAndRotation)(i + 7);

                var currentX = 0.5F + (w + xInterval) * i;

                var currentY = 0.8F + (h + yInterval) * rowIndex;

                GameState.Renderer.DrawQuadColorGui(currentX, currentY, w, h, backgroundColor);

                GameState.Renderer.DrawStringGui(currentX, currentY, w, h, (blockTypeAndRotation).ToString(), 24, TextAnchor.UpperCenter, Color.white);
            }
        }

    }

}