using Enums.Tile;
using KMath;
using UnityEngine;
using System;
using Collisions;
using PlanetTileMap;
using Particle;
using System.EnterpriseServices;
using Vehicle;

public class GameResources
{
    // sprite sheets ids

    // Tile CollisionIsotope
    public static int SB_R0000Sheet;
    public static int SB_R0001Sheet;
    public static int SB_R0010Sheet;
    public static int SB_R0011Sheet;
    public static int SB_R0100Sheet;
    public static int SB_R0101Sheet;
    public static int SB_R0110Sheet;
    public static int SB_R0111Sheet;
    public static int SB_R1000Sheet;
    public static int SB_R1001Sheet;
    public static int SB_R1010Sheet;
    public static int SB_R1011Sheet;
    public static int SB_R1100Sheet;
    public static int SB_R1101Sheet;
    public static int SB_R1110Sheet;
    public static int SB_R1111Sheet;
    public static int EmptyBlockSheet;

    //TGen
    /*public static int TGen_SB_R0,

        // HalfBlock
        TGen_HB_R0,
        TGen_HB_R1,
        TGen_HB_R2,
        TGen_HB_R3,

        //TriangleBlock
        TGen_TB_R0,
        TGen_TB_R1,
        TGen_TB_R2,
        TGen_TB_R3,
        TGen_TB_R4,
        TGen_TB_R5,
        TGen_TB_R6,
        TGen_TB_R7,

        //LBlock
        TGen_LB_R0,
        TGen_LB_R1,
        TGen_LB_R2,
        TGen_LB_R3,
        TGen_LB_R4,
        TGen_LB_R5,
        TGen_LB_R6,
        TGen_LB_R7,

        //HalfTriangleBlock
        TGen_HTB_R0,
        TGen_HTB_R1,
        TGen_HTB_R2,
        TGen_HTB_R3,
        TGen_HTB_R4,
        TGen_HTB_R5,
        TGen_HTB_R6,
        TGen_HTB_R7,

        //QuarterPlatform
        TGen_QP_R0,
        TGen_QP_R1,
        TGen_QP_R2,
        TGen_QP_R3,

        //HalfPlatform
        TGen_HP_R0,
        TGen_HP_R1,
        TGen_HP_R2,
        TGen_HP_R3,

        //FullPlatform
        TGen_FP_R0,
        TGen_FP_R1,
        TGen_FP_R2,
        TGen_FP_R3;*/

    public static int OreSprite;
    public static int Ore2Sprite;
    public static int Ore3Sprite;

    //agent sprite ids
    public static int SlimeMoveLeftBaseSpriteId;
    public static int DeadSlimeSpriteId;
    public static int CharacterSpriteId;


    // particle sprite ids used for icons
    // TODO(): create icons atlas
    public static int DustBaseSpriteId;
    public static int ExplosionBaseSpriteId;

    public static int OreIcon;
    
    public static int SlimeIcon;
    public static int BoneIcon;
    public static int PlacementToolIcon;
    public static int RemoveToolIcon;
    public static int MiningLaserToolIcon;
    public static int PipePlacementToolIcon;
    public static int ConstructionToolIcon;
    public static int HemeltSprite;
    public static int SuitSprite;

    public static int ChestIconItem;
    public static int FoodIcon;
    public static int ChestIconParticle;

    public static int PotIcon;
    public static int PotIconItem;

    // Temporary inventory slotIcons.
    public static int DyeSlotIcon;
    public static int HelmetSlotIcon;
    public static int ArmourSlotIcon;
    public static int GlovesSlotIcon;
    public static int RingSlotIcon;
    public static int BeltSlotIcon;

    public static int DefaultCursor;
    public static int AimCursor;
    public static int BuildCursor;

    private static bool IsInitialized = false; 


