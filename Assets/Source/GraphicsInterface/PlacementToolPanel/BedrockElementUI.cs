//imports UnityEngine

using Enums;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    public class BedrockElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private Image borderImage;

        private ImageWrapper border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;
            
            ID = ElementEnums.Bedrock;

            Icon = new ImageWrapper(iconImage, 16, 16,
                "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png", AtlasType.Gui);

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
            if(item != null)
            {
                item.itemTile.TileID = Enums.Tile.TileID.Bedrock;
                Toggle(true);
            }
        }
        
        public void Toggle(bool value)
        {
            border.SetImageColor(value ? Color.red : Color.yellow);
        }
    }
}
