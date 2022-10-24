//imports UnityEngine

using Enums;
using KGUI.Elements;
using KMath;
using UnityEngine.UI;
using Text = KGUI.Elements.Text;

namespace KGUI
{
    public class FuelElementUI : UIElement
    {
        [UnityEngine.SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float fuelLevel;
        private readonly Text infoText = new();

        public override void Init()
        {
            base.Init();

            ID = UIElementID.FuelElement;
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
            infoText.Update();
        }

        public override void Draw()
        {
            Icon.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Mouse Enter");

            // If Water level less than 50
            if (fuelLevel < 50)
            {
                infoText.Create("Fuel Indicator", "Fuel Bar\nStatus: Low", transform, 2.0f);
                infoText.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoText.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
            else
            {
                infoText.Create("Fuel DeIndicator", "Fuel Bar\nStatus: Normal", transform, 2.0f);
                infoText.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoText.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
        }
        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Mouse Exit");

            infoText.StartLifeTime = true;
        }
    }
}
