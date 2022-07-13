using System;
using Enums.Tile;
using KMath;
using PlanetTileMap;

// find xchunkmin, xchunkmax, ychunkmax, ychunkmin for each rectangle
// check if chunk is air/not loaded
// then no collision if not loaded


//TODO: First check xchunkmin, xchunkmax, ychunkmin, ychunk max for square
//TODO: If the chunks are empty, then no collisions (for vehicles flying; 1 to 4 ints/frame to check)

//Question: Check the tiles here in map?
// - can scan over chunk, grab chunks and scan sequentially, to avoid GetTile
// - avoids the chunk array computation
//Question: Map a function for checking rectangle grids of tiles?
// chunk_xmin, chunk_xmax, chunk_ymax, chunk_xmax

//Question: Use 

//Unit Test: test for vx,vy positive/negative
//Unit Test: make sure same square is not called multiple times

/*
Stages:

Stage1: Check chunks for object
- if zero, return nothing

Stage2:
- get the rectangular regions to check R1, R2

Stage3:
- for each region, get the chunk data min/max
-- can be 1 chunk, 2 chunks x, 2 chunks y, 4 chunks in x/y
- max of 4 chunks
- copy the chunk IsoCollision type data into internal array
- OR as iterating, skip any zero, do test for non-zero IsoCollisionTypes

Stage4:
- for each x,y, collision isotype
- get line array and do collision check
- keep the collision with lowest time
-- if multiple collisions with same lowest time, what to do?
*/

/*
Tests:
- vx/vy values, Q1, Q2, Q3, Q4
- same with vx,vy zeroed

Tests:
- XY random

*/
namespace Collisions
{
    //Return R1, R2
    //Then grab the map tile CollisionIsoTypes for R1/R2
    //Fill a static grid, max size, max vx/vy
    //16x16 max size

    public static class RectScanTilesRef1_v2
    {
        public struct RectRegion
        {
            public int xmin;
            public int xmax;
            public int ymin;
            public int ymax;
        }

        public static RectRegion R0; //not used except tests
        public static RectRegion R1;
        public static RectRegion R2;

        //Set R0, R1, R2 variables
        public static void SetRegions(Vec2f center, Vec2f halfSize, Vec2f velocity)
        {
            var box1_xmax = (int)Math.Ceiling(center.X + halfSize.X); //round up
            var box2_xmax = (int)Math.Ceiling(center.X + halfSize.X + velocity.X); //round up

            var box1_xmin = (int)Math.Floor(center.X - halfSize.X); //round down
            var box2_xmin = (int)Math.Floor(center.X - halfSize.X + velocity.X); //round down

            var box1_ymax = (int)Math.Ceiling(center.Y + halfSize.Y); //round up
            var box2_ymax = (int)Math.Ceiling(center.Y + halfSize.Y + velocity.Y); //round up

            var box1_ymin = (int)Math.Floor(center.Y - halfSize.Y); //round down
            var box2_ymin = (int)Math.Floor(center.Y - halfSize.Y + velocity.Y); //round down

            // r - region
            // r1 - region1 and etc.

            int r1_xmin = velocity.X >= 0f ? box1_xmin : box2_xmin;
            int r1_xmax = velocity.X >= 0f ? box2_xmax : box1_xmax;
            int r1_ymin = velocity.Y >= 0f ? box1_ymax : box2_ymin;
            int r1_ymax = velocity.Y >= 0f ? box2_ymax : box1_ymin;

            int r2_xmin = velocity.X >= 0f ? box1_xmax : box2_xmin;
            int r2_xmax = velocity.X >= 0f ? box2_xmax : box1_xmin;
            int r2_ymin = velocity.Y >= 0f ? box1_ymin : box2_ymin;
            int r2_ymax = velocity.Y >= 0f ? box2_ymax : box1_ymax;

            //self.R0 = 
            R1.xmin = r1_xmin;
            R1.xmax = r1_xmax;
            R1.ymin = r1_ymin;
            R1.ymax = r1_ymax;

            R2.xmin = r2_xmin;
            R2.xmax = r2_xmax;
            R2.ymin = r2_ymin;
            R2.ymax = r2_ymax;
        }

        public static void SetRegion0()
        {
            R0.xmin = 1;
            R0.xmax = 2;
            R0.ymin = 1;
            R0.ymax = 2;
        }

        public static void TestAssertRegionData()
        {
            //R1
            Utils.Assert(R1.xmin < R1.xmax);
            Utils.Assert(R1.ymin < R1.ymax);

            //R2
            Utils.Assert(R2.xmin < R2.xmax);
            Utils.Assert(R2.ymin < R2.ymax);
            
            Utils.Assert(!RectOverlapRect(R1.xmin, R1.xmax, R1.ymin, R1.ymax, 
                                          R2.xmin, R2.xmax, R2.ymin, R2.ymax));

            //R0
            Utils.Assert(R0.xmin < R0.xmax);
            Utils.Assert(R0.ymin < R0.ymax);
        }
       
