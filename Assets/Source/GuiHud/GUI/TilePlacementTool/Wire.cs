using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;
using Enums.Tile;
using PlanetTileMap;

namespace KGUI.Tiles
{
    public class Wire : GUIManager
    {
        private Image WireTile;
        private Image Background;

        private Planet.PlanetState _planet;

        private int toolBarID;
        private InventoryEntity Inventory;
        private int selectedSlot;
        private ItemInventoryEntity item;

        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            _planet = planet;

            Background = new Image("WireBackground", GameObject.Find("Canvas").transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"));
            Background.SetImageType(UnityEngine.UI.Image.Type.Tiled);
            Background.SetPosition(new Vector3(-120.0f, -80.2f, 0));
            Background.SetScale(new Vector3(0.7f, 0.7f, 0.7f));
            Background.SetImageColor(Color.yellow);

            int width = 128;
            int height = 128;
            WireTile = new Image("WireTile", Background.GetTransform(), width, height, "Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png");
            WireTile.SetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            WireTile.SetScale(new Vector3(0.8f, 0.8f, 0.8f));
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
                if (item.itemCastData.data.TileID == TileID.Wire)
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
                item.itemCastData.data.TileID = TileID.Wire;
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

