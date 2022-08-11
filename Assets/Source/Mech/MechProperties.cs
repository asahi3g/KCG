using Enums;
using KMath;
using System;
namespace Mech
{
    public struct MechProperties
    {
        public int MechID;

        public string Name;

        // Mech's Sprite ID
        public int SpriteID;

        public Vec2f SpriteSize;

        public int XMin, XMax, YMin, YMax;

        public ActionType Action;
        public Flags MechFlags;

        public int DropTableID;
        public int InventoryModelID; // Only used if has inventory.
        public int Durability;       // Mech "health" Use only if Mech is breakable.

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
