using Enums;
using Enums.PlanetTileMap;
using KMath;
using Newtonsoft.Json.Linq;
using Planet;
using PlanetTileMap;

namespace AI.Movement
{
    // Make bots follow a path.
    public class PathFollowing
    {
        // Todo: Figure it out a better way of doing this check? How can we use geometry to find out when we need to jump?
        // Check if there if there is an obstacle in the given direction.
        // if true bot can walk in the given direction. If false bot will need ot jump to reach its goal.
        static public bool IsPathFree(Vec2i currentTile, int direction)
        {
            ref PlanetState planet = ref GameState.Planet;

            TileID currentTileIDx = planet.TileMap.GetFrontTileID(currentTile.X, currentTile.Y);
            TileID frontTileIDX = planet.TileMap.GetFrontTileID((currentTile.X + direction), currentTile.Y);
            ref TileProperty cuurentTileProperty = ref GameState.TileCreationApi.GetTileProperty(currentTileIDx);
            if (cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.TB_R0 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L1_R2 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L1_R7 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L2_R2 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.TB_R1 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L1_R3 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L1_R4 ||
                cuurentTileProperty.BlockShapeType == TileGeometryAndRotation.L2_R6)
            {
                frontTileIDX = planet.TileMap.GetFrontTileID((currentTile.X + direction), currentTile.Y + 1);
            }
            else
                frontTileIDX = planet.TileMap.GetFrontTileID((currentTile.X + direction), currentTile.Y);

            if (frontTileIDX == TileID.Air)
                return true;

            ref TileProperty property = ref GameState.TileCreationApi.GetTileProperty(frontTileIDX);
            if (direction == 1)
            {
                if (property.BlockShapeType == TileGeometryAndRotation.TB_R0 ||
                    property.BlockShapeType == TileGeometryAndRotation.L1_R2 ||
                    property.BlockShapeType == TileGeometryAndRotation.L1_R7 ||
                    property.BlockShapeType == TileGeometryAndRotation.L2_R2)
                {
                    return true;
                }
            }
            if (direction == -1)
            {
                if (property.BlockShapeType == TileGeometryAndRotation.TB_R1 ||
                    property.BlockShapeType == TileGeometryAndRotation.L1_R3 ||
                    property.BlockShapeType == TileGeometryAndRotation.L1_R4 ||
                    property.BlockShapeType == TileGeometryAndRotation.L2_R6)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
