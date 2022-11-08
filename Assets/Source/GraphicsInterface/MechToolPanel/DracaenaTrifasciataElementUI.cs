//imports UnityEngine

using Utility;

namespace KGUI.MechTool
{
    public class DracaenaTrifasciataElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Image borderImage;

        private ImageWrapper border;
	
        public override void Init()
        {
			// Do all inititalization after base.Init()
            base.Init();
			
            ID = ElementEnums.DracaenaTrifasciataMT;
			
            Icon = new ImageWrapper(iconImage, 16, 16,
	            "Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", Enums.AtlasType.Gui);
			border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw() 
		{ 
			Icon.Draw();
			border.Draw();
		}

        public override void OnMouseStay()
        {
	        GameState.GUIManager.SelectedInventoryItem.itemMech.InputsActive = !gameObject.activeSelf;
        }

        public override void OnMouseExited()
        {
	        GameState.GUIManager.SelectedInventoryItem.itemMech.InputsActive = true;
        }

        public override void OnMouseClick()
        {
	        var item = GameState.GUIManager.SelectedInventoryItem;
	        if(item != null)
	        {
		        item.itemMech.MechID = Enums.MechType.DracaenaTrifasciata;
		        Toggle(true);
	        }
        }
		
		public void Toggle(bool value)
        {
            border.SetImageColor(value ? UnityEngine.Color.red : UnityEngine.Color.yellow);
        }
    }
}