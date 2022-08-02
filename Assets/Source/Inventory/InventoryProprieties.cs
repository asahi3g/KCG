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
            hasBackground       = 1 << 0,
            hasBorder           = 1 << 1,       // If false, only draw border for selected slot.
            hasTooBar           = 1 << 2,
            hasSlotTexture      = 1 << 3,       // Slots with restriction has specific background texture.
        }

        public Flags InventoryFlags;
        public Color Background;
        public Color Border;
        public Color SelectedBorder;

        public float TileSize;      // Size in pixels / [0 - 1]? Whole tile = Slot + Border + Space between slots.
        public float BorderOffset;
        public float SlotOffset;
    }

    struct Slot
    {
        public int ID;
        public Enums.ItemType Restriction;
        public int SlotBorderBackgroundIcon;    // Sprite icon for ring, armour, etc showing slot type, if 0 ignore/dont render
        public int ItemID;                      // If -1 slot is empty.

        public bool ToolBarSlot;
    }
}
