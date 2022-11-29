//imports UnityEngine

using KMath;
using TGen.DarkGreyBackground;

namespace Planet
{
    public class BackgroundPlacementTool
    {
        private bool enableTileGrid = true;

        private bool drawMapBorder = true;

        private TGen.DarkGreyBackground.BackgroundPlacementTool placementTool;

        public BackgroundPlacementTool(bool tileGrid, bool mapBorder)
        {
            enableTileGrid = tileGrid;
            drawMapBorder = mapBorder;
        }

        public void UpdateToolGrid()
        {
            placementTool.UpdateToolGrid();
        }

        public void Initialize(UnityEngine.Material Material, UnityEngine.Transform transform, int mapWidth,
            int mapHeight)
        {
            // Generating the map
            Vec2i mapSize = new Vec2i(mapWidth, mapHeight);

            GameState.Planet.InitializePlaceableBackground(Material, transform);

            GameState.BackgroundGrid.InitStage1(mapSize);

            if (enableTileGrid)
                GameState.BackgroundGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (drawMapBorder)
                GameState.BackgroundRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);

            placementTool = new TGen.DarkGreyBackground.BackgroundPlacementTool();
            placementTool.Initialize();
        }
    }
}