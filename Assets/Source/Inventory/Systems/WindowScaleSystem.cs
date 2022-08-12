using KMath;

namespace Inventory
{
    // If more than one window is open make sure they fit in  the screen.
    public class WindowScaleSystem
    {
        // todo: Right now each inventory has a grid. Use on single grid for every inventory.
        // It will make this simpler to position all inventories in the screen.
        public void OnOpenWindow(InventoryEntity inventory, InventoryList inventoryList)
        {
            const int MAX_OPEN_INVENTORY_COUNT = 10;
            int[] openInventoryIndices = new int[MAX_OPEN_INVENTORY_COUNT];
            int length = 0;

            for (int i = 0; i < inventoryList.Length; i++)
            {
                if (inventoryList.Get(i).hasInventoryEntity)
                {
                    Utils.Assert(length < MAX_OPEN_INVENTORY_COUNT);

                    openInventoryIndices[length] = i;
                    length++;
                }
            }


            Vec2f compountInventorySize = Vec2f.Zero;
            bool hiddenInventory = false;
            Window[] openWindows = new Window[length]; // Todo: prealocate this.

            for (int i = 0; i < length; i++)
            {
                openWindows[i] = GameState.InventoryCreationApi.Get(openInventoryIndices[i]).MainWindow;
                compountInventorySize += openWindows[i].Size;
            }

            // Check if windows ovelap each other.
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (j == i)
                        continue;

                    if (openWindows[i].IsAbove(ref openWindows[j]))
                        hiddenInventory = true;
                }
            }

            if (hiddenInventory)
            {
                // Calculate new position and scale
                UnityEngine.Debug.Log("testing.");

            }
        }

        public void OnCloseWindów(InventoryList inventoryList)
        {
            // Does every window now fit?
            // If does remove all windows if not ajust window again

            for (int i = 0; i < inventoryList.Length; i++)
            {
                if (inventoryList.Get(i).hasInventoryWindowAdjustment)
                {
                    inventoryList.Get(i).RemoveInventoryWindowAdjustment();
                }
            }
        }
    }
}