    public static void Initialize()
    {
        if (!IsInitialized)
        {
            long beginTime = DateTime.Now.Ticks;

            IsInitialized = true;

            int OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);

            SB_R0000Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0000.png", 32, 32);
            SB_R0001Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0001.png", 32, 32);
            SB_R0010Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0010.png", 32, 32);
            SB_R0011Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0011.png", 32, 32);
            SB_R0100Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0100.png", 32, 32);
            SB_R0101Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0101.png", 32, 32);
            SB_R0110Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0110.png", 32, 32);
            SB_R0111Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0111.png", 32, 32);
            SB_R1000Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1000.png", 32, 32);
            SB_R1001Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1001.png", 32, 32);
            SB_R1010Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1010.png", 32, 32);
            SB_R1011Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1011.png", 32, 32);
            SB_R1100Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1100.png", 32, 32);
            SB_R1101Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1101.png", 32, 32);
            SB_R1110Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1110.png", 32, 32);
            SB_R1111Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1111.png", 32, 32);
            EmptyBlockSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\EmptyBlock.png", 32, 32);
            ChestIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
            PotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);
            PotIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);

            ConstructionToolIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Development\\Furnitures\\Furniture2\\dev-furniture-2.png", 12, 12);
            DyeSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Dye.png", 64, 64);
            HelmetSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Helmet.png", 64, 64);
            ArmourSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Armour.png", 64, 64);
            GlovesSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Gloves.png", 64, 64);
            RingSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Ring.png", 64, 64);
            BeltSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Belt.png", 64, 64);

            // Cursors
            DefaultCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            AimCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            BuildCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);

            // particle sprite atlas
            ChestIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestIconItem, 0, 0, Enums.AtlasType.Particle);
            PotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIcon, 0, 0, Enums.AtlasType.Mech);
            PotIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIconItem, 0, 0, Enums.AtlasType.Particle);
            ConstructionToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ConstructionToolIcon, 0, 0, Enums.AtlasType.Particle);

            DyeSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DyeSlotIcon, 0, 0, Enums.AtlasType.Gui);
            HelmetSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetSlotIcon, 0, 0, Enums.AtlasType.Gui);
            ArmourSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ArmourSlotIcon, 0, 0, Enums.AtlasType.Gui);
            GlovesSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GlovesSlotIcon, 0, 0, Enums.AtlasType.Gui);
            RingSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RingSlotIcon, 0, 0, Enums.AtlasType.Gui);
            BeltSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BeltSlotIcon, 0, 0, Enums.AtlasType.Gui);

            // Cursors
            DefaultCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(DefaultCursor, 0, 0, Enums.AtlasType.Particle);
            AimCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(AimCursor, 2, 0, Enums.AtlasType.Particle);
            BuildCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(BuildCursor, 1, 1, Enums.AtlasType.Particle);

            // TileIsotypes.
            SB_R0000Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0000Sheet, 0, 0, 0);
            SB_R0001Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0001Sheet, 0, 0, 0);
            SB_R0010Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0010Sheet, 0, 0, 0);
            SB_R0011Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0011Sheet, 0, 0, 0);
            SB_R0100Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0100Sheet, 0, 0, 0);
            SB_R0101Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0101Sheet, 0, 0, 0);
            SB_R0110Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0110Sheet, 0, 0, 0);
            SB_R0111Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R0111Sheet, 0, 0, 0);
            SB_R1000Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1000Sheet, 0, 0, 0);
            SB_R1001Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1001Sheet, 0, 0, 0);
            SB_R1010Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1010Sheet, 0, 0, 0);
            SB_R1011Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1011Sheet, 0, 0, 0);
            SB_R1100Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1100Sheet, 0, 0, 0);
            SB_R1101Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1101Sheet, 0, 0, 0);
            SB_R1110Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1110Sheet, 0, 0, 0);
            SB_R1111Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(SB_R1111Sheet, 0, 0, 0);
            EmptyBlockSheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(EmptyBlockSheet, 0, 0, 0);

            OreSprite = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(OreSpriteSheet, 0, 0, 0);

            CreateDropTables();
            InitializeTGenTiles();

            CreateTiles();
            CreateAnimations();
            CreateItems();
            CreateAgents();
            CreateParticles();
            CreateParticleEmitters();
            CreateProjectiles();
            CreateMechs();
            CreateVehicles();

            Debug.Log("2d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");
        }
    }

    private static void CreateDropTables()
    {
        GameState.LootTableCreationAPI.InitializeResources();
    }

    private static void InitializeTGenTiles()
    {
        GameState.TGenRenderGridOverlay.InitializeResources();
    }

    private static void CreateTiles()
    {
        GameState.TileCreationApi.InitializeResources();
    }

    private static void CreateAnimations()
    {
        GameState.AnimationManager.InitializeResources();
    }

    public static void CreateItems()
    {
        GameState.ItemCreationApi.InitializeResources();
    }

    private static void CreateAgents()
    {
        GameState.AgentCreationApi.InitializeResources();
    }

    private static void CreateMechs()
    {
        GameState.MechCreationApi.InitializeResources();
    }

    private static void CreateParticles()
    {
        GameState.ParticleCreationApi.InitializeResources();
    }

    private static void CreateParticleEmitters()
    {
        GameState.ParticleCreationApi.InitializeEmitterResources();
    }

    private static void CreateProjectiles()
    {
        GameState.ProjectileCreationApi.InitializeResources();
    }

    private static void CreateVehicles()
    {
        GameState.VehicleCreationApi.InitializeResources();
    }
 
}
