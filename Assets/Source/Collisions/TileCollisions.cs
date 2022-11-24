using System.Collections.Generic;
using Enums.Tile;
using KMath;
using System;
using Utility;
using Enums.PlanetTileMap;

namespace Collisions
{
    public static class TileCollisions
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
        
        public static Hit GetCollisionHitAABB_AABB(float xmin, float xmax, float ymin, float ymax, Vec2f velocity)
        {
            var (r1, r2) = GetRegions(xmin, xmax, ymin, ymax, velocity);
            var R1_tiles = GetTilesRegionBroadPhase((int)r1.xmin, (int)r1.xmax, (int)r1.ymin, (int)r1.ymax);
            var R2_tiles = GetTilesRegionBroadPhase((int)r2.xmin, (int)r2.xmax, (int)r2.ymin, (int)r2.ymax);

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

        public struct TilesInProximityResult
        {
            public int MinX;
            public int MaxX;
            public int MinY;
            public int MaxY;
        }

        // getting closest tiles for moving box 2d
        public static TilesInProximityResult GetTilesInProximity(Vec2f position, Vec2f dimensions, Vec2f delta)
        {
            Vec2f min = new Vec2f(MathF.Min(position.X, position.X + delta.Y), MathF.Min(position.Y, position.Y + delta.Y));
            Vec2f max = new Vec2f(MathF.Max(position.X, position.X + delta.Y), MathF.Max(position.Y, position.Y + delta.Y)) + dimensions;


            TilesInProximityResult result = new TilesInProximityResult();
            result.MinX = (int)min.X - 1;
            result.MaxX = (int)(max.X + 1.0f) + 1;
            result.MinY = (int)min.Y - 1;
            result.MaxY = (int)(max.Y + 1.0f) + 1;

            return result;
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
        
        public static bool IsCollidingLeft(this ref AABox2D borders, Vec2f velocity)
        {
            ref var planet = ref GameState.Planet;
            if (velocity.X >= 0.0f) return false;
            
            int x = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            
            if (x >= 0 && x < planet.TileMap.MapSize.X)
            {
                for (int y = (int)borders.ymin; y <= (int)borders.ymax; y++)
                {
                    if (y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        var frontTileID = planet.TileMap.GetFrontTileID(x, y);
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

        public static bool HandleCollidingLeft(AgentEntity entity, Planet.PlanetState planet, bool reflect)
        {
            PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
            delta.Y = 0;

            if (delta.X >= 0) return false;
            
            Vec2f colliderPosition = physicsState.PreviousPosition + box2dCollider.Offset;
            
            int ymin = (int)(colliderPosition.Y);
            int ymax = (int)(colliderPosition.Y + box2dCollider.Size.Y);


            int xmin = (int)(colliderPosition.X);
            int xmax = (int)(colliderPosition.X + box2dCollider.Size.X);

            Line2D[] lines = new Line2D[128];
            Vec2f[] normals = new Vec2f[128];
            int linesCount = 0;

            //lines[linesCount++] = new Line2D(new Vec2f(7.0f, 16.0f), new Vec2f(7.0f, 17.0f));


            for(int x = xmin - 1; x <= xmin; x++)
            {
            if (x >= 0 && x < tileMap.MapSize.X)
            {
                for (int y = ymin; y <= ymax; y++)
                {
                    if (y >= 0 && y < tileMap.MapSize.Y)
                    {
                        var frontTileID = tileMap.GetFrontTileID(x, y);
                        if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                        {
                           PlanetTileMap.TileProperty properties = GameState.TileCreationApi.GetTileProperty(frontTileID);

                            GeometryProperties geometryProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);

                            for(int i = 0; i < geometryProperties.Size; i++)
                            {
                                TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(geometryProperties.Offset + i);  
                                Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);  
                                Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);                         

                                if (linesCount + 1 >= lines.Length)
                                {
                                    Array.Resize(ref lines, lines.Length * 2);
                                    Array.Resize(ref normals, lines.Length * 2);
                                }

                                lines[linesCount] = line;
                                normals[linesCount] = normal;

                                linesCount++;
                            }
                        }
                    }
                }
            }
            }


            
           float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            for(int i = 0; i < linesCount; i++)
            {
                
                Line2D line = lines[i];
                Vec2f normal = normals[i];

               // planet.AddDebugLine(line);
              /*  var result = 
                CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);*/


                Line2D testLine = new Line2D(colliderPosition, colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f));
               // planet.AddDebugLine(testLine);
                float time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);

                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f), colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y));
              //  planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y), colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y));
               // planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y), colliderPosition);
             //   planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }

