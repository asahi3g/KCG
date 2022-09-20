using Collisions;
using Enums.Tile;
using UnityEngine.PlayerLoop;
//MOST IMPORTANT TILE

/*

CreateTile(TileId)
SetTileName("Regolith") //map from string to TileId
SetTileLayer(TileMapLayerBackground)
SetTileTexture(TileSpriteId, 2,10) //2nd row, 10th column of TileSpriteId
SetTilePropertyISExplosive(true)
SetTileDurability(60)
EndTile()

Atlas is a pixel array
Atlas starts empty
Sprites are copied to Atlas and we get a AtlasSpriteId

SetTileTexture(TileSpriteId, 2,10) //2nd row, 10th column of TileSpriteId
- What does this do?
-- It blits (copy) the Sprite from TileSpriteLoader (TileSpriteSheetId)
-- to the TileSpriteAtlas
-- AND get the AtlasSpriteId (index into the Atlas texture sheet)

SetTileId(5)
// TileType, TileLayer, Name
DefineTile(BlockTypeSolid, LayerForegound, "regolith");
SetTileTexture(ImageId2, 2,10); //2nd row, 10th column, of i
push_texture(); //some might have more than one

SetTilePropertyIsExplosive(true); //example
SetTileDurability(60);

SetTileTextDescription("Regolith is a kind of dust commonly found on the surface of astronomical objects,\n");
EndTile();
*/

namespace PlanetTileMap
{
    //https://github.com/kk-digital/kcg/issues/89

    //ALL TILES CREATED OR USED IN GAME HAVE TO BE CREATED HERE
    //ALL TILES ARE CREATED FROM FUNCTIONS IN THIS FILE
    //ALL SPRITES FOR TILES ARE SET AND ASSIGNED FROM THIS API

    public class TileCreationApi
    {
        // Start is called before the first frame update
        private TileID CurrentTileIndex;
        public TileProperty[] TilePropertyArray;
        
        public TileCreationApi()
        {
            var tilePropertyArray = new TileProperty[4096];

            for (int i = 0; i < tilePropertyArray.Length; i++)
            {
                tilePropertyArray[i].TileID = TileID.Error;
                tilePropertyArray[i].BaseSpriteId = -1;
            }

            TilePropertyArray = tilePropertyArray;
            
            CurrentTileIndex = TileID.Error;
        }

        public ref TileProperty GetTileProperty(TileID tileID)
        {
            return ref TilePropertyArray[(int)tileID];
        }

        public void CreateTileProperty(TileID tileID)
        {
            if (tileID == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].TileID = tileID;
            CurrentTileIndex = tileID;
        }

