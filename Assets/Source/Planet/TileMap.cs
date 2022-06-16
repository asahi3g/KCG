﻿using System;
using Physics;
using UnityEngine;

namespace Planet
{
    public class TileMap
    {
        public Vector2Int MapSize;
        public Box2DBorders BoxBorders;
        public ChunkList Chunks;
        public Layers Layers;
        public HeightMap HeightMap;

        public TileMap(Vector2Int mapSize)
        {
            MapSize = mapSize;

            Chunks = new ChunkList(mapSize);

            BoxBorders = Vector2.zero.CreateBoxBorders(mapSize * 16);

            HeightMap = new HeightMap(MapSize);
            Layers = new Layers
            {
                LayerTextures = new Texture2D[Layers.Count],
                Tiles = new Tile.Tile[Layers.Count][],
                MapSize = mapSize
            };

            for(int layerIndex = 0; layerIndex < Layers.Count; layerIndex++)
            {
                int mapTileSize = mapSize.x * mapSize.y;
                Tile.Tile[] layerTiles = new Tile.Tile[mapTileSize];
                Layers.Tiles[layerIndex] = layerTiles;
                for(int tileIndex = 0; tileIndex < mapTileSize; tileIndex++)
                {
                    layerTiles[tileIndex] = Tile.Tile.EmptyTile;
                }
            }
        }

        public void BuildLayerTexture(Enums.Tile.MapLayerType planetLayer)
        {
            Layers.BuildLayerTexture(this, planetLayer);
        }

        #region TileApi

    
        
        public ref Tile.Tile GetTileRef(int x, int y, Enums.Tile.MapLayerType planetLayer)
        {
            return ref Layers.Tiles[(int)planetLayer][x + y * MapSize.x];
        }

        public void SetTile(int x, int y, Tile.Tile tile, Enums.Tile.MapLayerType planetLayer)
        {
            Layers.Tiles[(int)planetLayer][x + y * MapSize.x] = tile;
        }


        // placing a tile should update the tile sprite type 
        public void PlaceTile(int x, int y, Tile.Tile tile, Enums.Tile.MapLayerType planetLayer)
        {
            if (x >= 0 && x < MapSize.x && y >= 0 && y < MapSize.y)
            {
                SetTile(x, y, tile, planetLayer);

                for(int i = x - 1; i <= x + 1; i++)
                {
                    for(int j = y - 1; j <= y + 1; j++)
                    {
                        UpdateTilesOnPosition(i, j, planetLayer);
                    }
                }
            }
        }

        public void RemoveTile(int x, int y, Enums.Tile.MapLayerType planetLayer)
        {
            ref Tile.Tile tile = ref GetTileRef(x, y, planetLayer);

            tile.Type = -1;

            for(int i = x - 1; i <= x + 1; i++)
            {
                for(int j = y - 1; j <= y + 1; j++)
                {
                    UpdateTilesOnPosition(i, j, planetLayer);
                }
            }
        }

        #endregion

        #region TilePositionUpdater

        public void UpdateTilesOnPosition(int x, int y, Enums.Tile.MapLayerType planetLayer)
        {
            if (x >= 0 && x < MapSize.x && y >= 0 && y < MapSize.y)
            {
                // standard sheet mapping
                // every tile has a constant offset
                // in the sprite atlas

                // example: 15 is (3,3)
                //           8 is (0,2)
                //           1 is (1,0)
                int[] tilePositionToTileSet = {15, 12, 14, 13, 3, 0, 2, 1, 11, 8, 10, 9, 7, 4, 6, 5};
                
                ref Tile.Tile tile = ref GetTileRef(x, y, planetLayer);
                
                if (tile.Type >= 0)
                {
                    Tile.Type properties = GameState.TileCreationApi.GetTileProperties(tile.Type);
                    if (properties.AutoMapping)
                    {
                        // we have 4 neighbors per tile
                        // could be more but its 4 for now
                        // right/left/down/up
                        int[] neighbors = new int[4];

                        for(int i = 0; i < neighbors.Length; i++)
                        {
                            neighbors[i] = -1;
                        }

                        if (x + 1 < MapSize.x)
                        {
                            ref Tile.Tile neighborTile = ref GetTileRef(x + 1, y, planetLayer);
                            neighbors[(int)Enums.Tile.Neighbor.Right] = neighborTile.Type;
                        }

                        if (x - 1 >= 0)
                        {
                            ref Tile.Tile neighborTile = ref GetTileRef(x - 1, y, planetLayer);
                            neighbors[(int)Enums.Tile.Neighbor.Left] = neighborTile.Type;
                        }

                        if (y + 1 < MapSize.y)
                        {
                            ref Tile.Tile neighborTile = ref GetTileRef(x, y + 1, planetLayer);
                            neighbors[(int)Enums.Tile.Neighbor.Up] = neighborTile.Type;
                        }

                        if (y - 1 >= 0)
                        {
                            ref Tile.Tile neighborTile = ref GetTileRef(x, y - 1, planetLayer);
                            neighbors[(int)Enums.Tile.Neighbor.Down] = neighborTile.Type;
                        }


                        Enums.Tile.Position tilePosition = tile.GetTilePosition(neighbors, tile.Type);

                        // the sprite ids are next to each other in the sprite atlas
                        // we jus thave to know which one to draw based on the offset
                        tile.SpriteId = properties.BaseSpriteId + tilePositionToTileSet[(int)tilePosition];
                    }
                    else
                    {
                        tile.SpriteId = properties.BaseSpriteId;
                    }
                }
                else
                {
                    tile.SpriteId = -1;
                }
            }
        }
        public void UpdateTileMapPositions(Enums.Tile.MapLayerType planetLayer)
        {
            for(int y = 0; y < MapSize.y; y++)
            {
                for(int x = 0; x < MapSize.x; x++)
                {
                    UpdateTilesOnPosition(x, y, planetLayer);
                }
            }
        }

        #endregion
    }
}
