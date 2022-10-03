using Enums;
using KGUI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    public class WireElementUI : UIElement
    {
        [SerializeField] private Image borderImage;

        public ImageWrapper Border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;
            
            ID = UIElementID.WireElement;

            Icon = new ImageWrapper(iconImage, 128, 128, "Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png", AtlasType.Gui);

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
                        item.itemTile.TileID = Enums.Tile.TileID.Wire;
                        break;
                }
            }
        }
    }
}
