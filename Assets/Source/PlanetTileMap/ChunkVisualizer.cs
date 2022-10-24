using Enums.Tile;
using UnityEngine;

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
                        Gizmos.color = Color.black;
                    if (tileMap.GetFrontTileID(x, y) != TileID.Air)
                        Gizmos.color = Color.green;
                    if (tileMap.GetBackTileID(x, y) != TileID.Air)
                        Gizmos.color = Color.cyan;
                    if (tileMap.GetMidTileID(x, y) != TileID.Air)
                        Gizmos.color = Color.yellow;

                    if (!Utility.ObjectMesh.isOnScreen(x, y))
                        Gizmos.color = Color.blue;

                    // Draw colored cubes to the editor display (Debug)
                    Gizmos.DrawCube(new Vector3(x + xOffset, y + yOffset), new Vector3(1, 1));
                }
            }
        }
    }
}