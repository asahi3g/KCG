//import UnityEngine

using Utility;


namespace KGUI
{
	// row 19, column 19
	public class HSQ_0Element : ElementUI, IToggleElement
	{
        [UnityEngine.SerializeField] private ImageWrapper border;
	    
		public override void Init()
		{
			base.Init();
	        
			ID = ElementEnums.HSQ_0;
            
            var tileProperty = GameState.TileCreationApi.GetTileProperty(Enums.PlanetTileMap.TileID.HP_R0_Metal);
            
            icon.Init(32, 32, tileProperty.BaseSpriteId);
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
			item.itemTile.TileID = Enums.PlanetTileMap.TileID.HP_R0_Metal;
			Toggle(true);
		}
        
		public void Toggle(bool value)
		{
			border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
		}
    }
}