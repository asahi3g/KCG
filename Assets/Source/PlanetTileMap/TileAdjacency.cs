namespace PlanetTileMap
{





    public static class TileAdjacency
    {


        public static void UpdateFrontTile(int x, int y, TileMap tileMap)
        {
            ref var tile = ref tileMap.GetTile(x, y);
            var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
            int adjacency = GetAdjacencyFromTile(x, y, tileMap);

            UpdateTile(ref tile, properties.BlockShapeType, adjacency);
        }


        public static Collisions.TileLineSegment GetLineFromTileSide(int x, int y, Collisions.GeometryLineSide testSide, TileMap tileMap)
        {
            if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
            {
                ref var tile = ref tileMap.GetTile(x, y);
                var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                var shapeProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);
                for(int lineIndex = shapeProperties.Offset; lineIndex < shapeProperties.Offset + shapeProperties.Size; lineIndex++)
                {
                    Collisions.TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(lineIndex);
                    Collisions.GeometryLineSide side = GameState.GeometryPropertiesManager.GetSide(lineIndex);

                    if (testSide == side)
                    {
                        return lineEnum;
                    }
                }
            }

            return Collisions.TileLineSegment.Error;
        }

        public static int GetAdjacencyFromTile(int x, int y, TileMap tileMap)
        {
            int result = 0;

            ref var tile = ref tileMap.GetTile(x, y);
            var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);


            var shapeProperties = GameState.GeometryPropertiesManager.GetProperties(properties.BlockShapeType);
            for(int lineIndex = shapeProperties.Offset; lineIndex < shapeProperties.Offset + shapeProperties.Size; lineIndex++)
            {
                Collisions.TileLineSegment lineEnum = GameState.GeometryPropertiesManager.GetLine(lineIndex);
                Collisions.GeometryLineSide side = GameState.GeometryPropertiesManager.GetSide(lineIndex);

                Collisions.TileLineSegment testLine = Collisions.TileLineSegment.Error;
                switch(side)
                {
                    case Collisions.GeometryLineSide.Right:
                    {
                        testLine = GetLineFromTileSide(x + 1, y, Collisions.GeometryLineSide.Left, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.Left:
                    {
                        testLine = GetLineFromTileSide(x - 1, y, Collisions.GeometryLineSide.Right, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.Top:
                    {
                        testLine = GetLineFromTileSide(x, y + 1, Collisions.GeometryLineSide.Bottom, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.Bottom:
                    {
                        testLine = GetLineFromTileSide(x, y - 1, Collisions.GeometryLineSide.Top, tileMap);
                        break;
                    }
                }
                
                if (testLine != Collisions.TileLineSegment.Error)
                {
                    var lineProperties = GameState.LinePropertiesManager.GetLineProperties(lineEnum);
                    for(int matchIndex = lineProperties.MatchOffset; matchIndex < lineProperties.MatchOffset + lineProperties.MatchCount; matchIndex++)
                    {
                        var match = GameState.LinePropertiesManager.GetMatch(matchIndex);
                        if (match == testLine)
                        {
                            result |= (int)side;
                            break;
                        }
                    }
                }
            }

            return result;
        }


        public static void UpdateTile(ref Tile tile, Enums.TileGeometryAndRotation shape, int adjacency)
        {
            switch(shape)
            {
                case Enums.TileGeometryAndRotation.SB_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0000 + adjacency);

                    break;
                }


                case Enums.TileGeometryAndRotation.FP_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.FP_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.FP_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.FP_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.FP_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.FP_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.FP_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.FP_R3_A0000 + adjacency);

                    break;
                }


                case Enums.TileGeometryAndRotation.HB_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HB_R0_A0X00 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HB_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HB_R1_A00X0 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HB_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HB_R2_A000X + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HB_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HB_R3_AX000 + adjacency);

                    break;
                }




                case Enums.TileGeometryAndRotation.HP_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HP_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HP_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HP_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HP_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HP_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.HP_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.HP_R3_A0000 + adjacency);

                    break;
                }


                case Enums.TileGeometryAndRotation.L1_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R3_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R4:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R4_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R5:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R5_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R6:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R6_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L1_R7:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L1_R7_A0000 + adjacency);

                    break;
                }



                case Enums.TileGeometryAndRotation.L2_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R4:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R5:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R6:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.L2_R7:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.L2_R7_A0000 + adjacency);

                    break;
                }



                case Enums.TileGeometryAndRotation.QP_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.QP_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.QP_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.QP_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.QP_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.QP_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.QP_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.QP_R3_A0000 + adjacency);

                    break;
                }
           


                case Enums.TileGeometryAndRotation.TB_R0:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.TB_R1:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.TB_R2:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A0000 + adjacency);

                    break;
                }
                case Enums.TileGeometryAndRotation.TB_R3:
                {
                    tile.Adjacency = (Enums.TileGeometryAndRotationAndAdjacency)((int)Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0000 + adjacency);

                    break;
                }
                default:
                {
                    tile.Adjacency = Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1111;
                    break;
                }
            }
            
        }
    }
}