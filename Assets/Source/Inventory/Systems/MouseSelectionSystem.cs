using Entitas;
using UnityEngine;
using KMath;

namespace Inventory
{
    public class MouseSelectionSystem
    {
        // Todo: Move states to unique component.
        int             InventoryID = -1;
        int             ItemID = -1;
        int             SlotslotID = -1;
        bool            MouseDown = false;
        bool            MouseHold = false;
        float           StartTime = 0;

        public void OnMouseUP(Contexts contexts)
        {
            // Initialize states.
            if (InventoryID < -1)
                return;

            MouseDown = false;
            ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(InventoryID);

            if (!MouseHold) // if less than 250ms consider it a click.
            {
                inventory.SelectedSlotID = SlotslotID;
                SlotslotID = -1;
                InventoryID = -1;
                return;
            }

            MouseHold = false;
            SlotslotID = -1;

            Vector3 mousePos = Input.mousePosition;
            float scaleFacor = 1080f / Screen.height;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y) * scaleFacor;

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(i);
                if (!inventory.IsDrawOn())
                    continue;
           
                Window window = openInventory.MainWindow;
                int width = openInventory.Width;

                // Is mouse inside inventory.
                if (!window.IsInsideWindow(mPos))
                    continue;
                else
                {
                    int slotID = (int)((window.H - (mPos.Y - window.Y)) / window.TileSize);
                    slotID = slotID * width + (int)((mPos.X - window.X) / window.TileSize);
                    GameState.InventoryManager.AddItemAtSlot(
                        contexts,
                        contexts.itemInventory.GetEntityWithItemID(ItemID),
                        i, slotID);
                    return;
                }
            }

            var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                ref InventoryModel openInventory = ref GameState.InventoryCreationApi.Get(inventoryID);

                int width = openInventory.Width;
                Window window = openInventory.ToolBarWindow;

                // Is mouse inside inventory.
                if (!window.IsInsideWindow(mPos))
                    continue;
                else
                {
                    int slotID = (int)((window.H - (mPos.Y - window.Y)) / window.TileSize);
                    slotID = slotID * width + (int)((mPos.X - window.X) / window.TileSize);
                    GameState.InventoryManager.AddItemAtSlot(
                        contexts,
                        contexts.itemInventory.GetEntityWithItemID(ItemID),
                        inventoryID, slotID);
                }
            }

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

                Window window = openInventory.MainWindow;
                int width = openInventory.Width;

                // Is mouse inside inventory.
                 if (!window.IsInsideWindow(mPos))
                    continue;
                else
                {
                    MouseDown = true;
                    StartTime = Time.realtimeSinceStartup;

                    int slotID = (int)((window.H - (mPos.Y - window.Y)) / window.TileSize);
                    slotID = slotID * width + (int)((mPos.X - window.X) / window.TileSize);
                    SlotslotID = slotID;
                    InventoryID = i;
                    ItemID = openInventory.Slots[slotID].ItemID;
                    return;
                }
            }

            var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                ref InventoryModel openToolBar = ref GameState.InventoryCreationApi.Get(inventoryID);

                int width = openToolBar.Width;
                Window window = openToolBar.ToolBarWindow;

                // Is mouse inside inventory.
                if (!window.IsInsideWindow(mPos))
                    continue;
                else
                {
                    MouseDown = true;
                    StartTime = Time.realtimeSinceStartup;

                    int slotID = (int)((window.H - (mPos.Y - window.Y)) / window.TileSize);
                    slotID = slotID * width + (int)((mPos.X - window.X) / window.TileSize);
                    SlotslotID = slotID;
                    InventoryID = inventoryID;
                    ItemID = openToolBar.Slots[slotID].ItemID;
                }
            }

        }

        /// <summary>
        /// This needs to be called after Inventory.DrawSystem.Draw()
        /// </summary>
        /// <param name="contexts"></param>
        public void Draw(Contexts contexts)
        {
            if (MouseDown == false)
                return;

            // If less than 250ms consider it a click.
            if (Time.realtimeSinceStartup - StartTime < 0.15f || ItemID < 0)
                return;

            if (!MouseHold)
            {
                MouseHold = true;
                GameState.InventoryManager.RemoveItem(contexts, InventoryID, SlotslotID);
            }

            float scaleFacor = Screen.height / 1080f;
            Vector3 mousePos = Input.mousePosition;
            Vec2f pos = new Vec2f(mousePos.x, mousePos.y) * 1f / scaleFacor;

            ItemInventoryEntity itemEntity = contexts.itemInventory.GetEntityWithItemID(ItemID);
            int SpriteID = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).InventorSpriteID;

            Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);
            pos.X = pos.X - 60f * scaleFacor / 2; // Centralize the sprite
            float size = 60f * scaleFacor;
            GameState.Renderer.DrawSpriteGui(pos.X, pos.Y, size, size, sprite);
        }
    }
}
