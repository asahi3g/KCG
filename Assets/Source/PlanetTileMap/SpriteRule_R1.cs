using Enums.Tile;
using System;


namespace PlanetTileMap
{


    public static class SpriteRule_R1
    {

         // TODO: Refactor
         public static TilePosition GetTilePosition(MaterialType[] neighbors, MaterialType materialType)
         {
             int biggestMatch = 0;
             TilePosition tilePosition = 0;
 
             // we have 16 different values for the spriteId
             foreach(var position in (TilePosition[])Enum.GetValues(typeof(TilePosition)))
             {
                 int match = CheckTile(neighbors, position, materialType);
 
                 // pick only tiles with the biggest match count
                 if (match > biggestMatch)
                 {
                     biggestMatch = match;
                     tilePosition = position;
                 }
             }
 
             return tilePosition;
         }
 
 
 
         // TODO: Refactor
         public static int CheckTile(MaterialType[] neighbors, TilePosition rules, MaterialType materialType)
         {
             // 16 different values can be stored
             // using only 4 bits for the
             // adjacent tiles 
 
             int[] neighborBit = {
                 0x1, 0x2, 0x4, 0x8
             };
 
             int match = 0;
             // number of total neighbors is 4 right/left/down/up
             for(int i = 0; i < neighbors.Length; i++)
             {
                 // check if we have to have the same tileId
                 // in this particular neighbor                      
                 if (((int)rules & neighborBit[i]) == neighborBit[i])
                 {
                     // if this neighbor does not match return -1 immediately
                     if (neighbors[i] != materialType) return -1;
                     match++;
                 }
             }
 
 
             return match;
         }
 
         public static void UpdateBackSprite(int x, int y, TileMap tileMap)
         {
             ref var tile = ref tileMap.GetTile(x, y);
             ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.BackTileID);
             
             // standard sheet mapping
             // every tile has a constant offset
             // in the sprite atlas
 
             // example: 15 is (3,3)
             //           8 is (0,2)
             //           1 is (1,0)
             int[] tilePositionToTileSet = {15, 12, 14, 13, 3, 0, 2, 1, 11, 8, 10, 9, 7, 4, 6, 5};
 
             // we have 4 neighbors per tile
             // could be more but its 4 for now
             // right/left/down/up
             var neighbors = new MaterialType[4];
 
             for (int i = 0; i < neighbors.Length; i++)
             {
                 neighbors[i] = MaterialType.Air;
             }
 
             if (x + 1 < tileMap.MapSize.X)
             {
                 ref var neighborTile = ref tileMap.GetTile(x + 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.BackTileID).MaterialType;
                 neighbors[(int) Neighbor.Right] = neighborMaterialType;
             }
 
             if (x - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x - 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.BackTileID).MaterialType;
                 neighbors[(int) Neighbor.Left] = neighborMaterialType;
             }
 
             if (y + 1 < tileMap.MapSize.Y)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y + 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.BackTileID).MaterialType;
                 neighbors[(int) Neighbor.Up] = neighborMaterialType;
             }
 
             if (y - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y - 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.BackTileID).MaterialType;
                 neighbors[(int) Neighbor.Down] = neighborMaterialType;
             }
 
 
             var tilePosition = GetTilePosition(neighbors, tileProperty.MaterialType);
 
             // the sprite ids are next to each other in the sprite atlas
             // we just have to know which one to draw based on the offset
             tile.BackTileSpriteID = tileProperty.BaseSpriteId + tilePositionToTileSet[(int) tilePosition];
         }
 
 
 
 
         public static void UpdateMidSprite(int x, int y, TileMap tileMap)
         {
             ref var tile = ref tileMap.GetTile(x, y);
             ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.MidTileID);
             
             // standard sheet mapping
             // every tile has a constant offset
             // in the sprite atlas
 
             // example: 15 is (3,3)
             //           8 is (0,2)
             //           1 is (1,0)
             int[] tilePositionToTileSet = {15, 12, 14, 13, 3, 0, 2, 1, 11, 8, 10, 9, 7, 4, 6, 5};
 
             // we have 4 neighbors per tile
             // could be more but its 4 for now
             // right/left/down/up
             var neighbors = new MaterialType[4];
 
             for (int i = 0; i < neighbors.Length; i++)
             {
                 neighbors[i] = MaterialType.Air;
             }
 
             if (x + 1 < tileMap.MapSize.X)
             {
                 ref var neighborTile = ref tileMap.GetTile(x + 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.MidTileID).MaterialType;
                 neighbors[(int) Neighbor.Right] = neighborMaterialType;
             }
 
             if (x - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x - 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.MidTileID).MaterialType;
                 neighbors[(int) Neighbor.Left] = neighborMaterialType;
             }
 
             if (y + 1 < tileMap.MapSize.Y)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y + 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.MidTileID).MaterialType;
                 neighbors[(int) Neighbor.Up] = neighborMaterialType;
             }
 
             if (y - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y - 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.MidTileID).MaterialType;
                 neighbors[(int) Neighbor.Down] = neighborMaterialType;
             }
 
 
             var tilePosition = GetTilePosition(neighbors, tileProperty.MaterialType);
 
             // the sprite ids are next to each other in the sprite atlas
             // we just have to know which one to draw based on the offset
             tile.MidTileSpriteID = tileProperty.BaseSpriteId + tilePositionToTileSet[(int) tilePosition];
         }
 
 
 
         public static void UpdateFrontSprite(int x, int y, TileMap tileMap)
         {
             ref var tile = ref tileMap.GetTile(x, y);
             ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
             
             // standard sheet mapping
             // every tile has a constant offset
             // in the sprite atlas
 
             // example: 15 is (3,3)
             //           8 is (0,2)
             //           1 is (1,0)
             int[] tilePositionToTileSet = {15, 12, 14, 13, 3, 0, 2, 1, 11, 8, 10, 9, 7, 4, 6, 5};
 
             // we have 4 neighbors per tile
             // could be more but its 4 for now
             // right/left/down/up
             var neighbors = new MaterialType[4];
 
             for (int i = 0; i < neighbors.Length; i++)
             {
                 neighbors[i] = MaterialType.Air;
             }
 
             if (x + 1 < tileMap.MapSize.X)
             {
                 ref var neighborTile = ref tileMap.GetTile(x + 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.FrontTileID).MaterialType;
                 neighbors[(int) Neighbor.Right] = neighborMaterialType;
             }
 
             if (x - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x - 1, y);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.FrontTileID).MaterialType;
                 neighbors[(int) Neighbor.Left] = neighborMaterialType;
             }
 
             if (y + 1 < tileMap.MapSize.Y)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y + 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.FrontTileID).MaterialType;
                 neighbors[(int) Neighbor.Up] = neighborMaterialType;
             }
 
             if (y - 1 >= 0)
             {
                 ref var neighborTile = ref tileMap.GetTile(x, y - 1);
                 MaterialType neighborMaterialType = GameState.TileCreationApi.GetTileProperty(neighborTile.FrontTileID).MaterialType;
                 neighbors[(int) Neighbor.Down] = neighborMaterialType;
             }
 
 
             var tilePosition = GetTilePosition(neighbors, tileProperty.MaterialType);
 
             // the sprite ids are next to each other in the sprite atlas
             // we just have to know which one to draw based on the offset
             tile.FrontTileSpriteID = tileProperty.BaseSpriteId + tilePositionToTileSet[(int) tilePosition];
         }
    }
}