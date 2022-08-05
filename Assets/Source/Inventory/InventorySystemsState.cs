namespace Inventory
{
    public static class InventorySystemsState
    {
        // Todo: Move states to unique component.
        public static int   ClickedInventoryID = 0;
        public static int   GrabbedItemID = -1;
        public static int   ClickedSlotslotID = -1;
        public static bool  MouseDown = false;
        public static bool  MouseHold = false;
        public static float TimeSinceClick = 0;
    }
}
