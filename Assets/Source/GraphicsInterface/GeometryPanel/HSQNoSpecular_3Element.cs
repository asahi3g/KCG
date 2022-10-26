//imports UnityEngine

using Utility;
using UnityEngine.UI;

namespace KGUI
{
	// row 7, column 3
	public class HSQNoSpecular_3Element : ElementUI, IToggleElement
	{
		[UnityEngine.SerializeField] private Image borderImage;

		private ImageWrapper border;
	    
		public override void Init()
		{
			base.Init();
	        
			ID = ElementEnums.HSQNoSpecular_3;
            
			Icon = new ImageWrapper(iconImage, GameState.GUIManager.WhiteSquareBorder);
			border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
		}

		public override void Draw() 
		{ 
			Icon.Draw();
			border.Draw();
		}

		public override void OnMouseStay()
		{
			GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = false;
		}
        
		public override void OnMouseExited()
		{
			GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = true;
		}

		public override void OnMouseClick()
		{
			var item = GameState.GUIManager.SelectedInventoryItem;
			item.itemTile.TileID = Enums.PlanetTileMap.TileID.HB_R3_Metal;
			Toggle(true);
		}
        
		public void Toggle(bool value)
		{
			border.SetImageColor(value ? UnityEngine.Color.red : UnityEngine.Color.yellow);
		}
    }
}