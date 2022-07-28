using System.Collections.Generic;
using Enums.Tile;
using KMath;
using System;
using Utility;

namespace Collisions
{
    public static class TileCollisions
    {        
        public static Vec2i[] GetTilesRegionBroadPhase(PlanetTileMap.TileMap tileMap, int xmin, int xmax, int ymin, int ymax)
        {
            List<Vec2i> positions = new List<Vec2i>();
            
            for (int i = xmin; i <= xmax; i++)
            {
                for (int j = ymin; j < ymax; j++)
                {
                    var tile = tileMap.GetTile(i, j);
                    if (tile.FrontTileID != TileID.Air)
                    {
                        positions.Add(new Vec2i(i, j));
                    }
                }
            }
            
            return positions.ToArray();
        }

        public static float CalculateDistanceAABB_AABB(float box1_xmin, float box1_xmax, float box1_ymin, float box1_ymax,
            float box2_xmin, float box2_xmax, float box2_ymin, float box2_ymax, Vec2f velocity)
        {
            Vec2f line1 = default;
            Vec2f line2 = default;
            
            if (velocity.X > 0f)
            {
                line1.X = box1_xmax;
                line2.X = box2_xmin;
            }
            else if (velocity.X < 0f)
            {
                line1.X = box1_xmin;
                line2.X = box2_xmax;
            }
            
            if (velocity.Y > 0f)
            {
                line1.Y = box1_ymax;
                line2.Y = box2_ymin;
            }
            else if (velocity.Y < 0f)
            {
                line1.Y = box1_ymin;
                line2.Y = box2_ymax;
            }

            return Vec2f.Distance(line1, line2);
        }
        
        public static bool IsCollidingLeft(this AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.X >= 0.0f) return false;
            
            int x = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            
            if (x >= 0 && x < tileMap.MapSize.X)
            {
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
            }

            return false;
        }

        public static bool IsCollidingRight(this AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.X <= 0.0f) return false;
            
            int x = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (x >= 0 && x < tileMap.MapSize.X)
            {
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
            }

            return false;
        }
        
        public static bool IsCollidingBottom(this AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.Y >= 0.0f) return false;
            
            // LeftBottom.X >= 0f ? (int)LeftBottom.X : (int)LeftBottom.X - 1;
            
            int y = (int)borders.ymin;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (y >= 0 && y < tileMap.MapSize.Y)
            {
                for (int x = leftX; x <= rightX; x++)
                {
                    if (x >= 0 && x < tileMap.MapSize.X)
                    {
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
                }
            }

            return false;
        }

        public static bool IsCollidingTop(this AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            if (velocity.Y <= 0.0f) return false;
            
            int y = (int)borders.ymax;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (y >= 0 && y < tileMap.MapSize.Y)
            {
                for (int x = leftX; x <= rightX; x++)
                {
                    if (x >= 0 && x < tileMap.MapSize.X)
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
            }

            return false;
        }
    }
}