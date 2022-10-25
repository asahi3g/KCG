//imports UnityEngine

using Enums;
using KGUI.Elements;
using UnityEngine.UI;
using Utility;

namespace KGUI
{
    public class HealthElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private Image progressBarImage;
        [UnityEngine.SerializeField] private Image progressBarBorderImage;
        [UnityEngine.SerializeField] private Image progressBarDiv1Image;
        [UnityEngine.SerializeField] private Image progressBarDiv2Image;
        
        private ProgressBar progressBar;
        private ImageWrapper progressBarBorder;
        private ImageWrapper progressBarDiv1;
        private ImageWrapper progressBarDiv2;

        float healthAmount;
        private readonly TextWrapper infoTextWrapper = new();

        public override void Init()
        {
            base.Init();

            ID = ElementEnums.HealthIndicator;
            healthAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Health : 0.0f;
            
            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png", AtlasType.Gui);
            
            progressBarBorder = new ImageWrapper(progressBarBorderImage, 6, 8,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_border.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, 5, 5,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_fill.png", AtlasType.Gui);

            progressBarDiv1 = new ImageWrapper(progressBarDiv1Image, 1, 6,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div1.png", AtlasType.Gui);

            progressBarDiv2 = new ImageWrapper(progressBarDiv2Image, 1, 6,
                "Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div2.png", AtlasType.Gui);


            progressBar = new ProgressBar(progressBarImageWrapper, healthAmount, Image.FillMethod.Horizontal);
        }

        public override void Update()
        {
            base.Update();
            healthAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Health : 0.0f;
            progressBar.Update(healthAmount);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            Icon.Draw();
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
