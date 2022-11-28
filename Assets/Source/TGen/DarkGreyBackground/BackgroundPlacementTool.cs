//imports UnityEngine

using KMath;
using System;
using Utility;

namespace TGen.DarkGreyBackground
{
    public class BackgroundPlacementTool
    {
        private int initialX, initialY;

        private int cellSize = 32;

        private float interval = 2F;

        private ImageWrapper[] PlaceBlockButtons;

        private int selectedTileIsotype;

        public void Initialize()
        {
            var xInterval = cellSize * interval;

            var yInterval = -cellSize * interval;

            var size = 0.6F;

            initialX = 600;

            initialY = 300;

            PlaceBlockButtons = new ImageWrapper[GameState.BackgroundGridOverlay.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameState.BackgroundGridOverlay.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = new ImageWrapper(((BlockTypeAndRotation) i).ToString(),
                    UnityEngine.GameObject.Find("Canvas").transform, cellSize, cellSize,
                    GameState.BackgroundGridOverlay.TGenIsotypeSprites[i]);
                
                PlaceBlockButtons[i].SetPosition(new UnityEngine.Vector3(initialX + column * xInterval, initialY + row * yInterval));
                PlaceBlockButtons[i].SetScale(new UnityEngine.Vector3(size, -size, size));

                column++;

                if (column == 4)
                {
                    column = 0;
                    row++;
                }
            }
        }

        public void UpdateToolGrid(PlanetTileMap.TileMap tileMap)
        {
            var item = GameState.Planet.Player.GetItem();
            if(item != null)
            {
                if (item.itemType.Type == Enums.ItemType.PlacebleBackgroundTool)
                {
                    if (UnityEngine.Input.GetMouseButtonUp(0))
                    {
                        var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                        int x = (int)worldPosition.X;
                        int y = (int)worldPosition.Y;

                        tileMap.SetBackTile(x, y, Enums.PlanetTileMap.TileID.Background);
                    }
                    else if (UnityEngine.Input.GetMouseButtonDown(1))
                    {
                        var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                        int x = (int)worldPosition.X;
                        int y = (int)worldPosition.Y;

                        tileMap.RemoveBackTile(x, y);
                    }
                }
            }
        }
    }
}