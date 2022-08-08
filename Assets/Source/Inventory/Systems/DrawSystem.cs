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

        public void Draw(Contexts contexts, InventoryList inventoryList)
        {
            // Draw tool bar.
            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity inventoryEntity = inventoryList.Get(i);
                if (!inventoryEntity.hasInventoryToolBarDraw)
                    continue;

                ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                    inventoryEntity.inventoryEntity.InventoryModelID);
                
                DrawInventory(contexts, inventoryEntity, ref inventoryModel, true);
            }

            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity inventoryEntity = inventoryList.Get(i);
                if (!inventoryEntity.hasInventoryDraw)
                    continue;
                ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(i);
                DrawInventory(contexts, inventoryEntity, ref inventoryModel, false);
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

        private void DrawInventory(Contexts entitasContext, InventoryEntity inventoryEntity, ref InventoryModel inventoryModel, bool isDrawingToolBar)
        {
            // Get Inventory Info.
            int width = inventoryModel.Width;
            int height = isDrawingToolBar ? 1 : inventoryModel.Height;
            Window window = isDrawingToolBar ? inventoryModel.ToolBarWindow : inventoryModel.MainWindow;
                   
            float scaleFactor = (float)Screen.height / DEFAULT_SCREEN_HIGHT;
            window.Scale(scaleFactor);

            // Draw Background.
            if (inventoryModel.RenderProprieties.HasBackground())
                GameState.Renderer.DrawQuadColorGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y,
                    inventoryModel.RenderProprieties.BackgroundColor);
            if (inventoryModel.RenderProprieties.HasBackgroundTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(
                    inventoryModel.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y, sprite);
            }

            // Draw inventoryModel slots.
            for (int i = 0, length = width * height; i < length; i++)
            {
                GridSlot gridSlot = inventoryModel.Slots[i];
                
                if (gridSlot.SlotID == -1)
                    continue;
            
                float tilePosX = window.GridPosition.X + (i % width) * window.TileSize;
                float tilePosY = window.GridPosition.Y + ((length - 1 - i)  / width) * window.TileSize;
            
                DrawBorder(tilePosX, tilePosY, window.TileSize, window.SlotBorderOffset, i, inventoryEntity, ref inventoryModel, isDrawingToolBar);
           
                DrawSlot(entitasContext, tilePosX, tilePosY, window.TileSize, window.SlotOffset, scaleFactor, inventoryEntity,
                    ref inventoryModel, inventoryModel.Slots[i], isDrawingToolBar);

                // Draw tool bar numbers.
                if (i < width && inventoryModel.HasToolBar)
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
            int i, InventoryEntity inventoryEntity, ref InventoryModel inventoryModel, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * borderOffset;
            float sizeY = tileSize - 2 * borderOffset;
            float posX = tilePosX + borderOffset;
            float posY = tilePosY + borderOffset;

            if (inventoryModel.RenderProprieties.HasBorder())
            {
                if (inventoryEntity.inventoryEntity.SelectedSlotID == i
                    && !(isDrawingToolBar && inventoryEntity.hasInventoryDraw) // If inventoryModel is open doesn't draw selected border in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventoryModel.ID) // Only draw selection to one inventoryModel at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventoryModel.RenderProprieties.SelectedColor);
                }
                else
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventoryModel.RenderProprieties.SlotColor);
            }
        }

        void DrawSlot(Contexts contexts, float tilePosX, float tilePosY, float tileSize, float slotOffset,
            float scaleFactor, InventoryEntity inventoryEntity, ref InventoryModel inventoryModel, GridSlot gridSlot, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * slotOffset;
            float sizeY = tileSize - 2 * slotOffset;
            float posX = tilePosX + slotOffset;
            float posY = tilePosY + slotOffset;

            Color slotColor = inventoryModel.RenderProprieties.SlotColor;

            if (inventoryModel.RenderProprieties.HasDefaultSlotTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(inventoryModel.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }
            else
            {
                if (gridSlot.SlotID == inventoryEntity.inventoryEntity.SelectedSlotID
                    && !inventoryModel.RenderProprieties.HasBorder() // If there is no border slot will have selected slot color.
                    && !(isDrawingToolBar && inventoryEntity.hasInventoryDraw) // If inventoryModel is open doesn't draw slot with selected color in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventoryModel.ID) // Only draw selection to one inventoryModel at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    slotColor = inventoryModel.RenderProprieties.SelectedColor;
                }
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, slotColor);
            }

            if (gridSlot.SlotBackgroundIcon >= 0)
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(gridSlot.SlotBackgroundIcon, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }

            Slot slot = inventoryEntity.inventoryEntity.Slots[gridSlot.SlotID];
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
