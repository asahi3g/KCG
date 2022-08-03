using System;
using Utility;

namespace Inventory
{
    public struct InventoryModel
    {
        [Flags]
        public enum Flags : byte
        {
            Draw            = 1 << 0,   // If off doesn't draw.   
            DrawSelection   = 1 << 1,   // If off doesn't draw selection border.
            HasToolBar      = 1 << 2,   // If on has toolBar.
        }

        public int ID;
        public Flags InventoryFlags;
        public Window MainWindow;
        public Window ToolBarWindow;
        public int Width;
        public int Height;
        public int SelectedSlotID;
        public Slot[] Slots;
        public BitSet SlotsMask;        // Free slots set to 0/ Fill slots to 1.
        public RenderProprieties RenderProprieties;

        public bool IsDrawOn() { return InventoryFlags.HasFlag(Flags.Draw); }
        public bool IsDrawSelectionOn() { return InventoryFlags.HasFlag(Flags.DrawSelection); }
        public bool HasTooBar() { return InventoryFlags.HasFlag(Flags.HasToolBar); }
    }
}
