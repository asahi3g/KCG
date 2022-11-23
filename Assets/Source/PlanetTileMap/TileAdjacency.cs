namespace PlanetTileMap
{





    public static class TileAdjacency
    {


        public static void UpdateFrontTile(int x, int y, TileMap tileMap)
        {
            ref var tile = ref tileMap.GetTile(x, y);
            var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
            int adjacency = GetAdjacencyFromTile(x, y, tileMap);

            UnityEngine.Debug.Log("update x " + x + " : y " + y + " tile type " + tile.FrontTileID);

            UpdateTile(ref tile, properties.BlockShapeType, adjacency);
        }


        public static Collisions.TileLineSegment GetLineFromTileSide(int x, int y, Collisions.GeometryLineSide testSide, TileMap tileMap)
        {
            if (x >= 0 && x < tileMap.MapSize.X && y >= 0 && y < tileMap.MapSize.Y)
            {
                ref var tile = ref tileMap.GetTile(x, y);
                var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                var shapeProperties = GameState.GeometryCreationApi.GetProperties(properties.BlockShapeType);
                for(int lineIndex = shapeProperties.Offset; lineIndex < shapeProperties.Offset + shapeProperties.Size; lineIndex++)
                {
                    Collisions.TileLineSegment lineEnum = GameState.GeometryCreationApi.GetLine(lineIndex);
                    Collisions.GeometryLineSide side = GameState.GeometryCreationApi.GetSide(lineIndex);

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


            var shapeProperties = GameState.GeometryCreationApi.GetProperties(properties.BlockShapeType);
            for(int lineIndex = shapeProperties.Offset; lineIndex < shapeProperties.Offset + shapeProperties.Size; lineIndex++)
            {
                Collisions.TileLineSegment lineEnum = GameState.GeometryCreationApi.GetLine(lineIndex);
                Collisions.GeometryLineSide side = GameState.GeometryCreationApi.GetSide(lineIndex);

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

                UnityEngine.Debug.Log("lineEnum : " + lineEnum + " side : " + side + " testline : " + testLine);
                
                if (testLine != Collisions.TileLineSegment.Error)
                {
                    var lineProperties = GameState.LineCreationApi.GetLineProperties(lineEnum);
                    for(int matchIndex = lineProperties.MatchOffset; matchIndex < lineProperties.MatchOffset + lineProperties.MatchCount; matchIndex++)
                    {
                        var match = GameState.LineCreationApi.GetMatch(matchIndex);
                        UnityEngine.Debug.Log("match : " + match);
                        if (match == testLine)
                        {
                            result |= (int)side;
                            UnityEngine.Debug.Log("matched " + result);
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
                default:
                {
                    tile.Adjacency = Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1111;
                    break;
                }
            }

            UnityEngine.Debug.Log("conclusion : " + tile.Adjacency);
            
        }
    }
}