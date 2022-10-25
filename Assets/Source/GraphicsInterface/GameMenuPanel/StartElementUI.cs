using TMPro;
using UnityEngine;

namespace KGUI
{
    public class StartElementUI : ElementUI
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public override void Init()
        {
            base.Init();

            HitBoxObject = Icon.GameObject;
            
            ID = ElementEnums.StartButton;

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
