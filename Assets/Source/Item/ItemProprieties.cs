//import UnityEngine

using System;
using Enums;
using Enums.PlanetTileMap;
using KGUI;
using KMath;
using Mech;


namespace Item
{

    public struct ItemProprieties
    {
        public ItemType ItemType;
        public ItemGroups Group;
        public NodeType ToolActionType;
        public MechType MechType;          // Used only when item is a mech type.
        public TileID TileType;       // Used only when item is a material type.
        public ItemToolType ToolType;           // used for weapon/tool attachement
        public ItemAnimationSet AnimationSet; // used to change agent animations (like walking with rifle)
        public PanelEnums ItemPanelEnums;

        public ItemKeyUsage KeyUsage;

        public int InventorSpriteID;
        public int SpriteID;
        public Vec2f SpriteSize;
        public int MaxStackCount;              // Used only if stackable.

        public int FireWeaponID;

        public Flags ItemFlags;
        [Flags]
        public enum Flags : byte
        {
            Placeable = 1 << 0,
            Consumable = 1 << 1,
            Stackable = 1 << 2,
            Tool = 1 << 3,
            PlacementTool = 1 << 4,
            UI = 1 << 5
        }

        public bool IsStackable() => ItemFlags.HasFlag(Flags.Stackable);
        public bool IsTool() => ItemFlags.HasFlag(Flags.Tool);
        public bool IsPlacementTool() => ItemFlags.HasFlag(Flags.PlacementTool);
        public bool HasUI() => ItemFlags.HasFlag(Flags.UI);
    }

    public struct FireWeaponPropreties
    {
        public float BulletSpeed;
        /// <summary> How Long it takes to shoot again in seconds.</summary>
        public float CoolDown;
        public float Range;
        public int BasicDemage;
        public int BulletsPerShot;
        public int ClipSize;
        public float ReloadTime;
        public ProjectileType ProjectileType;

        // Charge propreties.
        public bool  CanCharge;
        /// <summary> Charge Precentage.</summary>
        public float ChargeRate;
        /// <summary>  Charge Multipiler.</summary>
        public float ChargeRatio;
        public float ChargePerShot;
        public float ChargeMin;
        public float ChargeMax;

        // If gun shoots more than one bullet.
        /// <summary> Cone angle in which bullets will be spreaded.</summary>
        public float SpreadAngle;
        public int NumOfBullets;

        // Define Accuracy of the firegun.
        /// <summary> Max Cone angle which shooted bullets can go to.</summary>
        public float MaxRecoilAngle;
        /// <summary> Cone angle of the first bullet.</summary>
        public float MinRecoilAngle;
        /// <summary> How much cone angle is increased after every shoot.</summary>
        public float RateOfChange;
        /// <summary> How long it takes for recoil to go back to min from MaxRecoilAngle in seconds.</summary>
        public float RecoverTime;
        /// <summary>  How long it takes to recoil startRecovering in seconds.</summary>
        public float RecoverDelay;

        /// Melee Attack Properties
        public float StaggerTime;
        [UnityEngine.Range(0, 1)]
        public float StaggerRate;
        public bool ShieldActive;

        /// Pulse Weapon Properties
        public bool IsLaunchGreanade;
        public int  NumberOfGrenades;
        public int  GrenadeClipSize;

        // Explosive Properties.
        public float BlastRadius;
        /// <summary> Damage at the origin</summary>
        public int MaxDamage;
        /// <summary> Time in seconds it takes to explode after first hit.</summary>
        public float Elapse;
        // Todo: Add function to specify how damage decrease with distance from the explosion.

        public Flags WeaponFlags;
        /// <summary>
        /// Weapon Flags
        /// HasClip -> If Weapon Has an Ammo Clip
        /// ShouldSpread -> Should Weapon Spread the Ammos (ex, pump shotgun)
        /// HasCharge -> If Weapon Chargable or Not
        /// PulseWeapon -> If the weapon is a pulse weapon or not
        /// Explosive -> Ammo explodes.
        /// </summary>
        [Flags]
        public enum Flags : byte
        {
            HasClip = 1 << 0,
            ShouldSpread = 2 << 1,
            HasCharge = 3 << 2,
            PulseWeapon = 4 << 3,
        }

        /// <summary>
        /// Grenades Flags
        /// Cocussions -> Cocussions Bombs
        /// Flame -> Flame Bombs
        /// Fragmentation -> Fragmentation Bombs
        /// </summary>
        public GrenadesFlags GrenadeFlags;
        [Flags]
        public enum GrenadesFlags : byte
        {
            Cocussions = 1 << 0,
            Flame = 2 << 1,
            Fragmentation = 3 << 2
        }

        /// <summary>
        /// Melee Flags
        /// Stab -> If Melee Weapon Stabs
        /// Slash -> If Melee Weapon Slashes
        /// </summary>
        public MeleeFlags MeleeAttackFlags;
        [Flags]
        public enum MeleeFlags : byte
        {
            Stab = 1 << 0,
            Slash = 2 << 1
        }

        public bool HasClip() { return WeaponFlags.HasFlag(Flags.HasClip); }
        public bool ShouldSpread() { return WeaponFlags.HasFlag(Flags.ShouldSpread); }
        public bool HasCharge() { return WeaponFlags.HasFlag(Flags.HasCharge); }
        public bool IsStab() { return WeaponFlags.HasFlag(MeleeFlags.Stab); }
        public bool IsSlash() { return WeaponFlags.HasFlag(MeleeFlags.Slash); }
        public bool IsPulse() { return WeaponFlags.HasFlag(Flags.PulseWeapon); }
    }
}
