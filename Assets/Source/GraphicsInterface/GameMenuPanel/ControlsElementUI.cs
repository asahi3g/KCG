//imports UnityEngine

using TMPro;

namespace KGUI
{
    public class ControlsElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private TextMeshProUGUI text;
        
        public override void Init()
        {
            base.Init();

            HitBoxObject = Icon.GameObject;
            
            ID = ElementEnums.ControlsButtonGM;

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

