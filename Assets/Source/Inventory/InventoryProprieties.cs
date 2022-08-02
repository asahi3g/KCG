using System;
using System.Collections.Generic;
using KMath;
using UnityEngine;

namespace Inventory
{
    public struct InventoryProprieties
    {
        [Flags]
        public enum Flags:byte
        { 
            HasBackground           = 1 << 0,       
            HasBackgroundTexture    = 1 << 1,       
            HasBorder               = 1 << 2,       // If false, only draw border for selected slot.
            HasTooBar               = 1 << 3,       // If has tool bar first row will be tool bar slots.
            HasSlotTexture          = 1 << 4,       // Slots with restriction has specific background texture.
        }

        public Flags InventoryFlags;
        public int   BackGroundSpriteID;
        public Color BackgroundColor;
        public Color SlotColor;
        public Color SelectedBorder;

        public float TileSize;      // Height in pixels -> Whole tile = Slot + Border + Space between slots.
        public float BorderOffset;  // Horizontal offset from the beggining of the tile.
        public float SlotOffset;    

        public bool HasBackground() { return InventoryFlags.HasFlag(Flags.HasBackground); }
        public bool HasBackgroundTexture() { return InventoryFlags.HasFlag(Flags.HasBackgroundTexture); }
        public bool HasBorder() { return InventoryFlags.HasFlag(Flags.HasBorder); }
        public bool HasTooBar() { return InventoryFlags.HasFlag(Flags.HasTooBar); }
        public bool HasSlotTexture() { return InventoryFlags.HasFlag(Flags.HasSlotTexture); }
    }
}
