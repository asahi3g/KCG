//imports UnityEngine

using System;
using Utility;

namespace KGUI
{
    public class GeometryTileElement : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private ImageWrapper border;
        [UnityEngine.SerializeField] private Enums.TileGeometryAndRotation geometryTileShape;
        [UnityEngine.SerializeField] private PlanetTileMap.MaterialType geometryTileMaterial;

        private Enums.PlanetTileMap.TileID geometryTileID;
	
        public override void Init()
        {
			// Do all inititalization after base.Init()
            base.Init();
            
            ChangeMaterial(PlanetTileMap.MaterialType.Metal);
            
			border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public void OnValidate()
        {
	        gameObject.name = geometryTileShape.ToString();
	        Enum.TryParse(geometryTileShape + "_GT", out ID);
        }

        public void ChangeMaterial(PlanetTileMap.MaterialType materialType)
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
	        item.itemTile.TileID = geometryTileID;
	        Toggle(true);
        }
		
		public void Toggle(bool value)
        {
            border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
    }
}