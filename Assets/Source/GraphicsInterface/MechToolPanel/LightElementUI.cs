//imports UnityEngine

using Utility;

namespace KGUI.MechTool
{
    public class LightElementUI : ElementUI, IToggleElement
    {
	    [UnityEngine.SerializeField]private ImageWrapper border;
	
        public override void Init()
        {
			// Do all inititalization after base.Init()
            base.Init();
            
            ID = ElementEnums.LightMT;

            icon.Init(48, 16,"Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png", Enums.AtlasType.Gui);
			border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw() 
		{ 
			icon.Draw();
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
		        item.itemMech.MechID = Enums.MechType.Light;
		        Toggle(true);
	        }
        }
		
		public void Toggle(bool value)
        {
            border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
    }
}