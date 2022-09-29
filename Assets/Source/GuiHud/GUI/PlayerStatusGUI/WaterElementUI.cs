using UnityEngine;
using Enums;
using KGUI.Elements;
using UnityEngine.UI;
using Text = KGUI.Elements.Text;

namespace KGUI
{
    public class WaterElementUI : UIElement
    {
        [SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float waterAmount;
        private readonly Text infoText = new();

        public override void Init()
        {
            base.Init();
            
            ID = UIElementID.WaterElement;
            
            waterAmount = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Water : 0.0f;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Water\\hud_status_water.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", AtlasType.Gui);
            
            progressBar = new ProgressBar(progressBarImageWrapper, waterAmount, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            waterAmount = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Water : 0.0f;
            progressBar.Update(waterAmount);
            infoText.Update();

        }
        public override void Draw()
        {
            Icon.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            Debug.LogWarning("Water Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            Debug.LogWarning("Water Bar Mouse Enter");

            // If Water level less than 50
            if (waterAmount < 50)
            {
                // Create Hover Text
                infoText.Create("Water Indicator", "Water Bar\nStatus: Low", transform, 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoText.Create("Water DeIndicator", "Water Bar\nStatus: Normal", transform, 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
            }
        }


        public override void OnMouseStay()
        {
            Debug.LogWarning("Water Bar Mouse Stay");
        }


        public override void OnMouseExited()
        {
            Debug.LogWarning("Water Bar Mouse Exit");

            infoText.StartLifeTime = true;
        }
    }
}