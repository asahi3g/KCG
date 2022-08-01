
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
        private Image DirtTile;

        private Planet.PlanetState _planet;

        int toolBarID;
        InventoryEntity Inventory;
        int selectedSlot;
        ItemInventoryEntity item;

        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            _planet = planet;

            int width = 16;
            int height = 16;
            DirtTile = new Image("DirtTile", GameObject.Find("Canvas").transform, width, height, "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png");
            DirtTile.SetPosition(new Vector3(-200.0f, -80.2f, 0));
            DirtTile.SetScale(new Vector3(0.6f, 0.6f, 0.6f));
        }

        public override void Update(AgentEntity agentEntity)
        {
            ObjectPosition = new KMath.Vec2f(DirtTile.GetTransform().position.x, DirtTile.GetTransform().position.y);

            toolBarID = agentEntity.agentToolBar.ToolBarID;
            Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            selectedSlot = Inventory.inventorySlots.Selected;

            item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item.itemType.Type == Enums.ItemType.PlacementTool)
            {
                DirtTile.GetGameObject().SetActive(true);
            }
            else
            {
                DirtTile.GetGameObject().SetActive(false);
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

