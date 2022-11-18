using KMath;

namespace Gui
{



    public class MainMenuPlayButton : MenuButton
    {


        public MainMenuPlayButton(Vec2f offsetFromParent, Vec2f dimensions, string text) : base(offsetFromParent, dimensions, text)
        {
        }

        public override void OnClicked()
        {
            UnityEngine.Debug.Log("play");
        }
    }
}