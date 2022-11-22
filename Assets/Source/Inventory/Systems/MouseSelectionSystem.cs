//import UnityEngine

using KMath;

namespace Inventory
{
    public class MouseSelectionSystem
    {
        public void OnMouseUP(InventoryList inventoryList)
        {
            // Initialize states.
            if (InventorySystemsState.ClickedSlotslotID < 0)
                return;

            ref var planet = ref GameState.Planet;
            InventorySystemsState.MouseDown = false;
            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(InventorySystemsState.ClickedInventoryID);

            if (!InventorySystemsState.MouseHold) // if less than 250ms consider it a click.
            {
                var player = planet.Player;
                if (player != null && inventoryEntity.inventoryInventory.SelectedSlotID !=
                    InventorySystemsState.ClickedSlotslotID)
                {
                    var item = GameState.InventoryManager.GetItemInSlot(inventoryEntity.inventoryID.ID, inventoryEntity.inventoryInventory.SelectedSlotID);
                    player.HandleItemDeselected(item);
                }
                inventoryEntity.inventoryInventory.SelectedSlotID = InventorySystemsState.ClickedSlotslotID;
                if (player != null)
                {
                    var item = GameState.InventoryManager.GetItemInSlot(inventoryEntity.inventoryID.ID, inventoryEntity.inventoryInventory.SelectedSlotID);
                    player.HandleItemSelected(item);
                }
                InventorySystemsState.ClickedSlotslotID = -1;
                return;
            }

            UnityEngine.Vector3 mousePos = UnityEngine.Input.mousePosition;
            float scaleFacor = 1080f / UnityEngine.Screen.height;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y) * scaleFacor;

            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity openInventoryEntity = inventoryList.Get(i);
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(
                    openInventoryEntity.inventoryInventory.InventoryModelID);

                if (!openInventoryEntity.hasInventoryDraw)
                    continue;
                if (TryAddItemToInv(ref openInventory, openInventoryEntity, mPos, false))
                    return;
            }

            // Tool bar.
            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity openInventoryEntity = inventoryList.Get(i);
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(
                    openInventoryEntity.inventoryInventory.InventoryModelID); 
                
                if (!openInventory.HasToolBar || !inventoryEntity.hasInventoryToolBarDraw)
                    continue;
                if (TryAddItemToInv(ref openInventory, openInventoryEntity, mPos, true))
                    return;
            }

            // If mouse is in not in a valid slot.
            GameState.InventoryManager.AddItemAtSlot(planet.EntitasContext.itemInventory.GetEntityWithItemID(InventorySystemsState.GrabbedItemID), InventorySystemsState.ClickedInventoryID, InventorySystemsState.ClickedSlotslotID);
            // Reset values.
            InventorySystemsState.MouseHold = false;
            InventorySystemsState.ClickedSlotslotID = -1;
            InventorySystemsState.ClickedInventoryID = -1;
        }

        public void OnMouseDown(InventoryList inventoryList)
        {
            UnityEngine.Vector3 mousePos = UnityEngine.Input.mousePosition;
            float scaleFacor = 1080f / UnityEngine.Screen.height;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y) * scaleFacor;

            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity openInventoryEntity = inventoryList.Get(i);
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(
                    openInventoryEntity.inventoryInventory.InventoryModelID);
                
                if (!openInventoryEntity.hasInventoryDraw)
                    continue;
                if (TryPickingUpItemFromInv(ref openInventory, openInventoryEntity, mPos, false))
                    return;
            }

            // Tool bar.
            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity openInventoryEntity = inventoryList.Get(i);
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(
                    openInventoryEntity.inventoryInventory.InventoryModelID);

                if (!openInventory.HasToolBar || !openInventoryEntity.hasInventoryToolBarDraw)
                    continue;
                if (TryPickingUpItemFromInv(ref openInventory, openInventoryEntity, mPos, true))
                    return;
            }
        }

        public void Update()
        {
            if (InventorySystemsState.MouseDown == false)
                return;

            // If less than 250ms consider it a click.
            if (UnityEngine.Time.realtimeSinceStartup - InventorySystemsState.TimeSinceClick < 0.15f || InventorySystemsState.GrabbedItemID < 0)
                return;

            if (!InventorySystemsState.MouseHold)
            {
                InventorySystemsState.MouseHold = true;
                GameState.InventoryManager.RemoveItem(InventorySystemsState.ClickedInventoryID, InventorySystemsState.ClickedSlotslotID);
            }
        }

        // Add Item To inventory if mouse is over it.
        private bool TryAddItemToInv(ref InventoryModel inventoryModel, InventoryEntity inventoryEntity, Vec2f mousePos, bool isToolBar)
        {
            Window window = isToolBar ? inventoryModel.ToolBarWindow :
                (inventoryEntity.hasInventoryWindowAdjustment) ? inventoryEntity.inventoryWindowAdjustment.window : inventoryModel.MainWindow;
            int width = inventoryModel.Width;

            // Is mouse inside inventory.
            if (!window.IsInsideWindow(mousePos))
                return false;
            else
            {
                int gridSlotID = (int)((window.GridSize.Y - (mousePos.Y - window.GridPosition.Y)) / window.TileSize);
                gridSlotID = gridSlotID * width + (int)((mousePos.X - window.GridPosition.X) / window.TileSize);
                GridSlot gridSlot = inventoryModel.Slots[gridSlotID];

                int slotID = gridSlot.SlotID;
                if (slotID == -1)
                    return false;

                if (GameState.InventoryManager.AddItemAtSlot(GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(
                    InventorySystemsState.GrabbedItemID), inventoryEntity.inventoryID.ID, slotID))
                {
                    inventoryEntity.inventoryInventory.SelectedSlotID = slotID;
                    InventorySystemsState.ClickedInventoryID = inventoryEntity.inventoryID.ID;
                    // Reset values.
                    InventorySystemsState.MouseHold = false;
                    InventorySystemsState.ClickedSlotslotID = -1;
                    return true;
                }
                return false;
            }
        }

        public bool TryPickingUpItemFromInv(ref InventoryModel inventoryModel, InventoryEntity inventoryEntity, Vec2f mousePos, bool isToolBar)
        {
            Window window = isToolBar ? inventoryModel.ToolBarWindow :
                (inventoryEntity.hasInventoryWindowAdjustment) ? inventoryEntity.inventoryWindowAdjustment.window : inventoryModel.MainWindow;
            int width = inventoryModel.Width;

            // Is mouse inside inventory.
            if (!window.IsInsideWindow(mousePos)) return false;
            
            InventorySystemsState.MouseDown = true;
            InventorySystemsState.TimeSinceClick = UnityEngine.Time.realtimeSinceStartup;

            int gridSlotID = (int)((window.GridSize.Y - (mousePos.Y - window.GridPosition.Y)) / window.TileSize);
            gridSlotID = gridSlotID * width + (int)((mousePos.X - window.GridPosition.X) / window.TileSize);
            GridSlot gridSlot = inventoryModel.Slots[gridSlotID];

            int slotID = gridSlot.SlotID;
            if (slotID == -1) return false;

            InventorySystemsState.ClickedSlotslotID = slotID;
            InventorySystemsState.ClickedInventoryID = inventoryEntity.inventoryID.ID;
            InventorySystemsState.GrabbedItemID = inventoryEntity.inventoryInventory.Slots[slotID].ItemID;
            return true;
        }
    }
}
