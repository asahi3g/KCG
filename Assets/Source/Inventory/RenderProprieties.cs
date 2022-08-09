using System;
using System.Collections.Generic;
using UnityEngine;
using KMath;

namespace Inventory
{
    // Information on how to render inventory
    public class RenderProprieties
    {
        [Flags]
        public enum Flags : byte
        {
            HasBackground =         1 << 0,
            HasBackgroundTexture =  1 << 1,
            HasBorder =             1 << 2,         // If false, SlectedColor defines slot color when selected. Not border color.
            HasDefaultSlotTexture = 1 << 3,         // Default Slots has a texture.
        }

        public Flags InventoryFlags;
        public int BackGroundSpriteID;
        public int DefaultSlotTextureID;
        public Color BackgroundColor;
        public Color SlotColor;
        public Color SelectedColor;
        public List<string> Strings;
        public List<Vec2f> StringPosOffsets;

        public bool HasBackground() { return InventoryFlags.HasFlag(Flags.HasBackground); }
        public bool HasBackgroundTexture() { return InventoryFlags.HasFlag(Flags.HasBackgroundTexture); }
        public bool HasBorder() { return InventoryFlags.HasFlag(Flags.HasBorder); }
        public bool HasDefaultSlotTexture() { return InventoryFlags.HasFlag(Flags.HasDefaultSlotTexture); }
    }
}
