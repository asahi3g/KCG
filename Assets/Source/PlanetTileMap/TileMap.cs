﻿

using System;
using Enums.Tile;
using KMath;
using UnityEngine;
using Utility;
using System.Collections.Generic;

namespace PlanetTileMap
{
    public class TileMap
    {
        public static Tile AirTile = new() {ID = TileID.Air, SpriteID = -1};
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        
        public bool[] NeedsUpdate;
        
        public Vec2i MapSize;
        public Vec2i ChunkSize;
        TileSpriteUpdateQueue TileSpriteUpdateQueue;
        
        //Array that maps to Chunk List
        public int[] ChunkIndexLookup;
        //Store Chunks
        public Chunk[] ChunkArray;
        public int ChunkArrayLength;
        public int ChunkArrayCapacity;

        public TileMap(Vec2i mapSize)
        {
            ChunkArrayLength = 0;
            TileSpriteUpdateQueue = new TileSpriteUpdateQueue();

            ChunkSize = new Vec2i(mapSize.X / 16 + 1, mapSize.Y / 16 + 1);
            
            ChunkArrayCapacity = ChunkSize.X * ChunkSize.Y;
            ChunkIndexLookup = new int[ChunkArrayCapacity];
            ChunkArray = new Chunk[ChunkArrayCapacity];

            // Initialize all chunks. They all be empty
            for (int chunkIndex = 0; chunkIndex < ChunkArray.Length; chunkIndex++)
            {
                ChunkArray[chunkIndex].Type = MapChunkType.Empty;
                ChunkArray[chunkIndex].TileArray = new Tile[LayerCount][];

                var layerLength = ChunkArray[chunkIndex].TileArray.Length;

                // For each layer...
                for (int layerIndex = 0; layerIndex < layerLength; layerIndex++)
                {
                    // ... create new tile array and...
                    ref var layer = ref ChunkArray[chunkIndex].TileArray[layerIndex];
                    layer = new Tile[256];
                    // ... for each tile in layer of tile array...
                    for (int tileIndex = 0; tileIndex < layer.Length; tileIndex++)
                    {
                        // ... set tile to Air
                        layer[tileIndex].ID = TileID.Air;
                        layer[tileIndex].SpriteID = -1;
                    }
                }
            
                ChunkArrayLength++;
            }

            MapSize = mapSize;
            
            NeedsUpdate = new bool[LayerCount];

            for(int layerIndex = 0; layerIndex < LayerCount; layerIndex++)
            {
                NeedsUpdate[layerIndex] = true;
            }
        }
        
        /// <summary>
        /// Checks if position is inside Map Size
        /// </summary>
        /// <param name="x">TileMap coordinates</param>
        /// <param name="y">TileMap coordinates</param>
        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < MapSize.X &&
                   y >= 0 && y < MapSize.Y;
        }

        #region Tile getters

        public ref Tile GetBackTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                return ref AirTile;
            }
            
            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;
            
            return ref chunk.TileArray[(int)MapLayerType.Back][tileIndex];
        }
        public ref Tile GetMidTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                return ref AirTile;
            }
            
            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;
            
            return ref chunk.TileArray[(int)MapLayerType.Mid][tileIndex];
        }
        public ref Tile GetFrontTile(int x, int y)
        {
            Utils.Assert(IsValid(x, y));
            
            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = (xChunkIndex + yChunkIndex);

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                return ref AirTile;
            }
            
            var xIndex = x & 0x0f;
            var yIndex = y & 0x0f;
            var tileIndex = xIndex + (yIndex << 4);
            
            chunk.ReadCount++;
            
            return ref chunk.TileArray[(int)MapLayerType.Front][tileIndex];
        }

        #endregion

        #region Tile removers

        public void RemoveBackTile(int x, int y)
        {
            ref var backTile = ref GetBackTile(x, y);
            backTile.ID = TileID.Air;
            backTile.SpriteID = -1;
            backTile.SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Back);
            //UpdateBackTile(x, y);
        }
        public void RemoveMidTile(int x, int y)
        {
            ref var midTile = ref GetMidTile(x, y);
            midTile.ID = TileID.Air;
            midTile.SpriteID = -1;
            midTile.SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Mid);
          //  UpdateBackTile(x, y);
        }
        public void RemoveFrontTile(int x, int y)
        {
            ref var frontTile = ref GetFrontTile(x, y);
            frontTile.ID = TileID.Air;
            frontTile.SpriteID = -1;
            frontTile.SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            TileSpriteUpdateQueue.Add(x, y, MapLayerType.Front);
            //UpdateBackTile(x, y);
        }

        #endregion

        #region Tile setters

        public void SetBackTile(int x, int y, TileID tileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                if (tileID != TileID.Air) chunk.Type = MapChunkType.NotEmpty;
            }

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[(int) MapLayerType.Back][tileIndex].ID = tileID;
            chunk.TileArray[(int) MapLayerType.Back][tileIndex].SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;
        }
        public void SetMidTile(int x, int y, TileID tileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                if (tileID != TileID.Air) chunk.Type = MapChunkType.NotEmpty;
            }

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[(int) MapLayerType.Mid][tileIndex].ID = tileID;
            chunk.TileArray[(int) MapLayerType.Mid][tileIndex].SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;
        }
        public void SetFrontTile(int x, int y, TileID tileID)
        {
            Utils.Assert(IsValid(x, y));

            var xChunkIndex = x >> 4;
            var yChunkIndex = ((y >> 4) * MapSize.X) >> 4;
            var chunkIndex = xChunkIndex + yChunkIndex;

            ref var chunk = ref ChunkArray[chunkIndex];
            
            if (chunk.Type == MapChunkType.Error)
            {
                if (tileID != TileID.Air) chunk.Type = MapChunkType.NotEmpty;
            }

            var xTileIndex = x & 0x0f;
            var yTileIndex = y & 0x0f;
            var tileIndex = xTileIndex + (yTileIndex << 4);

            chunk.TileArray[(int) MapLayerType.Front][tileIndex].ID = tileID;
            chunk.TileArray[(int) MapLayerType.Front][tileIndex].SpriteID = GameResources.LoadingTilePlaceholderSpriteId;
            chunk.Sequence++;
        }

        #endregion

        // Update neighbour sprites of tiles
        #region Tile neighbour updater

        public void UpdateBackTile(int x, int y)
        {
            TileSpriteUpdate.UpdateBackTile(x, y, this);
        }

        // updates all the sprite ids in the layer
        public void UpdateBackTileMapPositions()
        {
            TileSpriteUpdate.UpdateBackTileMapPositions(this);
        }

        public void UpdateMidTile(int x, int y)
        {
            TileSpriteUpdate.UpdateMidTile(x, y, this);
        }

        // updates all the sprite ids in the layer
        public void UpdateMidTileMapPositions()
        {
            TileSpriteUpdate.UpdateMidTileMapPositions(this);
        }


        public void UpdateFrontTile(int x, int y)
        {
            TileSpriteUpdate.UpdateFrontTile(x, y, this);
        }

        // updates all the sprite ids in the layer
        public void UpdateFrontTileMapPositions()
        {
            TileSpriteUpdate.UpdateFrontTileMapPositions(this);
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
