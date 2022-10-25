using UnityEngine;

namespace KGUI
{
    public class TestElement : ElementUI
    {
        public override void Init()
        {
            base.Init();

            ID = ElementEnums.Test;
        }

        public override void Draw() { }

        public override void OnMouseStay()
        {
            Debug.Log("On Mouse stay");
        }

        public override void OnMouseExited()
        {
            Debug.Log("On Mouse Exited");
        }

        public override void OnMouseClick()
        {
            Debug.Log("On Mouse Click");
        }
    }
}

