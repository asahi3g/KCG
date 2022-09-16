using Enums.Tile;
using KMath;
using UnityEngine;
using System;
using Collisions;
using PlanetTileMap;
using Particle;
using System.EnterpriseServices;

public class GameResources
{
    // sprite sheets ids
    public static int FoodSpriteSheet;
    public static int BoneSpriteSheet;
    public static int LoadingTilePlaceholderSpriteSheet;
    public static int BackgroundSpriteSheet;
    public static int ColoredNumberedWangSpriteSheet;
    public static int MoonSpriteSheet;
    public static int OreSpriteSheet;
    public static int Ore2SpriteSheet;
    public static int Ore3SpriteSheet;
    public static int GunSpriteSheet;
    public static int RockSpriteSheet;
    public static int RockDustSpriteSheet;
    public static int SlimeSpriteSheet;
    public static int CharacterSpriteSheet;
    public static int LaserSpriteSheet;
    public static int PipeSpriteSheet;
    public static int ChestSpriteSheet;
    public static int pipeIconSpriteSheet;
    public static int DustSpriteSheet;
    public static int GrenadeSpriteSheet;
    public static int SwordSpriteSheet;
    public static int HelmetsSpriteSheet;
    public static int SuitsSpriteSheet;

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
    public static int TGenBlockSpriteSheet;
    public static int[] TGenIsotypeSprites;
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


    public static int PlatformSpriteSheet;
    public static int StoneSpriteSheet;

    public static int OreSprite;
    public static int Ore2Sprite;
    public static int Ore3Sprite;

    //agent sprite ids
    public static int SlimeMoveLeftBaseSpriteId;
    public static int DeadSlimeSpriteId;
    public static int CharacterSpriteId;

    public static int GrenadeSpriteId;
    public static int GrenadeSprite5;
    public static int SwordSpriteId;


    // particle sprite ids used for icons
    // TODO(): create icons atlas
    public static int DustBaseSpriteId;
    public static int ExplosionBaseSpriteId;

    public static int OreIcon;
    
    public static int PistolIcon;
    public static int PulseIcon;
    public static int ShotgunIcon;
    public static int LongRifleIcon;
    public static int SniperRifleIcon;
    public static int RPGIcon;
    public static int SMGIcon;
    public static int SlimeIcon;
    public static int BoneIcon;
    public static int PlacementToolIcon;
    public static int RemoveToolIcon;
    public static int MiningLaserToolIcon;
    public static int PipePlacementToolIcon;
    public static int ConstructionToolIcon;
    public static int HemeltSprite;
    public static int SuitSprite;

    public static int ChestIcon;
    public static int ChestIconItem;
    public static int FoodIcon;
    public static int ChestIconParticle;

    public static int PotIcon;
    public static int PotIconItem;

    public static int MajestyPalm;
    public static int MajestyPalmS1;
    public static int MajestyPalmS2;
    public static int MajestyPalmIcon;

    public static int SagoPalm;
    public static int SagoPalmS1;
    public static int SagoPalmS2;
    public static int SagoPalmIcon;

    public static int DracaenaTrifasciata;
    public static int DracaenaTrifasciataS1;
    public static int DracaenaTrifasciataS2;
    public static int DracaenaTrifasciataIcon;

    public static int Light2Icon;
    public static int Light2IconItem;

    public static int WaterIcon;

    public static int JetChassis;

    // Temporary inventory slotIcons.
    public static int DyeSlotIcon;
    public static int HelmetSlotIcon;
    public static int ArmourSlotIcon;
    public static int GlovesSlotIcon;
    public static int RingSlotIcon;
    public static int BeltSlotIcon;

    public static int DirtIcon;
    public static int BedrockIcon;
    public static int PipeIcon;
    public static int WireIcon;

    public static int DefaultCursor;
    public static int AimCursor;
    public static int BuildCursor;

    public static int BloodSprite;

    public static int LoadingTilePlaceholderSpriteId;
    public static int LoadingTilePlaceholderTileId;

    private static bool IsInitialized = false; 


    public static void Initialize()
    {
        if (!IsInitialized)
        {
            long beginTime = DateTime.Now.Ticks;

            IsInitialized = true;
            // loading the sprite sheets
            FoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Food\\Food.png", 60, 60);
            BoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Bone\\Bone.png", 60, 60);
            LoadingTilePlaceholderSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\placeholder_loadingSprite.png", 32, 32);
            BackgroundSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\test - Copy.png", 16, 16);
            ColoredNumberedWangSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\colored-numbered-wang.png", 16, 16);
            MoonSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\Tiles_Moon.png", 16, 16);
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            Ore3SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Adamantine\\ore_adamantine_1.png", 16, 16);
            GunSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Pistol\\gun-temp.png", 44, 25);
            ShotgunIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun13.png", 48, 16);
            LongRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun10.png", 48, 16);
            SniperRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun8.png", 48, 16);
            PulseIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun17.png", 48, 16);
            RPGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun18.png", 48, 16);
            SMGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun6.png", 48, 16);
            RockSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Rock\\rock1.png", 16, 16);
            RockDustSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Rock\\rock1_dust.png", 16, 16);
            SlimeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Enemies\\Slime\\slime.png", 32, 32);
            CharacterSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Characters\\Player\\character.png", 32, 48);
            LaserSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\RailGun\\lasergun-temp.png", 195, 79);
            PipeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\pipesim.png", 16, 16);
            pipeIconSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", 16, 16);
            DustSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\Dust\\dust1.png", 16, 16);
            int ExplosionSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\explosion.png", 182, 182);
            GrenadeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades1.png", 16, 16);
            GrenadeSprite5 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades5.png", 16, 16);
            SwordSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Swords\\Sword1.png", 16, 48);
            HelmetsSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Character\\Helmets\\character-helmets.png", 64, 64);
            SuitsSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Character\\Suits\\character-suits.png", 64, 96);
            int BloodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Particles\\red_32x32.png", 32, 32);

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

            StoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Stone\\stone.png", 16, 16);
            PlatformSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Platform\\Platform1\\Platform_1.png",48,48);
            ChestSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
            ChestIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
            PotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);
            PotIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);

