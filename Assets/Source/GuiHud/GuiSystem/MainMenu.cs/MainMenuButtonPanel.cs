using KMath;


namespace Gui
{


    public class MainMenuButtonPanel : ButtonPanel
    {
        public enum ButtonsEnum 
        {
            Button_Play,
            Button_KeyBindings,
            Button_Video,
            Button_Audio,
            Button_Exit,
            Button_Count
        }

        public static readonly Vec2f ButtonPadding = new Vec2f(4.0f, 4.0f);
        public static readonly Vec2f ButtonDimensions = new Vec2f(350.0f, 100.0f);

        public MainMenuButtonPanel(Vec2f offsetFromParent) : 
        base(offsetFromParent, new Vec2f(ButtonDimensions.X + ButtonPadding.X * 2, ButtonDimensions.Y * 5 + ButtonPadding.Y * 6 + 300.0f))
        {
            Buttons = new MenuButton[(int)ButtonsEnum.Button_Count];
            Buttons[(int)ButtonsEnum.Button_Play] = new MainMenuPlayButton(new Vec2f(), new Vec2f(), "Play");
            Buttons[(int)ButtonsEnum.Button_KeyBindings] = new MainMenuKeyBindingsButton(new Vec2f(), new Vec2f(), "Key Bindings");
            Buttons[(int)ButtonsEnum.Button_Video] = new MainMenuVideoButton(new Vec2f(), new Vec2f(), "Video");
            Buttons[(int)ButtonsEnum.Button_Audio] = new MainMenuAudioButton(new Vec2f(), new Vec2f(), "Audio");
            Buttons[(int)ButtonsEnum.Button_Exit] = new MainMenuExitButton(new Vec2f(), new Vec2f(), "Exit");
            

            for(int i = 0; i < Buttons.Length; i++)
            {
                AddChild(Buttons[i]);
            }


            UpdateChildrenPositionsAndSize();
        }

        public override void Draw(GuiElement parent) 
        {
            GameState.Renderer.DrawQuadColorGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y,
                     new UnityEngine.Color(0.1f, 0.1f, 0.1f, 0.8f));

           /* GameState.Renderer.DrawStringGui(DrawPosition.X + 10.0f, DrawPosition.Y + DrawDimensions.Y - 200.0f, DrawDimensions.X - 20.0f, 150.0f, 
            "KCG", GameState.GuiResourceManager.RodinFont, 100, UnityEngine.TextAnchor.MiddleCenter, UnityEngine.Color.white);*/

            base.Draw(parent);
        }

        public void UpdateChildrenPositionsAndSize()
        {
            float currentY = Dimensions.Y * 0.7f - ButtonPadding.Y - ButtonDimensions.Y;

            foreach(var button in Buttons)
            {
                button.OffsetFromParent = new Vec2f(4.0f, currentY);
                button.Dimensions = ButtonDimensions;

                currentY -= ButtonDimensions.Y + ButtonPadding.Y;
            } 
        }


        public override void PostPositionAndScaleUpdate(GuiElement parent) 
        {
            UpdateChildrenPositionsAndSize();
        }
    }
}