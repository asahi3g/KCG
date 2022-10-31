namespace PlanetTileMap
{



    public class TileMapGeometry
    {



        public static bool FindLine(Enums.GeometryTileShape shape, Collisions.TileLineSegment line)
        {
            var properties = GameState.GeometryCreationApi.GetProperties(shape);
            for(int i = 0; i < properties.Size; i++)
            {
                Collisions.TileLineSegment testLine = GameState.GeometryCreationApi.GetLine(i + properties.Offset);

                if (testLine == line)
                {
                    return true;
                }
            }
            return false;
        }


        public static bool FindLine(Enums.GeometryTileShape shape, Collisions.TileLineSegment line1, Collisions.TileLineSegment line2)
        {
            if (shape != Enums.GeometryTileShape.Error)
            {
                var properties = GameState.GeometryCreationApi.GetProperties(shape);
                for(int i = 0; i < properties.Size; i++)
                {
                    Collisions.TileLineSegment testLine = GameState.GeometryCreationApi.GetLine(i + properties.Offset);

                    if (testLine == line1 || testLine == line2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void BuildGeometry(TileMap tileMap)
        {
            tileMap.GeometryArrayCount = 0;
            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for(int i = 0; i < tileMap.MapSize.X; i++)
                {
                    Enums.PlanetTileMap.TileID thisTile = tileMap.GetFrontTileID(i, j);
                    var properties = GameState.TileCreationApi.GetTileProperty(thisTile);

                    switch (properties.BlockShapeType)
                    {
                        case Enums.GeometryTileShape.SB_R0:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.SB_R0;
                            bool topLine = true;
                            bool rightLine = true;
                            bool leftLine = true;
                             bool bottomLine = true;


                            Enums.GeometryTileShape topGeometry = tileMap.GetFrontTileGeometry(i, j + 1);
                            if (FindLine(topGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                topLine = false;
                            }

                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape leftGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(leftGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C0_C1, Collisions.TileLineSegment.L_C1_C0))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C0_C1, i, j), 
                                GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C0_C1), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C1_C2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C1_C2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C2_C3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C2_C3), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C3_C0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C3_C0), shape);
                            }
                            break;
                        }

                        case Enums.GeometryTileShape.TB_R0:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.TB_R0;
                            bool topLine = true;
                            bool rightLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C3_C1, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C3_C1), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C1_C2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C1_C2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C2_C3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C2_C3), shape);
                            }

                            break;
                        }


                        case Enums.GeometryTileShape.TB_R1:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.TB_R1;
                            bool topLine = true;
                            bool leftLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C0_C2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C0_C2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C2_C3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C2_C3), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C3_C0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C3_C0), shape);
                            }



                            break;
                        }



                        case Enums.GeometryTileShape.HB_R0:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.HB_R0;
                            bool topLine = true;
                            bool rightLine = true;
                            bool leftLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape topGeometry = tileMap.GetFrontTileGeometry(i, j + 1);
                            if (FindLine(topGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                topLine = false;
                            }

                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape leftGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(leftGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C0_C1, Collisions.TileLineSegment.L_C1_C0))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C0_M0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C0_M0), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M0_M2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M0_M2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M2_C3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M2_C3), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C3_C0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C3_C0), shape);
                            }
                            break;
                        }



                        case Enums.GeometryTileShape.HB_R1:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.HB_R1;
                            bool topLine = true;
                            bool rightLine = true;
                            bool leftLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape topGeometry = tileMap.GetFrontTileGeometry(i, j + 1);
                            if (FindLine(topGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                topLine = false;
                            }

                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape leftGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(leftGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C0_C1, Collisions.TileLineSegment.L_C1_C0))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C0_C1, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C0_C1), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C1_M1, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C1_M1), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M1_M3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M1_M3), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M3_C0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M3_C0), shape);
                            }
                            break;
                        }




                        case Enums.GeometryTileShape.HB_R2:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.HB_R2;
                            bool topLine = true;
                            bool rightLine = true;
                            bool leftLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape topGeometry = tileMap.GetFrontTileGeometry(i, j + 1);
                            if (FindLine(topGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                topLine = false;
                            }

                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape leftGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(leftGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C0_C1, Collisions.TileLineSegment.L_C1_C0))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M0_C1, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M0_C1), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C1_C2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C1_C2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C2_M2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C2_M2), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M2_M0, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M2_M0), shape);
                            }
                            break;
                        }


                        case Enums.GeometryTileShape.HB_R3:
                        {
                            Enums.GeometryTileShape shape = Enums.GeometryTileShape.HB_R3;
                            bool topLine = true;
                            bool rightLine = true;
                            bool leftLine = true;
                            bool bottomLine = true;


                            Enums.GeometryTileShape topGeometry = tileMap.GetFrontTileGeometry(i, j + 1);
                            if (FindLine(topGeometry, Collisions.TileLineSegment.L_C2_C3, Collisions.TileLineSegment.L_C3_C2))
                            {
                                topLine = false;
                            }

                            Enums.GeometryTileShape rightGeometry = tileMap.GetFrontTileGeometry(i + 1, j);
                            if (FindLine(rightGeometry, Collisions.TileLineSegment.L_C3_C0, Collisions.TileLineSegment.L_C0_C3))
                            {
                                rightLine = false;
                            }

                            Enums.GeometryTileShape leftGeometry = tileMap.GetFrontTileGeometry(i - 1, j);
                            if (FindLine(leftGeometry, Collisions.TileLineSegment.L_C1_C2, Collisions.TileLineSegment.L_C2_C1))
                            {
                                leftLine = false;
                            }

                            Enums.GeometryTileShape bottomGeometry = tileMap.GetFrontTileGeometry(i, j - 1);
                            if (FindLine(bottomGeometry, Collisions.TileLineSegment.L_C0_C1, Collisions.TileLineSegment.L_C1_C0))
                            {
                                bottomLine = false;
                            }


                            if (topLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M3_M1, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M3_M1), shape);
                            }

                            if (rightLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_M1_C2, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_M1_C2), shape);
                            }

                            if (bottomLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C2_C3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C2_C3), shape);
                            }

                            if (leftLine)
                            {
                                tileMap.AddGeometryLine(GameState.LineCreationApi.GetLine(Collisions.TileLineSegment.L_C3_M3, i, j),
                                 GameState.LineCreationApi.GetNormal(Collisions.TileLineSegment.L_C3_M3), shape);
                            }
                            break;
                        }



                        
                    }
                }
            }
        }
    }
}