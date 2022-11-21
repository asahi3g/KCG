//imports UnityEngine

using Utility;

namespace KGUI.MechTool
{
    public class PlanterElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private ImageWrapper border;
	
        public override void Init()
        {
			// Do all inititalization after base.Init()
            base.Init();
            
            ID = ElementEnums.PlanterMT;

            icon.Init(32, 16,"Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", Enums.AtlasType.Gui);
			border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw() 
		{ 
			icon.Draw();
			border.Draw();
		}

        public override void OnMouseStay()
        {
	        GameState.GUIManager.SelectedInventoryItem.itemMechPlacement.InputsActive = !gameObject.activeSelf;
        }

        public override void OnMouseExited()
        {
	        GameState.GUIManager.SelectedInventoryItem.itemMechPlacement.InputsActive = true;
        }

        public override void OnMouseClick()
        {
	        var item = GameState.GUIManager.SelectedInventoryItem;
	        if(item != null)
	        {
		        item.itemMechPlacement.MechID = Enums.MechType.Planter;
		        Toggle(true);
	        }
        }
		
		public void Toggle(bool value)
        {
            border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
    }
}