using KMath;

namespace Inventory
{
    public struct GridSlot
    {
        public int SlotID;              // Real Slot Index. If = -1. Grid possition is off -> Doesn't draw.
        public int SlotBackgroundIcon;  // Sprite icon for ring, armour, etc showing slot type, if -1 ignore/dont render
        public Enums.ItemGroupType Restriction;
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

        public void Scale(float scaleFactor)
        {
            Position *= scaleFactor;
            GridPosition *= scaleFactor;
            Size *= scaleFactor;
            GridSize *= scaleFactor;
            TileSize *= scaleFactor;
            SlotBorderOffset *= scaleFactor;
            SlotOffset *= scaleFactor;
        }

        public bool IsInsideWindow(Vec2f pos)
        {
            if (pos.X < GridPosition.X || pos.Y < GridPosition.Y || pos.X > (GridPosition.X + GridSize.X) || pos.Y > (GridPosition.Y + GridSize.Y))
                return false;
            return true;
        }

        public bool IsInsideScreen()
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X + Size.X <= 1920 && Position.Y + Size.Y <= 1080)
                return true;
            return false;
        }

        public bool IsAbove(ref Window other)
        {
            float yMin = Position.Y;
            float yMax = Position.Y + Size.Y;
            float xMin = Position.X;
            float xMax = Position.X + Size.X;

            float yMinOther = other.Position.Y;
            float yMaxOther = other.Position.Y + other.Size.Y;
            float xMinOther = other.Position.X;
            float XMaxOther = other.Position.X + other.Size.X;

            bool horizontalOverlap = false;

            if (xMin > xMinOther)
            {
                if (xMin <= XMaxOther)                 // other is at left.
                    horizontalOverlap = true;
            }
            else
            {
                if (xMax > xMinOther || xMin == xMinOther)
                    horizontalOverlap = true;
            }

            if (horizontalOverlap)
            {
                if (yMin > yMinOther)                   // other is at lower.
                {
                    if (yMin <= yMaxOther)
                        return true;
                }
                else
                {
                    if (yMax > yMinOther || yMin == yMinOther)
                        return true;
                }
            }

            return false;
        }
    }

    public class Slot
    {
        private int _inventoryId;
        private int _index;
        private Enums.ItemGroupType _itemGroups;
        private int _itemId;
        

        public int InventoryId => _inventoryId;
        public int Index => _index;
        public Enums.ItemGroupType ItemGroups => _itemGroups;

        public int ItemID => _itemId;
        

        public Slot(int inventoryId, int index, Enums.ItemGroupType itemGroups) : this(inventoryId, index, itemGroups, -1) { }
        
        public Slot(int inventoryId, int index, Enums.ItemGroupType itemGroups, int itemId)
        {
            _inventoryId = inventoryId;
            _index = index;
            _itemGroups = itemGroups;
            _itemId = itemId;
        }


        public void SetItemId(int itemId)
        {
            _itemId = itemId;
        }

        public void ClearItemId()
        {
            SetItemId(-1);
        }
    }

}

