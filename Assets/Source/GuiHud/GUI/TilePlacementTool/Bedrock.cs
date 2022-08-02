using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;
using Enums.Tile;
using PlanetTileMap;

namespace KGUI.Tiles
{
    public class Bedrock : GUIManager
    {
        private Image BedrockTile;
        private Image Background;

        private Planet.PlanetState _planet;

        private int toolBarID;
        private InventoryEntity Inventory;
        private int selectedSlot;
        private ItemInventoryEntity item;

        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            _planet = planet;

            int width = 16;
            int height = 16;

            Background = new Image("BedrockBackground", GameObject.Find("Canvas").transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"));
            Background.SetImageType(UnityEngine.UI.Image.Type.Tiled);
            Background.SetPosition(new Vector3(-280.0f, -80.2f, 0));
            Background.SetScale(new Vector3(0.7f, 0.7f, 0.7f));
            Background.SetImageColor(Color.yellow);

            BedrockTile = new Image("BedrockTile", Background.GetTransform(), width, height, "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png");
            BedrockTile.SetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            BedrockTile.SetScale(new Vector3(0.8f, 0.8f, 0.8f));
        }

        public override void Update(AgentEntity agentEntity)
        {
            ObjectPosition = new KMath.Vec2f(Background.GetTransform().position.x, Background.GetTransform().position.y);

            toolBarID = agentEntity.agentToolBar.ToolBarID;
            Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            selectedSlot = Inventory.inventorySlots.Selected;

            item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item.itemType.Type == Enums.ItemType.PlacementTool)
            {
                Background.GetGameObject().SetActive(true);

                if (item.itemCastData.data.TileID == TileID.Bedrock)
                {
                    Background.SetImageColor(Color.red);
                }
                else
                {
                    Background.SetImageColor(Color.yellow);
                }
            }
            else
            {
                Background.GetGameObject().SetActive(false);
            }
        }

        public override void OnMouseClick(AgentEntity agentEntity)
        {
            toolBarID = agentEntity.agentToolBar.ToolBarID;
            Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            selectedSlot = Inventory.inventorySlots.Selected;

            item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item.itemType.Type == Enums.ItemType.PlacementTool)
            {
                item.itemCastData.data.TileID = TileID.Bedrock;
            }
        }

        public override void OnMouseEnter()
        {
            
        }

        public override void OnMouseStay()
        {
            
        }

        public override void OnMouseExit()
        {
            
        }
    }
}

