//imports UnityEngine

using System;
using Utility;

namespace KGUI
{
    public class GeometryTileElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private ImageWrapper border;
        [UnityEngine.SerializeField] private Enums.TileGeometryAndRotation geometryTileShape;
        [UnityEngine.SerializeField] private Enums.MaterialType geometryTileMaterial;

        private Enums.PlanetTileMap.TileID geometryTileID;
        private bool toggled;
	
        public override void Init()
        {
			// Do all inititalization after base.Init()
            base.Init();
            
            ChangeMaterial(Enums.MaterialType.Metal);
            
			border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public void OnValidate()
        {
	        gameObject.name = geometryTileShape.ToString();
	        Enum.TryParse(geometryTileShape + "_GT", out ID);
        }

        public override void Draw() 
		{ 
			icon.Draw();
			border.Draw();
		}

        public override void OnMouseEntered()
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
	        item.itemTile.TileID = geometryTileID;
	        Toggle(true);
        }
		
		public void Toggle(bool value)
		{
			toggled = value;
            border.UnityImage.color = toggled ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
		
		public void ChangeMaterial(Enums.MaterialType materialType)
		{
			geometryTileMaterial = materialType;
	        
			var tileProperty = GameState.TileCreationApi.GetTileProperty(geometryTileMaterial, geometryTileShape);

			if (tileProperty.TileID != Enums.PlanetTileMap.TileID.Error)
			{
				icon.Init(32, 32, tileProperty.BaseSpriteId);
			}
			else
			{
				icon.Init(GameState.GUIManager.WhiteSquareBorder);
			}

			geometryTileID = tileProperty.TileID;

			if (toggled)
			{
				var item = GameState.GUIManager.SelectedInventoryItem;
				item.itemTile.TileID = geometryTileID;
			}
		}
    }
}