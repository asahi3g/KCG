using KMath;
using System;
using UnityEngine;

namespace Collisions
{
    public static partial class Collisions
    {
        /// <summary>
        /// DDA Algorithm ==============================================
		/// https://lodev.org/cgtutor/raycasting.html
        /// </summary>
        public static RayCastResult RayCastAgainstTileMapBox2d(PlanetTileMap.TileMap tileMap, Line2D line, float width, float height)
        {
            if (line.A == line.B)
                return new RayCastResult();

            // Form ray cast from A into B
            Vec2f vRayStart = line.A;
            Vec2f offset = Vec2f.Zero;
            Vec2f vRayDir = (line.B - line.A).Normalized;

            Vec2f vRayUnitStepSize = new Vec2f( (float)Math.Sqrt(1 + (vRayDir.Y / vRayDir.X) * (vRayDir.Y / vRayDir.X)),
            (float)Math.Sqrt(1 + (vRayDir.X / vRayDir.Y) * (vRayDir.X / vRayDir.Y)) );

            if (vRayUnitStepSize.X != vRayUnitStepSize.X) // This returns true if vRayUnitStepSize.X is a float.NAN
                vRayUnitStepSize.X = 0;
            if (vRayUnitStepSize.Y != vRayUnitStepSize.Y)
                vRayUnitStepSize.Y = 0;

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
                offset.X = width;
                vRayStart.X += offset.X;
                vMapCheck.X = (int)vRayStart.X;
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
                offset.Y = height;
                vRayStart.Y += offset.Y;
                vMapCheck.Y = (int)vRayStart.Y;
                vStep.Y = 1;
                vRayLength1D.Y = ((float)(vMapCheck.Y + 1) - vRayStart.Y) * vRayUnitStepSize.Y;
            }


            // Perform "Walk" until collision or range check
            bool bTileFound = false;
            Vec2f surfaceNormal = new Vec2f();
            float fMaxDistance = 100.0f;
            float fDistance = 0.0f;
            int maxIterations = 1000;
            int i = 0;
            while (fDistance < fMaxDistance && !bTileFound && i < maxIterations)
            {
                i++;
                bool stepXdir = false; // False if step y and true if step x.
                
                // Walk along shortest path
                if (vRayLength1D.X < vRayLength1D.Y)
                {
                    stepXdir = false;
                    vMapCheck.X += vStep.X;
                    fDistance = vRayLength1D.X;
                    vRayLength1D.X += vRayUnitStepSize.X;
                }
                else
                {
                    stepXdir = true;
                    vMapCheck.Y += vStep.Y;
                    fDistance = vRayLength1D.Y;
                    vRayLength1D.Y += vRayUnitStepSize.Y;
                }

                if (fDistance > (line.B - line.A).Magnitude)
                    break;

                Vec2f currentPoint = vRayStart + vRayDir * fDistance - offset;
                Vec2f limit = currentPoint + new Vec2f(width, height);

                if ((currentPoint.X < 0 && limit.X < 0) ||
                    (currentPoint.Y < 0 && limit.Y < 0) ||
                    (currentPoint.X >= tileMap.MapSize.X && limit.X >= tileMap.MapSize.X) ||
                    (currentPoint.Y >= tileMap.MapSize.Y && limit.Y >= tileMap.MapSize.Y))
                {
                    continue;
                }

                int x = (int)currentPoint.X;
                int y = (int)currentPoint.Y;
                if (stepXdir)
                    y = vMapCheck.Y;

                else
                    x = vMapCheck.X;

                while (true)
                {
                    if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
                    {
                        Enums.Tile.TileID tileID = tileMap.GetFrontTileID(x, y);
                        PlanetTileMap.TileProperty tileProperty = GameState.TileCreationApi.GetTileProperty(tileID);
                        if (tileID != Enums.Tile.TileID.Air)
                        {
                            float diffx = (x + 0.5f) - currentPoint.X;
                            float diffy = (y + 0.5f) - currentPoint.Y;

                            if (Math.Abs(diffx) > Math.Abs(diffy))
                            {
                                if (diffx > 0)
                                    surfaceNormal = new Vec2f(-1.0f, 0.0f);
                                else
                                    surfaceNormal = new Vec2f(1.0f, 0.0f);
                            }
                            else
                            {
                                if (diffy > 0)
                                    surfaceNormal = new Vec2f(0.0f, -1.0f);
                                else
                                    surfaceNormal = new Vec2f(0.0f, 1.0f);
                            }

                            bTileFound = true;
                            break;
                        }
                    }

                    if (stepXdir)
                    {
                        x++;
                        if (x > (int)limit.X)
                            break;
                    }
                    else
                    {
                        y++;
                        if (y > (int)limit.Y)
                            break;
                    }
                }
            }

            // Calculate intersection location
            Vec2f vIntersection = new Vec2f();
            if (bTileFound)
            {
                vIntersection = vRayStart + vRayDir * fDistance;
                vIntersection -= offset;
            }

            RayCastResult result = new RayCastResult();
            result.Intersect = bTileFound;
            result.Point = vIntersection;
            result.Normal = surfaceNormal;

            return result;
        }
    }
}
