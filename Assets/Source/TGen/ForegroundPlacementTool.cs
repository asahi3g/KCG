//imports UnityEngine

using KMath;
using KGUI.Elements;

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
                PlaceBlockButtons[i] = Planet.AddUIImage(((BlockTypeAndRotation)i).ToString(),
                    UnityEngine.GameObject.Find("Canvas").transform, GameState.TGenRenderGridOverlay.TGenIsotypeSprites[i],
                    new Vec2f(initialX + column * xInterval, initialY + row * yInterval), new Vec3f(size, -size, size), cellSize, cellSize).kGUIElementsImage.ImageWrapper;

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
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                GameState.TGenGrid.SetTile(x, y, selectedTileIsotype);
            }
        }

    }

}