//imports UnityEngine

using TMPro;

namespace KGUI
{
    public class ExitElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private TextMeshProUGUI text;
        
        public override void Init()
        {
            base.Init();

            HitBoxObject = icon.GameObject;
            
            ID = ElementEnums.ExitButtonGM;

            icon = null;
        }

        public override void Draw()
        {
            icon.Draw();
        }

        public override void OnMouseStay()
        {
        }

        public override void OnMouseExited()
        {
        }

        public override void OnMouseClick()
        {
        }
    }
}

