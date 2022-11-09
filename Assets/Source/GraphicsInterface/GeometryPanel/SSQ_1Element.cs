//import UnityEngine


using Utility;
using UnityEngine.UI;

namespace KGUI
{
	// row 12, column 17
	public class SSQ_1Element : ElementUI, IToggleElement
	{
        [UnityEngine.SerializeField] private ImageWrapper border;
	    
		public override void Init()
		{
			base.Init();
	        
			ID = ElementEnums.SSQ_1;
            
			icon.Init(GameState.GUIManager.WhiteSquareBorder);
			border.Init(GameState.GUIManager.WhiteSquareBorder);
		}

		public override void Draw() 
		{ 
			icon.Draw();
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
			item.itemTile.TileID = Enums.PlanetTileMap.TileID.QP_R1_Metal;
			Toggle(true);
		}
        
		public void Toggle(bool value)
		{
			border.SetImageColor(value ? UnityEngine.Color.red : UnityEngine.Color.yellow);
		}
    }
}