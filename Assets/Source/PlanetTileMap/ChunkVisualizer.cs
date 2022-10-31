using Enums.PlanetTileMap;

namespace PlanetTileMap
{
    public static class ChunkVisualizer
    {
        // Display Chunk Visualizer
        public static void Draw(float xOffset, float yOffset)
        {
            ref var planet = ref GameState.Planet;
            if (planet.TileMap == null)
                return;

            // Draw square to every tile
            for (int y = 0; y < planet.TileMap.MapSize.Y; y++)
            {
                for (int x = 0; x < planet.TileMap.MapSize.X; x++)
                {
                    // If chunk is empty/air make it black
                    if (planet.TileMap.GetFrontTileID(x, y) == TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.black;
                    if (planet.TileMap.GetFrontTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.green;
                    if (planet.TileMap.GetBackTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.cyan;
                    if (planet.TileMap.GetMidTileID(x, y) != TileID.Air)
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