        public void SetTileMaterialType(MaterialType materialType)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].MaterialType = materialType;
        }

        public void SetTilePropertyShape(TileShape shape)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].BlockShapeType = shape;
        }

        public void SetTilePropertyName(string name)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].Name = name;
        }

        public void SetTilePropertySpriteSheet16(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
           

            if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R1 ||
                TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R2)
            {
                int baseId = 0;
                for(int j = column; j < column + 4; j++)
                {
                    for(int i = row; i < row + 4; i++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, i, j, 0);

                        // the first sprite id is the baseId
                        if (i == row && j == column)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R3)
            {
                int baseId = 0;
                for(int x = column; x < column + 5; x++)
                {
                    for(int y = row; y < row + 11; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else
            {
                int baseId = 0;
                for(int x = column; x < column + 1; x++)
                {
                    for(int y = row; y < row + 1; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
        }

        public void SetTilePropertySpriteSheet(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R1 ||
            TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R2)
            {
                int baseId = 0;
                
                for(int i = row; i <= row + 4; i++)
                {
                    for(int j = column; j <= column + 4; j++)
                    {
                        int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, i, j, 0);

                        // the first sprite id is the baseId
                        if (i == row && j == column)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R3)
            {
                int baseId = 0;
                for(int x = column; x < column + 5; x++)
                {
                    for(int y = row; y < row + 11; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }

                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
        }

        public void SetTilePropertyTexture(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            //FIX: Dont import GameState, make a method?
            //TileAtlas is imported by GameState, so TileAtlas should not import GameState
            int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, row, column, 0);
            TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = atlasSpriteId;
            TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = false;
        }

        public void SetTilePropertyTexture16(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
              
            int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, row, column, 0);
            TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = atlasSpriteId;
            TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = false;
            
        }

        public void SetTilePropertyCollisionType(CollisionType type)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].CollisionIsoType = type;
        }

        
        public void SetTilePropertyDurability(byte durability)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].Durability = durability;
        }

        public void SetTilePropertyDescription(byte durability)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            TilePropertyArray[(int)CurrentTileIndex].Durability = durability;
        }

        public void SetSpriteRuleType(SpriteRuleType spriteRuleType)
        {
            Utils.Assert((int)CurrentTileIndex >= 0 && (int)CurrentTileIndex < TilePropertyArray.Length);

            TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType = spriteRuleType;
        }

        public void SetCannotBeRemoved(bool flag)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            TilePropertyArray[(int)CurrentTileIndex].CannotBeRemoved = flag;
        }

        public void SetDropTableID(Enums.LootTableType dropTableID)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].DropTableID = dropTableID;
        }

        public void EndTileProperty()
        {
            CurrentTileIndex = TileID.Error;
        }

        public int LoadingTilePlaceholderSpriteId;
        public int LoadingTilePlaceholderSpriteSheet;
        public int OreSpriteSheet;
        public int MoonSpriteSheet;
        public int StoneSpriteSheet;
        public int ColoredNumberedWangSpriteSheet;
        public int Ore2SpriteSheet;
        public int Ore3SpriteSheet;
        public int PipeSpriteSheet;
        public int PlatformSpriteSheet;

        public int MetalGeometryTileSheet;

        public int SQ_0;
        public int SQ_1;
        public int SQ_2;
        public int SQ_3;
        public int SQ_4;

        public void InitializeResources()
        {
            LoadingTilePlaceholderSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\placeholder_loadingSprite.png", 32, 32);
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            MoonSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\Tiles_Moon.png", 16, 16);
            StoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Stone\\stone.png", 16, 16);
            ColoredNumberedWangSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\colored-numbered-wang.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            Ore3SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Adamantine\\ore_adamantine_1.png", 16, 16);
            PipeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\pipesim.png", 16, 16);
            PlatformSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Platform\\Platform1\\Platform_1.png", 48, 48);
            MetalGeometryTileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryMetal\\metal_tiles_geometry.png", 288, 736);

            LoadingTilePlaceholderSpriteId =
                    GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(LoadingTilePlaceholderSpriteSheet, 0, 0, 0);

            GameState.TileCreationApi.CreateTileProperty(TileID.Ore1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore1);
            GameState.TileCreationApi.SetTilePropertyName("ore_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetTilePropertyTexture16(OreSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Glass);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Glass);
            GameState.TileCreationApi.SetTilePropertyName("glass");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 11, 10);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Stone);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Stone);
            GameState.TileCreationApi.SetTilePropertyName("stone");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetTilePropertySpriteSheet(StoneSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Moon);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Moon);
            GameState.TileCreationApi.SetTilePropertyName("moon");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.MoonTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Background);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Background);
            GameState.TileCreationApi.SetTilePropertyName("background");
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(ColoredNumberedWangSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore2);
            GameState.TileCreationApi.SetTilePropertyName("ore_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore2SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore3);
            GameState.TileCreationApi.SetTilePropertyName("ore_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore3SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Pipe);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Pipe);
            GameState.TileCreationApi.SetTilePropertyName("pipe");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 0, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PipeTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Wire);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Wire);
            GameState.TileCreationApi.SetTilePropertyName("wire");
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 4, 12);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.WireTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Bedrock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Bedrock);
            GameState.TileCreationApi.SetTilePropertyName("Bedrock");
            GameState.TileCreationApi.SetCannotBeRemoved(true);
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 10);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.BedrockTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Platform);
            GameState.TileCreationApi.SetTilePropertyName("Platform");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetTilePropertyCollisionType(CollisionType.Platform);
            GameState.TileCreationApi.SetTilePropertyTexture16(PlatformSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SQ_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SQ_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 19, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SQ_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SQ_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 21, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SQ_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SQ_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 23, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SQ_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 25, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SQNoSpecular_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 1);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQNoSpecular_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQNoSpecular_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQNoSpecular_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQNoSpecular_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQNoSpecular_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQNoSpecular_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQNoSpecular_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQNoSpecular_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQ_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQ_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 19, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQ_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQ_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 21, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQ_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQ_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 23, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HSQ_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HSQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 25, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SSQ_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SSQ_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 10, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SSQ_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SSQ_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 12, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SSQ_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SSQ_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 14, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SSQ_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("SSQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 16, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TI_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TI_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TI_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TI_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TI_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TI_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TI_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TI_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TO_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TO_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TO_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TO_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TO_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TO_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TO_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("TO_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HTD_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HTD_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HTL_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HTL_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HTU_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HTU_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HTR_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("HTR_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RHTD_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RHTD_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RHTL_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RHTL_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RHTU_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RHTU_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RHTR_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RHTR_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CSQ_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("CSQ_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CSQ_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("CSQ_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CSQ_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("CSQ_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 1, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CSQ_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("CSQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 3, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RCSQ_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RCSQ_0");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RCSQ_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RCSQ_1");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RCSQ_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RCSQ_2");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 5, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RCSQ_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.MetalGeometry);
            GameState.TileCreationApi.SetTilePropertyName("RCSQ_3");
            GameState.TileCreationApi.SetTilePropertyShape(TileShape.FullBlock);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalGeometryTileSheet, 7, 15);
            GameState.TileCreationApi.EndTileProperty();
        }
    }
}
