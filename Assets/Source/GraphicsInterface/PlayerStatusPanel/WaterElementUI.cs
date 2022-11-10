// Imports UnityEngine

using KGUI.Elements;
using Utility;

namespace KGUI
{
    public class WaterElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private ProgressBar progressBar;
        
        private float waterAmount;
        private readonly TextWrapper infoTextWrapper = new TextWrapper();

        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.WaterIndicatorPS;
            
            waterAmount = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Water : 0.0f;

            icon.Init(9, 19, "Assets\\StreamingAssets\\UserInterface\\Icons\\Water\\hud_status_water.png", Enums.AtlasType.Gui);
            progressBar.Init(GameState.GUIManager.ProgressBar, waterAmount, UnityEngine.UI.Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            ref var planet = ref GameState.Planet;
            waterAmount = planet.Player != null ? planet.Player.agentStats.Water : 0.0f;
            progressBar.Update(waterAmount);
            infoTextWrapper.Update();

        }
        public override void Draw()
        {
            icon.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            UnityEngine.Debug.LogWarning("Water Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            UnityEngine.Debug.LogWarning("Water Bar Mouse Enter");

            // If Water level less than 50
            if (waterAmount < 50)
            {
                // Create Hover Text
                infoTextWrapper.Create("Water Indicator", "Water Bar\nStatus: Low", transform, 2.0f);

                // Set Size Delta
                infoTextWrapper.SetSizeDelta(new UnityEngine.Vector2(250, 50));

                // Set Position
                infoTextWrapper.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoTextWrapper.Create("Water DeIndicator", "Water Bar\nStatus: Normal", transform, 2.0f);

                // Set Size Delta
                infoTextWrapper.SetSizeDelta(new UnityEngine.Vector2(250, 50));

                // Set Position
                infoTextWrapper.SetPosition(new UnityEngine.Vector3(HitBoxPosition.x + HitBoxSize.x + 20f, 0, 0));
            }
        }


        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Water Bar Mouse Stay");
        }


        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Water Bar Mouse Exit");

            infoTextWrapper.StartLifeTime = true;
        }
    }
}
