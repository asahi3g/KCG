//imports UnityEngine

using System.Linq;

namespace KGUI
{
    public class PlacementMaterialTool : PanelUI
    {
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
            ref var planet = ref GameState.Planet;
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;
            
            var inventories =
                planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
            
            foreach (var inventory in inventories)
            {
                if (inventory.inventoryInventoryEntity.InventoryType != Enums.InventoryEntityType.AgentInventory) continue;

                var materialBag = planet.EntitasContext.inventory
                    .GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryInventoryEntity.Slots;

                for (int i = 0; i < materialBag.Length; i++)
                {
                    var materialBagSlot = GameState.InventoryManager.GetItemInSlot(inventory.inventoryID.ID, i);
                    if (materialBagSlot == null) continue;

                    switch (materialBagSlot.itemType.Type)
                    {
                        case Enums.ItemType.Bedrock:
                            if (ElementList.TryGetValue(ElementEnums.BedrockPT, out var bedrockElementUI))
                            {
                                bedrockElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case Enums.ItemType.Dirt:
                            if (ElementList.TryGetValue(ElementEnums.DirtPT, out var dirtElementUI))
                            {
                                dirtElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case Enums.ItemType.Pipe:
                            if (ElementList.TryGetValue(ElementEnums.PipePT, out var pipeElementUI))
                            {
                                pipeElementUI.gameObject.SetActive(true);
                            }
                            break;
                        case Enums.ItemType.Wire:
                            if (ElementList.TryGetValue(ElementEnums.WirePT, out var wireElementUI))
                            {
                                wireElementUI.gameObject.SetActive(true);
                            }
                            break;
                    }
                }
            }
            
            ToggleFirstElement();
        }

        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = Enums.PlanetTileMap.TileID.Error;
            }

            foreach (var element in ElementList.Values)
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
        
        private void ToggleFirstElement()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;

            foreach (var element in ElementList.Values.Where(element => element.gameObject.activeSelf))
            {
                switch (element.ID)
                {
                    case ElementEnums.Error:
                        selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.Error;
                        break;
                    case ElementEnums.BedrockPT:
                        ((IToggleElement)element).Toggle(true);
                        selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.Bedrock;
                        break;
                    case ElementEnums.DirtPT:
                        ((IToggleElement)element).Toggle(true);
                        selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.Moon;
                        break;
                    case ElementEnums.PipePT:
                        ((IToggleElement)element).Toggle(true);
                        selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.Pipe;
                        break;
                    case ElementEnums.WirePT:
                        ((IToggleElement)element).Toggle(true);
                        selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.Wire;
                        break;
                }
                break;
            }
        }
    }
}
