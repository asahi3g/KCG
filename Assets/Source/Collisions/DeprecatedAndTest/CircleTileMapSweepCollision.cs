using System;
using System.Collections.Generic;
using Collisions;
using Enums.PlanetTileMap;
using KMath;

namespace CollisionsTest
{
    public static class CircleTileMapSweepCollision
    {
        private static Vec2i[] GetTilesRegionBroadPhase(int xmin, int xmax, int ymin, int ymax)
        {
            List<Vec2i> positions = new List<Vec2i>();

            for (int i = xmin; i <= xmax; i++)
            {
                for (int j = ymin; j < ymax; j++)
                {
                    var tile = GameState.Planet.TileMap.GetTile(i, j);
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
            int r1_xmin = velocity.X >= 0f ? (int) Math.Floor(box1_xmin) : (int) Math.Floor(box2_xmin); //round down
            int r1_xmax = velocity.X >= 0f ? (int) Math.Ceiling(box2_xmax) : (int) Math.Ceiling(box1_xmax); //round up
            int r1_ymin = velocity.Y >= 0f ? (int) Math.Floor(box1_ymax) : (int) Math.Floor(box2_ymin); //round down
            int r1_ymax = velocity.Y >= 0f ? (int) Math.Ceiling(box2_ymax) : (int) Math.Ceiling(box1_ymin); //round up

            int r2_xmin = velocity.X >= 0f ? (int) Math.Floor(box1_xmax) : (int) Math.Floor(box2_xmin); //round down
            int r2_xmax = velocity.X >= 0f ? (int) Math.Ceiling(box2_xmax) : (int) Math.Ceiling(box1_xmin); //round up
            // We don't need to test blocks that are inside of region 1 square
            int r2_ymin = velocity.Y >= 0f ? (int) Math.Floor(box1_ymin) : r1_ymax; //round down
            int r2_ymax = velocity.Y >= 0f ? r1_ymin : (int) Math.Ceiling(box1_ymax); //round up

            var R1 = new AABox2D(
                new Vec2f(r1_xmin, r1_ymin),
                new Vec2f(r1_xmax - r1_xmin, r1_ymax - r1_ymin));

            var R2 = new AABox2D(
                new Vec2f(r2_xmin, r2_ymin),
                new Vec2f(r2_xmax - r2_xmin, r2_ymax - r2_ymin));

            return (R1, R2);
        }
        
        public static float CalculateDistanceCircle_AABB(float box_xmin, float box_xmax, float box_ymin, float box_ymax, Vec2f circleCenter, float radius, Vec2f velocity)
        {
            Vec2f box_halfsize = new Vec2f((box_xmax - box_xmin) / 2f, (box_ymax - box_ymin) / 2f);
            Vec2f box_center = new Vec2f(box_xmin + box_halfsize.X, box_ymin + box_halfsize.Y);

            Vec2f box_difference = circleCenter - box_center;
            Vec2f clamped = Vec2f.Clamp(box_difference, -box_halfsize, box_halfsize);
            Vec2f closestPoint = box_center + clamped;
            
            Vec2f pointOnEdge = velocity.Normalized * radius;
            Vec2f edgePosition = circleCenter + pointOnEdge;

            return Vec2f.Distance(edgePosition, closestPoint);
        }

        public static Hit GetCollisionHitCircle_AABB(float radius, Vec2f center, Vec2f velocity)
        {
            var (r1, r2) = GetRegions(center.X - radius, center.X + radius, center.Y - radius, center.Y + radius, velocity);
            var R1_tiles = GetTilesRegionBroadPhase((int)r1.xmin, (int)r1.xmax, (int)r1.ymin, (int)r1.ymax);
            var R2_tiles = GetTilesRegionBroadPhase((int)r2.xmin, (int)r2.xmax, (int)r2.ymin, (int)r2.ymax);
            
            float lowestTime = float.MaxValue;
            Vec2i nearestTilePos = default;

            for (int i = 0; i < R1_tiles.Length; i++)
            {
                var tile = new AABox2D(R1_tiles[i].X, R1_tiles[i].Y);
                var distance = CalculateDistanceCircle_AABB(tile.xmin, tile.xmax, tile.ymin, tile.ymax, center, radius, velocity);
                var newTimeX = distance / velocity.X;
                var newTimeY = distance / velocity.Y;
                if (newTimeX < lowestTime)
                {
                    lowestTime = newTimeX;
                    nearestTilePos = R1_tiles[i];
                }
                
                if (newTimeY < lowestTime)
                {
                    lowestTime = newTimeY;
                    nearestTilePos = R1_tiles[i];
                }
            }
            
            for (int i = 0; i < R2_tiles.Length; i++)
            {
                var tile = new AABox2D(R2_tiles[i].X, R2_tiles[i].Y);
                var distance = CalculateDistanceCircle_AABB(tile.xmin, tile.xmax, tile.ymin, tile.ymax, center, radius, velocity);
                var newTimeX = distance / velocity.X;
                var newTimeY = distance / velocity.Y;
                if (newTimeX < lowestTime)
                {
                    lowestTime = newTimeX;
                    nearestTilePos = R1_tiles[i];
                }
                
                if (newTimeY < lowestTime)
                {
                    lowestTime = newTimeY;
                    nearestTilePos = R1_tiles[i];
                }
            }

            return new Hit
            {
                time = lowestTime,
                point = (Vec2f)nearestTilePos
            };
        }
    }
}

