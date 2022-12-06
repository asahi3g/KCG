using KMath;

namespace GameScreen
{



    public class GameoverScreen : GameScreen
    {

        


        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();

        }

        public override void OnGui()
        {
            base.OnGui();

            // check if the sprite atlas teSetTilextures needs to be updated
            GameState.SpriteAtlasManager.UpdateAtlasTextures();

        }

        public override void LoadResources()
        {
            base.LoadResources();

        }

        public override void UnloadResources()
        {
            base.UnloadResources();
        }
    }
}