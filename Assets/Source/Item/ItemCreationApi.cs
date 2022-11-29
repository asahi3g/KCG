//imports UnityEngine

using System;
using Enums;
using Enums.PlanetTileMap;
using KGUI;
using KMath;
using Unity.VisualScripting;
using Utility;

/*
    How To use it:
        CreateItem(Item Type, Item Type Name);
        SetTexture(SpriteSheetID);
        SetInventoryItemIcon(SpriteSheetID);
        MakeStackable(Max number of items in a stack.);
        EndItem();
*/

namespace Item
{
    public class ItemCreationApi
    {
        // Constructor is called before the first frame update.
         
        // Note[Joao] this arrays are very memory expensive: use array of pointers instead?
        private ItemProperties[] PropertiesArray;
        private FireWeaponProperties[] WeaponList;

        private ItemType currentIndex;
        private int weaponListSize;

        public ItemCreationApi()
        {
            int length = Enum.GetValues(typeof(ItemType)).Length - 1; // -1 beacause of error item type.
            PropertiesArray = new ItemProperties[length];
            WeaponList = new FireWeaponProperties[16];
            currentIndex = ItemType.Error;
            weaponListSize = 0;

            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i].ItemType = currentIndex;
            }
        }

        public string GetLabel(ItemType type)
        {
            string itemLabel = PropertiesArray[(int)type].ItemLabel;

            return itemLabel;
        }


        public ItemProperties GetItemProperties(ItemType type)
        {
            ItemType itemType = PropertiesArray[(int)type].ItemType;
            IsItemTypeValid(itemType);

            return PropertiesArray[(int)type];
        }

        public FireWeaponProperties GetWeapon(ItemType type)
        {
            ItemType itemType = PropertiesArray[(int)type].ItemType;
            IsItemTypeValid(itemType);

            return WeaponList[PropertiesArray[(int)type].FireWeaponID];
        }

        public void CreateItem(ItemType itemType, string name)
        {
            currentIndex = itemType;

            PropertiesArray[(int)itemType].ItemType = itemType;
            PropertiesArray[(int)itemType].MechType = MechType.Error;
            PropertiesArray[(int)itemType].ItemLabel = name;
        }

        public void SetName(string name)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ItemLabel = name;
        }

        public void SetGroup(ItemGroups group)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].Group = group;
        }

        public void SetMech(MechType mech)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].MechType = mech;
        }

        public void SetTile(TileID tile)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].TileType = tile;
        }

        public void SetTexture(int spriteId)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].SpriteID = spriteId;
        }


        public void SetInventoryItemIcon(int spriteId)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].InventorSpriteID = spriteId;
        }

        public void SetAction(ItemUsageActionType  nodeID)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ToolActionType = nodeID;
            PropertiesArray[(int)currentIndex].ItemFlags |= ItemProperties.Flags.Tool;
        }

        public void SetConsumable()
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ItemFlags |= ItemProperties.Flags.Consumable;
        }

        public void SetStackable(int maxStackCount = 99)
        {
            IsItemTypeValid();
            PropertiesArray[(int)currentIndex].MaxStackCount = maxStackCount;
            PropertiesArray[(int)currentIndex].ItemFlags |= ItemProperties.Flags.Stackable;
        }

        public void SetUIPanel(PanelEnums panelEnums)
        {
            PropertiesArray[(int) currentIndex].ItemFlags |= ItemProperties.Flags.UI;
            PropertiesArray[(int) currentIndex].ItemPanelEnums = panelEnums;
        }

        public void SetPlaceable()
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ItemFlags |= ItemProperties.Flags.Placeable;
        }

        public void SetSpreadAngle(float spreadAngle)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.SpreadAngle = spreadAngle;
        }

        public void SetRangedWeaponAttribute (float bulletSpeed, float coolDown, float range, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = new FireWeaponProperties
            {
                BulletSpeed = bulletSpeed,
                CoolDown = coolDown,
                Range = range,
                BasicDemage = basicDamage,
            };

            if (weaponListSize == WeaponList.Length)
            {
                System.Array.Resize(ref WeaponList, weaponListSize + 16);
            }

            WeaponList[weaponListSize] = fireWeaponProperties;
            PropertiesArray[(int)currentIndex].FireWeaponID = weaponListSize++;
        }

        public void SetRangedWeaponAttribute(float bulletSpeed, float coolDown, float range, bool isLaunchGrenade, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = new FireWeaponProperties
            {
                BulletSpeed = bulletSpeed,
                CoolDown = coolDown,
                Range = range,
                IsLaunchGreanade = isLaunchGrenade,
                BasicDemage = basicDamage,
            };

            if (weaponListSize == WeaponList.Length)
            {
                System.Array.Resize(ref WeaponList, weaponListSize + 16);
            }

            WeaponList[weaponListSize] = fireWeaponProperties;
            PropertiesArray[(int)currentIndex].FireWeaponID = weaponListSize++;
        }

        public void SetRangedWeaponClip(int clipSize, int bulletsPerShot, float reloadTime)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.ClipSize = clipSize;
            fireWeaponProperties.BulletsPerShot = bulletsPerShot;
            fireWeaponProperties.ReloadTime = reloadTime;
            fireWeaponProperties.WeaponFlags |= FireWeaponProperties.Flags.HasClip;
        }

        public void SetRangedWeaponClip(int bulletClipSize, int greandeClipSize, int bulletsPerShot, float bulletReloadTime)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.ClipSize = bulletClipSize;
            fireWeaponProperties.GrenadeClipSize = greandeClipSize;
            fireWeaponProperties.NumberOfGrenades = greandeClipSize;
            fireWeaponProperties.BulletsPerShot = bulletsPerShot;
            fireWeaponProperties.ReloadTime = bulletReloadTime;
            fireWeaponProperties.WeaponFlags |= FireWeaponProperties.Flags.HasClip | FireWeaponProperties.Flags.PulseWeapon;
        }

        public void SetFireWeaponMultiShoot(float speadAngle, int numOfBullet)
        {
            IsItemTypeValid();

            ref FireWeaponProperties fireWeaponProperties = ref WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.SpreadAngle = speadAngle;
            fireWeaponProperties.NumOfBullets = numOfBullet;
        }

        public void SetFireWeaponRecoil(float maxRecoilAngle, float minRecoilAngle, float rateOfChange, float recoverTime, float recoverDelay)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.MaxRecoilAngle = maxRecoilAngle;
            fireWeaponProperties.MinRecoilAngle = minRecoilAngle;
            fireWeaponProperties.RateOfChange = rateOfChange;
            fireWeaponProperties.RecoverTime = recoverTime;
            fireWeaponProperties.RecoverDelay = recoverDelay;
        }

        public void SetMeleeWeapon(float coolDown, float range, float staggerTime, float staggerRate, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = new FireWeaponProperties
            {
                CoolDown = coolDown,
                Range = range,
                StaggerTime = staggerTime,
                StaggerRate = staggerRate,
                BasicDemage = basicDamage,
            };

            if (weaponListSize == WeaponList.Length)
            {
                System.Array.Resize(ref WeaponList, weaponListSize + 16);
            }

            WeaponList[weaponListSize] = fireWeaponProperties;
            PropertiesArray[(int)currentIndex].FireWeaponID = weaponListSize++;
        }

        public void SetShield(bool ShieldActive)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = new FireWeaponProperties
            {
                ShieldActive = ShieldActive,
            }; 

            if (weaponListSize == WeaponList.Length)
            {
                System.Array.Resize(ref WeaponList, weaponListSize + 16);
            }

            WeaponList[weaponListSize] = fireWeaponProperties;
            PropertiesArray[(int)currentIndex].FireWeaponID = weaponListSize++;
        }

        public void SetExplosion(float radius, int maxDamage, float elapse)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeapon = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeapon.BlastRadius = radius;
            fireWeapon.MaxDamage = maxDamage;
            fireWeapon.Elapse = elapse;
        }

        public void SetFlags(FireWeaponProperties.MeleeFlags flags)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeapon = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeapon.MeleeAttackFlags |= flags;
        }

        public void SetFlags(FireWeaponProperties.Flags flags)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.WeaponFlags |= flags;
        }

        public void SetFlags(FireWeaponProperties.GrenadesFlags flags)
        {
            IsItemTypeValid();

            FireWeaponProperties fireWeaponProperties = WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeaponProperties.GrenadeFlags |= flags;
        }

        public void SetFlags(ItemProperties.Flags flags)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ItemFlags |= flags;
        }

        public void SetProjectileType(ProjectileType projectileType)
        {
            IsItemTypeValid();

            ref var fireWeapon = ref WeaponList[PropertiesArray[(int)currentIndex].FireWeaponID];
            fireWeapon.ProjectileType = projectileType;
        }

        public void SetItemToolType(ItemToolType type)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].ToolType = type;
        }

        public void SetAnimationSet(ItemAnimationSet animationSet)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].AnimationSet = animationSet;
        }

        public void SetItemKeyUsage(ItemKeyUsage usage)
        {
            IsItemTypeValid();

            PropertiesArray[(int)currentIndex].KeyUsage = usage;
        }

        public void EndItem()
        {
            // Todo: Check if ItemType is valid in debug mode.
            currentIndex = ItemType.Error;
        }

        private void IsItemTypeValid(ItemType itemType)
        {
#if DEBUG
            if (itemType == ItemType.Error)
            {
                UnityEngine.Debug.Log("Not valid ItemType");
                Utils.Assert(false);
            }
#endif
        }

        private void IsItemTypeValid()
        {
            IsItemTypeValid(currentIndex);
        }

        public int SniperRifleIcon;
        public int LongRifleIcon;
        public int PulseIcon;
        public int SMGIcon;
        public int ShotgunIcon;
        public int PistolIcon;
        public int GunSpriteSheet;
        public int RPGIcon;
        public int GrenadeSpriteId;
        public int GrenadeSpriteSheet;
        public int GrenadeSprite5;
        public int SwordSpriteId;
        public int SwordSpriteSheet;
        public int OreIcon;
        public int OreSpriteSheet;
        public int SlimeIcon;
        public int SlimeSpriteSheet;
        public int FoodIcon;
        public int FoodSpriteSheet;
        public int BoneSpriteSheet;
        public int BoneIcon;
        public int PlacementToolIcon;
        public int RockSpriteSheet;
        public int RemoveToolIcon;
        public int Ore2SpriteSheet;
        public int PipePlacementToolIcon;
        public int pipeIconSpriteSheet;
        public int MiningLaserToolIcon;
        public int LaserSpriteSheet;
        public int MajestyPalmIcon;
        public int SagoPalmIcon;
        public int DracaenaTrifasciataIcon;
        public int WaterIcon;
        public int ConstructionToolIcon;
        public int ChestIconItem;
        public int PotIconItem;
        public int Light2IconItem;
        public int HemeltSprite;
        public int HelmetsSpriteSheet;
        public int SuitSprite;
        public int SuitsSpriteSheet;
        public int BedrockIcon;
        public int DirtIcon;
        public int PipeIcon;
        public int WireIcon;
        public int DyeSlotIcon;
        public int HelmetSlotIcon;
        public int ArmourSlotIcon;
        public int GlovesSlotIcon;
        public int RingSlotIcon;
        public int BeltSlotIcon;
        public int OreSprite;
        public int Ore2Sprite;
        public int Ore3Sprite;
        public int ChestIconParticle;
        public int WoodTile;

        public int MineOreSheet;

        public int Diamond_0;
        public int Diamond_1;
        public int Diamond_2;
        public int Diamond_3;
        public int Diamond_4;
        public int Diamond_5;
        public int Diamond_6;
        public int Diamond_7;

        public int PinkDiamond_0;
        public int PinkDiamond_1;
        public int PinkDiamond_2;
        public int PinkDiamond_3;
        public int PinkDiamond_4;
        public int PinkDiamond_5;
        public int PinkDiamond_6;
        public int PinkDiamond_7;

        public int RedStone_0;
        public int RedStone_1;
        public int RedStone_2;
        public int RedStone_3;
        public int RedStone_4;
        public int RedStone_5;
        public int RedStone_6;
        public int RedStone_7;

        public int Emerald_0;
        public int Emerald_1;
        public int Emerald_2;
        public int Emerald_3;
        public int Emerald_4;
        public int Emerald_5;
        public int Emerald_6;
        public int Emerald_7;

        public int Lapis_0;
        public int Lapis_1;
        public int Lapis_2;
        public int Lapis_3;
        public int Lapis_4;
        public int Lapis_5;
        public int Lapis_6;
        public int Lapis_7;

        public int Coal_0;
        public int Coal_1;
        public int Coal_2;
        public int Coal_3;
        public int Coal_4;
        public int Coal_5;
        public int Coal_6;
        public int Coal_7;

        public int Iron_0;
        public int Iron_1;
        public int Iron_2;
        public int Iron_3;
        public int Iron_4;
        public int Iron_5;
        public int Iron_6;
        public int Iron_7;

        public int Gold_0;
        public int Gold_1;
        public int Gold_2;
        public int Gold_3;
        public int Gold_4;
        public int Gold_5;
        public int Gold_6;
        public int Gold_7;

        private int IconToolPlacement;
        private int IconToolRemoveTile;
        private int IconToolGeometryPlacement;
        private int IconToolSpawnEnemyGunner;
        private int IconToolSpawnEnemySwordsman;
        private int IconToolConstruction;
        
        private int IconItemPotionHealth;
        

        public PanelUI PlacementToolPrefab;

        public void InitializeResources()
        {
            //Weapons
            GunSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Weapons\\Pistol\\icon_weapon_pistol.png", 32, 32);
            SMGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Weapons\\Smg\\icon_weapon_smg.png", 32, 32);
            
            SniperRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun8.png", 48, 16);
            LongRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun10.png", 48, 16);
            PulseIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun17.png", 48, 16);
            ShotgunIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun13.png", 48, 16);
            RPGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun18.png", 48, 16);
            GrenadeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades1.png", 16, 16);
            GrenadeSprite5 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades5.png", 16, 16);
            SwordSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Swords\\Sword1.png", 16, 48);
            
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            SlimeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Enemies\\Slime\\slime.png", 32, 32);
            FoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Food\\Food.png", 60, 60);
            BoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Bone\\Bone.png", 32, 32);
            RockSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Rock\\rock1.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            pipeIconSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", 16, 16);
            LaserSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\RailGun\\lasergun-temp.png", 32, 32);
            MajestyPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png", 16, 16);
            SagoPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png", 16, 16);
            DracaenaTrifasciataIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", 16, 16);
            WaterIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Water\\water_12px.png", 12, 12);
            ConstructionToolIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Development\\Furnitures\\Furniture2\\dev-furniture-2.png", 32, 32);
            int ChestIconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
            PotIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);
            Light2IconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png", 48, 16);
            HelmetsSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Character\\Helmets\\character-helmets.png", 64, 64);
            SuitsSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Character\\Suits\\character-suits.png", 64, 96);
            BedrockIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png", 16, 16);
            DirtIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png", 16, 16);
            PipeIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", 16, 16);
            WireIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png", 128, 128);
            DyeSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Dye.png", 64, 64);
            HelmetSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Helmet.png", 64, 64);
            ArmourSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Armour.png", 64, 64);
            GlovesSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Gloves.png", 64, 64);
            RingSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Ring.png", 64, 64);
            BeltSlotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\TestInventory\\Belt.png", 64, 64);
            WoodTile = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\MixedTileset\\mixed-tileset_00.png", 32, 32);
            MineOreSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Gems-Ores\\gems-ores.png", 16, 16);
            
            // === Items ===
            
            IconItemPotionHealth = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\icon_potion_health.png", 32, 32);
            IconItemPotionHealth = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconItemPotionHealth, 0, 0, AtlasType.Particle);
            
            
            // === Tools ===
            
            // PlacementTool
            IconToolPlacement = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_placement.png", 32, 32);
            IconToolPlacement = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolPlacement, 0, 0, AtlasType.Particle);
            
            // Remove Tile Tool
            IconToolRemoveTile = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_remove_tile.png", 32, 32);
            IconToolRemoveTile = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolRemoveTile, 0, 0, AtlasType.Particle);
            
            // Geometry Placement Tool
            IconToolGeometryPlacement = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_geometry_placement.png", 32, 32);
            IconToolGeometryPlacement = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolGeometryPlacement, 0, 0, AtlasType.Particle);
            
            // Spawn Enemy Gunner
            IconToolSpawnEnemyGunner = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_spawn_enemy_gunner.png", 32, 32);
            IconToolSpawnEnemyGunner = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolSpawnEnemyGunner, 0, 0, AtlasType.Particle);
            
            // Spawn Enemy Swordsman
            IconToolSpawnEnemySwordsman = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_spawn_enemy_swordsman.png", 32, 32);
            IconToolSpawnEnemySwordsman = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolSpawnEnemySwordsman, 0, 0, AtlasType.Particle);
            
            // Construction Tool
            IconToolConstruction = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tools\\icon_tool_construction.png", 32, 32);
            IconToolConstruction = GameState.SpriteAtlasManager.CopySpriteToAtlas(IconToolConstruction, 0, 0, AtlasType.Particle);
            


            SniperRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SniperRifleIcon, 0, 0, AtlasType.Particle);
            LongRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LongRifleIcon, 0, 0, AtlasType.Particle);
            PulseIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PulseIcon, 0, 0, AtlasType.Particle);
            SMGIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SMGIcon, 0, 0, AtlasType.Particle);
            ShotgunIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ShotgunIcon, 0, 0, AtlasType.Particle);
            PistolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GunSpriteSheet, 0, 0, AtlasType.Particle);
            GrenadeSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSpriteSheet, 0, 0, AtlasType.Particle);
            GrenadeSprite5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSprite5, 0, 0, AtlasType.Particle);
            SwordSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(SwordSpriteSheet, 0, 0, AtlasType.Particle);
            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, AtlasType.Particle);
            SlimeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 0, 0, AtlasType.Particle);
            FoodIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(FoodSpriteSheet, 0, 0, AtlasType.Particle);
            BoneIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BoneSpriteSheet, 0, 0, AtlasType.Particle);
            PlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RockSpriteSheet, 0, 0, AtlasType.Particle);
            RemoveToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Ore2SpriteSheet, 0, 0, AtlasType.Particle);
            PipePlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(pipeIconSpriteSheet, 0, 0, AtlasType.Particle);
            MiningLaserToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LaserSpriteSheet, 0, 0, AtlasType.Particle);
            MajestyPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmIcon, 0, 0, AtlasType.Particle);
            SagoPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmIcon, 0, 0, AtlasType.Particle);
            DracaenaTrifasciataIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataIcon, 0, 0, AtlasType.Particle);
            WaterIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WaterIcon, 0, 0, AtlasType.Particle);
            ConstructionToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ConstructionToolIcon, 0, 0, AtlasType.Particle);
            ChestIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestIconSheet, 0, 0, AtlasType.Particle);
            ChestIconParticle = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestIconSheet, 0, 0, AtlasType.Particle);
            PotIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIconItem, 0, 0, AtlasType.Particle);
            Light2IconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2IconItem, 0, 0, AtlasType.Particle);
            HemeltSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetsSpriteSheet, 0, 0, AtlasType.Particle);
            SuitSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(SuitsSpriteSheet, 0, 0, AtlasType.Particle);
            BedrockIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BedrockIcon, 0, 0, AtlasType.Particle);
            DirtIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DirtIcon, 0, 0, AtlasType.Particle);
            PipeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PipeIcon, 0, 0, AtlasType.Particle);
            WireIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WireIcon, 0, 0, AtlasType.Particle);
            WoodTile = GameState.SpriteAtlasManager.CopySpriteToAtlas(WoodTile, 6, 0, AtlasType.Particle);
            DyeSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DyeSlotIcon, 0, 0, AtlasType.Gui);
            HelmetSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetSlotIcon, 0, 0, AtlasType.Gui);
            ArmourSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ArmourSlotIcon, 0, 0, AtlasType.Gui);
            GlovesSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GlovesSlotIcon, 0, 0, AtlasType.Gui);
            RingSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RingSlotIcon, 0, 0, AtlasType.Gui);
            BeltSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BeltSlotIcon, 0, 0, AtlasType.Gui);

            Diamond_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 7, AtlasType.Particle);
            Diamond_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 7, AtlasType.Particle);
            Diamond_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 7, AtlasType.Particle);
            Diamond_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 7, AtlasType.Particle);
            Diamond_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 7, AtlasType.Particle);
            Diamond_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 7, AtlasType.Particle);
            Diamond_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 7, AtlasType.Particle);
            Diamond_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 7, AtlasType.Particle);

            Gold_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 6, AtlasType.Particle);
            Gold_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 6, AtlasType.Particle);
            Gold_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 6, AtlasType.Particle);
            Gold_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 6, AtlasType.Particle);
            Gold_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 6, AtlasType.Particle);
            Gold_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 6, AtlasType.Particle);
            Gold_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 6, AtlasType.Particle);
            Gold_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 6, AtlasType.Particle);

            Iron_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 3, AtlasType.Particle);
            Iron_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 3, AtlasType.Particle);
            Iron_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 3, AtlasType.Particle);
            Iron_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 3, AtlasType.Particle);
            Iron_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 3, AtlasType.Particle);
            Iron_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 3, AtlasType.Particle);
            Iron_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 3, AtlasType.Particle);
            Iron_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 3, AtlasType.Particle);

            Lapis_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 0, AtlasType.Particle);
            Lapis_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 0, AtlasType.Particle);
            Lapis_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 0, AtlasType.Particle);
            Lapis_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 0, AtlasType.Particle);
            Lapis_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 0, AtlasType.Particle);
            Lapis_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 0, AtlasType.Particle);
            Lapis_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 0, AtlasType.Particle);
            Lapis_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 0, AtlasType.Particle);

            Emerald_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 4, AtlasType.Particle);
            Emerald_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 4, AtlasType.Particle);
            Emerald_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 4, AtlasType.Particle);
            Emerald_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 4, AtlasType.Particle);
            Emerald_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 4, AtlasType.Particle);
            Emerald_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 4, AtlasType.Particle);
            Emerald_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 4, AtlasType.Particle);
            Emerald_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 4, AtlasType.Particle);

            Coal_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 1, AtlasType.Particle);
            Coal_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 1, AtlasType.Particle);
            Coal_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 1, AtlasType.Particle);
            Coal_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 1, AtlasType.Particle);
            Coal_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 1, AtlasType.Particle);
            Coal_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 1, AtlasType.Particle);
            Coal_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 1, AtlasType.Particle);
            Coal_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 1, AtlasType.Particle);

            RedStone_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 5, AtlasType.Particle);
            RedStone_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 5, AtlasType.Particle);
            RedStone_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 5, AtlasType.Particle);
            RedStone_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 5, AtlasType.Particle);
            RedStone_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 5, AtlasType.Particle);
            RedStone_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 5, AtlasType.Particle);
            RedStone_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 5, AtlasType.Particle);
            RedStone_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 5, AtlasType.Particle);

            Lapis_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 0, AtlasType.Particle);
            Lapis_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 0, AtlasType.Particle);
            Lapis_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 0, AtlasType.Particle);
            Lapis_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 0, AtlasType.Particle);
            Lapis_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 0, AtlasType.Particle);
            Lapis_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 0, AtlasType.Particle);
            Lapis_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 0, AtlasType.Particle);
            Lapis_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 0, AtlasType.Particle);

            PinkDiamond_0 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 0, 2, AtlasType.Particle);
            PinkDiamond_1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 1, 2, AtlasType.Particle);
            PinkDiamond_2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 2, 2, AtlasType.Particle);
            PinkDiamond_3 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 3, 2, AtlasType.Particle);
            PinkDiamond_4 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 4, 2, AtlasType.Particle);
            PinkDiamond_5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 5, 2, AtlasType.Particle);
            PinkDiamond_6 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 6, 2, AtlasType.Particle);
            PinkDiamond_7 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MineOreSheet, 7, 2, AtlasType.Particle);

            CreateItem(ItemType.SniperRifle, "SniperRifle");
            SetGroup(ItemGroups.Gun);
            SetTexture(SniperRifleIcon);
            SetInventoryItemIcon(SniperRifleIcon);
            SetRangedWeaponAttribute (bulletSpeed: 200.0f, coolDown: 1f, range: 350.0f, basicDamage: 60);
            SetRangedWeaponClip(clipSize: 6, bulletsPerShot: 1, reloadTime: 1.3f);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.LongRifle, "LongRifle");
            SetGroup(ItemGroups.Gun);
            SetTexture(LongRifleIcon);
            SetInventoryItemIcon(LongRifleIcon);
            SetRangedWeaponAttribute (bulletSpeed: 50.0f, coolDown: 1f, range: 20.0f, basicDamage: 40);
            SetRangedWeaponClip(clipSize: 25, bulletsPerShot: 1, reloadTime: 2f);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.PulseWeapon, "PulseWeapon");
            SetGroup(ItemGroups.Gun);
            SetTexture(PulseIcon);
            SetInventoryItemIcon(PulseIcon);
            SetRangedWeaponAttribute (bulletSpeed: 20.0f, coolDown: 0.5f, 10.0f, false, 25);
            SetRangedWeaponClip(25, 4, 1, 1);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootPulseWeaponAction);
            EndItem();

            CreateItem(ItemType.AutoCannon, "AutoCannon");
            SetGroup(ItemGroups.Gun);
            SetTexture(LongRifleIcon);
            SetInventoryItemIcon(LongRifleIcon);
            SetRangedWeaponAttribute (bulletSpeed: 50.0f, coolDown: 0.5f, range: 20.0f, basicDamage: 40);
            SetRangedWeaponClip(clipSize: 40, bulletsPerShot: 3, reloadTime: 4f);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.SMG, "SMG");
            SetGroup(ItemGroups.Gun);
            SetTexture(SMGIcon);
            SetInventoryItemIcon(SMGIcon);
            SetRangedWeaponAttribute (bulletSpeed: 50.0f, coolDown: 0.2f, range: 20.0f, basicDamage: 15);
            SetRangedWeaponClip(clipSize: 50,  bulletsPerShot: 1, reloadTime:1f);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            SetItemToolType(ItemToolType.Pistol);
            SetItemToolType(ItemToolType.Rifle);
            SetAnimationSet(ItemAnimationSet.HoldingRifle);
            SetItemKeyUsage(ItemKeyUsage.KeyDown);
            EndItem();

            CreateItem(ItemType.Shotgun, "Shotgun");
            SetGroup(ItemGroups.Gun);
            SetTexture(ShotgunIcon);
            SetInventoryItemIcon(ShotgunIcon);
            SetRangedWeaponAttribute (bulletSpeed: 30.0f, coolDown: 1f, range: 10.0f, basicDamage: 35);
            SetSpreadAngle(1.0f);
            SetRangedWeaponClip( clipSize:6, bulletsPerShot: 2, reloadTime: 2.5f);
            SetProjectileType(ProjectileType.Bullet);
            SetFlags(FireWeaponProperties.Flags.ShouldSpread);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.PumpShotgun, "PumpShotgun");
            SetGroup(ItemGroups.Gun);
            SetTexture(ShotgunIcon);
            SetInventoryItemIcon(ShotgunIcon);
            SetRangedWeaponAttribute (bulletSpeed: 20.0f, coolDown: 2f, range: 5.0f, basicDamage: 30);
            SetSpreadAngle(1.0f);
            SetRangedWeaponClip(clipSize: 8, bulletsPerShot: 4, reloadTime: 2.5f);
            SetFlags(FireWeaponProperties.Flags.ShouldSpread);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.Pistol, "Pistol");
            SetGroup(ItemGroups.Gun);
            SetTexture(PistolIcon);
            SetInventoryItemIcon(PistolIcon);
            SetRangedWeaponAttribute (bulletSpeed: 50.0f, coolDown: 0.4f, range: 100.0f, basicDamage: 25);
            SetRangedWeaponClip(clipSize: 8, bulletsPerShot: 1, reloadTime: 1f);
            SetProjectileType(ProjectileType.Bullet);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            SetItemToolType(ItemToolType.Pistol);
            SetAnimationSet(ItemAnimationSet.HoldingPistol);
            EndItem();

            CreateItem(ItemType.RPG, "RPG");
            SetGroup(ItemGroups.Gun);
            SetTexture(RPGIcon);
            SetInventoryItemIcon(RPGIcon);
            SetRangedWeaponAttribute (bulletSpeed: 50.0f, coolDown: 3f, range: 50.0f, basicDamage: 100);
            SetRangedWeaponClip(clipSize: 2, bulletsPerShot: 1, reloadTime: 3);
            SetExplosion(3.0f, 15, 0f);
            SetProjectileType(ProjectileType.Rocket);
            SetAction(ItemUsageActionType .ThrowFragGrenadeAction);
            EndItem();

            CreateItem(ItemType.GrenadeLauncher, "GrenadeLauncher");
            SetTexture(GrenadeSpriteId);
            SetInventoryItemIcon(GrenadeSpriteId);
            SetGroup(ItemGroups.Gun);
            SetRangedWeaponAttribute (bulletSpeed: 20.0f, coolDown: 1f, range: 20.0f, basicDamage: 25);
            SetRangedWeaponClip(clipSize: 4, bulletsPerShot: 1, reloadTime: 2);
            SetExplosion(4.0f, 15, 0f);
            SetFlags(FireWeaponProperties.GrenadesFlags.Flame);
            SetProjectileType(ProjectileType.Grenade);
            SetAction(ItemUsageActionType .ThrowFragGrenadeAction);
            EndItem();

            CreateItem(ItemType.Bow, "Bow");
            SetGroup(ItemGroups.None);
            SetTexture(PistolIcon);
            SetInventoryItemIcon(PistolIcon);
            SetRangedWeaponAttribute (bulletSpeed: 70.0f, coolDown: 3f, range: 100.0f, basicDamage: 30);
            SetRangedWeaponClip(clipSize: 1, bulletsPerShot: 1, reloadTime: 2f);
            SetProjectileType(ProjectileType.Arrow);
            SetAction(ItemUsageActionType .ShootFireWeaponAction);
            EndItem();

            CreateItem(ItemType.Moon, "Moon");
            SetGroup(ItemGroups.None);
            SetTexture(BedrockIcon);
            SetInventoryItemIcon(BedrockIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Moon);
            EndItem();

            CreateItem(ItemType.Dirt, "Dirt");
            SetGroup(ItemGroups.None);
            SetTexture(DirtIcon);
            SetInventoryItemIcon(DirtIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Stone);
            EndItem();

            CreateItem(ItemType.Bedrock, "Bedrock");
            SetGroup(ItemGroups.None);
            SetTexture(BedrockIcon);
            SetInventoryItemIcon(BedrockIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Bedrock);
            EndItem();

            CreateItem(ItemType.Pipe, "Pipe");
            SetGroup(ItemGroups.None);
            SetTexture(PipeIcon);
            SetInventoryItemIcon(PipeIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Pipe);
            EndItem();

            CreateItem(ItemType.Wire, "Wire");
            SetGroup(ItemGroups.None);
            SetTexture(WireIcon);
            SetInventoryItemIcon(WireIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetTile(TileID.Wire);
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            EndItem();

            CreateItem(ItemType.GasBomb, "GasBomb");
            SetGroup(ItemGroups.None);
            SetTexture(GrenadeSprite5);
            SetInventoryItemIcon(GrenadeSprite5);
            SetAction(ItemUsageActionType .ThrowGasBombAction);
            EndItem();

            CreateItem(ItemType.FragGrenade, "FragGrenade");
            SetGroup(ItemGroups.None);
            SetTexture(GrenadeSpriteId);
            SetInventoryItemIcon(GrenadeSpriteId);
            SetAction(ItemUsageActionType .ThrowFragGrenadeAction);
            EndItem();

            CreateItem(ItemType.Sword, "Sword");
            SetGroup(ItemGroups.Weapon);
            SetAnimationSet(Enums.ItemAnimationSet.HoldingSword);
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetMeleeWeapon(1.0f, 2.0f, 0.5f, 1.0f, 10);
            SetFlags(FireWeaponProperties.MeleeFlags.Stab);
            SetAction(ItemUsageActionType.MeleeAttackAction);
            EndItem();

            CreateItem(ItemType.StunBaton, "StunBaton");
            SetGroup(ItemGroups.Weapon);
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetMeleeWeapon(0.5f, 2.0f, 1.0f, 1.0f, 5);
            SetFlags(FireWeaponProperties.MeleeFlags.Slash);
            SetAction(ItemUsageActionType .MeleeAttackAction);
            EndItem();

            CreateItem(ItemType.RiotShield, "RiotShield");
            SetGroup(ItemGroups.None);
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetShield(false);
            SetAction(ItemUsageActionType .UseShieldAction);
            EndItem();

            CreateItem(ItemType.Ore, "Ore");
            SetGroup(ItemGroups.None);
            SetTexture(OreIcon);
            SetInventoryItemIcon(OreIcon);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Slime, "Slime");
            SetGroup(ItemGroups.None);
            SetTexture(SlimeIcon);
            SetInventoryItemIcon(SlimeIcon);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Food, "Food");
            SetGroup(ItemGroups.None);
            SetTexture(FoodIcon);
            SetInventoryItemIcon(FoodIcon);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Bone, "Bone");
            SetGroup(ItemGroups.None);
            SetTexture(BoneIcon);
            SetInventoryItemIcon(BoneIcon);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PotionTool, "PotionTool");
            SetGroup(ItemGroups.None);
            SetTexture(BoneIcon);
            SetInventoryItemIcon(BoneIcon);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetUIPanel(PanelEnums.PotionTool);
            SetAction(ItemUsageActionType .ToolActionPotion);
            EndItem();

            CreateItem(ItemType.HealthPotion, "HealthPotion");
            SetGroup(ItemGroups.Potion);
            SetTexture(IconItemPotionHealth);
            SetInventoryItemIcon(IconItemPotionHealth);
            SetAction(ItemUsageActionType .DrinkPotionAction);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Ore, "Ore");
            SetGroup(ItemGroups.None);
            SetTexture(OreIcon);
            SetInventoryItemIcon(OreIcon);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PlacementTool, "PlacementTool");
            SetGroup(ItemGroups.BuildTools);
            SetTexture(IconToolPlacement);
            SetInventoryItemIcon(IconToolPlacement);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetUIPanel(PanelEnums.PlacementTool);
            SetAction(ItemUsageActionType .ToolActionPlaceTile);
            EndItem();

            CreateItem(ItemType.PlacementMaterialTool, "PlaceMaterial");
            SetGroup(ItemGroups.BuildTools);
            SetTexture(PlacementToolIcon);
            SetInventoryItemIcon(PlacementToolIcon);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetUIPanel(PanelEnums.PlacementMaterialTool);
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            EndItem();

            CreateItem(ItemType.RemoveTileTool, "RemoveTileTool");
            SetGroup(ItemGroups.None);
            SetTexture(IconToolRemoveTile);
            SetInventoryItemIcon(IconToolRemoveTile);
            SetAction(ItemUsageActionType .ToolActionRemoveTile);
            EndItem();

            CreateItem(ItemType.SpawnEnemySlimeTool, "SpawnSlimeTool");
            SetGroup(ItemGroups.None);
            SetTexture(SlimeIcon);
            SetInventoryItemIcon(SlimeIcon);
            SetAction(ItemUsageActionType .ToolActionEnemySpawn);
            EndItem();

            CreateItem(ItemType.SpawnEnemyGunnerTool, "SpawnEnemyGunnerTool");
            SetGroup(ItemGroups.None);
            SetTexture(IconToolSpawnEnemyGunner);
            SetInventoryItemIcon(IconToolSpawnEnemyGunner);
            SetAction(ItemUsageActionType .ToolActionEnemyGunnerSpawn);
            EndItem();

            CreateItem(ItemType.SpawnEnemySwordmanTool, "SpawnEnemySwordmanTool");
            SetGroup(ItemGroups.None);
            SetTexture(IconToolSpawnEnemySwordsman);
            SetInventoryItemIcon(IconToolSpawnEnemySwordsman);
            SetAction(ItemUsageActionType .ToolActionEnemySwordmanSpawn);
            EndItem();

            CreateItem(ItemType.MiningLaserTool, "MiningLaserTool");
            SetGroup(ItemGroups.None);
            SetTexture(MiningLaserToolIcon);
            SetInventoryItemIcon(MiningLaserToolIcon);
            SetAction(ItemUsageActionType .ToolActionMiningLaser);
            EndItem();

            CreateItem(ItemType.ParticleEmitterPlacementTool, "ParticleEmitterPlacementTool");
            SetGroup(ItemGroups.None);
            SetTexture(OreIcon);
            SetInventoryItemIcon(OreIcon);
            SetAction(ItemUsageActionType .ToolActionPlaceParticleEmitter);
            EndItem();

            CreateItem(ItemType.ChestPlacementTool, "ChestPlacementTool");
            SetGroup(ItemGroups.None);
            SetTexture(OreIcon);
            SetInventoryItemIcon(OreIcon);
            SetAction(ItemUsageActionType .ToolActionPlaceChest);
            EndItem();

            CreateItem(ItemType.MajestyPalm, "MajestyPlant");
            SetTexture(MajestyPalmIcon);
            SetInventoryItemIcon(MajestyPalmIcon);
            SetAction(ItemUsageActionType .PlantAction);
            EndItem();

            CreateItem(ItemType.SagoPalm, "SagoPlant");
            SetTexture(SagoPalmIcon);
            SetInventoryItemIcon(SagoPalmIcon);
            SetAction(ItemUsageActionType .PlantAction);
            EndItem();

            CreateItem(ItemType.DracaenaTrifasciata, "DracaenaTrifasciata");
            SetTexture(DracaenaTrifasciataIcon);
            SetInventoryItemIcon(DracaenaTrifasciataIcon);
            SetAction(ItemUsageActionType .PlantAction);
            EndItem();

            CreateItem(ItemType.WaterBottle, "Water");
            SetTexture(WaterIcon);
            SetInventoryItemIcon(WaterIcon);
            SetAction(ItemUsageActionType .WaterAction);
            EndItem();

            CreateItem(ItemType.HarvestTool, "HarvestTool");
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetAction(ItemUsageActionType .HarvestAction);
            EndItem();

            CreateItem(ItemType.ConstructionTool, "ConstructionTool");
            SetTexture(IconToolConstruction);
            SetInventoryItemIcon(IconToolConstruction);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetUIPanel(PanelEnums.MechTool);
            SetAction(ItemUsageActionType .ToolActionConstruction);
            EndItem();

            CreateItem(ItemType.Chest, "Chest");
            SetGroup(ItemGroups.Mech);
            SetTexture(ChestIconItem);
            SetInventoryItemIcon(ChestIconItem);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .MechPlacementAction);
            EndItem();

            CreateItem(ItemType.SmashableBox, "SmashableBox");
            SetGroup(ItemGroups.Mech);
            SetTexture(ChestIconItem);
            SetInventoryItemIcon(ChestIconItem);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .MechPlacementAction);
            EndItem();

            CreateItem(ItemType.SmashableEgg, "SmashableEgg");
            SetGroup(ItemGroups.Mech);
            SetTexture(ChestIconItem);
            SetInventoryItemIcon(ChestIconItem);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .MechPlacementAction);
            EndItem();

            CreateItem(ItemType.Planter, "Planter");
            SetGroup(ItemGroups.Mech);
            SetTexture(PotIconItem);
            SetInventoryItemIcon(PotIconItem);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .MechPlacementAction);
            EndItem();

            CreateItem(ItemType.Light, "Light");
            SetGroup(ItemGroups.Mech);
            SetTexture(Light2IconItem);
            SetInventoryItemIcon(Light2IconItem);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .MechPlacementAction);
            EndItem();

            CreateItem(ItemType.RemoveMech, "RemoveMech");
            SetTexture(ConstructionToolIcon);
            SetInventoryItemIcon(ConstructionToolIcon);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetAction(ItemUsageActionType .ToolActionRemoveMech);
            EndItem();

            CreateItem(ItemType.ScannerTool, "ScannerTool");
            SetTexture(ConstructionToolIcon);
            SetInventoryItemIcon(ConstructionToolIcon);
            SetAction(ItemUsageActionType .ToolActionScanner);
            EndItem();

            CreateItem(ItemType.Helmet, "Helmet");
            SetGroup(ItemGroups.Helmet);
            SetTexture(HemeltSprite);
            SetInventoryItemIcon(HemeltSprite);
            EndItem();

            CreateItem(ItemType.Suit, "Suit");
            SetGroup(ItemGroups.Armour);
            SetTexture(SuitSprite);
            SetInventoryItemIcon(SuitSprite);
            EndItem();


            CreateItem(ItemType.Moon, "Moon");
            SetGroup(ItemGroups.None);
            SetTexture(BedrockIcon);
            SetInventoryItemIcon(BedrockIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Moon);
            EndItem();

            CreateItem(ItemType.Dirt, "Dirt");
            SetGroup(ItemGroups.None);
            SetTexture(DirtIcon);
            SetInventoryItemIcon(DirtIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Stone);
            EndItem();

            CreateItem(ItemType.Bedrock, "Bedrock");
            SetGroup(ItemGroups.None);
            SetTexture(BedrockIcon);
            SetInventoryItemIcon(BedrockIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Bedrock);
            EndItem();

            CreateItem(ItemType.Pipe, "Pipe");
            SetGroup(ItemGroups.None);
            SetTexture(PipeIcon);
            SetInventoryItemIcon(PipeIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Pipe);
            EndItem();

            CreateItem(ItemType.Wire, "Wire");
            SetGroup(ItemGroups.None);
            SetTexture(WireIcon);
            SetInventoryItemIcon(WireIcon);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetTile(TileID.Wire);
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            EndItem();

            CreateItem(ItemType.GasBomb, "GasBomb");
            SetGroup(ItemGroups.None);
            SetTexture(GrenadeSprite5);
            SetInventoryItemIcon(GrenadeSprite5);
            SetAction(ItemUsageActionType .ThrowGasBombAction);
            EndItem();

            CreateItem(ItemType.Flare, "Flare");
            SetGroup(ItemGroups.None);
            SetTexture(GrenadeSprite5);
            SetInventoryItemIcon(GrenadeSprite5);
            SetAction(ItemUsageActionType .ThrowFlareAction);
            EndItem();

            CreateItem(ItemType.FragGrenade, "FragGrenade");
            SetGroup(ItemGroups.None);
            SetTexture(GrenadeSpriteId);
            SetInventoryItemIcon(GrenadeSpriteId);
            SetAction(ItemUsageActionType .ThrowFragGrenadeAction);
            EndItem();

            CreateItem(ItemType.GeometryPlacementTool, "GeometryPlacementTool");
            SetGroup(ItemGroups.None);
            SetTexture(IconToolGeometryPlacement);
            SetInventoryItemIcon(IconToolGeometryPlacement);
            SetFlags(ItemProperties.Flags.PlacementTool);
            SetUIPanel(PanelEnums.GeometryTool);
            SetAction(ItemUsageActionType .ToolActionGeometryPlacement);
            EndItem();

            CreateItem(ItemType.AxeTool, "AxeTool");
            SetGroup(ItemGroups.None);
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetAction(ItemUsageActionType .AxeAction);
            EndItem();

            CreateItem(ItemType.Pickaxe, "Pickaxe");
            SetGroup(ItemGroups.None);
            SetTexture(SwordSpriteId);
            SetInventoryItemIcon(SwordSpriteId);
            SetAction(ItemUsageActionType .PickaxeAction);
            EndItem();

            CreateItem(ItemType.Wood, "Wood");
            SetGroup(ItemGroups.None);
            SetTexture(WoodTile);
            SetInventoryItemIcon(WoodTile);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            SetAction(ItemUsageActionType .MaterialPlacementAction);
            SetTile(TileID.Stone);
            EndItem();

            CreateItem(ItemType.Diamond_0, "Diamond_0");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_0);
            SetInventoryItemIcon(Diamond_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_1, "Diamond_1");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_1);
            SetInventoryItemIcon(Diamond_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_2, "Diamond_2");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_2);
            SetInventoryItemIcon(Diamond_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_3, "Diamond_3");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_3);
            SetInventoryItemIcon(Diamond_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_4, "Diamond_4");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_4);
            SetInventoryItemIcon(Diamond_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_5, "Diamond_5");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_5);
            SetInventoryItemIcon(Diamond_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_6, "Diamond_6");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_6);
            SetInventoryItemIcon(Diamond_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Diamond_7, "Diamond_7");
            SetGroup(ItemGroups.None);
            SetTexture(Diamond_7);
            SetInventoryItemIcon(Diamond_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_0, "Gold_0");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_0);
            SetInventoryItemIcon(Gold_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_1, "Gold_1");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_1);
            SetInventoryItemIcon(Gold_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_2, "Gold_2");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_2);
            SetInventoryItemIcon(Gold_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_3, "Gold_3");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_3);
            SetInventoryItemIcon(Gold_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_4, "Gold_4");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_4);
            SetInventoryItemIcon(Gold_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_5, "Gold_5");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_5);
            SetInventoryItemIcon(Gold_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_6, "Gold_6");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_6);
            SetInventoryItemIcon(Gold_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Gold_7, "Gold_7");
            SetGroup(ItemGroups.None);
            SetTexture(Gold_7);
            SetInventoryItemIcon(Gold_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_0, "Iron_0");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_0);
            SetInventoryItemIcon(Iron_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_1, "Iron_1");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_1);
            SetInventoryItemIcon(Iron_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_2, "Iron_2");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_2);
            SetInventoryItemIcon(Iron_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_3, "Iron_3");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_3);
            SetInventoryItemIcon(Iron_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_4, "Iron_4");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_4);
            SetInventoryItemIcon(Iron_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_5, "Iron_5");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_5);
            SetInventoryItemIcon(Iron_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_6, "Iron_6");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_6);
            SetInventoryItemIcon(Iron_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Iron_7, "Iron_7");
            SetGroup(ItemGroups.None);
            SetTexture(Iron_7);
            SetInventoryItemIcon(Iron_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_0, "Emerald_0");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_0);
            SetInventoryItemIcon(Emerald_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_1, "Emerald_1");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_1);
            SetInventoryItemIcon(Emerald_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_2, "Emerald_2");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_2);
            SetInventoryItemIcon(Emerald_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_3, "Emerald_3");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_3);
            SetInventoryItemIcon(Emerald_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_4, "Emerald_4");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_4);
            SetInventoryItemIcon(Emerald_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_5, "Emerald_5");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_5);
            SetInventoryItemIcon(Emerald_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_6, "Emerald_6");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_6);
            SetInventoryItemIcon(Emerald_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Emerald_7, "Emerald_7");
            SetGroup(ItemGroups.None);
            SetTexture(Emerald_7);
            SetInventoryItemIcon(Emerald_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_0, "Coal_0");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_0);
            SetInventoryItemIcon(Coal_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_1, "Coal_1");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_1);
            SetInventoryItemIcon(Coal_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_2, "Coal_2");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_2);
            SetInventoryItemIcon(Coal_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_3, "Coal_3");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_3);
            SetInventoryItemIcon(Coal_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_4, "Coal_4");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_4);
            SetInventoryItemIcon(Coal_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_5, "Coal_5");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_5);
            SetInventoryItemIcon(Coal_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_6, "Coal_6");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_6);
            SetInventoryItemIcon(Coal_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Coal_7, "Coal_7");
            SetGroup(ItemGroups.None);
            SetTexture(Coal_7);
            SetInventoryItemIcon(Coal_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_0, "Lapis_0");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_0);
            SetInventoryItemIcon(Lapis_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_1, "Lapis_1");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_1);
            SetInventoryItemIcon(Lapis_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_2, "Lapis_2");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_2);
            SetInventoryItemIcon(Lapis_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_3, "Lapis_3");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_3);
            SetInventoryItemIcon(Lapis_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_4, "Lapis_4");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_4);
            SetInventoryItemIcon(Lapis_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_5, "Lapis_5");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_5);
            SetInventoryItemIcon(Lapis_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_6, "Lapis_6");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_6);
            SetInventoryItemIcon(Lapis_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.Lapis_7, "Lapis_7");
            SetGroup(ItemGroups.None);
            SetTexture(Lapis_7);
            SetInventoryItemIcon(Lapis_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_0, "PinkDia_0");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_0);
            SetInventoryItemIcon(PinkDiamond_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_1, "PinkDia_1");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_1);
            SetInventoryItemIcon(PinkDiamond_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_2, "PinkDia_2");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_2);
            SetInventoryItemIcon(PinkDiamond_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_3, "PinkDia_3");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_3);
            SetInventoryItemIcon(PinkDiamond_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_4, "PinkDia_4");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_4);
            SetInventoryItemIcon(PinkDiamond_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_5, "PinkDia_5");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_5);
            SetInventoryItemIcon(PinkDiamond_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_6, "PinkDia_6");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_6);
            SetInventoryItemIcon(PinkDiamond_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.PinkDia_7, "PinkDia_7");
            SetGroup(ItemGroups.None);
            SetTexture(PinkDiamond_7);
            SetInventoryItemIcon(PinkDiamond_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_0, "RedStone_0");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_0);
            SetInventoryItemIcon(RedStone_0);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_1, "RedStone_1");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_1);
            SetInventoryItemIcon(RedStone_1);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_2, "RedStone_2");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_2);
            SetInventoryItemIcon(RedStone_2);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_3, "RedStone_3");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_3);
            SetInventoryItemIcon(RedStone_3);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_4, "RedStone_4");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_4);
            SetInventoryItemIcon(RedStone_4);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_5, "RedStone_5");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_5);
            SetInventoryItemIcon(RedStone_5);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_6, "RedStone_6");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_6);
            SetInventoryItemIcon(RedStone_6);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

            CreateItem(ItemType.RedStone_7, "RedStone_7");
            SetGroup(ItemGroups.None);
            SetTexture(RedStone_7);
            SetInventoryItemIcon(RedStone_7);
            SetFlags(ItemProperties.Flags.Stackable);
            SetStackable();
            EndItem();

        }
    }
}
