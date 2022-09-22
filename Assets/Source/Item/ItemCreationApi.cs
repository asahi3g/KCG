using Entitas;
using UnityEngine;
using System;
using KMath;
using Agent;
using System.Collections.Generic;
using Enums;
using Planet;
using Sprites;
using System.Drawing;

/*
    How To use it:
        Item.CreationApi.CreateItem(Item Type, Item Type Name);
        Item.CreationApi.SetTexture(SpriteSheetID);
        Item.CreationApi.SetInventoryTexture(SpriteSheetID);
        Item.CreationApi.MakeStackable(Max number of items in a stack.);
        Item.CreationApi.EndItem();
*/

namespace Item
{
    public class ItemCreationApi
    {
        // Constructor is called before the first frame update.
         
        // Note[Joao] this arrays are very memory expensive: use array of pointers instead?
        private ItemProprieties[] PropertiesArray;
        private FireWeaponPropreties[] WeaponList;
        private string[] ItemTypeLabels;

        ItemType CurrentIndex;
        int WeaponListSize;

        public ItemCreationApi()
        {
            int length = Enum.GetValues(typeof(ItemType)).Length - 1; // -1 beacause of error item type.
            PropertiesArray = new ItemProprieties[length];
            ItemTypeLabels = new string[length];
            WeaponList = new FireWeaponPropreties[16];
            CurrentIndex = ItemType.Error;
            WeaponListSize = 0;

            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i].ItemType = CurrentIndex;
            }
        }

        public string GetLabel(Enums.ItemType type)
        {
            ItemType itemType = PropertiesArray[(int)type].ItemType;
            IsItemTypeValid(itemType);

            return ItemTypeLabels[(int)type];
        }


        public ItemProprieties Get(Enums.ItemType type)
        {
            ItemType itemType = PropertiesArray[(int)type].ItemType;
            IsItemTypeValid(itemType);

            return PropertiesArray[(int)type];
        }

        public FireWeaponPropreties GetWeapon(Enums.ItemType type)
        {
            ItemType itemType = PropertiesArray[(int)type].ItemType;
            IsItemTypeValid(itemType);

            return WeaponList[PropertiesArray[(int)type].FireWeaponID];
        }

        public void CreateItem(Enums.ItemType itemType, string name)
        {
            CurrentIndex = itemType;

            PropertiesArray[(int)itemType].ItemType = itemType;
            PropertiesArray[(int)itemType].MechType = Mech.MechType.Error;
            ItemTypeLabels[(int)itemType] = name;
        }

        public void SetName(string name)
        {
            IsItemTypeValid();

            ItemTypeLabels[(int)CurrentIndex] = name;
        }

        public void SetGroup(Enums.ItemGroups group)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].Group = group;
        }

        public void SetMech(Mech.MechType mech)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].MechType = mech;
        }

        public void SetSpriteSize(Vec2f size)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].SpriteSize = size;
        }

        public void SetTexture(int spriteId)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].SpriteID = spriteId;
        }


        public void SetInventoryTexture(int spriteId)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].InventorSpriteID = spriteId;
        }

        public void SetAction(Enums.ActionType actionID)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].ToolActionType = actionID;
            PropertiesArray[(int)CurrentIndex].ItemFlags |= ItemProprieties.Flags.Tool;
        }

        public void SetConsumable()
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].ItemFlags |= ItemProprieties.Flags.Consumable;
        }

        public void SetStackable(int maxStackCount = 99)
        {
            IsItemTypeValid();
            PropertiesArray[(int)CurrentIndex].MaxStackCount = maxStackCount;
            PropertiesArray[(int)CurrentIndex].ItemFlags |= ItemProprieties.Flags.Stackable;
        }

        public void SetPlaceable()
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].ItemFlags |= ItemProprieties.Flags.Placeable;
        }

        public void SetSpreadAngle(float spreadAngle)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.SpreadAngle = spreadAngle;
        }

        public void SetRangedWeapon(float bulletSpeed, float coolDown, float range, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponPropreties fireWeapon = new FireWeaponPropreties()
            {
                BulletSpeed = bulletSpeed,
                CoolDown = coolDown,
                Range = range,
                BasicDemage = basicDamage,
            };

            WeaponList[WeaponListSize] = fireWeapon;
            PropertiesArray[(int)CurrentIndex].FireWeaponID = WeaponListSize++;
        }

        public void SetRangedWeapon(float bulletSpeed, float coolDown, float range, bool isLaunchGrenade, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponPropreties fireWeapon = new FireWeaponPropreties()
            {
                BulletSpeed = bulletSpeed,
                CoolDown = coolDown,
                Range = range,
                IsLaunchGreanade = isLaunchGrenade,
                BasicDemage = basicDamage,
            };

            WeaponList[WeaponListSize] = fireWeapon;
            PropertiesArray[(int)CurrentIndex].FireWeaponID = WeaponListSize++;
        }

        public void SetRangedWeaponClip(int clipSize, int bulletsPerShot, float reloadTime)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.ClipSize = clipSize;
            fireWeapon.BulletsPerShot = bulletsPerShot;
            fireWeapon.ReloadTime = reloadTime;
            fireWeapon.WeaponFlags |= FireWeaponPropreties.Flags.HasClip;
        }

        public void SetRangedWeaponClip(int bulletClipSize, int greandeClipSize, int bulletsPerShot, float bulletReloadTime)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.ClipSize = bulletClipSize;
            fireWeapon.GrenadeClipSize = greandeClipSize;
            fireWeapon.NumberOfGrenades = greandeClipSize;
            fireWeapon.BulletsPerShot = bulletsPerShot;
            fireWeapon.ReloadTime = bulletReloadTime;
            fireWeapon.WeaponFlags |= FireWeaponPropreties.Flags.HasClip | FireWeaponPropreties.Flags.PulseWeapon;
        }

        public void SetFireWeaponMultiShoot(float speadAngle, int numOfBullet)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.SpreadAngle = speadAngle;
            fireWeapon.NumOfBullets = numOfBullet;
        }

        public void SetFireWeaponRecoil(float maxRecoilAngle, float minRecoilAngle, float rateOfChange, float recoverTime, float recoverDelay)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.MaxRecoilAngle = maxRecoilAngle;
            fireWeapon.MinRecoilAngle = minRecoilAngle;
            fireWeapon.RateOfChange = rateOfChange;
            fireWeapon.RecoverTime = recoverTime;
            fireWeapon.RecoverDelay = recoverDelay;
        }

        public void SetMeleeWeapon(float coolDown, float range, float staggerTime, float staggerRate, int basicDamage)
        {
            IsItemTypeValid();

            FireWeaponPropreties fireWeapon = new FireWeaponPropreties()
            {
                CoolDown = coolDown,
                Range = range,
                StaggerTime = staggerTime,
                StaggerRate = staggerRate,
                BasicDemage = basicDamage,
            };

            WeaponList[WeaponListSize] = fireWeapon;
            PropertiesArray[(int)CurrentIndex].FireWeaponID = WeaponListSize++;
        }

        public void SetShield(bool ShieldActive)
        {
            IsItemTypeValid();

            FireWeaponPropreties fireWeapon = new FireWeaponPropreties()
            {
                ShieldActive = ShieldActive,
            }; 

            WeaponList[WeaponListSize] = fireWeapon;
            PropertiesArray[(int)CurrentIndex].FireWeaponID = WeaponListSize++;
        }

        public void SetExplosion(float radius, int maxDamage, float elapse)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.BlastRadius = radius;
            fireWeapon.MaxDamage = maxDamage;
            fireWeapon.Elapse = elapse;
        }

        public void SetFlags(FireWeaponPropreties.MeleeFlags flags)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.MeleeAttackFlags |= flags;
        }

        public void SetFlags(FireWeaponPropreties.Flags flags)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.WeaponFlags |= flags;
        }

        public void SetFlags(FireWeaponPropreties.GrenadesFlags flags)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.GrenadeFlags |= flags;
        }

        public void SetFlags(ItemProprieties.Flags flags)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].ItemFlags |= flags;
        }

        public void SetProjectileType(Enums.ProjectileType projectileType)
        {
            IsItemTypeValid();

            ref FireWeaponPropreties fireWeapon = ref WeaponList[PropertiesArray[(int)CurrentIndex].FireWeaponID];
            fireWeapon.ProjectileType = projectileType;
        }

        public void SetItemToolType(Enums.ItemToolType type)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].ToolType = type;
        }

        public void SetAnimationSet(Enums.ItemAnimationSet animationSet)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].AnimationSet = animationSet;
        }

        public void SetItemKeyUsage(Enums.ItemKeyUsage usage)
        {
            IsItemTypeValid();

            PropertiesArray[(int)CurrentIndex].KeyUsage = usage;
        }

        public void EndItem()
        {
            // Todo: Check if ItemType is valid in debug mode.
            CurrentIndex = ItemType.Error;
        }

        private void IsItemTypeValid(ItemType itemType)
        {
#if DEBUG
            if (itemType == ItemType.Error)
            {
                Debug.Log("Not valid ItemType");
                Utils.Assert(false);
            }
#endif
        }

        private void IsItemTypeValid()
        {
            IsItemTypeValid(CurrentIndex);
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
        public int OreSprite;
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
        public int Ore2Sprite;
        public int Ore3Sprite;
        public int ChestIconParticle;

        public void InitializeResources()
        {
            SniperRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun8.png", 48, 16);
            LongRifleIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun10.png", 48, 16);
            PulseIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun17.png", 48, 16);
            SMGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun6.png", 48, 16);
            ShotgunIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun13.png", 48, 16);
            GunSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Pistol\\gun-temp.png", 44, 25);
            RPGIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Guns\\Pistol\\Guns\\Gun18.png", 48, 16);
            GrenadeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades1.png", 16, 16);
            GrenadeSprite5 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades5.png", 16, 16);
            SwordSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Weapons\\Swords\\Sword1.png", 16, 48);
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            SlimeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Enemies\\Slime\\slime.png", 32, 32);
            FoodSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Food\\Food.png", 60, 60);
            BoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Bone\\Bone.png", 60, 60);
            RockSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Rock\\rock1.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            pipeIconSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", 16, 16);
            LaserSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\RailGun\\lasergun-temp.png", 195, 79);
            MajestyPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png", 16, 16);
            SagoPalmIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png", 16, 16);
            DracaenaTrifasciataIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", 16, 16);
            WaterIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Water\\water_12px.png", 12, 12);
            ConstructionToolIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Development\\Furnitures\\Furniture2\\dev-furniture-2.png", 12, 12);
            ChestIconItem = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
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


            SniperRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SniperRifleIcon, 0, 0, Enums.AtlasType.Particle);
            LongRifleIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LongRifleIcon, 0, 0, Enums.AtlasType.Particle);
            PulseIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PulseIcon, 0, 0, Enums.AtlasType.Particle);
            SMGIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SMGIcon, 0, 0, Enums.AtlasType.Particle);
            ShotgunIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ShotgunIcon, 0, 0, Enums.AtlasType.Particle);
            PistolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GunSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            GrenadeSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            GrenadeSprite5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSprite5, 0, 0, Enums.AtlasType.Particle);
            SwordSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(SwordSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            SlimeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SlimeSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            FoodIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(FoodSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            BoneIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BoneSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RockSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            RemoveToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Ore2SpriteSheet, 0, 0, Enums.AtlasType.Particle);
            PipePlacementToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(pipeIconSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            MiningLaserToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(LaserSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            MajestyPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmIcon, 0, 0, Enums.AtlasType.Particle);
            SagoPalmIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmIcon, 0, 0, Enums.AtlasType.Particle);
            DracaenaTrifasciataIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataIcon, 0, 0, Enums.AtlasType.Particle);
            WaterIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WaterIcon, 0, 0, Enums.AtlasType.Particle);
            ConstructionToolIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ConstructionToolIcon, 0, 0, Enums.AtlasType.Particle);
            ChestIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestIconItem, 0, 0, Enums.AtlasType.Particle);
            PotIconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIconItem, 0, 0, Enums.AtlasType.Particle);
            Light2IconItem = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2IconItem, 0, 0, Enums.AtlasType.Particle);
            OreSprite = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(OreSpriteSheet, 0, 0, 0);
            HemeltSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetsSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            SuitSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(SuitsSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            BedrockIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BedrockIcon, 0, 0, Enums.AtlasType.Particle);
            DirtIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DirtIcon, 0, 0, Enums.AtlasType.Particle);
            PipeIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PipeIcon, 0, 0, Enums.AtlasType.Particle);
            WireIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(WireIcon, 0, 0, Enums.AtlasType.Particle);
            DyeSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(DyeSlotIcon, 0, 0, Enums.AtlasType.Gui);
            HelmetSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(HelmetSlotIcon, 0, 0, Enums.AtlasType.Gui);
            ArmourSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ArmourSlotIcon, 0, 0, Enums.AtlasType.Gui);
            GlovesSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GlovesSlotIcon, 0, 0, Enums.AtlasType.Gui);
            RingSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RingSlotIcon, 0, 0, Enums.AtlasType.Gui);
            BeltSlotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(BeltSlotIcon, 0, 0, Enums.AtlasType.Gui);


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
            GameState.ItemCreationApi.SetRangedWeapon(50.0f, 0.25f, 20.0f, 15);
            GameState.ItemCreationApi.SetRangedWeaponClip(99999, 1, 1f);
            GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
            GameState.ItemCreationApi.SetProjectileType(Enums.ProjectileType.Bullet);
            GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionFireWeapon);
            GameState.ItemCreationApi.SetItemToolType(Enums.ItemToolType.Rifle);
            GameState.ItemCreationApi.SetAnimationSet(Enums.ItemAnimationSet.HoldingRifle);
            GameState.ItemCreationApi.SetItemKeyUsage(Enums.ItemKeyUsage.KeyDown);
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
            GameState.ItemCreationApi.SetRangedWeapon(50.0f, 0.4f, 100.0f, 25);
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

            GameState.ItemCreationApi.CreateItem(Enums.ItemType.GeometryPlacementTool, "GeometryPlacementTool");
            GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
            GameState.ItemCreationApi.SetTexture(OreIcon);
            GameState.ItemCreationApi.SetInventoryTexture(OreIcon);
            GameState.ItemCreationApi.SetSpriteSize(new Vec2f(0.5f, 0.5f));
            GameState.ItemCreationApi.SetAction(Enums.ActionType.ToolActionGeometryPlacement);
            GameState.ItemCreationApi.EndItem();
        }
    }
}
