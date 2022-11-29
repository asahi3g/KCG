namespace PlanetTileMap
{





    public static class TileAdjacency
    {


        public static void UpdateFrontTile(int x, int y, TileMap tileMap)
        {
            ref var tile = ref tileMap.GetTile(x, y);
            var properties = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
            int adjacency = GetAdjacencyFromTile(x, y, tileMap);
         

            UpdateTile(ref tile, properties.BlockShapeType, (Enums.TileAdjacency)adjacency);
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
                    case Collisions.GeometryLineSide.North:
                    {
                        testLine = GetLineFromTileSide(x, y + 1, Collisions.GeometryLineSide.South, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.East:
                    {
                        testLine = GetLineFromTileSide(x + 1, y, Collisions.GeometryLineSide.West, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.South:
                    {
                        testLine = GetLineFromTileSide(x, y - 1, Collisions.GeometryLineSide.North, tileMap);
                        break;
                    }
                    case Collisions.GeometryLineSide.West:
                    {
                        testLine = GetLineFromTileSide(x - 1, y, Collisions.GeometryLineSide.East, tileMap);
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


        public static void UpdateTile(ref Tile tile, Enums.TileGeometryAndRotation shape, Enums.TileAdjacency adjacency)
        {
            int ShapeMask = GameState.GeometryPropertiesManager.GetProperties(shape).Mask;
            tile.Adjacency  = GameState.GeometryPropertiesManager.GetAdjacency(shape, (Enums.TileAdjacency)((int)adjacency & ShapeMask));
        }
    }

}