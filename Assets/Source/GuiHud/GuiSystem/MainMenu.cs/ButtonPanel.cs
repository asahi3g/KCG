using KMath;


namespace Gui
{


    public class ButtonPanel : GuiElement
    {
        public MenuButton[] Buttons;


        public ButtonPanel(Vec2f offsetFromParent, Vec2f dimensions) : 
        base(offsetFromParent, dimensions)
        {
        }

        public void SetButtonSelected(GuiButton button)
        {
            for(int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].IsSelected = Buttons[i] == button;
            }
        }

    }
}