//imports UnityEngine

using KGUI.Elements;
using Utility;

namespace KGUI
{
    public class HealthElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private ProgressBar progressBar;
        [UnityEngine.SerializeField] private ImageWrapper progressBarBorder;
        [UnityEngine.SerializeField] private ImageWrapper progressBarDiv1;
        [UnityEngine.SerializeField] private ImageWrapper progressBarDiv2;

        private float healthAmount;
        private readonly TextWrapper infoTextWrapper = new TextWrapper();

        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.HealthIndicatorPS;
            healthAmount = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Health.GetValue() : 0;
            
            icon.Init(13, 13, "Assets\\StreamingAssets\\UserInterface\\Icons\\hud_hp_icon.png", Enums.AtlasType.Gui);
            
            progressBarBorder.Init(6, 8, "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_border.png", Enums.AtlasType.Gui);
            progressBarDiv1.Init(1, 6, "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div1.png", Enums.AtlasType.Gui);
            progressBarDiv2.Init(1, 6, "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div2.png", Enums.AtlasType.Gui);
            progressBar.Init(5, 5, "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_fill.png", Enums.AtlasType.Gui, healthAmount, UnityEngine.UI.Image.FillMethod.Horizontal);
        }

        public override void Update()
        {
            base.Update();
            healthAmount = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Health.GetValue() : 0;
            progressBar.Update(healthAmount);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            icon.Draw();
            progressBarDiv1.Draw();
            progressBarDiv2.Draw();
            progressBarBorder.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            UnityEngine.Debug.LogWarning("Health Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            UnityEngine.Debug.LogWarning("Health Bar Mouse Enter");
            
            if (healthAmount < 50)
            {
            }
            else
            {
                infoTextWrapper.Create("Health DeIndicator", "Health Bar\nStatus: Normal", transform, 2.0f);
                infoTextWrapper.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoTextWrapper.SetPosition(new UnityEngine.Vector3(700.0f, 0, 0));
            }
        }
        
        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Health Bar Mouse Stay");
        }
        
        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Health Bar Mouse Exit");
            infoTextWrapper.StartLifeTime = true;
        }
    }
}
