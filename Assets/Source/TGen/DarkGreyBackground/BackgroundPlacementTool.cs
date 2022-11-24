//imports UnityEngine

using KMath;
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

            PlaceBlockButtons = new ImageWrapper[GameState.TGenRenderGridOverlay.TGenIsotypeSprites.Length];

            var row = 0;
            var column = 0;

            for (int i = 0; i < GameState.TGenRenderGridOverlay.TGenIsotypeSprites.Length; i++)
            {
                PlaceBlockButtons[i] = new ImageWrapper(((BlockTypeAndRotation) i).ToString(),
                    UnityEngine.GameObject.Find("Canvas").transform, cellSize, cellSize,
                    GameState.TGenRenderGridOverlay.TGenIsotypeSprites[i]);
                
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

        public void UpdateToolGrid()
        {
            if(UnityEngine.Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < PlaceBlockButtons.Length; i++)
                {
                    if(PlaceBlockButtons[i].IsMouseOver(new Vec2f(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y)))
                    {
                        selectedTileIsotype = i + 1;

                        var blockIsotype = (BlockTypeAndRotation)(selectedTileIsotype);



                        UnityEngine.Debug.Log(string.Format("Select {0}", blockIsotype.ToString()));
                    }
                }
            }
            else if(UnityEngine.Input.GetMouseButtonUp(2))
            {
                var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                int x = (int)worldPosition.X;
                int y = (int)worldPosition.Y;

                GameState.TGenGrid.SetTile(x, y, selectedTileIsotype);
            }
        }

    }

}