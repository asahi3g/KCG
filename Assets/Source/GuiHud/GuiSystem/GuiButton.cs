using KMath;

namespace Gui
{

    public class GuiButton : GuiElement
    {
        
        string Text;

        public bool IsActive;
        public Enums.ButtonState State;

        public GuiButton(Vec2f offsetFromParent, Vec2f dimensions, string text) : base(offsetFromParent, dimensions)
        {
            Text = text;
            IsActive = true;
            State = Enums.ButtonState.Active;
        }


        public override void OnMouseEntered() 
        {
            base.OnMouseEntered();
            if (IsActive)
            {
                State = Enums.ButtonState.Hover;
            }
            else
            {
                State = Enums.ButtonState.Inactive;
            }
        }

        public override void OnMouseExit() 
        {
            base.OnMouseExit();
            if (IsActive)
            {
                State = Enums.ButtonState.Active;
            }
            else
            {
                State = Enums.ButtonState.Inactive;
            }
        }

        public override void OnMouseHeld()
        {
            base.OnMouseHeld();
            if (IsActive)
            {
                State = Enums.ButtonState.Pressed;
            }
            else
            {
                State = Enums.ButtonState.Inactive;
            }
        }


        public override void Draw(GuiElement parent) 
        {

            switch(State)
            {
                case Enums.ButtonState.Active:
                {
                    //Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(GameState.GuiResourceManager.ActiveButtonSprite, Enums.AtlasType.Gui);
                    //GameState.Renderer.DrawSpriteGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, sprite);
                //    GameState.Renderer.DrawQuadColorGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y,
                //     UnityEngine.Color.gray);
                    break;
                }
                case Enums.ButtonState.Inactive:
                {
                    
                    break;
                }
                case Enums.ButtonState.Pressed:
                {
                    //Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(GameState.GuiResourceManager.PressedButtonSprite, Enums.AtlasType.Gui);
                    //GameState.Renderer.DrawSpriteGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, sprite);
                //    GameState.Renderer.DrawQuadColorGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y,
                 //    UnityEngine.Color.white);
                    break;
                }
                case Enums.ButtonState.Hover:
                {
                  //  GameState.Renderer.DrawQuadColorGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y,
                  //   UnityEngine.Color.green);

                    //Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(GameState.GuiResourceManager.HoverButtonSprite, Enums.AtlasType.Gui);
                    //GameState.Renderer.DrawSpriteGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, sprite);
                    break;
                }
            }
            
            GameState.Renderer.DrawStringGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, Text,
                     GameState.GuiResourceManager.RodinFont, 36, UnityEngine.TextAnchor.MiddleCenter, new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1.0f));


            base.Draw(parent);
        }
    }
}