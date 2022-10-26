using System;
using Utility;

namespace Inventory
{
    // If more than one window is open make sure they fit in  the screen.
    public class WindowScaleSystem
    {
        // Todo: run this only once per frame.
        // todo: Right now each inventory has a grid. Use on single grid for every inventory.
        // If inventory ovelap with another. Find empty spot and put it there.
        // It will make this simpler to position all inventories in the screen.
        public void OnOpenWindow(InventoryEntity inventory)
        {
            const int MAX_OPEN_INVENTORY_COUNT = 10;
            int[] openInventoryIndices = new int[MAX_OPEN_INVENTORY_COUNT];
            int length = 0;

            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.InventoryList.Length; i++)
            {
                if (planet.InventoryList.Get(i).hasInventoryDraw)
                {
                    Utils.Assert(length < MAX_OPEN_INVENTORY_COUNT);

                    openInventoryIndices[length] = i;
                    length++;
                }
            }
                

            Window[] openWindows = new Window[length];

            for (int i = 0; i < length; i++)
            {
                openWindows[i] = GameState.InventoryCreationApi.Get(openInventoryIndices[i]).MainWindow;
            }

            // Todo: for every open window holds an array with the index of the windows that overlaps with this one.
            int[] hiddenWindowsIndex = new int[length];
            Array.Fill(hiddenWindowsIndex, -1);
            int hiddenWindowLength = 0;
            float accumulatedY = 0;

            // Check if windows ovelap each other.
            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    if (j == i)
                        continue;

                    if (openWindows[i].IsAbove(ref openWindows[j]))
                    {
                        if (!Array.Exists(hiddenWindowsIndex, element => (element == i)))
                        {
                            hiddenWindowsIndex[hiddenWindowLength] = i;
                            accumulatedY += openWindows[i].Size.Y;
                            hiddenWindowLength++;
                        }
                        if (!Array.Exists(hiddenWindowsIndex, element => (element == j)))
                        {
                            hiddenWindowsIndex[hiddenWindowLength] = j;
                            accumulatedY += openWindows[j].Size.Y;
                            hiddenWindowLength++;
                        }
                    }
                }
            }

            // If there is overlap, move the windows.
            if (hiddenWindowLength > 0)
            {
                const float MINIMUM_Y = 120;
                const float DEFAULT_HEGIHT = 1080;
                const float INVENTORIES_GAP = 40;    // Gap between inventories.

                accumulatedY += MINIMUM_Y + INVENTORIES_GAP * (hiddenWindowLength - 1);

                if (accumulatedY > DEFAULT_HEGIHT)
                {
                    // todo: Scale windows.
                    // todo: ajust accumulatedY value to new scale
                }

                float relativePosition = 0; // Vertical position relative to lowest inventory Y position.
                while (hiddenWindowLength > 0)
                {
                    int j = 0;
                    int index = hiddenWindowsIndex[j];

                    for (int i = 1; i < hiddenWindowLength; i++)
                    {
                        if (openWindows[index].Position.Y > openWindows[hiddenWindowsIndex[i]].Position.Y)
                        {
                            index = hiddenWindowsIndex[i];
                            j = i;
                        }
                    }

                    // Divide by 2 to centralize. 
                    float posOffsetY = openWindows[index].GridPosition.Y - openWindows[index].Position.Y;
                    openWindows[index].Position.Y = (DEFAULT_HEGIHT / 2 + MINIMUM_Y) - accumulatedY / 2 + relativePosition;
                    openWindows[index].GridPosition.Y = openWindows[index].Position.Y + posOffsetY;

                    relativePosition += openWindows[index].Size.Y + INVENTORIES_GAP;
                    if (!planet.InventoryList.Get(openInventoryIndices[index]).hasInventoryWindowAdjustment)
                        planet.InventoryList.Get(openInventoryIndices[index]).AddInventoryWindowAdjustment(openWindows[index]);
                    else
                        planet.InventoryList.Get(openInventoryIndices[index]).inventoryWindowAdjustment.window = openWindows[index];

                    hiddenWindowsIndex[j] = hiddenWindowsIndex[--hiddenWindowLength]; // Decrease hiddenWindowsLength and remove ajusted window.
                }
            }
        }

        public void OnCloseWindow(InventoryList inventoryList)
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
