//imports UnityEngine

using KMath;
using Item;

namespace Inventory
{
    public class DrawSystem
    {
        const int DEFAULT_SCREEN_HIGHT = 1080;

        public void Draw()
        {
            ref var planet = ref GameState.Planet;
            var inventoryList = planet.InventoryList;

            // Draw tool bar.
            int id = planet.Player.agentInventory.InventoryID;
            InventoryEntity playerInventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(id);
            DrawInventory(playerInventory, ref GameState.InventoryCreationApi.Get(playerInventory.inventoryInventoryEntity.InventoryEntityTemplateID), true);

            for (int i = 0; i < inventoryList.Length; i++)
            {
                InventoryEntity inventoryEntity = inventoryList.Get(i);
                if (!inventoryEntity.hasInventoryDraw)
                    continue;
                ref InventoryTemplateData InventoryEntityTemplate = ref GameState.InventoryCreationApi.Get(
                    inventoryEntity.inventoryInventoryEntity    .InventoryEntityTemplateID);
                DrawInventory(inventoryEntity, ref InventoryEntityTemplate, false);
            }

            if (InventorySystemsState.MouseHold)
            {
                float scaleFacor = UnityEngine.Screen.height / 1080f;
                UnityEngine.Vector3 mousePos = UnityEngine.Input.mousePosition;
                Vec2f pos = new Vec2f(mousePos.x, mousePos.y);
                float size = 60f * scaleFacor;

                ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(InventorySystemsState.GrabbedItemID);
                int SpriteID = GameState.ItemCreationApi.Get(itemEntity.itemType.Type).InventorSpriteID;

                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(pos.X, pos.Y, size, size, sprite);
            }
        }

        private void DrawInventory(InventoryEntity inventoryEntity, ref InventoryTemplateData InventoryEntityTemplate, bool isDrawingToolBar)
        {
            // Get Inventory Info.
            int width = InventoryEntityTemplate.Width;
            int height = isDrawingToolBar ? 1 : InventoryEntityTemplate.Height;
            Window window = isDrawingToolBar ? InventoryEntityTemplate.ToolBarWindow : 
                (inventoryEntity.hasInventoryWindowAdjustment) ? inventoryEntity.inventoryWindowAdjustment.window : InventoryEntityTemplate.MainWindow;
                   
            float scaleFactor = (float)UnityEngine.Screen.height / DEFAULT_SCREEN_HIGHT;
            window.Scale(scaleFactor);

            // Draw Background.
            if (InventoryEntityTemplate.RenderProprieties.HasBackground())
                GameState.Renderer.DrawQuadColorGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y,
                    InventoryEntityTemplate.RenderProprieties.BackgroundColor);
            if (InventoryEntityTemplate.RenderProprieties.HasBackgroundTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(
                    InventoryEntityTemplate.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(window.Position.X, window.Position.Y, window.Size.X, window.Size.Y, sprite);
            }

            // Draw InventoryEntityTemplate slots.
            for (int i = 0, length = width * height; i < length; i++)
            {
                GridSlot gridSlot = InventoryEntityTemplate.Slots[i];
                
                if (gridSlot.SlotID == -1)
                    continue;
            
                float tilePosX = window.GridPosition.X + (i % width) * window.TileSize;
                float tilePosY = window.GridPosition.Y + ((length - 1 - i)  / width) * window.TileSize;
            
                DrawBorder(tilePosX, tilePosY, window.TileSize, window.SlotBorderOffset, i, inventoryEntity, ref InventoryEntityTemplate, isDrawingToolBar);
           
                DrawSlot(tilePosX, tilePosY, window.TileSize, window.SlotOffset, scaleFactor, inventoryEntity,
                    ref InventoryEntityTemplate, InventoryEntityTemplate.Slots[i], isDrawingToolBar);

                // Draw tool bar numbers.
                if (i < width && InventoryEntityTemplate.HasToolBar)
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
                        alignment: UnityEngine.TextAnchor.UpperCenter,
                        color: new UnityEngine.Color(255, 255, 255, 255));
                }
            }
        }

        void DrawBorder(float tilePosX, float tilePosY, float tileSize, float borderOffset, 
            int i, InventoryEntity inventoryEntity, ref InventoryTemplateData InventoryEntityTemplate, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * borderOffset;
            float sizeY = tileSize - 2 * borderOffset;
            float posX = tilePosX + borderOffset;
            float posY = tilePosY + borderOffset;

            if (InventoryEntityTemplate.RenderProprieties.HasBorder())
            {
                if (inventoryEntity.inventoryInventoryEntity.SelectedSlotID == i
                    && !(isDrawingToolBar && inventoryEntity.hasInventoryDraw) // If InventoryEntityTemplate is open doesn't draw selected border in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventoryEntity.inventoryID.ID) // Only draw selection to one InventoryEntityTemplate at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, InventoryEntityTemplate.RenderProprieties.SelectedColor);
                }
                else
                    GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, InventoryEntityTemplate.RenderProprieties.SlotColor);
            }
        }

        void DrawSlot(float tilePosX, float tilePosY, float tileSize, float slotOffset,
            float scaleFactor, InventoryEntity inventoryEntity, ref InventoryTemplateData InventoryEntityTemplate, GridSlot gridSlot, bool isDrawingToolBar)
        {
            float sizeX = tileSize - 2 * slotOffset;
            float sizeY = tileSize - 2 * slotOffset;
            float posX = tilePosX + slotOffset;
            float posY = tilePosY + slotOffset;

            UnityEngine.Color slotColor = InventoryEntityTemplate.RenderProprieties.SlotColor;

            if (InventoryEntityTemplate.RenderProprieties.HasDefaultSlotTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(InventoryEntityTemplate.RenderProprieties.BackGroundSpriteID, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }
            else
            {
                if (gridSlot.SlotID == inventoryEntity.inventoryInventoryEntity.SelectedSlotID
                    && !InventoryEntityTemplate.RenderProprieties.HasBorder() // If there is no border slot will have selected slot color.
                    && !(isDrawingToolBar && inventoryEntity.hasInventoryDraw) // If InventoryEntityTemplate is open doesn't draw slot with selected color in tool bar.
                    && !(!isDrawingToolBar && InventorySystemsState.ClickedInventoryID != inventoryEntity.inventoryID.ID) // Only draw selection to one InventoryEntityTemplate at time.
                    && !InventorySystemsState.MouseDown) // If grabbing item doesn't draw selection.
                {
                    slotColor = InventoryEntityTemplate.RenderProprieties.SelectedColor;
                }
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, slotColor);
            }

            if (gridSlot.SlotBackgroundIcon >= 0)
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(gridSlot.SlotBackgroundIcon, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);
            }

            Slot slot = inventoryEntity.inventoryInventoryEntity.Slots[gridSlot.SlotID];
            if (slot.ItemID != -1)
            {
                ItemInventoryEntity entity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(slot.ItemID);
                ItemProperties ItemProperties = GameState.ItemCreationApi.Get(entity.itemType.Type);
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(ItemProperties.InventorSpriteID, Enums.AtlasType.Particle);
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
                        UnityEngine.TextAnchor.LowerRight,
                        UnityEngine.Color.white);
                }
            }
        }
    }
}
