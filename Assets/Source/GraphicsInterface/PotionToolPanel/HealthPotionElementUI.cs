//imports UnityEngine
using Utility;

namespace KGUI
{
    public class HealthPotionElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private ImageWrapper border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = border.UnityImage.gameObject;

            ID = ElementEnums.HealthPotionPT;
            
            icon.Init(19, 19,"Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png", Enums.AtlasType.Gui);
            border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            icon.Draw();
            border.Draw();
        }

        public override void OnMouseStay()
        {
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            item.itemPotion.potionType = Enums.PotionType.HealthPotion;
            Toggle(true);
        }
        
        public void Toggle(bool value)
        {
            border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
    }
}