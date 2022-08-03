using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;
using Enums.Tile;
using PlanetTileMap;

namespace KGUI.Tiles
{
    public class Dirt : GUIManager
    {
        // Image's
        private Image DirtTile;
        private Image Background;

        // Planet
        private Planet.PlanetState _planet;

        // Item Component Elements
        private int toolBarID;
        private InventoryEntity Inventory;
        private int selectedSlot;
        private ItemInventoryEntity item;

        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Planet Assign
            _planet = planet;

            // Create Background
            Background = new Image("DirtBackground", GameObject.Find("Canvas").transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"));
            Background.SetImageType(UnityEngine.UI.Image.Type.Tiled);
            Background.SetPosition(new Vector3(-200.0f, -80.2f, 0));
            Background.SetScale(new Vector3(0.7f, 0.7f, 0.7f));
            Background.SetImageColor(Color.yellow);

            // Create Tile
            int width = 16;
            int height = 16;
            DirtTile = new Image("DirtTile", Background.GetTransform(), width, height, "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png");
            DirtTile.SetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            DirtTile.SetScale(new Vector3(0.8f, 0.8f, 0.8f));
        }

        public override void Update(AgentEntity agentEntity)
        {
            // Set Object Position
            ObjectPosition = new KMath.Vec2f(Background.GetTransform().position.x, Background.GetTransform().position.y);

            // Set Inventory Element
            toolBarID = agentEntity.agentToolBar.ToolBarID;
            Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            selectedSlot = Inventory.inventorySlots.Selected;

            // Create Item
            item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item.itemType.Type == Enums.ItemType.PlacementTool)
            {
                // Activate Background
                Background.GetGameObject().SetActive(true);
                if(item.itemCastData.data.TileID == TileID.Moon)
                {
                    // Set Red After Selected
                    Background.SetImageColor(Color.red);
                }
                else
                {
                    // Set Yellow After Unselected
                    Background.SetImageColor(Color.yellow);
                }
            }
            else
            {
                // Deactivate Background
                Background.GetGameObject().SetActive(false);
            }
        }

        public override void OnMouseClick(AgentEntity agentEntity)
        {
            // Set Inventory Elements
            toolBarID = agentEntity.agentToolBar.ToolBarID;
            Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            selectedSlot = Inventory.inventorySlots.Selected;

            // Create Item
            item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item.itemType.Type == Enums.ItemType.PlacementTool)
            {
                // Set Data Tile ID to Pipe
                item.itemCastData.data.TileID = TileID.Moon;
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

