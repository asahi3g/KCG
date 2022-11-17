using KMath;

namespace Gui
{



    public class MainMenuKeyBindingsButton : GuiButton
    {


        public MainMenuKeyBindingsButton(Vec2f offsetFromParent, Vec2f dimensions, string text) : base(offsetFromParent, dimensions, text)
        {
        }

        public override void OnClicked()
        {
            UnityEngine.Debug.Log("key");
        }
    }
}