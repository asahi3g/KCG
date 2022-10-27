//imports UnityEngine

using Utility;

namespace KGUI
{
    public class DirtElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Image borderImage;

        private ImageWrapper border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;
            
            ID = ElementEnums.Dirt;

            Icon = new ImageWrapper(iconImage, 16, 16, "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png", Enums.AtlasType.Gui);
            border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            Icon.Draw();
            border.Draw();
        }

        public override void OnMouseStay()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = !gameObject.activeSelf;
        }
        
        public override void OnMouseExited()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = true;
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if (item != null)
            {
                item.itemTile.TileID = Enums.PlanetTileMap.TileID.Moon;
                Toggle(true);
            }
        }

        public void Toggle(bool value)
        {
            border.SetImageColor(value ? UnityEngine.Color.red : UnityEngine.Color.yellow);
        }
    }
}
