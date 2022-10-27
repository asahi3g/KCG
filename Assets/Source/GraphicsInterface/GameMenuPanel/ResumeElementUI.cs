//imports UnityEngine

using TMPro;

namespace KGUI
{
    public class ResumeElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private TextMeshProUGUI text;
        
        public override void Init()
        {
            base.Init();

            HitBoxObject = Icon.GameObject;
            
            ID = ElementEnums.ResumeButton;

            Icon = null;
        }

        public override void Draw()
        {
            Icon.Draw();
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

