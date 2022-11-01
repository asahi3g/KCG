using System;
using Enums.PlanetTileMap;
using KMath;
using Utility;

namespace CollisionsTest
{
    public static class AABBTileMapStaticCollision
    {
        public static bool IsCollidingLeft(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.X >= 0.0f) return false;
            
            int x = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;

            if (x < 0 || x >= tileMap.MapSize.X) return false;
            
            for (int y = (int)borders.ymin; y <= (int)borders.ymax; y++)
            {
                if (y >= 0 && y < tileMap.MapSize.Y)
                {
                    var frontTileID = tileMap.GetFrontTileID(x, y);
                    if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                    {
                        var tileBorders = new AABox2D(x, y);
                        tileBorders.DrawBox();
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsCollidingRight(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.X <= 0.0f) return false;
            
            int x = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;

            if (x < 0 || x >= tileMap.MapSize.X) return false;
            
            for (int y = (int)borders.ymin; y <= (int)borders.ymax; y++)
            {
                if (y < 0 || y >= tileMap.MapSize.Y) continue;
                
                var frontTileID = tileMap.GetFrontTileID(x, y);
                if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                {
                    var tileBorders = new AABox2D(x, y);
                    tileBorders.DrawBox();
                    return true;
                }
            }

            return false;
        }
        
        public static bool IsCollidingBottom(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.Y >= 0.0f) return false;
            
            // LeftBottom.X >= 0f ? (int)LeftBottom.X : (int)LeftBottom.X - 1;
            
            int y = (int)borders.ymin;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;

            if (y < 0 || y >= tileMap.MapSize.Y) return false;
            
            for (int x = leftX; x <= rightX; x++)
            {
                if (x < 0 || x >= tileMap.MapSize.X) continue;
                
                var frontTileID = tileMap.GetFrontTileID(x, y);
                if (frontTileID != TileID.Air )
                {
                            
                    var tileBorders = new AABox2D(x, y);
                    if (Math.Abs(borders.ymin - tileBorders.ymax) > 0.3f && frontTileID  == TileID.Platform)
                    {
                        return false;
                    }
                    tileBorders.DrawBox();
                    return true;
                }
            }

            return false;
        }

        public static bool IsCollidingTop(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.Y <= 0.0f) return false;
            
            int y = (int)borders.ymax;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;

            if (y < 0 || y >= tileMap.MapSize.Y) return false;
            
            for (int x = leftX; x <= rightX; x++)
            {
                if (x < 0 || x >= tileMap.MapSize.X) continue;
                
                var frontTileID = tileMap.GetFrontTileID(x, y);
                if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                {
                    var tileBorders = new AABox2D(x, y);
                    tileBorders.DrawBox();
                    return true;
                }
            }

            return false;
        }
    }
}

