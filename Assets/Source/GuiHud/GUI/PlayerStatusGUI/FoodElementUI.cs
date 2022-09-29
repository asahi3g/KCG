using Enums;
using UnityEngine;
using KGUI.Elements;
using KMath;
using UnityEngine.UI;
using Text = KGUI.Elements.Text;

namespace KGUI
{
    public class FoodElementUI : UIElement
    {
        [SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float foodAmount;
        private readonly Text infoText = new();

        public override void Init()
        {
            base.Init();
            
            ID = UIElementID.FoodElement;
            
            foodAmount = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Food : 0.0f;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Food\\hud_status_food.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", AtlasType.Gui);
            
            progressBar = new ProgressBar(progressBarImageWrapper, foodAmount, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            foodAmount = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Food : 0.0f;
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
            Debug.LogWarning("Food Bar Clicked");
        }
        public override void OnMouseEntered()
        {
            Debug.LogWarning("Food Bar Mouse Enter");
            
            if (foodAmount < 50)
            {
                infoText.Create("Food Indicator", "Hunger Bar\nStatus: Low", transform, 2.0f);
                infoText.SetSizeDelta(new Vector2(250, 50));
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
                infoText.Draw();
            }
            else 
            {
                infoText.Create("Food DeIndicator", "Hunger Bar\nStatus: Normal", transform, 2.0f);
                infoText.SetSizeDelta(new Vector2(250, 50));
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
                infoText.Draw();
            }
        }
        public override void OnMouseStay()
        {
            Debug.LogWarning("Food Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            Debug.LogWarning("Food Bar Mouse Exit");

            infoText.StartLifeTime = true;
        }
    }
}