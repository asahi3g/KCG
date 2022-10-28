//imports UnityEngine

using Enums;
using KGUI.Elements;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    public class FoodElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float foodAmount;
        private readonly TextWrapper infoText = new TextWrapper();

        public override void Init()
        {
            ref var planet = ref GameState.Planet;
            base.Init();
            
            ID = ElementEnums.FoodIndicator;
            
            foodAmount = planet.Player != null ? planet.Player.agentStats.Food : 0.0f;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Food\\hud_status_food.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, GameState.GUIManager.ProgressBar);
            
            progressBar = new ProgressBar(progressBarImageWrapper, foodAmount, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            ref var planet = ref GameState.Planet;
            foodAmount = planet.Player != null ? planet.Player.agentStats.Food : 0.0f;
            progressBar.Update(foodAmount);
            infoText.Update();
        }

        public override void Draw()
        {
            Icon.Draw();
            progressBar.Draw();
        }

        public override void OnMouseClick()
        {
            UnityEngine.Debug.LogWarning("Food Bar Clicked");
        }
        public override void OnMouseEntered()
        {
            UnityEngine.Debug.LogWarning("Food Bar Mouse Enter");
            
            if (foodAmount < 50)
            {
                infoText.Create("Food Indicator", "Hunger Bar\nStatus: Low", transform, 2.0f);
                infoText.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoText.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
                infoText.Draw();
            }
            else 
            {
                infoText.Create("Food DeIndicator", "Hunger Bar\nStatus: Normal", transform, 2.0f);
                infoText.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoText.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
                infoText.Draw();
            }
        }
        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Food Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Food Bar Mouse Exit");

            infoText.StartLifeTime = true;
        }
    }
}
