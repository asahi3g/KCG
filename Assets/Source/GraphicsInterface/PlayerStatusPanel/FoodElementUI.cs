//imports UnityEngine

using KGUI.Elements;
using Utility;

namespace KGUI
{
    public class FoodElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private ProgressBar progressBar;
        private float foodAmount;
        private readonly TextWrapper infoText = new TextWrapper();

        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.FoodIndicatorPS;
            
            foodAmount = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Food.GetValue() : 0.0f;

            icon.Init(13, 13,"Assets\\StreamingAssets\\UserInterface\\Icons\\hud_status_food.png", Enums.AtlasType.Gui);
            progressBar.Init(GameState.GUIManager.ProgressBar, foodAmount, UnityEngine.UI.Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            ref var planet = ref GameState.Planet;
            foodAmount = planet.Player?.agentStats.Food.GetValue() ?? 0.0f;
            progressBar.Update(foodAmount);
            infoText.Update();
        }

        public override void Draw()
        {
            icon.Draw();
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
