using System.Linq;
using Enums.PlanetTileMap;

namespace KGUI
{
    public class GeometryToolPanel : PanelUI
    {
        [UnityEngine.SerializeField] private PlanetTileMap.MaterialType geometryTileMaterial;
        
        public override void Init()
        {
            ID = PanelEnums.GeometryTool;
            
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
            if(selectedInventoryItem == null) return;

            foreach (var element in ElementList.Values)
            {
                element.gameObject.SetActive(true);
                ((GeometryTileElement)element).ChangeMaterial(geometryTileMaterial);
            }
            
            ToggleFirstElement();
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
        
        private void ToggleFirstElement()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;
            
            if (ElementList.First().Value is IToggleElement firstElement)
            {
                firstElement.Toggle(true);
                selectedInventoryItem.itemTile.TileID = TileID.SB_R0_Metal;
            }
        }
    }
}