               /* if (result.Time < minTime)
                {
                    minTime = result.Time;
                    minNormal = result.Normal;
                }*/
            }

            if (minTime < 1.0f)
            {
                //physicsState.Position.X = physicsState.PreviousPosition.X;
                physicsState.Position = physicsState.PreviousPosition + delta * minTime;
                physicsState.Position -= delta.Normalize() * 0.01f;
                //physicsState.Velocity = new Vec2f();
                //physicsState.Acceleration = new Vec2f();

                Vec2f reflectVelocity = delta - 1.0f * Vec2f.Dot(delta, minNormal) * minNormal;
                physicsState.PreviousPosition = physicsState.Position;
              // Vec2f reflectVelocity = -delta;
              if (reflect)
              {
                physicsState.Position += reflectVelocity * (1.0f - minTime) * 1.0f;
              }
            }


            return minTime < 1.0f;
        }

        public static bool IsCollidingRight(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            ref var planet = ref GameState.Planet;
            if (velocity.X <= 0.0f) return false;
            
            int x = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (x >= 0 && x < planet.TileMap.MapSize.X)
            {
                for (int y = (int)borders.ymin; y <= (int)borders.ymax; y++)
                {
                    if (y >= 0 && y < planet.TileMap.MapSize.Y)
                    {
                        var frontTileID = planet.TileMap.GetFrontTileID(x, y);
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

        public struct HandleCollisionResult
        {
            public float MinTime;
            public Vec2f MinNormal;
            public Enums.TileGeometryAndRotation GeometryTileShape;
            public Enums.MaterialType Material;
        }


        

    
        public struct CapsuleCollisionResult
        {
            public float MinTime;
            public Vec2f MinNormal;
            public Enums.TileGeometryAndRotation GeometryTileShape;
            public Enums.MaterialType Material;

            public HandleCollisionResult BottomCollision;
        }

        public static CapsuleCollisionResult CapsuleCollision(AgentEntity entity, Vec2f delta, Planet.PlanetState planet)
        {
            CapsuleCollisionResult result = new CapsuleCollisionResult();

            var bottomCollision = TileCollisions.HandleCollisionCircleBottom(entity,  delta, planet);

            var topCollision = TileCollisions.HandleCollisionCircleTop(entity, delta, planet);

            result.BottomCollision = bottomCollision;



            if (bottomCollision.MinTime <= topCollision.MinTime)
            {
                result.MinTime = bottomCollision.MinTime;
                result.MinNormal = bottomCollision.MinNormal;
                result.GeometryTileShape = bottomCollision.GeometryTileShape;
                result.Material = bottomCollision.Material;
            }
            else
            {
                result.MinTime = topCollision.MinTime;
                result.MinNormal = topCollision.MinNormal;
                result.GeometryTileShape = topCollision.GeometryTileShape;
                result.Material = topCollision.Material;
            }


            return result;
        }

        public static HandleCollisionResult HandleCollisionCircleTop(AgentEntity entity, Vec2f delta, Planet.PlanetState planet)
        {
            HandleCollisionResult result = new HandleCollisionResult();
            result.MinTime = 1.0f;

           PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;
            if (delta.X == 0 && delta.Y == 0) return result;

           Vec2f colliderPosition = physicsState.PreviousPosition + box2dCollider.Offset + new Vec2f(0.0f, box2dCollider.Size.Y - box2dCollider.Size.X / 2.0f);
           
           var tilesInProximity = GetTilesInProximity(physicsState.PreviousPosition + box2dCollider.Offset, box2dCollider.Size, delta);

            float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            Enums.TileGeometryAndRotation minShape = 0;
            Enums.MaterialType minMaterial = 0;


            for(int y = tilesInProximity.MinY; y <= tilesInProximity.MaxY; y++)
            {
                for(int x = tilesInProximity.MinX; x <= tilesInProximity.MaxX; x++)
                {
                    if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
                    {
                        PlanetTileMap.Tile tile = planet.TileMap.GetTile(x, y);
                        var tileProperties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
                        Enums.TileGeometryAndRotation shape = tileProperties.BlockShapeType;
                        Enums.MaterialType material = tileProperties.MaterialType;


                        if (tile.Adjacency != Enums.TileGeometryAndRotationAndAdjacency.Error)
                        {
                        var adjacencyProperties = GameState.AdjacencyPropertiesManager.GetProperties(tile.Adjacency);
                        for(int i = adjacencyProperties.Offset; i < adjacencyProperties.Offset + adjacencyProperties.Size; i++)
                        {
                            var lineEnum = GameState.AdjacencyPropertiesManager.GetLine(i);

                            Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);
                            Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);

                            planet.AddDebugLine(line, UnityEngine.Color.red);

                            // make sure its not a platform
                            // we cant collide with a platform
                            if (((shape != Enums.TileGeometryAndRotation.QP_R0 && shape != Enums.TileGeometryAndRotation.QP_R1 && 
                            shape != Enums.TileGeometryAndRotation.QP_R2 && shape != Enums.TileGeometryAndRotation.QP_R3)))
                            {
                                // sweep test for circle line collision
                                var collisionResult = 
                                CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);

                                // keep the minimum collision
                                if (collisionResult.Time < minTime)
                                {
                                    minTime = collisionResult.Time;
                                    minNormal = collisionResult.Normal;
                                    minShape = shape;
                                    minMaterial = material;
                                }
                            }
                        }
                        }
                    }
                }
            }

            //TODO(Mahdi):
            // 1- do not iterate over all the lines in the tile map
            // instead get only the closest lines
            // 2- velocity quadrants
            for(int i = 0; i < planet.TileMap.GeometryArrayCount; i++)
            {
                
                Line2D line = planet.TileMap.GeometryArray[i].Line;
                Vec2f normal = planet.TileMap.GeometryArray[i].Normal;
                Enums.TileGeometryAndRotation shape = planet.TileMap.GeometryArray[i].Shape;
                Enums.MaterialType material = planet.TileMap.GeometryArray[i].Material;



                // make sure its not a platform
                // we cant collide with a platform
                if (((shape != Enums.TileGeometryAndRotation.QP_R0 && shape != Enums.TileGeometryAndRotation.QP_R1 && 
                shape != Enums.TileGeometryAndRotation.QP_R2 && shape != Enums.TileGeometryAndRotation.QP_R3)))
                {


                    // sweep test for circle line collision
                    var collisionResult = 
                    CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);

                    // keep the minimum collision
                    if (collisionResult.Time < minTime)
                    {
                        minTime = collisionResult.Time;
                        minNormal = collisionResult.Normal;
                        minShape = shape;
                        minMaterial = material;
                    }
                }

            }


            result.MinTime = minTime;
            result.MinNormal = minNormal;
            result.GeometryTileShape = minShape;
            result.Material = minMaterial;


            return result;

        }

        public static HandleCollisionResult HandleCollisionCircleBottom(AgentEntity entity, Vec2f delta, Planet.PlanetState planet)
        {
            HandleCollisionResult result = new HandleCollisionResult();
            result.MinTime = 1.0f;

           PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;
            if (delta.X == 0 && delta.Y == 0) return result;

           Vec2f colliderPosition = physicsState.PreviousPosition + box2dCollider.Offset;

           var tilesInProximity = GetTilesInProximity(physicsState.PreviousPosition + box2dCollider.Offset, box2dCollider.Size, delta);
            
            float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            Enums.TileGeometryAndRotation minShape = 0;
            Enums.MaterialType minMaterial = 0;



            for(int y = tilesInProximity.MinY; y <= tilesInProximity.MaxY; y++)
            {
                for(int x = tilesInProximity.MinX; x <= tilesInProximity.MaxX; x++)
                {
                    if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
                    {
                        PlanetTileMap.Tile tile = planet.TileMap.GetTile(x, y);
                        var tileProperties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
                        Enums.TileGeometryAndRotation shape = tileProperties.BlockShapeType;
                        Enums.MaterialType material = tileProperties.MaterialType;




                        if (tile.Adjacency != Enums.TileGeometryAndRotationAndAdjacency.Error)
                        {
                        var adjacencyProperties = GameState.AdjacencyPropertiesManager.GetProperties(tile.Adjacency);
                        for(int i = adjacencyProperties.Offset; i < adjacencyProperties.Offset + adjacencyProperties.Size; i++)
                        {
                            var lineEnum = GameState.AdjacencyPropertiesManager.GetLine(i);

                            Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);
                            Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);

                            

                            // if its a platform
                            // only collide when going downwards
                            if (!(delta.Y >= 0.0f && (shape == Enums.TileGeometryAndRotation.QP_R0 || shape == Enums.TileGeometryAndRotation.QP_R1 || 
                            shape == Enums.TileGeometryAndRotation.QP_R2 || shape == Enums.TileGeometryAndRotation.QP_R3)))
                            {

                                planet.AddDebugLine(line, UnityEngine.Color.red);

                                // circle line sweep test
                                var collisionResult = 
                                CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);

                                if (collisionResult.Time < minTime)
                                {
                                    minTime = collisionResult.Time;
                                    minNormal = normal;// collisionResult.Normal;
                                    minShape = shape;
                                    minMaterial = material;
                                }
                            }
                        }
                        }
                    }
                }
            }


            //TODO(Mahdi):
            // 1- do not iterate over all the lines in the tile map
            // instead get only the closest lines
            // 2- velocity quadrants
            for(int i = 0; i < planet.TileMap.GeometryArrayCount; i++)
            {
                
                Line2D line = planet.TileMap.GeometryArray[i].Line;
                Vec2f normal = planet.TileMap.GeometryArray[i].Normal;
                Enums.TileGeometryAndRotation shape = planet.TileMap.GeometryArray[i].Shape;
                Enums.MaterialType material = planet.TileMap.GeometryArray[i].Material;


                // if its a platform
                // only collide when going downwards
                if (!(delta.Y >= 0.0f && (shape == Enums.TileGeometryAndRotation.QP_R0 || shape == Enums.TileGeometryAndRotation.QP_R1 || 
                shape == Enums.TileGeometryAndRotation.QP_R2 || shape == Enums.TileGeometryAndRotation.QP_R3)))
                {

                    // circle line sweep test
                    var collisionResult = 
                    CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);

                    if (collisionResult.Time < minTime)
                    {
                        minTime = collisionResult.Time;
                        minNormal = normal;// collisionResult.Normal;
                        minShape = shape;
                        minMaterial = material;
                    }
                }

            }


            result.MinTime = minTime;
            result.MinNormal = minNormal;
            result.GeometryTileShape = minShape;
            result.Material = minMaterial;


            return result;

        }

        public static bool IsCollidingBottom(this ref AABox2D borders, Vec2f velocity)
        {
            ref var planet = ref GameState.Planet;
            if (velocity.Y >= 0.0f) return false;
            
            // LeftBottom.X >= 0f ? (int)LeftBottom.X : (int)LeftBottom.X - 1;
            
            int y = (int)borders.ymin;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                for (int x = leftX; x <= rightX; x++)
                {
                    if (x >= 0 && x < planet.TileMap.MapSize.X)
                    {
                        var frontTileID = planet.TileMap.GetFrontTileID(x, y);
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


        public static bool HandleCollidingTop(AgentEntity entity, Planet.PlanetState planet, bool reflect)
        {
           PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
            delta.X = 0;

            if (delta.Y <= 0) return false;
            
            Vec2f colliderPosition = physicsState.PreviousPosition + box2dCollider.Offset;
            
            int ymin = (int)(colliderPosition.Y);
            int ymax = (int)(colliderPosition.Y + box2dCollider.Size.Y);


            int xmin = (int)(colliderPosition.X);
            int xmax = (int)(colliderPosition.X + box2dCollider.Size.X);

            Line2D[] lines = new Line2D[128];
            Vec2f[] normals = new Vec2f[128];
            int linesCount = 0;

            //lines[linesCount++] = new Line2D(new Vec2f(7.0f, 16.0f), new Vec2f(7.0f, 17.0f));


            for(int x = xmin - 1; x <= xmax + 1; x++)
            {
            if (x >= 0 && x < tileMap.MapSize.X)
            {
                for (int y = ymax; y <= ymax + 1; y++)
                {
                    if (y >= 0 && y < tileMap.MapSize.Y)
                    {
                        var frontTileID = tileMap.GetFrontTileID(x, y);
                        if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                        {
                            PlanetTileMap.TileProperty properties = GameState.TileCreationApi.GetTileProperty(frontTileID);

                            GeometryProperties geometryProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);

                            for(int i = 0; i < geometryProperties.Size; i++)
                            {
                                TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(geometryProperties.Offset + i);  
                                Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);  
                                Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);                         

                                if (linesCount + 1 >= lines.Length)
                                {
                                    Array.Resize(ref lines, lines.Length * 2);
                                    Array.Resize(ref normals, lines.Length * 2);
                                }

                                lines[linesCount] = line;
                                normals[linesCount] = normal;

                                linesCount++;
                            }
                        }
                    }
                }
            }
            }


            
           float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            for(int i = 0; i < linesCount; i++)
            {
                
                Line2D line = lines[i];
                Vec2f normal = normals[i];

          //      planet.AddDebugLine(line);
              /*  var result = 
                CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);*/


                Line2D testLine = new Line2D(colliderPosition, colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f));
          //      planet.AddDebugLine(testLine);
                float time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);

                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f), colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y));
          //      planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y), colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y));
          //      planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y), colliderPosition);
          //      planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }

               /* if (result.Time < minTime)
                {
                    minTime = result.Time;
                    minNormal = result.Normal;
                }*/
            }

            if (minTime < 1.0f)
            {
                //physicsState.Position.X = physicsState.PreviousPosition.X;
                physicsState.Position = physicsState.PreviousPosition + delta * minTime;
                physicsState.Position -= delta.Normalize() * 0.01f;
                //physicsState.Velocity = new Vec2f();
                //physicsState.Acceleration = new Vec2f();

                Vec2f reflectVelocity = delta - 1.0f * Vec2f.Dot(delta, minNormal) * minNormal;
                physicsState.PreviousPosition = physicsState.Position;
              // Vec2f reflectVelocity = -delta;
              if (reflect)
              {
                physicsState.Position += reflectVelocity * (1.0f - minTime) * 1.0f;
              }
            }


            return minTime < 1.0f;
        }

        public static bool HandleCollidingBottom(AgentEntity entity, Planet.PlanetState planet, bool reflect)
        {
           PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            Vec2f delta = physicsState.Position - physicsState.PreviousPosition;
            delta.X = 0;

            if (delta.Y >= 0) return false;
            
            Vec2f colliderPosition = physicsState.PreviousPosition + box2dCollider.Offset;
            
            int ymin = (int)(colliderPosition.Y);
            int ymax = (int)(colliderPosition.Y + box2dCollider.Size.Y);


            int xmin = (int)(colliderPosition.X);
            int xmax = (int)(colliderPosition.X + box2dCollider.Size.X);

            Line2D[] lines = new Line2D[128];
            Vec2f[] normals = new Vec2f[128];
            int linesCount = 0;

            //lines[linesCount++] = new Line2D(new Vec2f(7.0f, 16.0f), new Vec2f(7.0f, 17.0f));


            for(int x = xmin - 1; x <= xmax + 1; x++)
            {
            if (x >= 0 && x < tileMap.MapSize.X)
            {
                for (int y = ymin - 1; y <= ymin; y++)
                {
                    if (y >= 0 && y < tileMap.MapSize.Y)
                    {
                        var frontTileID = tileMap.GetFrontTileID(x, y);
                        if (frontTileID != TileID.Air)
                        {
                            PlanetTileMap.TileProperty properties = GameState.TileCreationApi.GetTileProperty(frontTileID);

                            GeometryProperties geometryProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);

                            for(int i = 0; i < geometryProperties.Size; i++)
                            {
                                TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(geometryProperties.Offset + i);  
                                Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);  
                                Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);                         

                                if (linesCount + 1 >= lines.Length)
                                {
                                    Array.Resize(ref lines, lines.Length * 2);
                                    Array.Resize(ref normals, lines.Length * 2);
                                }

                                lines[linesCount] = line;
                                normals[linesCount] = normal;

                                linesCount++;
                            }
                        }
                    }
                }
            }
            }


            
           float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            for(int i = 0; i < linesCount; i++)
            {
                
                Line2D line = lines[i];
                Vec2f normal = normals[i];

           //     planet.AddDebugLine(line);
              /*  var result = 
                CircleLineCollision.TestCollision(colliderPosition + box2dCollider.Size.X / 2.0f, box2dCollider.Size.X / 2.0f, delta, line.A, line.B);*/


                Line2D testLine = new Line2D(colliderPosition, colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f));
          //      planet.AddDebugLine(testLine);
                float time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);

                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f), colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y));
            //    planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y), colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y));
            //    planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }
                testLine = new Line2D(colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y), colliderPosition);
          //      planet.AddDebugLine(testLine);
                time = LineLineSweepTest.TestCollision(testLine, delta, line.A, line.B);
                if (time < minTime)
                {
                    minTime = time;
                    minNormal = normal;
                }

               /* if (result.Time < minTime)
                {
                    minTime = result.Time;
                    minNormal = result.Normal;
                }*/
            }

            if (minTime < 1.0f)
            {
                //physicsState.Position.X = physicsState.PreviousPosition.X;
                physicsState.Position = physicsState.PreviousPosition + delta * minTime;
                physicsState.Position -= delta.Normalize() * 0.01f;
                //physicsState.Velocity = new Vec2f();
                //physicsState.Acceleration = new Vec2f();

                Vec2f reflectVelocity = delta - 1.0f * Vec2f.Dot(delta, minNormal) * minNormal;
                physicsState.PreviousPosition = physicsState.Position;
              // Vec2f reflectVelocity = -delta;
              if (reflect)
              {
                physicsState.Position += reflectVelocity * (1.0f - minTime) * 1.0f;
              }
            }


            return minTime < 1.0f;
        }

        public static bool IsCollidingTop(this ref AABox2D borders, PlanetTileMap.TileMap tileMap, Vec2f velocity)
        {
            ref var planet = ref GameState.Planet;
            if (velocity.Y <= 0.0f) return false;
            
            int y = (int)borders.ymax;
            int leftX = borders.xmin < 0 ? (int) borders.xmin - 1 : (int)borders.xmin;
            int rightX = borders.xmax < 0 ? (int) borders.xmax - 1 : (int)borders.xmax;
            
            if (y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                for (int x = leftX; x <= rightX; x++)
                {
                    if (x >= 0 && x < planet.TileMap.MapSize.X)
                    {
                        var frontTileID = planet.TileMap.GetFrontTileID(x, y);
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


        public static void StaticCheck(AgentEntity entity, Planet.PlanetState planet)
        {
            PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            Vec2f colliderPosition = physicsState.Position + box2dCollider.Offset;

            int ymin = (int)(colliderPosition.Y);
            int ymax = (int)(colliderPosition.Y + box2dCollider.Size.Y);


            int xmin = (int)(colliderPosition.X);
            int xmax = (int)(colliderPosition.X + box2dCollider.Size.X);



            Vec2f[] agentVertices = new Vec2f[4];
            Vec2f[] tileVertices = new Vec2f[32];
            int tileVerticesCount = 0;


            for(int x = xmin - 1; x <= xmax + 1; x++)
            {
                if (x >= 0 && x < tileMap.MapSize.X)
                {
                    for (int y = ymin - 1; y <= ymin; y++)
                    {
                        if (y >= 0 && y < tileMap.MapSize.Y)
                        {
                            var frontTileID = tileMap.GetFrontTileID(x, y);
                            if (frontTileID != TileID.Air)
                            {
                                PlanetTileMap.TileProperty properties = GameState.TileCreationApi.GetTileProperty(frontTileID);

                                GeometryProperties geometryProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);


                                colliderPosition = physicsState.Position + box2dCollider.Offset;
                                agentVertices[0] = colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y);
                                agentVertices[1] = colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y);
                                agentVertices[2] = colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f);;
                                agentVertices[3] = colliderPosition;

                                tileVerticesCount = 0;

                                for(int i = 0; i < geometryProperties.Size; i++)
                                {
                                    TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(geometryProperties.Offset + i);  
                                    Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);  
                                    Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);                         

                                    if (tileVerticesCount + 1 >= tileVertices.Length)
                                    {
                                        Array.Resize(ref tileVertices, tileVertices.Length * 2);
                                    }

                                    tileVertices[tileVerticesCount] = line.A;
                                    tileVerticesCount++;
                                }

                                Vec2f[] tmpV = new Vec2f[tileVerticesCount];
                                for(int i = 0; i < tileVerticesCount; i++)
                                {
                                    tmpV[i] = tileVertices[i];
                                }


                                for(int i = 0; i < tmpV.Length; i++)
                                {
                                   /* planet.AddDebugLine(new Line2D(tmpV[i], tmpV[(i + 1 >= tileVerticesCount) ? 0 : i + 1]));*/
                                }

                                MinimumMagnitudeVector mtv = SAT.CollisionDetection(agentVertices, tmpV);
                                physicsState.Position -= mtv.Axis * mtv.Value;
                            }
                        }
                    }
                }
            }
        }



        public static void StaticCheckY(AgentEntity entity, Planet.PlanetState planet)
        {
            PlanetTileMap.TileMap tileMap = planet.TileMap;

            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            Vec2f colliderPosition = physicsState.Position + box2dCollider.Offset;

            int ymin = (int)(colliderPosition.Y);
            int ymax = (int)(colliderPosition.Y + box2dCollider.Size.Y);


            int xmin = (int)(colliderPosition.X);
            int xmax = (int)(colliderPosition.X + box2dCollider.Size.X);



            Vec2f[] agentVertices = new Vec2f[4];
            Vec2f[] tileVertices = new Vec2f[32];
            int tileVerticesCount = 0;


            for(int x = xmin - 1; x <= xmax + 1; x++)
            {
                if (x >= 0 && x < tileMap.MapSize.X)
                {
                    for (int y = ymin - 1; y <= ymin; y++)
                    {
                        if (y >= 0 && y < tileMap.MapSize.Y)
                        {
                            var frontTileID = tileMap.GetFrontTileID(x, y);
                            if (frontTileID != TileID.Air)
                            {
                                PlanetTileMap.TileProperty properties = GameState.TileCreationApi.GetTileProperty(frontTileID);

                                GeometryProperties geometryProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);


                                colliderPosition = physicsState.Position + box2dCollider.Offset;
                                agentVertices[3] = colliderPosition + new Vec2f(0.0f, box2dCollider.Size.Y);
                                agentVertices[2] = colliderPosition + new Vec2f(box2dCollider.Size.X, box2dCollider.Size.Y);
                                agentVertices[1] = colliderPosition + new Vec2f(box2dCollider.Size.X, 0.0f);;
                                agentVertices[0] = colliderPosition;

                                tileVerticesCount = 0;

                                for(int i = 0; i < geometryProperties.Size; i++)
                                {
                                    TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(geometryProperties.Offset + i);  
                                    Line2D line = GameState.LinePropertiesManager.GetLine(lineEnum, x, y);  
                                    Vec2f normal = GameState.LinePropertiesManager.GetNormal(lineEnum);                         

                                    if (tileVerticesCount + 2 >= tileVertices.Length)
                                    {
                                        Array.Resize(ref tileVertices, tileVertices.Length * 2);
                                    }

                                    

                                    tileVertices[tileVerticesCount] = line.A;
                                    tileVerticesCount++;
                                }

                                Vec2f[] tmpV = new Vec2f[tileVerticesCount];
                                for(int i = 0; i < tileVerticesCount; i++)
                                {
                                    tmpV[i] = tileVertices[i];
                                }

                                if (tmpV.Length == 4)
                                {

                                Vec2f tmp = tmpV[0];
                                tmpV[0] = tmpV[3];
                                tmpV[3] = tmp;
                                tmp = tmpV[1];
                                tmpV[1] = tmpV[2];
                                tmpV[2] = tmp;
                                }

                              /*  for(int i = 0; i < tmpV.Length; i++)
                                {
                                    planet.AddDebugLine(new Line2D(tmpV[i], tmpV[(i + 1 >= tileVerticesCount) ? 0 : i + 1]));
                                }*/

                                MinimumMagnitudeVector mtv = SAT.CollisionDetection(agentVertices, tmpV);
                                physicsState.Position.Y -= mtv.Axis.Y * mtv.Value;
                            }
                        }
                    }
                }
            }
        }
    }
}
