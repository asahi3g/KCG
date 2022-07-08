﻿

using System;
using Enums.Tile;
using KMath;
using UnityEngine;
using Utility;
using System.Collections.Generic;

namespace PlanetTileMap
{
    public struct TileMap
    {
        public static Tile AirTile = new() {ID = TileID.Air, SpriteID = -1};
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        
        FrameMesh[] LayerMeshes;
        public bool[] NeedsUpdate;
        
        public Vec2i MapSize;
        public Vec2i ChunkSize;
        List<UpdateTile> ToUpdateTiles;
        
        //Array that maps to Chunk List
        public int[] ChunkIndexLookup;
        //Store Chunks
        public Chunk[] ChunkArray;
        public int ChunkArrayLength;
        public int ChunkArrayCapacity;

        public TileMap(Vec2i mapSize)
        {
            ChunkArrayLength = 0;
            ToUpdateTiles = new List<UpdateTile>();

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
            
            LayerMeshes = new FrameMesh[LayerCount];
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
        
        public void SetTile(int x, int y, Enums.Tile.TileID tileId, MapLayerType layer)
        {
            ref Tile tile = ref GetTile(x, y, layer);
            tile.ID = tileId;
            ToUpdateTiles.Add(new UpdateTile(new Vec2i(x, y), layer));
            //UpdateTile(x, y, layer);
        }

        public ref Tile GetTile(int x, int y, MapLayerType planetLayer)
        {
            Utils.Assert(x >= 0 && x < MapSize.X &&
                         y >= 0 && y < MapSize.Y);
            
            var xChunkIndex = x / 16;
            var yChunkIndex = ((y / 16) * ChunkSize.X);
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
            
            return ref chunk.TileArray[(int)planetLayer][tileIndex];
        }
        
        public ref Tile GetBackTile(int x, int y)
        {
            return ref GetTile(x, y, MapLayerType.Back);
        }
        public ref Tile GetMidTile(int x, int y)
        {
             return ref GetTile(x, y, MapLayerType.Mid);
        }
        public ref Tile GetFrontTile(int x, int y)
        {
             return ref GetTile(x, y, MapLayerType.Front);
        }

        #endregion

        #region Tile removers

        public void RemoveBackTile(int x, int y)
        {
            ref var backTile = ref GetBackTile(x, y);
            backTile.ID = TileID.Air;
            backTile.SpriteID = -1;
            ToUpdateTiles.Add(new UpdateTile(new Vec2i(x, y), MapLayerType.Back));
            //UpdateBackTile(x, y);
        }
        public void RemoveMidTile(int x, int y)
        {
            ref var midTile = ref GetMidTile(x, y);
            midTile.ID = TileID.Air;
            midTile.SpriteID = -1;
            ToUpdateTiles.Add(new UpdateTile(new Vec2i(x, y), MapLayerType.Mid));
          //  UpdateBackTile(x, y);
        }
        public void RemoveFrontTile(int x, int y)
        {
            ref var frontTile = ref GetFrontTile(x, y);
            frontTile.ID = TileID.Air;
            frontTile.SpriteID = -1;
            ToUpdateTiles.Add(new UpdateTile(new Vec2i(x, y), MapLayerType.Front));
            //UpdateBackTile(x, y);
        }

        #endregion

        #region Tile setters

        public void SetBackTile(int x, int y, TileID tileID)
        {
           SetTile(x, y, tileID, MapLayerType.Back);
        }
        public void SetMidTile(int x, int y, TileID tileID)
        {
            SetTile(x, y, tileID, MapLayerType.Mid);
        }
        public void SetFrontTile(int x, int y, TileID tileID)
        {
            SetTile(x, y, tileID, MapLayerType.Front);
        }

        #endregion
        
        // Update data of tile, update sprites of tile and etc.
        #region Tile updater

        private void UpdateTile(int x, int y, MapLayerType type)
        {
            for(int i = x - 1; i <= x + 1; i++)
            {
                if (!IsValid(i, 0)) continue;
                for(int j = y - 1; j <= y + 1; j++)
                {
                    if (!IsValid(i, j)) continue;
                    UpdateNeighbourTiles(i, j, type);
                }
            }
        }

        public void UpdateTileMapPositions(MapLayerType planetLayer)
        {
            for(int y = 0; y < MapSize.Y; y++)
            {
                for(int x = 0; x < MapSize.X; x++)
                {
                    UpdateNeighbourTiles(x, y, planetLayer);
                }
            }
        }

        #endregion
        
        #region Tile neighbour getter


        #endregion

        // Update neighbour sprites of tiles
        #region Tile neighbour updater

        
        
        private void UpdateNeighbourTiles(int x, int y, MapLayerType planetLayer)
        {
            ref var tile = ref GetTile(x, y, planetLayer);
            
            if (tile.ID != TileID.Error)
            {
                ref var property = ref GameState.TileCreationApi.GetTileProperty(tile.ID);
                if (property.IsAutoMapping)
                {
                    if (property.SpriteRuleType == SpriteRuleType.R1)
                    {
                        TileMapping.UpdateSpriteRule_R1(x, y, planetLayer, ref this);
                    }
                    else if (property.SpriteRuleType == SpriteRuleType.R2)
                    {
                        TileMapping.UpdateSpriteRule_R2(x, y, planetLayer, ref this);
                    }
                    else if (property.SpriteRuleType == SpriteRuleType.R3)
                    {
                        TileMapping.UpdateSpriteRule_R3(x, y, planetLayer, ref this);
                    }
                }
                else
                {
                    tile.SpriteID = property.BaseSpriteId;
                }
            }
            else
            {
                tile.SpriteID = -1;
            }

            NeedsUpdate[(int) planetLayer] = true;
        }

        #endregion

        #region Layers

        public void InitializeLayerMesh(Material material, Transform transform, int drawOrder)
        {
            for (int i = 0; i < LayerCount; i++)
            {
                LayerMeshes[i] = new Utility.FrameMesh(("layerMesh" + i), material, transform,
                    GameState.TileSpriteAtlasManager.GetSpriteAtlas(0), drawOrder + i);
            }
        }
        public void DrawLayer(MapLayerType planetLayer)
        {
            Render.DrawFrame(ref LayerMeshes[(int)planetLayer], GameState.TileSpriteAtlasManager.GetSpriteAtlas(0));
        }


        public void UpdateTiles()
        {
            for(int i = 0; i < 128 && i < ToUpdateTiles.Count; i++)
            {
                UpdateTile updateTile = ToUpdateTiles[i];
                UpdateTile(updateTile.Position.X, updateTile.Position.Y, updateTile.Layer);
            }
            ToUpdateTiles.RemoveRange(0, Math.Min(128, ToUpdateTiles.Count));
            
        }

            
        public void UpdateMidLayerMesh()
        {
            UpdateLayerMesh(MapLayerType.Mid);
        }

        public void UpdateFrontLayerMesh()
        {
            UpdateLayerMesh(MapLayerType.Front);
           
        }

        public void UpdateLayerMesh(MapLayerType planetLayer)
        {
            if (Camera.main==null) {Debug.LogError("Camera.main not found, failed to create edge colliders"); return;}

            var cam = Camera.main;
            if (!cam.orthographic) {Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return;}

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
            
            LayerMeshes[(int)planetLayer].Clear();

            int index = 0;
            for (int y = (int)(bottomLeft.y - 10); y < MapSize.Y&& y <= (topRight.y + 10); y++)
            {
                for (int x = (int)(bottomLeft.x - 10); x < MapSize.X && x <= (bottomRight.x + 10); x++)
                {
                    if (x >= 0  && y >= 0)
                    {
                    ref var tile = ref GetTile(x, y, planetLayer);

                    var spriteId = tile.SpriteID;

                    if (spriteId >= 0)
                    {
                        Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(spriteId).TextureCoords;

                        const float width = 1;
                        const float height = 1;

                        if (!Utility.ObjectMesh.isOnScreen(x, y))
                            continue;

                        // Update UVs
                        LayerMeshes[(int)planetLayer].UpdateUV(textureCoords, (index) * 4);
                        // Update Vertices
                        LayerMeshes[(int)planetLayer].UpdateVertex((index * 4), x, y, width, height);
                        index++;
                    }
                    }
                }
            }
        }

       
    }
}

#endregion