using UnityEngine;
using KMath;
using KGUI.Elements;
using Utility;

namespace TGen
{
    public class ForegroundPlacementTool
    {

        private int initialX, initialY;

        private int cellSize = 32;

        private float interval = 2F;

        private ImageWrapper[] PlaceBlockButtons;

        private int selectedTileIsotype;

        public void Initialize(ref Planet.PlanetState Planet)
        {
            var xInterval = cellSize * interval;

            var yInterval = -cellSize * interval;

            var size = 0.6F;

            initialX = 600;

            initialY = 300;

            PlaceBlockButtons = new ImageWrapper[GameState.TGenRenderGridOverlay.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameState.TGenRenderGridOverlay.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = new ImageWrapper(((BlockTypeAndRotation) i).ToString(),
                    GameObject.Find("Canvas").transform, cellSize, cellSize,
                    GameState.TGenRenderGridOverlay.TGenIsotypeSprites[i]);
                
                PlaceBlockButtons[i].SetPosition(new Vector3(initialX + column * xInterval, initialY + row * yInterval));
                PlaceBlockButtons[i].SetScale(new Vector3(size, -size, size));

                column++;

                if (column == 4)
                {
                    column = 0;
                    row++;
                }
            }
        }

        public void UpdateToolGrid()
        {
            if(Input.GetMouseButtonUp(0))
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
            else if(Input.GetMouseButtonUp(2))
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                GameState.TGenGrid.SetTile(x, y, selectedTileIsotype);
            }
        }

    }

}