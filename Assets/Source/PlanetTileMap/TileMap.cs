using Collisions;
using Enums.PlanetTileMap;
using KMath;
using Utility;

namespace PlanetTileMap
{
    // TODO: go through map chunk implementation and do partial allocation
    // do partial allocation of chunks, instead of full allocation
    // remove chunk type
    public class TileMap
    {
        public Vec2i MapSize;
        public int ChunkSizeX;
        public int ChunkSizeY;
        TileSpriteUpdateQueue TileSpriteUpdateQueue;
        
        //Array that maps to Chunk List
        public int[] ChunkIndexLookup;
        //Store Chunks
        public Chunk[] ChunkArray;
        public int ChunkArrayLength;
        public int ChunkArrayCapacity;

        public Line2D[] GeometryArray;
        public Vec2f[] GeometryNormalArray;
        public Enums.TileGeometryAndRotation[] GeometryShapeArray;
        public int GeometryArrayCount = 0;

        public TileMap(Vec2i mapSize)
        {
            ChunkArrayLength = 0;
            TileSpriteUpdateQueue = new TileSpriteUpdateQueue();

            // >> 4 == / 16
            // & 0x0F == & 15
            // 17 & 15 = 1
            // 16 & 15 = 0
            ChunkSizeX = mapSize.X >> 4;
            ChunkSizeY = mapSize.Y >> 4;
            Utils.Assert((mapSize.X & 0x0F) == 0);
            Utils.Assert((mapSize.Y & 0x0F) == 0);

            ChunkArrayCapacity = ChunkSizeX * ChunkSizeY;
            ChunkIndexLookup = new int[ChunkArrayCapacity];
            ChunkArray = new Chunk[ChunkArrayCapacity];

            // Initialize all chunks. They all be empty
            for (int chunkIndex = 0; chunkIndex < ChunkArray.Length; chunkIndex++)
            {
                ChunkArray[chunkIndex].Type = MapChunkType.Empty;
                ChunkArray[chunkIndex].TileArray = new Tile[256];

                var tileArrayLength = ChunkArray[chunkIndex].TileArray.Length;

                // For each tile...
                for (int tileIndex = 0; tileIndex < tileArrayLength; tileIndex++)
                {
                    // ... initialize air tile
                    ref var tile = ref ChunkArray[chunkIndex].TileArray[tileIndex];
                    tile.BackTileID = TileID.Air;
                    tile.BackTileSpriteID = -1;
                    
                    tile.MidTileID = TileID.Air;
                    tile.MidTileSpriteID = -1;
                    
                    tile.FrontTileID = TileID.Air;
                    tile.FrontTileSpriteID = -1;
                }
            
                ChunkArrayLength++;
            }

            MapSize = mapSize;

            GeometryArray = new Line2D[1024];
            GeometryNormalArray = new Vec2f[1024];
            GeometryShapeArray = new Enums.TileGeometryAndRotation[1024];
            GeometryArrayCount = 0;
        }


        
        public void AddGeometryLine(Line2D line, Vec2f normal, Enums.TileGeometryAndRotation shape)
        {
            if (GeometryArrayCount + 1 >= GeometryArray.Length)
            {
                System.Array.Resize(ref GeometryArray, GeometryArray.Length + 1024);
                System.Array.Resize(ref GeometryNormalArray, GeometryArray.Length + 1024);
                System.Array.Resize(ref GeometryShapeArray, GeometryArray.Length + 1024);
            }

            GeometryShapeArray[GeometryArrayCount] = shape;
            GeometryNormalArray[GeometryArrayCount] = normal;
            GeometryArray[GeometryArrayCount++] = line;
        }

        public Enums.TileGeometryAndRotation GetFrontTileGeometry(int x, int y)
        {
            if (x >= 0 && x < MapSize.X && y >= 0 && y < MapSize.Y)
            {
                TileID tile = GetFrontTileID(x, y);
                var properties = GameState.TileCreationApi.GetTileProperty(tile);
                return properties.BlockShapeType;
            }
            else
            {
                return Enums.TileGeometryAndRotation.Error;
            }
        }
        
        // Checks if position is inside Map Size
        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < MapSize.X &&
                   y >= 0 && y < MapSize.Y;
        }

        public ref Tile GetTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;
            
            return ref chunk.TileArray[tileIndex];
        }
        
        #region Tile ID getters

        public TileID GetBackTileID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.BackTileID;
        }
        public TileID GetMidTileID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.MidTileID;
        }
        public TileID GetFrontTileID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.FrontTileID;
        }

        #endregion
        
        #region Tile Sprite ID getters

        public int GetBackTileSpriteID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.BackTileSpriteID;
        }
        public int GetMidTileSpriteID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.MidTileSpriteID;
        }
        public int GetFrontTileSpriteID(int x, int y)
        {
            ref var tile = ref GetTile(x, y);
            return tile.FrontTileSpriteID;
        }

        #endregion

        #region Tile removers

        public void RemoveBackTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;

            chunk.TileArray[tileIndex].BackTileID = TileID.Air;
            chunk.TileArray[tileIndex].BackTileSpriteID = -1;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Back);
        }
        public void RemoveMidTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;

            chunk.TileArray[tileIndex].MidTileID = TileID.Air;
            chunk.TileArray[tileIndex].MidTileSpriteID = -1;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Mid);
        }
        public void RemoveFrontTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;

            chunk.TileArray[tileIndex].FrontTileID = TileID.Air;
            chunk.TileArray[tileIndex].FrontTileSpriteID = -1;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Front);
        }

        #endregion

        #region Tile setters

        public void SetBackTile(int x, int y, TileID backTileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[tileIndex].BackTileID = backTileID;
            chunk.TileArray[tileIndex].BackTileSpriteID = GameState.TileCreationApi.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Back);
        }
        public void SetMidTile(int x, int y, TileID midTileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            Utils.Assert(chunk.Type != MapChunkType.Error);

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[tileIndex].MidTileID = midTileID;
            chunk.TileArray[tileIndex].MidTileSpriteID = GameState.TileCreationApi.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Mid);
        }
        public void SetFrontTile(int x, int y, TileID frontTileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            Utils.Assert(chunk.Type != MapChunkType.Error);
            
            if (frontTileID != TileID.Air)
            {
                chunk.Type = MapChunkType.NotEmpty;
            }

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[tileIndex].FrontTileID = frontTileID;
            chunk.TileArray[tileIndex].FrontTileSpriteID = GameState.TileCreationApi.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;

            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Front);
        }

        #endregion

        // Update neighbour sprites of tiles
        #region Tile neighbour updater

        // updates all the sprite ids in the layer
        public void UpdateBackTileMapPositions(int x, int y)
        {
            TileSpriteUpdate.UpdateBackTileMapPositions(this, x, y);
        }

        // updates all the sprite ids in the layer
        public void UpdateMidTileMapPositions(int x, int y)
        {
            TileSpriteUpdate.UpdateMidTileMapPositions(this, x, y);
        }


        // updates all the sprite ids in the layer
        public void UpdateFrontTileMapPositions(int x, int y)
        {
            TileSpriteUpdate.UpdateFrontTileMapPositions(this, x, y);
        }
        
        // this is called every frame to update a limited number of sprite ids
        // the excess will be pushed to the next frame
        public void UpdateTileSprites()
        {
            TileSpriteUpdateQueue.UpdateTileSprites(this);
        }


        #endregion
    }
}
