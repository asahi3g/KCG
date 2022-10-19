using Enums;
using Enums.Tile;
using UnityEngine;

namespace KGUI
{
    public class PlacementMaterialToolUI : UIPanel
    {
        [SerializeField] private BedrockElementUI bedrockElementUI;
        [SerializeField] private DirtElementUI dirtElementUI;
        [SerializeField] private PipeElementUI pipeElementUI;
        [SerializeField] private WireElementUI wireElementUI;

        public override void Update()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;

            var planet = GameState.GUIManager.Planet;
            var inventories =
                planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID,
                    InventoryMatcher.InventoryName));
            
            foreach (var inventory in inventories)
            {
                if (inventory.inventoryName.Name != "MaterialBag") continue;

                var materialBag = planet.EntitasContext.inventory
                    .GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryEntity.Slots;

                for (int i = 0; i < materialBag.Length; i++)
                {
                    var materialBagSlot =
                        GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventory.inventoryID.ID, i);
                    if (materialBagSlot == null) continue;

                    switch (materialBagSlot.itemType.Type)
                    {
                        case ItemType.Dirt:
                            dirtElementUI.gameObject.SetActive(true);
                            break;
                        case ItemType.Bedrock:
                            bedrockElementUI.gameObject.SetActive(true);
                            break;
                        case ItemType.Pipe:
                            pipeElementUI.gameObject.SetActive(true);
                            break;
                        case ItemType.Wire:
                            wireElementUI.gameObject.SetActive(true);
                            break;
                    }

                    if (materialBagSlot.hasItemStack)
                    {
                        bedrockElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Bedrock
                            ? Color.red
                            : Color.yellow);
                        dirtElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Moon
                            ? Color.red
                            : Color.yellow);

                        pipeElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Pipe
                            ? Color.red
                            : Color.yellow);

                        wireElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Wire
                            ? Color.red
                            : Color.yellow);
                    }
                }
            }
        }

        public override void Init()
        {
            ID = UIPanelID.PlacementMaterialTool;
            
            UIElementList.Add(bedrockElementUI.ID, bedrockElementUI);
            UIElementList.Add(dirtElementUI.ID, dirtElementUI);
            UIElementList.Add(pipeElementUI.ID, pipeElementUI);
            UIElementList.Add(wireElementUI.ID, wireElementUI);

            base.Init();
        }

        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = TileID.Error;
            }
            
            bedrockElementUI.Border.SetImageColor(Color.yellow);
            dirtElementUI.Border.SetImageColor(Color.yellow);
            pipeElementUI.Border.SetImageColor(Color.yellow);
            wireElementUI.Border.SetImageColor(Color.yellow);
        }
    }
}
