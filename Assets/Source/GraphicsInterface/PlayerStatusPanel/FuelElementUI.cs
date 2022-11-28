//imports UnityEngine

using KGUI.Elements;
using Utility;

namespace KGUI
{
    public class FuelElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private ProgressBar progressBar;
        
        private float fuelLevel;
        private readonly TextWrapper infoTextWrapper = new TextWrapper();

        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.FuelIndicatorPS;
            fuelLevel = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Fuel : 0;

            icon.Init(13, 13,"Assets\\StreamingAssets\\UserInterface\\Icons\\hud_status_fuel.png", Enums.AtlasType.Gui);
            progressBar.Init(GameState.GUIManager.ProgressBar, fuelLevel, UnityEngine.UI.Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            
            fuelLevel = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Fuel : 0;
            
            if (fuelLevel <= 0)
            {
                fuelLevel = 0;
            }
            
            progressBar.Update(fuelLevel);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            icon.Draw();
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
            }
            else
            {
                infoTextWrapper.Create("Fuel DeIndicator", "Fuel Bar\nStatus: Normal", transform, 2.0f);
                infoTextWrapper.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoTextWrapper.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
        }
        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Fuel Bar Mouse Exit");

            infoTextWrapper.StartLifeTime = true;
        }
    }
}