            MajestyPalm = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png", 16, 16);
            MajestyPalmS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3_v1.png", 16, 16);
            MajestyPalmS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3_v2.png", 16, 32);
            MajestyPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png", 16, 16);

            SagoPalm = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png", 16, 16);
            SagoPalmS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7_v1.png", 16, 16);
            SagoPalmS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7_v2.png", 16, 32);
            SagoPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png", 16, 16);

            DracaenaTrifasciata = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", 16, 16);
            DracaenaTrifasciataS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6_v1.png", 16, 16);
            DracaenaTrifasciataS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6_v2.png", 16, 32);
            DracaenaTrifasciataIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", 16, 16);

            Light2Icon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png", 48, 16);
            Light2IconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png", 48, 16);
            WaterIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Water\\water_12px.png", 12, 12);
            ConstructionToolIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Development\\Furnitures\\Furniture2\\dev-furniture-2.png", 12, 12);
            DyeSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Dye.png", 64, 64);
            HelmetSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Helmet.png", 64, 64);
            ArmourSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Armour.png", 64, 64);
            GlovesSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Gloves.png", 64, 64);
            RingSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Ring.png", 64, 64);
            BeltSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Belt.png", 64, 64);
            DirtIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png", 16, 16);
            BedrockIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png", 16, 16);
            WireIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png", 128, 128);
            PipeIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", 16, 16);

