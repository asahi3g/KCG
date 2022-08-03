using KMath;

namespace Inventory
{
    public struct Slot
    {
        public int ID;
        public Enums.ItemGroups Restriction;
        public int SlotBackgroundIcon;          // Sprite icon for ring, armour, etc showing slot type, if -1 ignore/dont render
        public int ItemID;                      // If -1 slot is empty.

        public bool IsOn;                       // If off can't have item or draw.
    }

    public struct Window
    {
        public Vec2f Position;
        public Vec2f Size;
        public float TileSize;
        public float SlotBorderOffset;
        public float SlotOffset;

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }

        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public float W
        {
            get => Size.X;
            set => Size.X = value;
        }

        public float H
        {
            get => Size.Y;
            set => Size.Y = value;
        }

        public void Scale(float scaleFactor)
        {
            Position *= scaleFactor;
            Size *= scaleFactor;
            TileSize *= scaleFactor;
            SlotBorderOffset *= scaleFactor;
            SlotOffset *= scaleFactor;
        }

        public bool IsInsideWindow(Vec2f pos)
        {
            if (pos.X < X || pos.Y < Y || pos.X > (X + W) || pos.Y > (Y + H))
                return false;
            return true;
        }
    }
}

