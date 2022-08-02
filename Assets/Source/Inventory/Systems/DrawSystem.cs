using UnityEngine;
using System.Collections.Generic;
using KMath;
using Entitas;
using Item;

namespace Inventory
{
    public class DrawSystem
    {
        public void Draw(Contexts contexts)
        {
            var openInventories = contexts.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryDrawable, InventoryMatcher.InventoryID));
            // If empty Draw ToolBar.

            foreach (InventoryEntity inventoryEntity in openInventories)
            {
                DrawInventory(contexts, inventoryEntity);
            }
        }

        private void DrawInventory(Contexts entitasContext, InventoryEntity inventoryEntity)
        {
            // Todo: Change font size with the size of the inventory.
            // Todo: Deals with scaling proprerly.

            // Calculate Positions and Tile Sizes relative to sceen.

            InventoryProprieties proprietis = GameState.InventoryCreationApi.Get(inventoryEntity.inventoryID.TypeID);

            int screenHeight = 1080;
            int screenWidth = 1920;

            Vec2f tileSize =        new Vec2f(proprietis.TileSize     / screenHeight, proprietis.TileSize     / screenHeight * screenWidth / screenHeight);
            Vec2f borderOffset =    new Vec2f(proprietis.BorderOffset / screenHeight, proprietis.BorderOffset / screenHeight * screenWidth / screenHeight);
            Vec2f slotOffset =      new Vec2f(proprietis.SlotOffset   / screenHeight, proprietis.SlotOffset   / screenHeight * screenWidth / screenHeight);

            // Get Inventory Info.
            int width = inventoryEntity.inventoryEntity.Width;
            int height = inventoryEntity.inventoryEntity.Height;

            float h = height * tileSize.Y;
            float w = width * tileSize.X;

            // Get inventory positon.
            float x = 0.5f;
            float y = 0.5f;

            x -= w / 2f;
            y -= h / 2f;

            // If is tool bar draw at the botton of the screen.
            //if (proprietis.HasTooBar())
            //    y = tileSize.Y / 2f;

            // Draw Background.
            if (proprietis.HasBackground())
                GameState.Renderer.DrawQuadColorGui(x, y, w, h, proprietis.BackgroundColor);
            if (proprietis.HasBackgroundTexture())
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(proprietis.BackGroundSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(x, y, w, h, sprite);
            }

            // Draw inventory slots.
            for (int i = 0, length = inventoryEntity.inventoryEntity.Slots.Length; i < length; i++)
            {
                ref Slot slot = ref inventoryEntity.inventoryEntity.Slots[i];
            
                float tilePosX = x + (i % width) * tileSize.X;
                float tilePosY = y + ((length - 1 - i)  / width) * tileSize.Y;
            
                DrawBorder(tilePosX, tilePosY, tileSize, borderOffset, i, ref proprietis, ref inventoryEntity);
           
                DrawSlot(entitasContext, tilePosX, tilePosY, tileSize, slotOffset, ref proprietis, ref inventoryEntity.inventoryEntity.Slots[i]);

                // Draw tool bar numbers.
                if (i < width)
                {
                    float offset = (20f / screenHeight);
                    GameState.Renderer.DrawStringGui(
                        tilePosX,
                        tilePosY + offset - tileSize.Y,
                        tileSize.X,
                        tileSize.Y,
                        label: (i + 1).ToString(),
                        fontSize: 25,
                        alignment: TextAnchor.UpperCenter,
                        color: new Color(255, 255, 255, 255));
                }
            }
        }

        void DrawBorder(float tilePosX, float tilePosY, Vec2f tileSize, Vec2f borderOffset, 
            int i, ref InventoryProprieties proprietis, ref InventoryEntity inventoryEntity)
        {
            float sizeX = tileSize.X - 2 * borderOffset.X;
            float sizeY = tileSize.Y - 2 * borderOffset.Y;
            float posX = tilePosX + borderOffset.X;
            float posY = tilePosY + borderOffset.Y;
            if (proprietis.HasBorder() && (inventoryEntity.inventoryEntity.SelectedID != i))
            {
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, proprietis.SlotColor);
            }
            if (inventoryEntity.inventoryEntity.SelectedID == i)
            {
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, proprietis.SelectedColor);
            }
        }

        void DrawSlot(Contexts contexts, float tilePosX, float tilePosY, Vec2f tileSize, Vec2f slotOffset, 
            ref InventoryProprieties proprietis, ref Slot slot)
        {
            float sizeX = tileSize.X - 2 * slotOffset.X;
            float sizeY = tileSize.Y - 2 * slotOffset.Y;
            float posX = tilePosX + slotOffset.X;
            float posY = tilePosY + slotOffset.Y;

            GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, proprietis.SlotColor);

            if (slot.ItemID != -1)
            {
                ItemInventoryEntity entity = contexts.itemInventory.GetEntityWithItemID(slot.ItemID);
                ItemProprieties itemProprieties = GameState.ItemCreationApi.Get(entity.itemType.Type);
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(itemProprieties.InventorSpriteID, Enums.AtlasType.Particle);
                GameState.Renderer.DrawSpriteGui(posX, posY, sizeX, sizeY, sprite);

                // Draw Count if stackable.
                if (entity.hasItemStack)
                {
                    GameState.Renderer.DrawStringGui(
                        posX,
                        posY,
                        sizeX,
                        sizeY,
                        entity.itemStack.Count.ToString(),
                        20,
                        TextAnchor.LowerRight,
                        Color.white);
                }
            }
        }
    }
}
