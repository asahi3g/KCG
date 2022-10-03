using Enums;
using KGUI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    public class BedrockElementUI : UIElement
    {
        [SerializeField] private Image borderImage;

        public ImageWrapper Border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;
            
            ID = UIElementID.BedrockElement;

            Icon = new ImageWrapper(iconImage, 16, 16,
                "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png", AtlasType.Gui);

            Border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            Icon.Draw();
            Border.Draw();
        }

        public override void OnMouseStay()
        {
            GameState.GUIManager.InventorySlotItem.itemTile.InputsActive = !gameObject.activeSelf;
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.InventorySlotItem;
            if(item != null)
            {
                switch (item.itemType.Type)
                {
                    case ItemType.PlacementTool:
                    case ItemType.PlacementMaterialTool:
                        item.itemTile.TileID = Enums.Tile.TileID.Bedrock;
                        break;
                }
            }
        }
    }
}