        public static void TestData1()
        {
            //for 16*1024 times

            var rnd = new Random();
            
            SetRegion0();

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    Vec2f center   = new Vec2f(rnd.Next(0, 16), rnd.Next(0, 16));
                    Vec2f halfSize = new Vec2f(rnd.Next(0, 3), rnd.Next(0, 3));
                    Vec2f velocity = new Vec2f(rnd.Next(0, 6), rnd.Next(0, 6));
                    SetRegions(center, halfSize, velocity);
                    TestAssertRegionData();
                }
            }

            //test1:
            //generate random center, 0 to 16
            //generate random halfSize, 0 to 3
            //generate random vx,vy 0 to 6
            //call SetRegions()
            //Call TestAssertRegionData()
        }
        
        public static void TestData2() {
            //test2:
            //same as test 1, but vx, vy, position, width, snapped to 1/16 f 
            
            var rnd = new Random();
            
            SetRegion0();

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    Vec2f center   = new Vec2f(rnd.Next(0, 16), rnd.Next(0, 16));
                    Vec2f halfSize = new Vec2f(rnd.Next(0, 3), rnd.Next(0, 3));
                    Vec2f velocity = new Vec2f(rnd.Next(0, 6), rnd.Next(0, 6));
                    SetRegions(center, halfSize, velocity);
                    TestAssertRegionData();
                }
            }
        }

        public static void TestData3()
        {
            //test3:
            //same as test 1, bu position snapped to 1.0f
            
            var rnd = new Random();

            SetRegion0();
            
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    // NextDouble - number from 0f to 1f
                    Vec2f center   = new Vec2f((float)rnd.NextDouble(), (float)rnd.NextDouble());
                    Vec2f halfSize = new Vec2f(rnd.Next(0, 3), rnd.Next(0, 3));
                    Vec2f velocity = new Vec2f(rnd.Next(0, 6), rnd.Next(0, 6));
                    SetRegions(center, halfSize, velocity);
                    TestAssertRegionData();
                }
            }
        }

        public static Vec2i[] BoxTileCollisionCheck(Vec2f center, Vec2f halfSize, Vec2f velocity)
        {
            return null;
        }

        //box1 is rectangle for position at start of frame
        //box2 is rectangle after applying velocity and doing linear displacement

        //public static Vec2i[] RectScan(xpos float, ypos, float xhalfsize, float yhalfsize, float vx, float vy)
        public static Vec2i[] RectScan(ref TileMap tileMap)
        {
            int outputCount = (R1.xmax - R1.xmin) * (R1.ymax - R1.ymin);
            outputCount += (R2.xmax - R2.xmin) * (R2.ymax - R2.ymin);

            var tilePositions = new Vec2i[outputCount];
            var index = 0;

            for (int x = R1.xmin; x <= R1.xmax; x++)
            {
                for (int y = R1.ymin; y <= R1.ymax; y++)
                {
                    tilePositions[index] = new Vec2i(x, y);
                    index++;
                }
            }

            for (int y = R2.xmin; y <= R2.xmax; y++)
            {
                for (int x = R2.ymin; x <= R2.ymax; x++)
                {
                    tilePositions[index] = new Vec2i(x, y);
                    index++;
                }
            }

            foreach (var pos in tilePositions)
            {
                var tile = tileMap.GetTile(pos.X, pos.Y, MapLayerType.Front);

                if (tile.ID == TileID.Air)
                {
                    
                }
            }

            return tilePositions;
        }
        


        public static Chunk[] ChunkScan(ref TileMap tileMap)
        {
            var chunkSize = tileMap.ChunkSize;
            
            int GetChunkIndex(int x, int y)
            {
                var xChunkIndex = x / 16;
                var yChunkIndex = (y / 16) * chunkSize.X;
                return xChunkIndex + yChunkIndex;
            }
            
            ref var xchunkmin = ref tileMap.ChunkArray[GetChunkIndex(R1.xmin, R1.ymax)];
            ref var xchunkmax = ref tileMap.ChunkArray[GetChunkIndex(R1.xmax, R1.ymax)];
            ref var ychunkmin = ref tileMap.ChunkArray[GetChunkIndex(R2.xmin, R2.ymin)];
            ref var ychunkmax = ref tileMap.ChunkArray[GetChunkIndex(R2.xmin, R2.ymax)];

            var outputCount = xchunkmin.Type;

            return null;
        }

        public static bool RectOverlapRect(float r1_xmin, float r1_xmax, float r1_ymin, float r1_ymax, float r2_xmin, float r2_xmax, float r2_ymin, float r2_ymax) 
        {
            // are the sides of one rectangle touching the other?

            if (r1_xmax >= r2_xmin &&    // r1 right edge past r2 left
                r1_xmin <= r2_xmax &&    // r1 left edge past r2 right
                r1_ymax >= r2_ymin &&    // r1 top edge past r2 bottom
                r1_ymin <= r2_ymax) {    // r1 bottom edge past r2 top
                return true;
            }
            return false;
        }
    }
}