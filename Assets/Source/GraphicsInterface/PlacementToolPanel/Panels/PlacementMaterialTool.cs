//imports UnityEngine

using System.Linq;
using Enums;
using Enums.Tile;

namespace KGUI
{
    public class PlacementMaterialTool : PanelUI
    {
        [UnityEngine.SerializeField] private BedrockElementUI bedrockElementUI;
        [UnityEngine.SerializeField] private DirtElementUI dirtElementUI;
        [UnityEngine.SerializeField] private PipeElementUI pipeElementUI;
        [UnityEngine.SerializeField] private WireElementUI wireElementUI;

        public override void Init()
        {
            ID = PanelEnums.PlacementMaterialTool;
            
            base.Init();
        }
        
        public override void HandleClickEvent(ElementEnums elementID)
        {
            foreach (var element in ElementList.Values.Where(element => element.ID != elementID))
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
        
        public override void OnActivate()
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
                            if (ElementList.TryGetValue(ElementEnums.Dirt, out var dirtElementUI))
                            {
                                dirtElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case ItemType.Bedrock:
                            if (ElementList.TryGetValue(ElementEnums.Bedrock, out var bedrockElementUI))
                            {
                                bedrockElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case ItemType.Pipe:
                            if (ElementList.TryGetValue(ElementEnums.Pipe, out var pipeElementUI))
                            {
                                pipeElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case ItemType.Wire:
                            if (ElementList.TryGetValue(ElementEnums.Wire, out var wireElementUI))
                            {
                                wireElementUI.gameObject.SetActive(true);
                            }
                            break;
                    }

                    //if (materialBagSlot.hasItemStack)
                    //{
                    //    bedrockElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Bedrock
                    //        ? UnityEngine.Color.red
                    //        : UnityEngine.Color.yellow);
                    //    dirtElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Moon
                    //        ? UnityEngine.Color.red
                    //        : UnityEngine.Color.yellow);

                    //    pipeElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Pipe
                    //        ? UnityEngine.Color.red
                    //        : UnityEngine.Color.yellow);

                    //    wireElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Wire
                    //        ? UnityEngine.Color.red
                    //        : UnityEngine.Color.yellow);
                    //}
                }
            }
        }

        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = TileID.Error;
            }

            foreach (var element in ElementList.Values)
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
    }
}
