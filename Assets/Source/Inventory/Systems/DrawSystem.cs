using UnityEngine;
using System.Collections.Generic;
using KMath;
using Entitas;
using Item;

namespace Inventory
{
    public class DrawSystem
    {
        public void Draw(Contexts contexts, Transform transform)
        {
            var openInventories = contexts.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryDrawable, InventoryMatcher.InventoryID));
            // If empty Draw ToolBar.

            foreach (InventoryEntity inventoryEntity in openInventories)
            {
                DrawInventory(contexts, transform, inventoryEntity);
            }
        }

        private void DrawInventory(Contexts entitasContext, Transform transform, InventoryEntity inventoryEntity)
        {
            // Todo: Add scrool bar.
            // Todo: Allow user to move inventory position?
            // Todo: Change font size with the size of the inventory.

            // Calculate Positions and Tile Sizes relative to sceen.

            Vec2f tileSize = new Vec2f(1f / 16f, 1f / 16f * Screen.width / Screen.height);
            Vec2f slotSize = tileSize * 0.9f;   // Area of icon.

            // Get Inventory Info.
            int width = inventoryEntity.inventorySize.Width;
            int height = inventoryEntity.inventorySize.Height;

            float h = height * tileSize.Y;
            float w = width * tileSize.X;

            // Get Initial Positon.
            float x = 0.5f;
            float y = 0.5f;

            x -= w / 2f;
            y -= h / 2f;

            // If is tool bar draw at the botton of the screen.
            if (inventoryEntity.isInventoryToolBar)
                y = tileSize.Y / 2f;

            DrawBackGround(x, y, w, h);

            DrawCells(x, y, width, height, tileSize, slotSize, inventoryEntity);

            var itemsInInventory = entitasContext.itemInventory.GetEntitiesWithItemInventory(inventoryEntity.inventoryID.ID);
            DrawIcons(entitasContext, x, y, width, height, tileSize, slotSize, transform, itemsInInventory);
        }

        void DrawBackGround(float x, float y, float w, float h)
        {
            Color backGround = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            GameState.Renderer.DrawQuadColorGui(x, y, w, h, backGround);
        }

        void DrawCells(float x, float y, int width, int height, Vec2f tileSize, Vec2f slotSize, InventoryEntity inventoryEntity)
        {
            Color borderColor = Color.grey;
            Color selectedBorderColor = Color.yellow;

            int selectedSlotPos = inventoryEntity.inventorySlots.Selected;
            int selectedSlotPosX = selectedSlotPos % width;
            int selectedSlotPosY = (height - 1) - (selectedSlotPos - selectedSlotPosX) / width;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Assign Border Color.
                    Color quadColor = borderColor;
                    if (selectedSlotPosX == i && selectedSlotPosY == j)
                        quadColor = selectedBorderColor;

                    // Get Quad Position
                    float slotX = x + i * tileSize.X;
                    float slotY = y + j * tileSize.Y;

                    // Draw OutsideBorder Cell.
                    float posX = slotX + (tileSize.X - slotSize.X) / 2.0f;
                    float posY = slotY + (tileSize.Y - slotSize.Y) / 2.0f;
                    GameState.Renderer.DrawQuadColorGui(posX, posY, slotSize.X, slotSize.Y, quadColor);
                    // Draw cell border.
                    Vec2f spriteSize = slotSize * 0.8f;
                    posX = slotX + (tileSize.X - spriteSize.X) / 2.0f;
                    posY = slotY + (tileSize.Y - spriteSize.Y) / 2.0f;
                    GameState.Renderer.DrawQuadColorGui(posX, posY, spriteSize.X, spriteSize.Y, borderColor);


                    //if (inventoryEntity.isInventoryToolBar)
                    //{
                    //    GameState.Renderer.DrawStringGui(
                    //            slotX + (tileSize.X - slotSize.X) / 2.0f,
                    //            slotY + (tileSize.Y - slotSize.Y) / 2.0f - slotSize.Y,
                    //            slotSize.X,
                    //            slotSize.Y,
                    //            label: (i + 1).ToString(),
                    //            fontSize: 16,
                    //            alignment: TextAnchor.UpperCenter,
                    //            color: new Color(255, 255, 255, 255));
                    //}
                }
            }
        }

        void DrawIcons(Contexts entitasContext, float x, float y, int width, int height, Vec2f tileSize, Vec2f slotSize, Transform transform, HashSet<ItemInventoryEntity> itemInInventory)
        {
            foreach (ItemInventoryEntity itemEntity in itemInInventory)
            {
                if (!itemEntity.isEnabled)
                    return;

                int slotNumber = itemEntity.itemInventory.SlotNumber;
                int i = slotNumber % width;
                int j = (height - 1) - (slotNumber - i) / width;

                // Calculate Slot Border positon.
                float slotX = x + i * tileSize.X;
                float slotY = y + j * tileSize.Y;

                // Draw sprites.
                Item.ItemProprieties itemProprieties = GameState.ItemCreationApi.Get(itemEntity.itemType.Type);
                int SpriteID = itemProprieties.SpriteID;

                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);

                Vec2f spriteSize = slotSize * 0.8f;
                float posX = slotX + (tileSize.X - spriteSize.X) / 2.0f;
                float posY = slotY + (tileSize.Y - spriteSize.Y) / 2.0f;
                GameState.Renderer.DrawSpriteGui(posX, posY, spriteSize.X, spriteSize.Y, sprite);

                // Draw Count if stackable.
                //if (itemEntity.hasItemStack)
                //{
                //    GameState.Renderer.DrawStringGui(
                //        slotX + (tileSize.X - slotSize.X) / 2.0f,
                //        slotY + (tileSize.Y - slotSize.Y) / 2.0f,
                //        slotSize.X,
                //        slotSize.Y,
                //        itemEntity.itemStack.Count.ToString(),
                //        20,
                //        TextAnchor.LowerRight,
                //        Color.white);
                //}
            }
        }
    }
}
