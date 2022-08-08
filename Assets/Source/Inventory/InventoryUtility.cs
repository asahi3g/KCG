using KMath;

namespace Inventory
{
    public struct GridSlot
    {
        public int SlotID;              // Real Slot Index. If = -1. Grid possition is off -> Doesn't draw.
        public int SlotBackgroundIcon;  // Sprite icon for ring, armour, etc showing slot type, if -1 ignore/dont render
        public Enums.ItemGroups Restriction;
    }

    public struct Window
    {
        public Vec2f Position;
        public Vec2f GridPosition;
        public Vec2f Size;
        public Vec2f GridSize;
        public float TileSize;
        public float SlotBorderOffset;
        public float SlotOffset;

        /// <summary>
        /// These define offset from start of background image to start of the grid.
        /// </summary>
        public float UpBorderOffSet;
        public float DownBorderOffSet;
        public float LeftBorderOffSet;
        public float RightBorderOffSet;

        public void Scale(float scaleFactor)
        {
            Position *= scaleFactor;
            GridPosition *= scaleFactor;
            Size *= scaleFactor;
            GridSize *= scaleFactor;
            TileSize *= scaleFactor;
            SlotBorderOffset *= scaleFactor;
            SlotOffset *= scaleFactor;
            UpBorderOffSet *= scaleFactor;
            DownBorderOffSet *= scaleFactor;
            LeftBorderOffSet *= scaleFactor;
            RightBorderOffSet *= scaleFactor;
        }

        public bool IsInsideWindow(Vec2f pos)
        {
            if (pos.X < GridPosition.X || pos.Y < GridPosition.Y || pos.X > (GridPosition.X + GridSize.X) || pos.Y > (GridPosition.Y + GridSize.Y))
                return false;
            return true;
        }
    }

    public struct Slot
    {
        public int GridSlotID;                  // Index of Gui slot.                        
        public int ItemID;                      // If -1 slot is empty.
        public Enums.ItemGroups Restriction;
    }

}