            // Cursors
            DefaultCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            AimCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            BuildCursor = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);

            //Vehicles
            JetChassis = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Vehicles\\Jet\\Chassis\\Jet_chassis.png", 144, 96);


            OreSprite = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(OreSpriteSheet, 0, 0, 0);
            Ore2Sprite = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(Ore2SpriteSheet, 0, 0, 0);
            Ore3Sprite = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(Ore3SpriteSheet, 0, 0, 0);

            // agent sprite atlas
            SlimeMoveLeftBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(SlimeSpriteSheet, 0, 0, 3, 0, Enums.AtlasType.Agent);
            DeadSlimeSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 0, 4, Enums.AtlasType.Agent);
            CharacterSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(CharacterSpriteSheet, 0, 0, Enums.AtlasType.Agent);

            GrenadeSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            GrenadeSprite5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSprite5, 0, 0, Enums.AtlasType.Particle);
            SwordSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(SwordSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            // particle sprite atlas

            BloodSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(BloodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            FoodIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(FoodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            BoneIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BoneSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PistolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GunSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            ShotgunIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ShotgunIcon, 0, 0, Enums.AtlasType.Particle);
            LongRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LongRifleIcon, 0, 0, Enums.AtlasType.Particle);
            SniperRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SniperRifleIcon, 0, 0, Enums.AtlasType.Particle);
            PulseIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PulseIcon, 0, 0, Enums.AtlasType.Particle);
            RPGIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RPGIcon, 0, 0, Enums.AtlasType.Particle);
            SMGIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SMGIcon, 0, 0, Enums.AtlasType.Particle);
            SlimeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RockSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            RemoveToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Ore2SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            MiningLaserToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LaserSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PipePlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(pipeIconSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            DustBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(DustSpriteSheet, 0, 0, 5, 0, Enums.AtlasType.Particle);
            ExplosionBaseSpriteId = GameState.SpriteAtlasManager.CopySpritesToAtlas(ExplosionSpriteSheet, 0, 0, 4, 1, Enums.AtlasType.Particle);
            ChestIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestSpriteSheet, 0, 0, Enums.AtlasType.Mech);
            ChestIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestIconItem, 0, 0, Enums.AtlasType.Particle);
            ChestIconParticle = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIcon, 0, 0, Enums.AtlasType.Mech);
            PotIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIconItem, 0, 0, Enums.AtlasType.Particle);
            MajestyPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalm, 0, 0, Enums.AtlasType.Mech);
            MajestyPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS1, 0, 0, Enums.AtlasType.Mech);
            MajestyPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS2, 0, 0, Enums.AtlasType.Mech);
            MajestyPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmIcon, 0, 0, Enums.AtlasType.Particle);
            Light2Icon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2Icon, 0, 0, Enums.AtlasType.Mech);
            Light2IconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2IconItem, 0, 0, Enums.AtlasType.Particle);
            WaterIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WaterIcon, 0, 0, Enums.AtlasType.Particle);
            ConstructionToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ConstructionToolIcon, 0, 0, Enums.AtlasType.Particle);
            HemeltSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetsSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            SuitSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(SuitsSpriteSheet, 0, 0, Enums.AtlasType.Particle);

            DyeSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DyeSlotIcon, 0, 0, Enums.AtlasType.Gui);
            HelmetSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetSlotIcon, 0, 0, Enums.AtlasType.Gui);
            ArmourSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ArmourSlotIcon, 0, 0, Enums.AtlasType.Gui);
            GlovesSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GlovesSlotIcon, 0, 0, Enums.AtlasType.Gui);
            RingSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RingSlotIcon, 0, 0, Enums.AtlasType.Gui);
            BeltSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BeltSlotIcon, 0, 0, Enums.AtlasType.Gui);

            // Plants
            SagoPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalm, 0, 0, Enums.AtlasType.Mech);
            SagoPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS1, 0, 0, Enums.AtlasType.Mech);
            SagoPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS2, 0, 0, Enums.AtlasType.Mech);
            SagoPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmIcon, 0, 0, Enums.AtlasType.Particle);
            DracaenaTrifasciata = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciata, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciataS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS1, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciataS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS2, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciataIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataIcon, 0, 0, Enums.AtlasType.Particle);

            // Material Icons
            DirtIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DirtIcon, 0, 0, Enums.AtlasType.Particle);
            BedrockIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BedrockIcon, 0, 0, Enums.AtlasType.Particle);
            WireIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WireIcon, 0, 0, Enums.AtlasType.Particle);
            PipeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PipeIcon, 0, 0, Enums.AtlasType.Particle);

            // Cursors
            DefaultCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(DefaultCursor, 0, 0, Enums.AtlasType.Particle);
            AimCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(AimCursor, 2, 0, Enums.AtlasType.Particle);
            BuildCursor = GameState.SpriteAtlasManager.CopySpriteToAtlas(BuildCursor, 1, 1, Enums.AtlasType.Particle);

            // Vehicles
            JetChassis = GameState.SpriteAtlasManager.CopySpriteToAtlas(JetChassis, 0, 0, Enums.AtlasType.Vehicle);

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
        GameState.LootTableCreationAPI.Create(Enums.LootTableType.SlimeEnemyDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Slime, 1);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Slime, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 30);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Food, 4);
        GameState.LootTableCreationAPI.SetEntry(1, 25);
        GameState.LootTableCreationAPI.SetEntry(2, 40);
        GameState.LootTableCreationAPI.SetEntry(3, 25);
        GameState.LootTableCreationAPI.SetEntry(4, 5);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Bone, 4);
        GameState.LootTableCreationAPI.SetEntry(3, 50);
        GameState.LootTableCreationAPI.SetEntry(4, 25);
        GameState.LootTableCreationAPI.SetEntry(5, 15);
        GameState.LootTableCreationAPI.SetEntry(6, 10);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.ChestDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Chest, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.PlanterDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Planter, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.LightDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Light, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.SmashableBoxDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.SmashableBox, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();
        
        GameState.LootTableCreationAPI.Create(Enums.LootTableType.SmashableEggDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.SmashableEgg, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.MoonTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Moon, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.DirtTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Dirt, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.DirtTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Dirt, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.BedrockTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Bedrock, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.PipeTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Pipe, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();

        GameState.LootTableCreationAPI.Create(Enums.LootTableType.WireTileDrop);
        GameState.LootTableCreationAPI.AddItem(Enums.ItemType.Wire, 1);
        GameState.LootTableCreationAPI.SetEntry(1, 100);
        GameState.LootTableCreationAPI.End();
    }

    private static void InitializeTGenTiles()
    {
        TGenBlockSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Test\\testBlocks.png", 32, 32);

        var emptySprite = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\EmptyBlock.png", 32, 32);

        int tileCount = 42;

        TGenIsotypeSprites = new int[tileCount];

        TGenIsotypeSprites[0] = GameState.SpriteAtlasManager.CopySpriteToAtlas(emptySprite, 0, 0, Enums.AtlasType.TGen);

        TGenIsotypeSprites[1] = GameState.SpriteAtlasManager.CopySpriteToAtlas(TGenBlockSpriteSheet, 1, 1, Enums.AtlasType.TGen);

        var row = 3;
        var column = 1;

        for (int i = 2; i < tileCount; i++)
        {
            TGenIsotypeSprites[i] = GameState.SpriteAtlasManager.CopySpriteToAtlas(TGenBlockSpriteSheet, column, row, Enums.AtlasType.TGen);

            column += 2;

            if (column > 8)
            {
                column = 1;
                row += 2;
            }
        }
    }

    private static void CreateTiles()
    {
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

    }

    private static void CreateAnimations()
    {
        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.CharacterMoveLeft);
        GameState.AnimationManager.SetName("character-move-left");
        GameState.AnimationManager.SetTimePerFrame(0.15f);
        GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
        GameState.AnimationManager.SetFrameCount(1);
        GameState.AnimationManager.EndAnimation();

        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.CharacterMoveLeft);
        GameState.AnimationManager.SetName("character-move-right");
        GameState.AnimationManager.SetTimePerFrame(0.15f);
        GameState.AnimationManager.SetBaseSpriteID(CharacterSpriteId);
        GameState.AnimationManager.SetFrameCount(1);
        GameState.AnimationManager.EndAnimation();

        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.SlimeMoveLeft);
        GameState.AnimationManager.SetName("slime-move-left");
        GameState.AnimationManager.SetTimePerFrame(0.35f);
        GameState.AnimationManager.SetBaseSpriteID(SlimeMoveLeftBaseSpriteId);
        GameState.AnimationManager.SetFrameCount(4);
        GameState.AnimationManager.EndAnimation();

        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.Dust);
        GameState.AnimationManager.SetName("dust");
        GameState.AnimationManager.SetTimePerFrame(0.075f);
        GameState.AnimationManager.SetBaseSpriteID(DustBaseSpriteId);
        GameState.AnimationManager.SetFrameCount(6);
        GameState.AnimationManager.EndAnimation();

        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.Smoke);
        GameState.AnimationManager.SetName("smoke");
        GameState.AnimationManager.SetTimePerFrame(0.075f);
        GameState.AnimationManager.SetBaseSpriteID(DustBaseSpriteId);
        GameState.AnimationManager.SetFrameCount(6);
        GameState.AnimationManager.EndAnimation();


        GameState.AnimationManager.CreateAnimation((int)Animation.AnimationType.Explosion);
        GameState.AnimationManager.SetName("explosion");
        GameState.AnimationManager.SetTimePerFrame(0.075f);
        GameState.AnimationManager.SetBaseSpriteID(ExplosionBaseSpriteId);
        GameState.AnimationManager.SetFrameCount(7);
        GameState.AnimationManager.EndAnimation();
    }

    public static void CreateItems()
    {
        // Sniper Rifle Item Creation
        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SniperRifle, "SniperRifle");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(SniperRifleIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SniperRifleIcon);
        GameState.ItemCreationApi.SetRangedWeapon(200.0f, 1f, 350.0f, 60);
        GameState.ItemCreationApi.SetRangedWeaponClip(6, 1, 1.3f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.LongRifle, "LongRifle");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(LongRifleIcon);
        GameState.ItemCreationApi.SetInventoryTexture(LongRifleIcon);
        GameState.ItemCreationApi.SetRangedWeapon(50.0f, 1f, 20.0f, 40);
        GameState.ItemCreationApi.SetRangedWeaponClip(25, 1, 2f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PulseWeapon, "PulseWeapon");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(PulseIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PulseIcon);
        GameState.ItemCreationApi.SetRangedWeapon(20.0f, 0.5f, 10.0f, false, 25);
        GameState.ItemCreationApi.SetRangedWeaponClip(25, 4, 1, 1);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPulseWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.AutoCannon, "AutoCannon");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(LongRifleIcon);
        GameState.ItemCreationApi.SetInventoryTexture(LongRifleIcon);
        GameState.ItemCreationApi.SetRangedWeapon(50.0f, 0.5f, 20.0f, 40);
        GameState.ItemCreationApi.SetRangedWeaponClip(40, 3, 4f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SMG, "SMG");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(SMGIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SMGIcon);
        GameState.ItemCreationApi.SetRangedWeapon(50.0f, 0.2f, 20.0f, 15);
        GameState.ItemCreationApi.SetRangedWeaponClip(99999, 1, 1f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Shotgun, "Shotgun");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(ShotgunIcon);
        GameState.ItemCreationApi.SetInventoryTexture(ShotgunIcon);
        GameState.ItemCreationApi.SetRangedWeapon(30.0f, 1f, 10.0f, 35);
        GameState.ItemCreationApi.SetSpreadAngle(1.0f);
        GameState.ItemCreationApi.SetRangedWeaponClip(6, 2, 2.5f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetFlags(Item.FireWeaponPropreties.Flags.ShouldSpread);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PumpShotgun, "PumpShotgun");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(ShotgunIcon);
        GameState.ItemCreationApi.SetInventoryTexture(ShotgunIcon);
        GameState.ItemCreationApi.SetRangedWeapon(20.0f, 2f, 5.0f, 30);
        GameState.ItemCreationApi.SetSpreadAngle(1.0f);
        GameState.ItemCreationApi.SetRangedWeaponClip(8, 4, 2.5f);
        GameState.ItemCreationApi.SetFlags(Item.FireWeaponPropreties.Flags.ShouldSpread);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Pistol, "Pistol");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(PistolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PistolIcon);
        GameState.ItemCreationApi.SetRangedWeapon(20.0f, 0.4f, 10.0f, 25);
        GameState.ItemCreationApi.SetRangedWeaponClip(8, 1, 1f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.RPG, "RPG");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetTexture(RPGIcon);
        GameState.ItemCreationApi.SetInventoryTexture(RPGIcon);
        GameState.ItemCreationApi.SetRangedWeapon(50.0f, 3f, 50.0f, 100);
        GameState.ItemCreationApi.SetRangedWeaponClip(2, 1, 3);
        GameState.ItemCreationApi.SetExplosion(3.0f, 15, 0f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Rocket);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionThrowGrenade);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.GrenadeLauncher, "GrenadeLauncher");
        GameState.ItemCreationApi.SetTexture(GrenadeSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(GrenadeSpriteId);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Gun);
        GameState.ItemCreationApi.SetRangedWeapon(20.0f, 1f, 20.0f, 25);
        GameState.ItemCreationApi.SetRangedWeaponClip(4, 1, 2);
        GameState.ItemCreationApi.SetExplosion(4.0f, 15, 0f);
        GameState.ItemCreationApi.SetFlags(Item.FireWeaponPropreties.GrenadesFlags.Flame);
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Grenade);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionThrowGrenade);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.GasBomb, "GasBomb");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(GrenadeSprite5);
        GameState.ItemCreationApi.SetInventoryTexture(GrenadeSprite5);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionGasBomb);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Bow, "Bow");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(PistolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PistolIcon);
        GameState.ItemCreationApi.SetRangedWeapon(70.0f, 3f, 100.0f, 30);
        GameState.ItemCreationApi.SetRangedWeaponClip(1, 1, 2f);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Arrow);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Sword, "Sword");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Weapon);
        GameState.ItemCreationApi.SetTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetMeleeWeapon(1.0f, 2.0f, 0.5f, 1.0f, 10);
        GameState.ItemCreationApi.SetFlags(Item.FireWeaponPropreties.MeleeFlags.Stab);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMeleeAttack);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.StunBaton, "StunBaton");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Weapon);
        GameState.ItemCreationApi.SetTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetMeleeWeapon(0.5f, 2.0f, 1.0f, 1.0f, 5);
        GameState.ItemCreationApi.SetFlags(Item.FireWeaponPropreties.MeleeFlags.Slash);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMeleeAttack);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.RiotShield, "RiotShield");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetShield(false);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionShield);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Ore, "Ore");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(OreIcon);
        GameState.ItemCreationApi.SetInventoryTexture(OreIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();


        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Slime, "Slime");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(SlimeIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SlimeIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Food, "Food");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(FoodIcon);
        GameState.ItemCreationApi.SetInventoryTexture(FoodIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Bone, "Bone");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(BoneIcon);
        GameState.ItemCreationApi.SetInventoryTexture(BoneIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PotionTool, "PotionTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(BoneIcon);
        GameState.ItemCreationApi.SetInventoryTexture(BoneIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPotion);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.HealthPositon, "HealthPosition");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Potion);
        GameState.ItemCreationApi.SetTexture(BoneIcon);
        GameState.ItemCreationApi.SetInventoryTexture(BoneIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.DrinkPotion);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Ore, "Ore");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(OreIcon);
        GameState.ItemCreationApi.SetInventoryTexture(OreIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PlacementTool, "PlacementTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.BuildTools);
        GameState.ItemCreationApi.SetTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.PlaceTilMoonAction);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PlacementMaterialTool, "PlaceMaterial");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.BuildTools);
        GameState.ItemCreationApi.SetTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMaterialPlacement);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PlacementToolBack, "BackgroundPlacementTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PlacementToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.PlaceTilBackgroundAction);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.RemoveTileTool, "RemoveTileTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(RemoveToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(RemoveToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionRemoveTile);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SpawnEnemySlimeTool, "SpawnSlimeTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(SlimeIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SlimeIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionEnemySpawn);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SpawnEnemyGunnerTool, "SpawnEnemyGunnerTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(SlimeIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SlimeIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionEnemyGunnerSpawn);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SpawnEnemySwordmanTool, "SpawnEnemySwordmanTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(SlimeIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SlimeIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionEnemySwordmanSpawn);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.PipePlacementTool, "PipePlacementTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(PipePlacementToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PipePlacementToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.PlaceTilPipeAction);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.MiningLaserTool, "MiningLaserTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(MiningLaserToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(MiningLaserToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMiningLaser);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.ParticleEmitterPlacementTool, "ParticleEmitterPlacementTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(OreIcon);
        GameState.ItemCreationApi.SetInventoryTexture(OreIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPlaceParticle);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.ChestPlacementTool, "ChestPlacementTool");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(OreIcon);
        GameState.ItemCreationApi.SetInventoryTexture(OreIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPlaceChest);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.MajestyPalm, "MajestyPlant");
        GameState.ItemCreationApi.SetTexture(MajestyPalmIcon);
        GameState.ItemCreationApi.SetInventoryTexture(MajestyPalmIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPlanter);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SagoPalm, "SagoPlant");
        GameState.ItemCreationApi.SetTexture(SagoPalmIcon);
        GameState.ItemCreationApi.SetInventoryTexture(SagoPalmIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPlanter);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.DracaenaTrifasciata, "DracaenaTrifasciata");
        GameState.ItemCreationApi.SetTexture(DracaenaTrifasciataIcon);
        GameState.ItemCreationApi.SetInventoryTexture(DracaenaTrifasciataIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionPlanter);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.WaterBottle, "Water");
        GameState.ItemCreationApi.SetTexture(WaterIcon);
        GameState.ItemCreationApi.SetInventoryTexture(WaterIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionWater);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.HarvestTool, "HarvestTool");
        GameState.ItemCreationApi.SetTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(SwordSpriteId);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionHarvest);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.ConstructionTool, "ConstructionTool");
        GameState.ItemCreationApi.SetTexture(ConstructionToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(ConstructionToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionConstruction);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Chest, "Chest");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Mech);
        GameState.ItemCreationApi.SetTexture(ChestIconItem);
        GameState.ItemCreationApi.SetInventoryTexture(ChestIconItem);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMechPlacement);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SmashableBox, "SmashableBox");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Mech);
        GameState.ItemCreationApi.SetTexture(ChestIconItem);
        GameState.ItemCreationApi.SetInventoryTexture(ChestIconItem);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMechPlacement);
        GameState.ItemCreationApi.EndItem();
        
        GameState.ItemCreationApi.CreateItem(Enums.ItemType.SmashableEgg, "SmashableEgg");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Mech);
        GameState.ItemCreationApi.SetTexture(ChestIconItem);
        GameState.ItemCreationApi.SetInventoryTexture(ChestIconItem);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMechPlacement);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Planter, "Planter");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Mech);
        GameState.ItemCreationApi.SetTexture(PotIconItem);
        GameState.ItemCreationApi.SetInventoryTexture(PotIconItem);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMechPlacement);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Light, "Light");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Mech);
        GameState.ItemCreationApi.SetTexture(Light2IconItem);
        GameState.ItemCreationApi.SetInventoryTexture(Light2IconItem);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionMechPlacement);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.RemoveMech, "RemoveMech");
        GameState.ItemCreationApi.SetTexture(ConstructionToolIcon);
        GameState.ItemCreationApi.SetInventoryTexture(ConstructionToolIcon);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.PlacementTool);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionRemoveMech);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.ScannerTool, "ScannerTool");
        GameState.ItemCreationApi.SetTexture(OreSprite);
        GameState.ItemCreationApi.SetInventoryTexture(OreSprite);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionScanner);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Helmet, "Helmet");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Helmet);
        GameState.ItemCreationApi.SetTexture(HemeltSprite);
        GameState.ItemCreationApi.SetInventoryTexture(HemeltSprite);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Suit, "Suit");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.Armour);
        GameState.ItemCreationApi.SetTexture(SuitSprite);
        GameState.ItemCreationApi.SetInventoryTexture(SuitSprite);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();


        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Moon, "Moon");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(BedrockIcon);
        GameState.ItemCreationApi.SetInventoryTexture(BedrockIcon);
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.Stackable);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Dirt, "Dirt");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(DirtIcon);
        GameState.ItemCreationApi.SetInventoryTexture(DirtIcon);
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.Stackable);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Bedrock, "Bedrock");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(BedrockIcon);
        GameState.ItemCreationApi.SetInventoryTexture(BedrockIcon);
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.Stackable);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Pipe, "Pipe");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(PipeIcon);
        GameState.ItemCreationApi.SetInventoryTexture(PipeIcon);
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.Stackable);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Wire, "Wire");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(WireIcon);
        GameState.ItemCreationApi.SetInventoryTexture(WireIcon);
        GameState.ItemCreationApi.SetFlags(Item.ItemProprieties.Flags.Stackable);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.GasBomb, "GasBomb");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(GrenadeSprite5);
        GameState.ItemCreationApi.SetInventoryTexture(GrenadeSprite5);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionGasBomb);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.FragGrenade, "FragGrenade");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(GrenadeSpriteId);
        GameState.ItemCreationApi.SetInventoryTexture(GrenadeSpriteId);
        GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
        GameState.ItemCreationApi.SetAction(Enums.ActionType.FragGrenade);
        GameState.ItemCreationApi.EndItem();

    }

    private static void CreateAgents()
    {
        GameState.AgentCreationApi.Create((int)Enums.AgentType.Player);
        GameState.AgentCreationApi.SetName("player");
        GameState.AgentCreationApi.SetMovement(10f, 3.5f, 2);
        GameState.AgentCreationApi.SetHealth(300.0f);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.35f, 0.0f), new Vec2f(0.75f, 2.8f));
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.Agent);
        GameState.AgentCreationApi.SetName("agent");
        GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.25f, 0.0f), new Vec2f(0.5f, 1.5f));
        GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.CharacterMoveLeft);
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.Slime);
        GameState.AgentCreationApi.SetName("Slime");
        GameState.AgentCreationApi.SetMovement(5f, 3.5f, 1);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
        GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Slime);
        GameState.AgentCreationApi.SetDetectionRadius(4.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.SetAttackCooldown(0.8f);
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.FlyingSlime);
        GameState.AgentCreationApi.SetName("Flying Slime");
        GameState.AgentCreationApi.SetFlyingMovement(3.0f);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(0.125f, 0.0f), new Vec2f(0.75f, 0.5f));
        GameState.AgentCreationApi.SetStartingAnimation((int)Animation.AnimationType.SlimeMoveLeft);
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Slime);
        GameState.AgentCreationApi.SetDetectionRadius(4.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.SetAttackCooldown(0.8f);
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemySwordman);
        GameState.AgentCreationApi.SetName("enemy-swordman");
        GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Swordman);
        GameState.AgentCreationApi.SetDetectionRadius(16.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyGunner);
        GameState.AgentCreationApi.SetName("enemy-gunner");
        GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Gunner);
        GameState.AgentCreationApi.SetDetectionRadius(24.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.End();

        GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyInsect);
        GameState.AgentCreationApi.SetName("enemy-insect");
        GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Insect);
        GameState.AgentCreationApi.SetDetectionRadius(16.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.End();


        GameState.AgentCreationApi.Create((int)Enums.AgentType.EnemyHeavy);
        GameState.AgentCreationApi.SetName("enemy-insect-heavy");
        GameState.AgentCreationApi.SetMovement(3f, 3.5f, 2);
        GameState.AgentCreationApi.SetDropTableID(Enums.LootTableType.SlimeEnemyDrop, Enums.LootTableType.SlimeEnemyDrop);
        GameState.AgentCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.5f));
        GameState.AgentCreationApi.SetCollisionBox(new Vec2f(-0.25f, 0.0f), new Vec2f(0.75f, 2.5f));
        GameState.AgentCreationApi.SetEnemyBehaviour(Agent.EnemyBehaviour.Insect);
        GameState.AgentCreationApi.SetDetectionRadius(16.0f);
        GameState.AgentCreationApi.SetHealth(100.0f);
        GameState.AgentCreationApi.End();
    }

    private static void CreateMechs()
    {
        GameState.MechCreationApi.Create((int)Mech.MechType.Storage);
        GameState.MechCreationApi.SetName("chest");
        GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.ChestDrop);
        GameState.MechCreationApi.SetTexture(ChestIcon);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1f, 1.0f));
        GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
        GameState.MechCreationApi.End();

        GameState.MechCreationApi.Create((int)Mech.MechType.Planter);
        GameState.MechCreationApi.SetName("planter");
        GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.PlanterDrop);
        GameState.MechCreationApi.SetTexture(PotIcon);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
        GameState.MechCreationApi.End();

        GameState.MechCreationApi.Create((int)Mech.MechType.Light);
        GameState.MechCreationApi.SetName("light");
        GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.LightDrop);
        GameState.MechCreationApi.SetTexture(Light2Icon);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
        GameState.MechCreationApi.End();

        GameState.MechCreationApi.Create((int)Mech.MechType.MajestyPalm);
        GameState.MechCreationApi.SetName("majesty");
        GameState.MechCreationApi.SetTexture(MajestyPalm);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
        GameState.MechCreationApi.End();

        GameState.MechCreationApi.Create((int)Mech.MechType.SagoPalm);
        GameState.MechCreationApi.SetName("sago");
        GameState.MechCreationApi.SetTexture(SagoPalm);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
        GameState.MechCreationApi.End();

        GameState.MechCreationApi.Create((int)Mech.MechType.DracaenaTrifasciata);
        GameState.MechCreationApi.SetName("dracaenatrifasciata");
        GameState.MechCreationApi.SetTexture(DracaenaTrifasciata);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
        GameState.MechCreationApi.End();
        
        GameState.MechCreationApi.Create((int)Mech.MechType.SmashableBox);
        GameState.MechCreationApi.SetName("smashableBox");
        GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.SmashableBoxDrop);
        GameState.MechCreationApi.SetTexture(ChestIcon);
        GameState.MechCreationApi.SetAction(Enums.ActionType.OpenChestAction);
        GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
        GameState.MechCreationApi.SetDurability(100);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
		GameState.MechCreationApi.End();
        
        GameState.MechCreationApi.Create((int)Mech.MechType.SmashableEgg);
        GameState.MechCreationApi.SetName("smashableEgg");
        GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.SmashableEggDrop);
        GameState.MechCreationApi.SetTexture(ChestIcon);
        GameState.MechCreationApi.SetDurability(100);
        GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
        GameState.MechCreationApi.End();
    }

    private static void CreateParticles()
    {
        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Ore);
        GameState.ParticleCreationApi.SetName("Ore");
        GameState.ParticleCreationApi.SetDecayRate(1.0f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -20.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
        GameState.ParticleCreationApi.SetDeltaScale(0.0f);
        GameState.ParticleCreationApi.SetSpriteId(OreIcon);
        GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 10.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.End();

        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.OreExplosionParticle);
        GameState.ParticleCreationApi.SetName("ore-explosion-particle");
        GameState.ParticleCreationApi.SetDecayRate(1.0f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(130.0f);
        GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
        GameState.ParticleCreationApi.SetSpriteId(OreIcon);
        GameState.ParticleCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.End();

        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.DustParticle);
        GameState.ParticleCreationApi.SetName("dust-particle");
        GameState.ParticleCreationApi.SetDecayRate(4.0f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(0);
        GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
        GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Dust);
        GameState.ParticleCreationApi.SetSize(new Vec2f(0.35f, 0.35f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.End();


        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Debris);
        GameState.ParticleCreationApi.SetName("debris");
        GameState.ParticleCreationApi.SetDecayRate(0.5f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -15.0f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.SetIsCollidable(true);
        GameState.ParticleCreationApi.End();

        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.GasParticle);
        GameState.ParticleCreationApi.SetName("gas-particle");
        GameState.ParticleCreationApi.SetDecayRate(0.17f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(0);
        GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
        GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Smoke);
        GameState.ParticleCreationApi.SetSize(new Vec2f(4.5f, 4.5f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetStartingRotation(10.3f);
        GameState.ParticleCreationApi.SetStartingScale(20.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255f, 72f, 0f, 255.0f));
        GameState.ParticleCreationApi.End();


        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Blood);
        GameState.ParticleCreationApi.SetName("Blood");
        GameState.ParticleCreationApi.SetDecayRate(0.5f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, -10.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(90.0f);
        GameState.ParticleCreationApi.SetDeltaScale(0.0f);
        GameState.ParticleCreationApi.SetSpriteId(BloodSprite);
        GameState.ParticleCreationApi.SetSize(new Vec2f(0.075f, 0.075f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(1.0f, 5.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.SetIsCollidable(true);
        GameState.ParticleCreationApi.End();

        GameState.ParticleCreationApi.Create((int)Particle.ParticleType.Explosion);
        GameState.ParticleCreationApi.SetName("explosion");
        GameState.ParticleCreationApi.SetDecayRate(2.0f);
        GameState.ParticleCreationApi.SetAcceleration(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetDeltaRotation(0);
        GameState.ParticleCreationApi.SetDeltaScale(-1.0f);
        GameState.ParticleCreationApi.SetAnimationType(Animation.AnimationType.Explosion);
        GameState.ParticleCreationApi.SetSize(new Vec2f(3.0f, 3.0f));
        GameState.ParticleCreationApi.SetStartingVelocity(new Vec2f(0.0f, 0.0f));
        GameState.ParticleCreationApi.SetStartingRotation(0.0f);
        GameState.ParticleCreationApi.SetStartingScale(1.0f);
        GameState.ParticleCreationApi.SetStartingColor(new Color(255.0f, 255.0f, 255.0f, 255.0f));
        GameState.ParticleCreationApi.End();
    }

    private static void CreateParticleEmitters()
    {
        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.OreFountain);
        GameState.ParticleEmitterCreationApi.SetName("ore-fountain");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Ore);
        GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(1);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(0.05f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, 0.0f), new Vec2f(1.0f, 0.0f));
        GameState.ParticleEmitterCreationApi.End();

        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.OreExplosion);
        GameState.ParticleEmitterCreationApi.SetName("ore-explosion");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.OreExplosionParticle);
        GameState.ParticleEmitterCreationApi.SetDuration(0.15f);
        GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(15);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.0f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-10.0f, -10.0f), new Vec2f(10.0f, 10.0f));
        GameState.ParticleEmitterCreationApi.End();
        
        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.DustEmitter);
        GameState.ParticleEmitterCreationApi.SetName("dust-emitter");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.DustParticle);
        GameState.ParticleEmitterCreationApi.SetDuration(0.1f);
        GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(1);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
        GameState.ParticleEmitterCreationApi.End();

        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.GasEmitter);
        GameState.ParticleEmitterCreationApi.SetName("gas-emitter");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.GasParticle);
        GameState.ParticleEmitterCreationApi.SetDuration(0.5f);
        GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.25f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(1);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(1.02f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0), new Vec2f(0.0f, 0));
        GameState.ParticleEmitterCreationApi.End();

        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.Blood);
        GameState.ParticleEmitterCreationApi.SetName("blood");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Blood);
        GameState.ParticleEmitterCreationApi.SetDuration(2.0f);
        GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.1f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(30);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(3.0f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(-1.0f, -1.0f), new Vec2f(1.0f, 1.0f));
        GameState.ParticleEmitterCreationApi.End();

        GameState.ParticleEmitterCreationApi.Create((int)Particle.ParticleEmitterType.ExplosionEmitter);
        GameState.ParticleEmitterCreationApi.SetName("blood");
        GameState.ParticleEmitterCreationApi.SetParticleType(Particle.ParticleType.Explosion);
        GameState.ParticleEmitterCreationApi.SetDuration(4.0f);
        GameState.ParticleEmitterCreationApi.SetSpawnRadius(0.7f);
        GameState.ParticleEmitterCreationApi.SetParticleCount(5);
        GameState.ParticleEmitterCreationApi.SetTimeBetweenEmissions(10.0f);
        GameState.ParticleEmitterCreationApi.SetVelocityInterval(new Vec2f(0.0f, 0.0f), new Vec2f(0.0f, 0.0f));
        GameState.ParticleEmitterCreationApi.End();
    }


    private static void CreateProjectiles()
    {
        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Bullet);
        GameState.ProjectileCreationApi.SetName("bullet");
        GameState.ProjectileCreationApi.SetSpriteId(OreIcon);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.33f, 0.33f));
        GameState.ProjectileCreationApi.SetStartVelocity(0.50f);
        GameState.ProjectileCreationApi.SetLinearDrag(0.73f, 0.01f);
        GameState.ProjectileCreationApi.End();

        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Grenade);
        GameState.ProjectileCreationApi.SetName("grenade");
        GameState.ProjectileCreationApi.SetSpriteId(GrenadeSpriteId);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ProjectileCreationApi.SetStartVelocity(15.0f);
        GameState.ProjectileCreationApi.SetAffectedByGravity();
        GameState.ProjectileCreationApi.End();

        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.GasGrenade);
        GameState.ProjectileCreationApi.SetName("gas_grenade");
        GameState.ProjectileCreationApi.SetSpriteId(GrenadeSprite5);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ProjectileCreationApi.SetStartVelocity(15.0f);
        GameState.ProjectileCreationApi.SetLinearDrag(0, 0);
        GameState.ProjectileCreationApi.SetBounce(0.5f);
        GameState.ProjectileCreationApi.SetAffectedByGravity();
        GameState.ProjectileCreationApi.End();

        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Rocket);
        GameState.ProjectileCreationApi.SetName("rocket");
        GameState.ProjectileCreationApi.SetSpriteId(GrenadeSpriteId);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ProjectileCreationApi.SetStartVelocity(20.0f);
        GameState.ProjectileCreationApi.SetRamp(40f);
        GameState.ProjectileCreationApi.SetRampAcceleration(4.0f);
        GameState.ProjectileCreationApi.End();

        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Arrow);
        GameState.ProjectileCreationApi.SetName("arrow");
        GameState.ProjectileCreationApi.SetSpriteId(OreIcon);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
        GameState.ProjectileCreationApi.SetStartVelocity(20.0f);
        GameState.ProjectileCreationApi.SetAffectedByGravity();
        GameState.ProjectileCreationApi.End();
    }

    private static void CreateVehicles()
    {
        GameState.VehicleCreationApi.Create((int)Enums.VehicleType.Jet);
        GameState.VehicleCreationApi.SetName("Car");
        GameState.VehicleCreationApi.SetSpriteId(JetChassis);
        GameState.VehicleCreationApi.SetSize(new Vec2f(3.0f, 3.0f));
        GameState.VehicleCreationApi.SetCollisionSize(new Vec2f(2.0f, 2.0f));
        GameState.VehicleCreationApi.SetCollisionOffset(new Vec2f(0, -3.0f));
        GameState.VehicleCreationApi.SetScale(new Vec2f(1.0f, 1.0f));
        GameState.VehicleCreationApi.SetRotation(-90.0f);
        GameState.VehicleCreationApi.SetAngularVelocity(Vec2f.Zero);
        GameState.VehicleCreationApi.SetAngularMass(14f);
        GameState.VehicleCreationApi.SetAngularAcceleration(4f);
        GameState.VehicleCreationApi.SetCenterOfGravity(-6f);
        GameState.VehicleCreationApi.SetCenterOfRotation(Vec2f.Zero);
        GameState.VehicleCreationApi.SetAffectedByGravity(true);
        GameState.VehicleCreationApi.End();

    }
 
}
