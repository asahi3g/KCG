using Enums.PlanetTileMap;

namespace PlanetTileMap
{
    public static class ChunkVisualizer
    {
        // Display Chunk Visualizer
        public static void Draw(float xOffset, float yOffset)
        {
            if (GameState.Planet.TileMap == null)
                return;

            // Draw square to every tile
            for (int y = 0; y < GameState.Planet.TileMap.MapSize.Y; y++)
            {
                for (int x = 0; x < GameState.Planet.TileMap.MapSize.X; x++)
                {
                    // If chunk is empty/air make it black
                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) == TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.black;
                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.green;
                    if (GameState.Planet.TileMap.GetBackTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.cyan;
                    if (GameState.Planet.TileMap.GetMidTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.yellow;

                    if (!Utility.ObjectMesh.isOnScreen(x, y))
                        UnityEngine.Gizmos.color = UnityEngine.Color.blue;

                    // Draw colored cubes to the editor display (Debug)
                    UnityEngine.Gizmos.DrawCube(new UnityEngine.Vector3(x + xOffset, y + yOffset), new UnityEngine.Vector3(1, 1));
                }
            }
        }
    }
}