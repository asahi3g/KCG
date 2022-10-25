using UnityEngine;
using Enums;
using KGUI.Elements;
using KMath;
using UnityEngine.UI;

namespace KGUI
{
    public class FuelElementUI : ElementUI
    {
        [SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float fuelLevel;
        private readonly TextWrapper infoTextWrapper = new();

        public override void Init()
        {
            base.Init();

            ID = ElementEnums.FuelIndicator;
            fuelLevel = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Fuel : 0.0f;
            
            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Fuel\\hud_status_fuel.png", AtlasType.Gui);
            
            var progressBarImageWrapper = new ImageWrapper(progressBarImage, GameState.GUIManager.ProgressBar);
            
            progressBar = new ProgressBar(progressBarImageWrapper, fuelLevel, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            
            fuelLevel = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Fuel : 0.0f;
            
            if (fuelLevel <= 0)
            {
                fuelLevel = 0;
            }
            
            progressBar.Update(fuelLevel);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            Icon.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            Debug.LogWarning("Fuel Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            Debug.LogWarning("Fuel Bar Mouse Enter");

            // If Water level less than 50
            if (fuelLevel < 50)
            {
                infoTextWrapper.Create("Fuel Indicator", "Fuel Bar\nStatus: Low", transform, 2.0f);
                infoTextWrapper.SetSizeDelta(new Vector2(250, 50));
                infoTextWrapper.SetPosition(new Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
            else
            {
                infoTextWrapper.Create("Fuel DeIndicator", "Fuel Bar\nStatus: Normal", transform, 2.0f);
                infoTextWrapper.SetSizeDelta(new Vector2(250, 50));
                infoTextWrapper.SetPosition(new Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
        }
        public override void OnMouseStay()
        {
            Debug.LogWarning("Fuel Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            Debug.LogWarning("Fuel Bar Mouse Exit");

            infoTextWrapper.StartLifeTime = true;
        }
    }
}
