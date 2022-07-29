using UnityEngine;
using System.Collections.Generic;
using KMath;
using Entitas;
using Item;

namespace Mech
{

    public class MechGUIDrawSystem
    {
        public void Draw(Contexts contexts, Transform transform)
        {
            /*var openInventories = contexts.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryDrawable, InventoryMatcher.InventoryID));
            // If empty Draw ToolBar.

            foreach (InventoryEntity inventoryEntity in openInventories)
            {
                DrawInventory(contexts, transform, inventoryEntity);
            }*/
            // Get Initial Positon.

            Vec2f tileSize = new Vec2f(1f / 16f, 1f / 16f * Screen.width / Screen.height);
            Vec2f slotSize = tileSize * 0.9f;

            // Get Inventory Info.
            int width = 1;
            int height = 1;

            float h = height * tileSize.Y;
            float w = width * tileSize.X;
            float x = 0.5f;
            float y = 0.3f;

            x -= w / 2f;
            y -= h / 2f;

            // var mechs = contexts.mech.GetGroup(MechMatcher.MechSprite2D);

            Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(0, Enums.AtlasType.Mech);



            DrawBackGround(x, y, w, h);

            GameState.Renderer.DrawSpriteGui(x, y, w * 0.8F, h * 0.8F, sprite);
        }

        private void DrawBackGround(float x, float y, float w, float h)
        {
            Color backGround = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            GameState.Renderer.DrawQuadColorGui(x, y, w, h, backGround);
        }


        void DrawIcon(Contexts entitasContext, float x, float y, int width, int height, Vec2f tileSize, Vec2f slotSize, Transform transform, HashSet<ItemInventoryEntity> itemInInventory)
        {
            foreach (ItemInventoryEntity itemEntity in itemInInventory)
            {
                int slotNumber = itemEntity.itemInventory.SlotNumber;
                int i = slotNumber % width;
                int j = (height - 1) - (slotNumber - i) / width;

                // Calculate Slot Border positon.
                float slotX = x + i * tileSize.X;
                float slotY = y + j * tileSize.Y;

                // Draw Count if stackable.
                if (itemEntity.hasItemStack)
                {
                    //int fontSize = 50;

                    // these Change with Camera size. Find better soluiton. AutoSize? MeshPro?
                    float characterSize = 0.05f * Camera.main.pixelWidth / 1024.0f;
                    float posOffset = 0.04f;

                    var rect = new Rect(600 * Random.value, 450 * Random.value, 200, 150);

                    // Todo: Implement or import librart to draw string on screen immediately. 
                    //GameState.Renderer.DrawString(slotX + posOffset, slotY + posOffset, characterSize, itemEntity.itemStack.Count.ToString(), fontSize, Color.white, transform, drawOrder + 4);
                }

                // Draw sprites.
                Item.ItemProprieties itemProprieties = GameState.ItemCreationApi.Get(itemEntity.itemType.Type);
                int SpriteID = itemProprieties.SpriteID;

                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle);

                Vec2f spriteSize = slotSize * 0.8f;
                slotX = slotX + (tileSize.X - spriteSize.X) / 2.0f;
                slotY = slotY + (tileSize.Y - spriteSize.Y) / 2.0f;
                GameState.Renderer.DrawSpriteGui(slotX, slotY, spriteSize.X, spriteSize.Y, sprite);
            }
        }
    }

}