//imports UnityEngine

using Enums;
using KGUI.Elements;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    public class HealthPotionElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private Image borderImage;

        private ImageWrapper border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = borderImage.gameObject;

            ID = ElementEnums.HealthPotion;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png", AtlasType.Gui);

            border = new ImageWrapper(borderImage, GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            Icon.Draw();
            border.Draw();
        }

        public override void OnMouseStay()
        {
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            item.itemPotion.potionType = PotionType.HealthPotion;
            Toggle(true);
        }
        
        public void Toggle(bool value)
        {
            border.SetImageColor(value ? UnityEngine.Color.red : UnityEngine.Color.yellow);
        }
    }
}