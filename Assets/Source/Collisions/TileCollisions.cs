using System.Collections.Generic;
using Enums.Tile;
using KMath;
using System;
using Utility;

namespace Collisions
{
    public static class TileCollisions
    {
        private static Vec2i[] GetTilesRegionBroadPhase(PlanetTileMap.TileMap tileMap, int xmin, int xmax, int ymin, int ymax)
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
        
        private static (AABox2D R1, AABox2D R2) GetRegions(float r_xmin, float r_xmax, float r_ymin, float r_ymax, Vec2f velocity)
        {
            var box1_xmin = r_xmin;
            var box1_xmax = r_xmax;
            var box1_ymin = r_ymin;
            var box1_ymax = r_ymax;

            var box2_xmin = r_xmin + velocity.X;
            var box2_xmax = r_xmax + velocity.X;
            var box2_ymin = r_ymin + velocity.Y;
            var box2_ymax = r_ymax + velocity.Y;
            
            // r - region
            // r1 - region1 and etc.
            int r1_xmin = velocity.X >= 0f ? (int)Math.Floor(box1_xmin) : (int)Math.Floor(box2_xmin);       //round down
            int r1_xmax = velocity.X >= 0f ? (int)Math.Ceiling(box2_xmax) : (int)Math.Ceiling(box1_xmax);   //round up
            int r1_ymin = velocity.Y >= 0f ? (int)Math.Floor(box1_ymax) : (int)Math.Floor(box2_ymin);       //round down
            int r1_ymax = velocity.Y >= 0f ? (int)Math.Ceiling(box2_ymax) : (int)Math.Ceiling(box1_ymin);   //round up

            int r2_xmin = velocity.X >= 0f ? (int)Math.Floor(box1_xmax) : (int)Math.Floor(box2_xmin);       //round down
            int r2_xmax = velocity.X >= 0f ? (int)Math.Ceiling(box2_xmax) : (int)Math.Ceiling(box1_xmin);   //round up
            // We don't need to test blocks that are inside of region 1 square
            int r2_ymin = velocity.Y >= 0f ? (int)Math.Floor(box1_ymin) : r1_ymax;                          //round down
            int r2_ymax = velocity.Y >= 0f ? r1_ymin : (int)Math.Ceiling(box1_ymax);                        //round up

            var R1 = new AABox2D(
                new Vec2f((r1_xmax - r1_xmin) / 2f, (r1_ymax - r1_ymin) / 2f),
                new Vec2f(r1_xmax - r1_xmin, r1_ymax - r1_ymin));

            var R2 = new AABox2D(
                new Vec2f((r2_xmax - r2_xmin) / 2f, (r2_ymax - r2_ymin) / 2f),
                new Vec2f(r2_xmax - r2_xmin, r2_ymax - r2_ymin));

            return (R1, R2);
        }
        
        public static Hit GetCollisionHitAABB_AABB(PlanetTileMap.TileMap tileMap, float xmin, float xmax, float ymin, float ymax, Vec2f velocity)
        {
            var (r1, r2) = GetRegions(xmin, xmax, ymin, ymax, velocity);
            var R1_tiles = GetTilesRegionBroadPhase(tileMap, (int)r1.xmin, (int)r1.xmax, (int)r1.ymin, (int)r1.ymax);
            var R2_tiles = GetTilesRegionBroadPhase(tileMap, (int)r2.xmin, (int)r2.xmax, (int)r2.ymin, (int)r2.ymax);

            float lowestTime = float.MaxValue;
            Vec2i nearestTilePos = default;

            for (int i = 0; i < R1_tiles.Length; i++)
            {
                var tile = new AABox2D(R1_tiles[i].X, R1_tiles[i].Y);
                var distance = CalculateDistanceAABB_AABB(
                    xmin, xmax, ymin, ymax,
                    tile.xmin, tile.xmax, tile.ymin, tile.ymax, velocity);
                var newTime = distance / velocity.Y;
                if (newTime < lowestTime)
                {
                    lowestTime = newTime;
                    nearestTilePos = R1_tiles[i];
                }
            }
        
            for (int i = 0; i < R2_tiles.Length; i++)
            {
                var tile = new AABox2D(R2_tiles[i].X, R2_tiles[i].Y);
                var distance = CalculateDistanceAABB_AABB(
                    xmin, xmax, ymin, ymax,
                    tile.xmin, tile.xmax, tile.ymin, tile.ymax, velocity);
                var newTime = distance / velocity.X;
                if (newTime < lowestTime)
                {
                    lowestTime = newTime;
                    nearestTilePos = R1_tiles[i];
                }
            }

            return new Hit
            {
                point = (Vec2f) nearestTilePos,
                time = lowestTime
            };
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
                line1.X = box2_xmax;
                line2.X = box1_xmin;
            }
            
            if (velocity.Y > 0f)
            {
                line1.Y = box1_ymax;
                line2.Y = box2_ymin;
            }
            else if (velocity.Y < 0f)
            {
                line1.Y = box2_ymax;
                line2.Y = box1_ymin;
            }

            return Vec2f.Distance(line1, line2);
        }
        
        public static bool IsCollidingLeft(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
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

        public static bool IsCollidingRight(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
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
        
        public static bool IsCollidingBottom(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
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
                        if (frontTileID != TileID.Air)
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

        public static bool IsCollidingTop(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
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
