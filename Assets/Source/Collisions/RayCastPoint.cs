using KMath;
using System;

namespace Collisions
{



    public static partial class Collisions
    {


        public struct RayCastResult
        {
            public Vec2f Point;
            public bool Intersect;
        }

        public static RayCastResult RayCastPointAgainstTileMap(PlanetTileMap.TileMap tileMap, Line2D line)
        {
            // Form ray cast from A into B
            Vec2f vRayStart = line.A;
            Vec2f vRayDir = (line.B - line.A).Normalized;


            Vec2f vRayUnitStepSize = new Vec2f( (float)Math.Sqrt(1 + (vRayDir.Y / vRayDir.X) * (vRayDir.Y / vRayDir.X)), 
            (float)Math.Sqrt(1 + (vRayDir.X / vRayDir.Y) * (vRayDir.X / vRayDir.Y)) );
            Vec2i vMapCheck = new Vec2i((int)vRayStart.X, (int)vRayStart.Y);
            Vec2f vRayLength1D;
            Vec2i vStep;

            // Establish Starting Conditions
            if (vRayDir.X < 0)
            {
                vStep.X = -1;
                vRayLength1D.X = (vRayStart.X - (float)(vMapCheck.X)) * vRayUnitStepSize.X;
            }
            else
            {
                vStep.X = 1;
                vRayLength1D.X = ((float)(vMapCheck.X + 1) - vRayStart.X) * vRayUnitStepSize.X;
            }

            if (vRayDir.Y < 0)
            {
                vStep.Y = -1;
                vRayLength1D.Y = (vRayStart.Y - (float)(vMapCheck.Y)) * vRayUnitStepSize.Y;
            }
            else
            {
                vStep.Y = 1;
                vRayLength1D.Y = ((float)(vMapCheck.Y + 1) - vRayStart.Y) * vRayUnitStepSize.Y;
            }


            // Perform "Walk" until collision or range check
            bool bTileFound = false;
            float fMaxDistance = 100.0f;
            float fDistance = 0.0f;
            while (!bTileFound && fDistance < fMaxDistance)
            {
                // Walk along shortest path
                if (vRayLength1D.X < vRayLength1D.Y)
                {
                    vMapCheck.X += vStep.X;
                    fDistance = vRayLength1D.X;
                    vRayLength1D.X += vRayUnitStepSize.X;
                }
                else
                {
                    vMapCheck.Y += vStep.Y;
                    fDistance = vRayLength1D.Y;
                    vRayLength1D.Y += vRayUnitStepSize.Y;
                }

                // Test tile at new test point
                if (vMapCheck.X >= 0 && vMapCheck.X < tileMap.MapSize.X && vMapCheck.Y >= 0 && vMapCheck.Y < tileMap.MapSize.Y)
                {
                    Enums.Tile.TileID tileID = tileMap.GetFrontTileID(vMapCheck.X, vMapCheck.Y);
                    PlanetTileMap.TileProperty tileProperty = GameState.TileCreationApi.GetTileProperty(tileID);
                    
                    if (tileID != Enums.Tile.TileID.Air)
                    {
                        bTileFound = true;
                    }
                }
            }


            // Calculate intersection location
            Vec2f vIntersection = new Vec2f();
            if (bTileFound)
            {
                vIntersection = vRayStart + vRayDir * fDistance;
            }


            RayCastResult result = new RayCastResult();
            result.Intersect = bTileFound;
            result.Point = vIntersection;

            return result;

        }
    }
}