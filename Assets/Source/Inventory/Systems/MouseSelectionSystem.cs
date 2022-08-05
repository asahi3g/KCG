using Entitas;
using UnityEngine;
using KMath;

namespace Inventory
{
    public class MouseSelectionSystem
    {
        public void OnMouseUP(Contexts contexts)
        {
            // Initialize states.
            if (InventorySystemsState.ClickedSlotslotID < 0)
                return;

            InventorySystemsState.MouseDown = false;
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(InventorySystemsState.ClickedInventoryID);

            if (!InventorySystemsState.MouseHold) // if less than 250ms consider it a click.
            {
                inventory.SelectedSlotID = InventorySystemsState.ClickedSlotslotID;
                InventorySystemsState.ClickedSlotslotID = -1;
                return;
            }

            Vector3 mousePos = Input.mousePosition;
            float scaleFacor = 1080f / Screen.height;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y) * scaleFacor;

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(i);
                if (!openInventory.IsDrawOn())
                    continue;
                if (TryAddItemToInv(contexts, ref openInventory, mPos, false))
                    return;
            }

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel openToolBar = ref GameState.InventoryCreationApi.Get(i);
                if (openToolBar.HasTooBar() || !openToolBar.IsDrawToolBarOn())
                    continue;
                if (TryAddItemToInv(contexts, ref openToolBar, mPos, true))
                    return;
            }

            // If mouse is in not in a valid slot.
            GameState.InventoryManager.AddItemAtSlot(contexts, contexts.itemInventory.GetEntityWithItemID(InventorySystemsState.GrabbedItemID), InventorySystemsState.ClickedInventoryID, InventorySystemsState.ClickedSlotslotID);
            // Reset values.
            InventorySystemsState.MouseHold = false;
            InventorySystemsState.ClickedSlotslotID = -1;
            InventorySystemsState.ClickedInventoryID = -1;
        }

        public void OnMouseDown(Contexts contexts)
        {
            Vector3 mousePos = Input.mousePosition;
            float scaleFacor = 1080f / Screen.height;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y) * scaleFacor;

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(i);
                if (!openInventory.IsDrawOn())
                    continue;
                if (TryPickingUpItemFromInv(contexts, ref openInventory, mPos, false))
                    return;
            }

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel openToolBar = ref GameState.InventoryCreationApi.Get(i);
                if (openToolBar.HasTooBar() || !openToolBar.IsDrawToolBarOn())
                    continue;
                if (TryPickingUpItemFromInv(contexts, ref openToolBar, mPos, true))
                    return;
            }
        }

        public void Update(Contexts contexts)
        {
            if (InventorySystemsState.MouseDown == false)
                return;

            // If less than 250ms consider it a click.
            if (Time.realtimeSinceStartup - InventorySystemsState.TimeSinceClick < 0.15f || InventorySystemsState.GrabbedItemID < 0)
                return;

            if (!InventorySystemsState.MouseHold)
            {
                InventorySystemsState.MouseHold = true;
                GameState.InventoryManager.RemoveItem(contexts, InventorySystemsState.ClickedInventoryID, InventorySystemsState.ClickedSlotslotID);
            }
        }

        /// <summary>
        /// Add Item To inventory if mouse is over it.
        /// </summary>
        private bool TryAddItemToInv(Contexts contexts, ref InventoryModel inventory, Vec2f mousePos, bool isToolBar)
        {
            if (!inventory.IsDrawOn())
                return false;

            Window window = isToolBar ? inventory.ToolBarWindow : inventory.MainWindow;
            int width = inventory.Width;

            // Is mouse inside inventory.
            if (!window.IsInsideWindow(mousePos))
                return false;
            else
            {
                int slotID = (int)((window.GridSize.Y - (mousePos.Y - window.GridPosition.Y)) / window.TileSize);
                slotID = slotID * width + (int)((mousePos.X - window.GridPosition.X) / window.TileSize);
                if (GameState.InventoryManager.AddItemAtSlot(
                    contexts, contexts.itemInventory.GetEntityWithItemID(InventorySystemsState.GrabbedItemID), inventory.ID, slotID))
                {
                    inventory.SelectedSlotID = slotID;
                    InventorySystemsState.ClickedInventoryID = inventory.ID;
                    // Reset values.
                    InventorySystemsState.MouseHold = false;
                    InventorySystemsState.ClickedSlotslotID = -1;
                    return true;
                }
                return false;
            }
        }

        public bool TryPickingUpItemFromInv(Contexts contexts, ref InventoryModel inventory, Vec2f mousePos, bool isToolBar)
        {
            if (!inventory.IsDrawOn())
                return false;

            Window window = isToolBar ? inventory.ToolBarWindow : inventory.MainWindow;
            int width = inventory.Width;

            // Is mouse inside inventory.
            if (!window.IsInsideWindow(mousePos))
                return false;
            else
            {
                InventorySystemsState.MouseDown = true;
                InventorySystemsState.TimeSinceClick = Time.realtimeSinceStartup;

                int slotID = (int)((window.GridSize.Y - (mousePos.Y - window.GridPosition.Y)) / window.TileSize);
                slotID = slotID * width + (int)((mousePos.X - window.GridPosition.X) / window.TileSize);
                InventorySystemsState.ClickedSlotslotID = slotID;
                InventorySystemsState.ClickedInventoryID = inventory.ID;
                InventorySystemsState.GrabbedItemID = inventory.Slots[slotID].ItemID;
                return true;
            }
        }
    }
}
