using System.Linq;
using Enums.PlanetTileMap;

namespace KGUI
{
    public class GeometryToolPanel : PanelUI
    {
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