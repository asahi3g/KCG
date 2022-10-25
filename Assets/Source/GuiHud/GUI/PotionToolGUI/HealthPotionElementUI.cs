using Enums;
using KGUI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    public class HealthPotionElementUI : ElementUI
    {
        [SerializeField] private Image borderImage;

        public ImageWrapper Border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;

            ID = ElementEnums.HealthPotion;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png", AtlasType.Gui);

            Border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            Icon.Draw();
            Border.Draw();
        }

        public override void OnMouseStay()
        {
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            item.itemPotion.potionType = PotionType.HealthPotion;
        }
    }
}