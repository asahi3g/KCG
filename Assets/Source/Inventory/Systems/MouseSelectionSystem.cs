using Entitas;
using UnityEngine;
using KMath;

namespace Inventory
{
    public class MouseSelectionSystem
    {
        // Todo: Move states to unique component.
        InventoryEntity ClickedInventoryEntity = null;
        int             ItemID = -1;
        int             SlotslotID = -1;
        bool            MouseDown = false;
        bool            MouseHold = false;
        float           StartTime = 0;

        public void InventoryPosCalculation(InventoryEntity inventoryEntity, ref float x, ref float y, ref float w, ref float h, ref Vec2f tileSize, bool isToolBar = false)
        {
            InventoryProprieties proprietis = GameState.InventoryCreationApi.Get(inventoryEntity.inventoryID.TypeID);

            tileSize = new Vec2f(proprietis.TileSize, proprietis.TileSize);

            // Get Inventory Info.
            int width = inventoryEntity.inventoryEntity.Width;
            int height = inventoryEntity.inventoryEntity.Height;

            w = width * tileSize.X;
            h = height * tileSize.Y;

            // Get inventory positon.
            x = proprietis.DefaultPosition.X;
            y = proprietis.DefaultPosition.Y;

            x -= w / 2f;
            if (isToolBar)
            {
                x = 960f - w / 2f;
                y = tileSize.Y / 2f;
                h = tileSize.Y;
            }
            else
                y -= h / 2f;
        }

        public void OnMouseUP(Contexts contexts)
        {
            // Initialize states.
            if (ClickedInventoryEntity == null)
                return;

            MouseDown = false;
            if (!MouseHold) // if less than 250ms consider it a click.
            {
                ClickedInventoryEntity.inventoryEntity.SelectedID = SlotslotID;
                SlotslotID = -1;
                ClickedInventoryEntity = null;
                return;
            }
            MouseHold = false;
            SlotslotID = -1;

            Vector3 mousePos = Input.mousePosition;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y);

            //Slot slot = GameState.InventoryManager.GetSlotInPos(contexts, worldPosition.x, worldPosition.y);

            var openInventories = contexts.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryDrawable, InventoryMatcher.InventoryID));

            foreach (var inventoryEntity in openInventories)
            {
                // Get Inventory Info.
                float x = 0, y = 0, w = 0, h = 0;
                int width = inventoryEntity.inventoryEntity.Width;
                Vec2f tileSize = new Vec2f();
                InventoryPosCalculation(inventoryEntity, ref x, ref y, ref w, ref h, ref tileSize);

                // Is mouse inside inventory.
                if (mPos.X < x ||
                    mPos.Y < y ||
                    mPos.X > (x + w) ||
                    mPos.Y > (y + h))
                    continue;
                else
                {
                    int slotID = (int)((h - (mPos.Y - y)) / tileSize.Y);
                    slotID = slotID * width + (int)((mPos.X - x) / tileSize.X);
                    GameState.InventoryManager.AddItemAtSlot(
                        contexts, 
                        contexts.itemInventory.GetEntityWithItemID(ItemID), 
                        inventoryEntity.inventoryID.ID, slotID);
                    return;
                }
            }

            var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);

                // Get Inventory Info.
                float x = 0, y = 0, w = 0, h = 0;
                int width = inventoryEntity.inventoryEntity.Width;
                Vec2f tileSize = new Vec2f();
                InventoryPosCalculation(inventoryEntity, ref x, ref y, ref w, ref h, ref tileSize, true);

                // Is mouse inside inventory.
                if (mPos.X < x ||
                    mPos.Y < y ||
                    mPos.X > (x + w) ||
                    mPos.Y > (y + h))
                    continue;
                else
                {
                    int slotID = (int)((h - (mPos.Y - y)) / tileSize.Y);
                    slotID = slotID * width + (int)((mPos.X - x) / tileSize.X);
                    GameState.InventoryManager.AddItemAtSlot(
                        contexts,
                        contexts.itemInventory.GetEntityWithItemID(ItemID),
                        inventoryEntity.inventoryID.ID, slotID);
                }
            }

        }

        public void OnMouseDown(Contexts contexts)
        {
            Vector3 mousePos = Input.mousePosition;
            Vec2f mPos = new Vec2f(mousePos.x, mousePos.y);

            var openInventories = contexts.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryDrawable, InventoryMatcher.InventoryID));

            foreach (var inventoryEntity in openInventories)
            {
                // Get Inventory Info.
                float x = 0, y = 0, w = 0, h = 0;
                int width = inventoryEntity.inventoryEntity.Width;
                Vec2f tileSize = new Vec2f();
                InventoryPosCalculation(inventoryEntity, ref x, ref y, ref w, ref h, ref tileSize);
                
                // Is mouse inside inventory.
                if (mPos.X < x ||
                    mPos.Y < y ||
                    mPos.X > (x + w) ||
                    mPos.Y > (y + h))
                    continue;
                else
                {
                    MouseDown = true;
                    StartTime = Time.realtimeSinceStartup;

                    int slotID = (int)((h - (mPos.Y - y)) / tileSize.Y);
                    slotID = slotID * width + (int)((mPos.X - x) / tileSize.X);
                    SlotslotID = slotID;
                    ClickedInventoryEntity = inventoryEntity;
                    ItemID = inventoryEntity.inventoryEntity.Slots[slotID].ItemID;
                    return;
                }
            }

            var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                InventoryEntity inventoryEntity = contexts.inventory.GetEntityWithInventoryIDID(inventoryID);
            
                // Get Inventory Info.
                float x = 0, y = 0, w = 0, h = 0;
                int width = inventoryEntity.inventoryEntity.Width;
                Vec2f tileSize = new Vec2f();
                InventoryPosCalculation(inventoryEntity, ref x, ref y, ref w, ref h, ref tileSize, true);
            
                // Is mouse inside inventory.
                if (mPos.X < x ||
                    mPos.Y < y ||
                    mPos.X > (x + w) ||
                    mPos.Y > (y + h))
                    continue;
                else
                {
                    MouseDown = true;
                    StartTime = Time.realtimeSinceStartup;
            
                    int slotID = (int)((h - (mPos.Y - y)) / tileSize.Y);
                    slotID = slotID * width + (int)((mPos.X - x) / tileSize.X);
                    SlotslotID = slotID;
                    ClickedInventoryEntity = inventoryEntity;
                    ItemID = inventoryEntity.inventoryEntity.Slots[slotID].ItemID;
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
                GameState.InventoryManager.RemoveItem(contexts, ClickedInventoryEntity, SlotslotID);
            }

            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vec2f pos = new Vec2f(mousePos.x, mousePos.y);

            ItemInventoryEntity itemEntity = contexts.itemInventory.GetEntityWithItemID(ItemID);
            int SpriteID = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).InventorSpriteID;

            Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);
            GameState.Renderer.DrawSpriteGui(pos.X, pos.Y, 60f / 1920f, 60f / 1080, sprite);
        }
    }
}
