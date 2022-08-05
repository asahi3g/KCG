using UnityEngine;
using System.Collections.Generic;
using KMath;
using Entitas;
using Item;

namespace Inventory
{
    public class DrawSystem
    {
        const int DEFAULT_SCREEN_HIGHT = 1080;

        public void Draw(Contexts contexts)
        {
            // Draw tool bar.
            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(i);
                if (!inventory.HasTooBar())
                    continue;
                if (!inventory.IsDrawToolBarOn())
                    continue;
                
                DrawInventory(contexts, inventory, true);
            }

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(i);
                if (!inventory.IsDrawOn())
                    continue;
                DrawInventory(contexts, inventory, false);
            }

            if (InventorySystemsState.MouseHold)
            {
                float scaleFacor = Screen.height / 1080f;
                Vector3 mousePos = Input.mousePosition;
                Vec2f pos = new Vec2f(mousePos.x, mousePos.y);
                float size = 60f * scaleFacor;

                ItemInventoryEntity itemEntity = contexts.itemInventory.GetEntityWithItemID(InventorySystemsState.GrabbedItemID);
                int SpriteID = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).InventorSpriteID;

                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(pos.X, pos.Y, size, size, sprite);
            }
        }

        private void DrawInventory(Contexts entitasContext, InventoryModel inventory, bool isDrawingToolBar)
        {
            // Get Inventory Info.
            int width = inventory.Width;
            int height = isDrawingToolBar ? 1 : inventory.Height;
            Window window = isDrawingToolBar ? inventory.ToolBarWindow : inventory.MainWindow;
                   
            float scaleFactor = (float)Screen.height / DEFAULT_SCREEN_HIGHT;
            window.Scale(scaleFactor);

            // Draw Background.
            if (inventory.RenderProprieties.HasBackground())
                GameState.Renderer.DrawQuadColorGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y,
                    inventory.RenderProprieties.BackgroundColor);
            if (inventory.RenderProprieties.HasBackgroundTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(
                    inventory.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y, sprite);
            }

            // Draw inventory slots.
            for (int i = 0, length = width * height; i < length; i++)
            {
                ref Slot slot = ref inventory.Slots[i];
                
                if (!slot.IsOn)
                    continue;
            
                float tilePosX = window.GridPosition.X + (i % width) * window.TileSize;
                float tilePosY = window.GridPosition.Y + ((length - 1 - i)  / width) * window.TileSize;
            
                DrawBorder(tilePosX, tilePosY, window.TileSize, window.SlotBorderOffset, i, ref inventory, isDrawingToolBar);
           
                DrawSlot(entitasContext, tilePosX, tilePosY, window.TileSize, window.SlotOffset, scaleFactor, 
                    ref inventory, ref inventory.Slots[i], isDrawingToolBar);

                // Draw tool bar numbers.
                if (i < width && inventory.HasTooBar())
                {
                    int font = (int)(25f * scaleFactor);
                    float offset = 20f * scaleFactor;
                    GameState.Renderer.DrawStringGui(
                        tilePosX,
                        tilePosY + offset - window.TileSize,
                        window.TileSize,
                        window.TileSize,
                        label: (i + 1).ToString(),
                        fontSize: font,
                        alignment: TextAnchor.UpperCenter,
                        color: new Color(255, 255, 255, 255));
                }
            }
        }

        void DrawBorder(float tilePosX, float tilePosY, float tileSize, float borderOffset, 
            int i, ref InventoryModel inventory, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * borderOffset;
            float sizeY = tileSize - 2 * borderOffset;
            float posX = tilePosX + borderOffset;
            float posY = tilePosY + borderOffset;

            if (inventory.RenderProprieties.HasBorder())
            {
                if (inventory.SelectedSlotID == i
                    && !(isDrawingToolBar && inventory.IsDrawOn()) // If inventory is open doesn't draw selected border in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventory.ID) // Only draw selection to one inventory at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventory.RenderProprieties.SelectedColor);
                }
                else
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventory.RenderProprieties.SlotColor);
            }
        }

        void DrawSlot(Contexts contexts, float tilePosX, float tilePosY, float tileSize, float slotOffset,
            float scaleFactor, ref InventoryModel inventory, ref Slot slot, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * slotOffset;
            float sizeY = tileSize - 2 * slotOffset;
            float posX = tilePosX + slotOffset;
            float posY = tilePosY + slotOffset;

            Color slotColor = inventory.RenderProprieties.SlotColor;

            if (inventory.RenderProprieties.HasDefaultSlotTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(inventory.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }
            else
            {
                if (slot.ID == inventory.SelectedSlotID
                    && !inventory.RenderProprieties.HasBorder() // If there is no border slot will have selected slot color.
                    && !(isDrawingToolBar && inventory.IsDrawOn()) // If inventory is open doesn't draw slot with selected color in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventory.ID) // Only draw selection to one inventory at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    slotColor = inventory.RenderProprieties.SelectedColor;
                }
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, slotColor);
            }

            if (slot.SlotBackgroundIcon >= 0)
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(slot.SlotBackgroundIcon, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }

            if (slot.ItemID != -1)
            {
                ItemInventoryEntity entity = contexts.itemInventory.GetEntityWithItemID(slot.ItemID);
                ItemProprieties itemProprieties = GameState.ItemCreationApi.Get(entity.itemType.Type);
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(itemProprieties.InventorSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);

                // Draw Count if stackable.
                if (entity.hasItemStack)
                {
                    int font = (int)(25f * scaleFactor);
                    GameState.Renderer.DrawStringGui(
                        posX,
                        posY,
                        sizeX,
                        sizeY,
                        entity.itemStack.Count.ToString(),
                        font,
                        TextAnchor.LowerRight,
                        Color.white);
                }
            }
        }
    }
}
