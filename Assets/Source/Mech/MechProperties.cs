using Enums;
using KMath;
using System;
namespace Mech
{
    public struct MechProperties
    {
        public MechType Type;
        public MechGroup Group;

        public string Name;
        public int SpriteID;
        public Vec2f SpriteSize;
        public int XMin, XMax, YMin, YMax;

        public NodeType Action;
        public Flags MechFlags;

        public Enums.LootTableType DropTableID;
        public int InventoryModelID; // Only used if has inventory.
        public int Durability;       // Mech "health" Use only if Mech is breakable.

        public int TreeHealth;      // Mech "health" Use only if Mech is breakable.
        public int TreeSize;        // Mech "TreeSize" Use only if Mech is tree.

        #region Plants
        // SpriteID is the stage 1 sprite.
        public int Stage2Sprite;
        public int Stage3Sprite;
        public Vec2f Stage2SpriteSize;
        public Vec2f Stage3SpriteSize;
        #endregion

        [Flags]
        public enum Flags : byte
        { 
            HasInventory = 1 << 0,
            IsBreakable = 1 << 1,
        }

        public bool HasInventory() { return MechFlags.HasFlag(Flags.HasInventory); }
        public bool IsBreakable() { return MechFlags.HasFlag(Flags.IsBreakable); }
    }
}
