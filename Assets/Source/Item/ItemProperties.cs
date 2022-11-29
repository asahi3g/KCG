//import UnityEngine

using System;
using Enums;
using Enums.PlanetTileMap;
using KGUI;
using KMath;

namespace Item
{
    public struct ItemProperties
    {
        public string ItemLabel;                 // Item Label
        public ItemType ItemType;
        public ItemGroups Group;
        public ItemUsageActionType ToolActionType;
        public MechType  MechType;               // Used only when item is a mech type.
        public TileID TileType;                 // Used only when item is a material type.
        public ItemToolType ToolType;           // used for weapon/tool attachement
        public ItemAnimationSet AnimationSet;   // used to change agent animations (like walking with rifle)
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

    public class FireWeaponProperties
    {
        public float BulletSpeed;
        //How Long it takes to use again in seconds.
        public float CoolDown;
        public float Range;
        public int BasicDemage;

        //
        // Fire Weapon Properties
        //
        public int BulletsPerShot;
        public int ClipSize;
        public float ReloadTime;
        public ProjectileType ProjectileType;

        //
        // Melee Attack Properties
        //
        public float StaggerTime;
        [UnityEngine.Range(0, 1)]
        public float StaggerRate;
        public bool ShieldActive;

        //
        // Pulse Weapon Properties
        //
        public bool IsLaunchGreanade;
        public int NumberOfGrenades;
        public int GrenadeClipSize;


        // Charge propreties.
        public bool  CanCharge;
        // Charge Precentage.
        public float ChargeRate;
        // Charge Multipiler.
        public float ChargeRatio;
        public float ChargePerShot;
        public float ChargeMin;
        public float ChargeMax;

        // If gun shoots more than one bullet.
        // Cone angle in which bullets will be spreaded.
        public float SpreadAngle;
        public int NumOfBullets;

        // Define Accuracy of the firegun.
        // Max Cone angle which shooted bullets can go to.
        public float MaxRecoilAngle;
        // Cone angle of the first bullet.
        public float MinRecoilAngle;
        // How much cone angle is increased after every shoot.
        public float RateOfChange;
        // How long it takes for recoil to go back to min from MaxRecoilAngle in seconds.
        public float RecoverTime;
        // How long it takes to recoil startRecovering in seconds.
        public float RecoverDelay;

        //
        // Explosive Properties.
        //
        public float BlastRadius;
        // Damage at the origin
        public int MaxDamage;
        // Time in seconds it takes to explode after first hit.
        public float Elapse;
        // Todo: Add function to specify how damage decrease with distance from the explosion.

        public Flags WeaponFlags;
        // Weapon Flags
        // HasClip -> If Weapon Has an Ammo Clip
        // ShouldSpread -> Should Weapon Spread the Ammos (ex, pump shotgun)
        // HasCharge -> If Weapon Chargable or Not
        // PulseWeapon -> If the weapon is a pulse weapon or not
        // Explosive -> Ammo explodes.
        [Flags]
        public enum Flags : byte
        {
            HasClip = 1 << 0,
            ShouldSpread = 2 << 1,
            HasCharge = 3 << 2,
            PulseWeapon = 4 << 3,
        }

        // Grenades Flags
        // Cocussions -> Cocussions Bombs
        // Flame -> Flame Bombs
        // Fragmentation -> Fragmentation Bombs
        public GrenadesFlags GrenadeFlags;
        [Flags]
        public enum GrenadesFlags : byte
        {
            Cocussions = 1 << 0,
            Flame = 2 << 1,
            Fragmentation = 3 << 2
        }

        // Melee Flags
        // Stab -> If Melee Weapon Stabs
        // Slash -> If Melee Weapon Slashes
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
