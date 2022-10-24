using Enums.Tile;

namespace PlanetTileMap
{
    public static class ChunkVisualizer
    {
        // Display Chunk Visualizer
        public static void Draw(PlanetTileMap.TileMap tileMap, float xOffset, float yOffset)
        {
            if (tileMap == null)
                return;

            // Draw square to every tile
            for (int y = 0; y < tileMap.MapSize.Y; y++)
            {
                for (int x = 0; x < tileMap.MapSize.X; x++)
                {
                    // If chunk is empty/air make it black
                    if (tileMap.GetFrontTileID(x, y) == TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.black;
                    if (tileMap.GetFrontTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.green;
                    if (tileMap.GetBackTileID(x, y) != TileID.Air)
                        UnityEngine.Gizmos.color = UnityEngine.Color.cyan;
                    if (tileMap.GetMidTileID(x, y) != TileID.Air)
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