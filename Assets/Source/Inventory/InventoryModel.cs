using System;
using Utility;

namespace Inventory
{
    // Todo: Allow personalize toolbar.
    public struct InventoryModel
    {
        public int ID;
        public bool HasToolBar;
        public Window MainWindow;
        public Window ToolBarWindow;
        public int Width;               // Width of the grid.
        public int Height;              // Height of the grid.
        public int SlotCount;           // Active slot count. Number of slot that can be used in the grid.
        public GridSlot[] Slots;
        public RenderProprieties RenderProprieties;
    }
}
