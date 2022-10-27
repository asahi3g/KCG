using Enums.PlanetTileMap;
using Utility;
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

            CurrentTileIndex = tileID;
            TilePropertyArray[(int)CurrentTileIndex].TileID = tileID;
        }

        public void SetTileMaterialType(MaterialType materialType)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].MaterialType = materialType;
        }

        public void SetTilePropertyShape(Enums.GeometryTileShape shape)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].BlockShapeType = shape;
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

        public int MetalTileSheet;

        public int RockTileSheet;

        public int SQ_0;
        public int SQ_1;
        public int SQ_2;
        public int SQ_3;
        public int SQ_4;

        public int OreStoneSheet;
        public int OreStone_0;


        public void CreateMetalGeometryTiles()
        {
            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
                     GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 19, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 21, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 23, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 25, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 1);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 19, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 21, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 23, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 25, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 10, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 12, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 14, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 16, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R2);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 5);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R3);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 5);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R4_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R5_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R5);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 7);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R6_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R7_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R4_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R5_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R6_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R7_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R4_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R5_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R6_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R7_Metal);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 15);
            GameState.TileCreationApi.EndTileProperty();
        }




         public void CreateRockGeometryTiles()
        {
           GameState.TileCreationApi.CreateTileProperty(TileID.FP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
                     GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 19, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 21, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 23, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.FP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 25, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 1);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 19, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 21, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 23, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.HP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 25, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 10, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 12, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 14, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.QP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 16, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R2);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 5);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R3);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 5);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R4_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R5_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R5);
<<<<<<< HEAD
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 7);
=======
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 7);
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R6_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R7_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.TB_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R4_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R5_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R6_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R7_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L1_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R4_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R5_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R6_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R7_Rock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.L2_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 15);
            GameState.TileCreationApi.EndTileProperty();
        }

        public void InitializeResources()
        {
            LoadingTilePlaceholderSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\placeholder_loadingSprite.png", 32, 32);
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            MoonSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\Tiles_Moon.png", 16, 16);
            StoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Stone\\stone.png", 16, 16);
            ColoredNumberedWangSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\test - Copy.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            Ore3SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Adamantine\\ore_adamantine_1.png", 16, 16);
            PipeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\pipesim.png", 16, 16);
            PlatformSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Platform\\Platform1\\Platform_1.png", 48, 48);
            MetalTileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryMetal\\metal_tiles_geometry.png", 288, 736);
            RockTileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryRock\\rock_tiles_geometry.png", 32, 32);
            OreStoneSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Gems-Ores\\Stone\\gems-ores-stone.png", 128, 128);

            LoadingTilePlaceholderSpriteId =
                    GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(LoadingTilePlaceholderSpriteSheet, 0, 0, 0);

            GameState.TileCreationApi.CreateTileProperty(TileID.Ore1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore1);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(OreSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Glass);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Glass);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 11, 10);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Stone);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Stone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetTilePropertySpriteSheet(StoneSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Moon);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Moon);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.MoonTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 6);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.GoldBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 7);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.DiamondBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.LapisBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID. EmeraldBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 4);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.EmeraldBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 5);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.RedStoneBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 3);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.IronBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 1);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.CoalBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_0Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_1Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_2Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_3Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_4Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_5Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_6Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 2);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PinkDiaBlock_7Drop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Background);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Background);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(ColoredNumberedWangSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore2);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore2);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore2SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore3);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Ore3);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore3SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Pipe);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Pipe);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 0, 0);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.PipeTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Wire);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Wire);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 4, 12);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.WireTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Bedrock);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Bedrock);
            GameState.TileCreationApi.SetCannotBeRemoved(true);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 10);
            GameState.TileCreationApi.SetDropTableID(Enums.LootTableType.BedrockTileDrop);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Platform);
            GameState.TileCreationApi.SetTileMaterialType(MaterialType.Moon);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.GeometryTileShape.SB_R0);
            GameState.TileCreationApi.SetTilePropertyCollisionType(CollisionType.Platform);
            GameState.TileCreationApi.SetTilePropertyTexture16(PlatformSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            CreateMetalGeometryTiles();
            CreateRockGeometryTiles();
        }
    }
}
