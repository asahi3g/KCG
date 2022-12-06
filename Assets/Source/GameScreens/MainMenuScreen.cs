
using KMath;
using UnityEngine;

namespace GameScreen
{



    public class MainMenuScreen : GameScreen
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

        public override void Init(Transform screenTransform)
        {
            base.Init(screenTransform);
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