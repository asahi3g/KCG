using KMath;

namespace Gui
{

    public class MenuButton : GuiButton
    {

        public bool IsSelected = false;
        
        public MenuButton(Vec2f offsetFromParent, Vec2f dimensions, string text) : base(offsetFromParent, dimensions, text)
        {
        }

         public override void OnMouseEntered() 
        {
            base.OnMouseEntered();
            if (IsActive)
            {
                ((ButtonPanel)(Parent)).SetButtonSelected(this);
            }
        }



        public override void Draw(GuiElement parent) 
        {
            UnityEngine.Color textColor = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1.0f);

            if (!IsSelected)
            {
                switch(State)
                {
                    case Enums.ButtonState.Active:
                    {
                        break;
                    }
                    case Enums.ButtonState.Inactive:
                    {
                        
                        break;
                    }
                    case Enums.ButtonState.Pressed:
                    {
                        textColor = UnityEngine.Color.white;
                        break;
                    }
                    case Enums.ButtonState.Hover:
                    {
                        textColor = UnityEngine.Color.white;
                        break;
                    }
                }
            }
            else
            {
                textColor = UnityEngine.Color.white;
                Sprites.Sprite sprite = GameState.SpriteAtlasManager.GetSprite(GameState.GuiResourceManager.ButtonHighlightSprite, Enums.AtlasType.Gui);
                GameState.Renderer.DrawSpriteGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, sprite);
            }
            
            GameState.Renderer.DrawStringGui(DrawPosition.X, DrawPosition.Y, DrawDimensions.X, DrawDimensions.Y, Text,
                     GameState.GuiResourceManager.RodinFont, 36, UnityEngine.TextAnchor.MiddleCenter, textColor);


            base.DrawChildren();
        }
    }
}