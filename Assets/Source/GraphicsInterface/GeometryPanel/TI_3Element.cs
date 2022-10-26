using UnityEngine;
using Utility;
using UnityEngine.UI;

namespace KGUI
{
	// row 3, column 7
	public class TI_3Element : ElementUI, IToggleElement
	{
		[SerializeField] private Image borderImage;

		private ImageWrapper border;
	    
		public override void Init()
		{
			base.Init();
	        
			ID = ElementEnums.TI_3;
            
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
			item.itemTile.TileID = Enums.Tile.TileID.TB_R5_Metal;
			Toggle(true);
		}
        
		public void Toggle(bool value)
		{
			border.SetImageColor(value ? Color.red : Color.yellow);
		}
    }
}