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
            var players = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
                DrawInventory(contexts, inventory, inventory.ToolBarWindow, 1);
            }

            for (int i = 0; i < GameState.InventoryCreationApi.GetArrayLength(); i++)
            {
                ref InventoryModel inventory = ref GameState.InventoryCreationApi.Get(i);
                if (!inventory.IsDrawOn())
                    continue;
                DrawInventory(contexts, inventory, inventory.MainWindow, inventory.Height);
            }
        }

        private void DrawInventory(Contexts entitasContext, InventoryModel inventory, Window window, int height)
        {
            // Todo: Change font size with the size of the inventory.
            // Todo: Deals with scaling proprerly.

            // Calculate Positions and Tile Sizes relative to sceen.

            // Get Inventory Info.
            int width = inventory.Width;

            float scaleFactor = Screen.height / DEFAULT_SCREEN_HIGHT;
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
            
                float tilePosX = window.Position.X + (i % width) * window.TileSize;
                float tilePosY = window.Position.Y + ((length - 1 - i)  / width) * window.TileSize;
            
                DrawBorder(tilePosX, tilePosY, window.TileSize, window.SlotBorderOffset, i, ref inventory);
           
                DrawSlot(entitasContext, tilePosX, tilePosY, window.TileSize, window.SlotOffset, 
                    scaleFactor, ref inventory.RenderProprieties, ref inventory.Slots[i]);

                // Draw tool bar numbers.
                if (i < width)
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
            int i, ref InventoryModel inventory)
        {
            float sizeX = tileSize - 2 * borderOffset;
            float sizeY = tileSize - 2 * borderOffset;
            float posX = tilePosX + borderOffset;
            float posY = tilePosY + borderOffset;

            if (inventory.RenderProprieties.HasBorder() && (inventory.SelectedSlotID != i))
            {
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventory.RenderProprieties.SlotColor);
            }
            if (inventory.SelectedSlotID == i)
            {
                GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, inventory.RenderProprieties.SelectedColor);
            }
        }

        void DrawSlot(Contexts contexts, float tilePosX, float tilePosY, float tileSize, float slotOffset,
            float scaleFactor, ref RenderProprieties renderProprieties, ref Slot slot)
        {
            float sizeX = tileSize - 2 * slotOffset;
            float sizeY = tileSize - 2 * slotOffset;
            float posX = tilePosX + slotOffset;
            float posY = tilePosY + slotOffset;

            GameState.Renderer.DrawQuadColorGui(posX, posY, sizeX, sizeY, renderProprieties.SlotColor);

            if (slot.SlotBackgroundIcon >= 0)
            {
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(slot.SlotBackgroundIcon, Enums.AtlasType.Particle);
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
