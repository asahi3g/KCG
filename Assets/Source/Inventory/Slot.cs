namespace Inventory
{
    public struct Slot
    {
        public int ID;
        public Enums.ItemGroups Restriction;
        public int SlotBorderBackgroundIcon;    // Sprite icon for ring, armour, etc showing slot type, if -1 ignore/dont render
        public int ItemID;                      // If -1 slot is empty.
    }
}
