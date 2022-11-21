using System.Linq;

namespace KGUI.MechTool
{
    public class MechToolPanel : PanelUI
    {
        public override void Init()
        {
            ID = PanelEnums.MechTool;

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
            
            ToggleFirstElement();
        }
        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemMechPlacement.MechID = Enums.MechType.Error;
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
                selectedInventoryItem.itemMechPlacement.MechID = Enums.MechType.Storage;
            }
        }
    }
}