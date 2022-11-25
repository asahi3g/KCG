
using KMath;
using UnityEngine;

namespace GameScreen
{



    public class MainMenuScreen : GameScreen
    {

        
        public Gui.GuiElement Root;
        public Gui.MainMenuButtonPanel MainMenuButtonPanel;


        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();

            Root.Dimensions = new Vec2f(UnityEngine.Screen.width, UnityEngine.Screen.height);
        }

        public override void OnGui()
        {
            base.OnGui();

            // check if the sprite atlas teSetTilextures needs to be updated
            GameState.SpriteAtlasManager.UpdateAtlasTextures();

            Root.UpdatePositionAndScale(null);
            Root.Update(null);
            Root.Draw(null);
        }

        public override void Init(Transform screenTransform)
        {
            base.Init(screenTransform);
        }

        public override void LoadResources()
        {
            base.LoadResources();

            Root = new Gui.GuiElement(new Vec2f(), new Vec2f(UnityEngine.Screen.width, UnityEngine.Screen.height));
            MainMenuButtonPanel = new Gui.MainMenuButtonPanel(new Vec2f(700f, 400f));
            Root.AddChild(MainMenuButtonPanel);
            MainMenuButtonPanel.LayoutLeft();

        }

        public override void UnloadResources()
        {
            base.UnloadResources();

            Root = null;
            MainMenuButtonPanel = null;
        }
    }
}