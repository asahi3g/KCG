//imports UnityEngine

using KMath;
using TGen;

namespace Planet
{
    public class BackgroundPlacementTool
    {
        private bool enableTileGrid = true;

        private bool drawMapBorder = true;

        private ForegroundPlacementTool placementTool;

        public BackgroundPlacementTool(bool tileGrid, bool mapBorder)
        {
            enableTileGrid = tileGrid;
            drawMapBorder = mapBorder;
        }

        public void UpdateToolGrid()
        {
            placementTool.UpdateToolGrid();
        }

        public void Initialize(UnityEngine.Material Material, UnityEngine.Transform transform)
        {
            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);

            GameState.Planet.InitializeTGen(Material, transform);

            GameState.TGenGrid.InitStage1(mapSize);

            if (enableTileGrid)
                GameState.TGenRenderGridOverlay.Initialize(Material, transform, mapSize.X, mapSize.Y, 30);

            if (drawMapBorder)
                GameState.TGenRenderMapBorder.Initialize(Material, transform, mapSize.X - 1, mapSize.Y - 1, 31);

            placementTool = new ForegroundPlacementTool();
            placementTool.Initialize();
        }
    }
}