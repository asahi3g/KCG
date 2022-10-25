using UnityEngine;
using Enums;
using KGUI.Elements;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    public class WaterElementUI : ElementUI
    {
        [SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float waterAmount;
        private readonly TextWrapper infoTextWrapper = new();

        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.WaterIndicator;
            
            waterAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Water : 0.0f;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Water\\hud_status_water.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, GameState.GUIManager.ProgressBar);
            
            progressBar = new ProgressBar(progressBarImageWrapper, waterAmount, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            waterAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Water : 0.0f;
            progressBar.Update(waterAmount);
            infoTextWrapper.Update();

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
                infoTextWrapper.Create("Water Indicator", "Water Bar\nStatus: Low", transform, 2.0f);

                // Set Size Delta
                infoTextWrapper.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoTextWrapper.SetPosition(new Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoTextWrapper.Create("Water DeIndicator", "Water Bar\nStatus: Normal", transform, 2.0f);

                // Set Size Delta
                infoTextWrapper.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoTextWrapper.SetPosition(new Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
        }


        public override void OnMouseStay()
        {
            Debug.LogWarning("Water Bar Mouse Stay");
        }


        public override void OnMouseExited()
        {
            Debug.LogWarning("Water Bar Mouse Exit");

            infoTextWrapper.StartLifeTime = true;
        }
    }
}
