using KMath;


namespace Gui
{


    public class MainMenuButtonPanel : GuiElement
    {
        public static readonly Vec2f ButtonPadding = new Vec2f(4.0f, 4.0f);
        public static readonly Vec2f ButtonDimensions = new Vec2f(300.0f, 100.0f);


        public MainMenuPlayButton PlayButton;
        public MainMenuKeyBindingsButton KeyBindingsButton;
        public MainMenuVideoButton VideoButton;
        public MainMenuAudioButton AudioButton;
        public MainMenuExitButton ExitButton;


        public MainMenuButtonPanel(Vec2f offsetFromParent) : 
        base(offsetFromParent, new Vec2f(ButtonDimensions.X + ButtonPadding.X * 2, ButtonDimensions.Y * 5 + ButtonPadding.Y * 6 + 300.0f))
        {
            PlayButton = new MainMenuPlayButton(new Vec2f(), new Vec2f(), "Play");
            AddChild(PlayButton);

            KeyBindingsButton = new MainMenuKeyBindingsButton(new Vec2f(), new Vec2f(), "Key Bindings");
            AddChild(KeyBindingsButton);

            VideoButton = new MainMenuVideoButton(new Vec2f(), new Vec2f(), "Video");
            AddChild(VideoButton);

            AudioButton = new MainMenuAudioButton(new Vec2f(), new Vec2f(), "Audio");
            AddChild(AudioButton);

            ExitButton = new MainMenuExitButton(new Vec2f(), new Vec2f(), "Exit");
            AddChild(ExitButton);


            UpdateChildrenPositionsAndSize();
        }

        public override void Draw(GuiElement parent) 
        {
            GameState.Renderer.DrawQuadColorGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y,
                     new UnityEngine.Color(0.1f, 0.1f, 0.1f, 0.8f));

            GameState.Renderer.DrawStringGui(DrawPosition.X + 10.0f, DrawPosition.Y + DrawDimensions.Y - 200.0f, DrawDimensions.X - 20.0f, 150.0f, 
            "KCG", GameState.GuiResourceManager.RodinFont, 100, UnityEngine.TextAnchor.MiddleCenter, UnityEngine.Color.white);

            base.Draw(parent);
        }

        public void UpdateChildrenPositionsAndSize()
        {
            float currentY = Dimensions.Y * 0.7f - ButtonPadding.Y - ButtonDimensions.Y;

            PlayButton.OffsetFromParent = new Vec2f(4.0f, currentY);
            PlayButton.Dimensions = ButtonDimensions;

            currentY -= ButtonDimensions.Y + ButtonPadding.Y;

            KeyBindingsButton.OffsetFromParent = new Vec2f(4.0f, currentY);
            KeyBindingsButton.Dimensions = ButtonDimensions;

            currentY -= ButtonDimensions.Y + ButtonPadding.Y;

            VideoButton.OffsetFromParent = new Vec2f(4.0f, currentY);
            VideoButton.Dimensions = ButtonDimensions;

            currentY -= ButtonDimensions.Y + ButtonPadding.Y;

            AudioButton.OffsetFromParent = new Vec2f(4.0f, currentY);
            AudioButton.Dimensions = ButtonDimensions;

            currentY -= ButtonDimensions.Y + ButtonPadding.Y;

            ExitButton.OffsetFromParent = new Vec2f(4.0f, currentY);
            ExitButton.Dimensions = ButtonDimensions;

            currentY -= ButtonDimensions.Y + ButtonPadding.Y;

            
        }


        public override void PostPositionAndScaleUpdate(GuiElement parent) 
        {
            UpdateChildrenPositionsAndSize();
        }
    }
}