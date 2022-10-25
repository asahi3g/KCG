//imports UnityEngine

using Enums;
using KGUI.Elements;
using UnityEngine.UI;

namespace KGUI
{
    public class OxygenElementUI : ElementUI
    {
        [UnityEngine.SerializeField] private Image progressBarImage;
        
        private ProgressBar progressBar;
        private float oxygenAmount;
        private readonly TextWrapper infoTextWrapper = new();
        
        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.OxygenIndicator;
            
            oxygenAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Oxygen : 0.0f;

            Icon = new ImageWrapper(iconImage, 19, 19,
                "Assets\\StreamingAssets\\UserInterface\\Icons\\Oxygen\\hud_status_oxygen.png", AtlasType.Gui);

            var progressBarImageWrapper = new ImageWrapper(progressBarImage, GameState.GUIManager.ProgressBar);
            
            progressBar = new ProgressBar(progressBarImageWrapper, oxygenAmount, Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            oxygenAmount = GameState.GUIManager.Planet.Player != null ? GameState.GUIManager.Planet.Player.agentStats.Oxygen : 0.0f;
            progressBar.Update(oxygenAmount);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            Icon.Draw();
            progressBar.Draw();
        }
        
        public override void OnMouseClick()
        {
            UnityEngine.Debug.LogWarning("Oxygen Bar Clicked");
        }
        
        public override void OnMouseEntered()
        {
            UnityEngine.Debug.LogWarning("Oxygen Bar Mouse Enter");
            
            if (oxygenAmount < 50)
            {
            }
            else
            {
                infoTextWrapper.Create("Oxygen DeIndicator", "Oxygen Bar\nStatus: Normal", transform, 2.0f);
                infoTextWrapper.SetSizeDelta(new UnityEngine.Vector2(250, 50));
                infoTextWrapper.SetPosition(new UnityEngine.Vector3(260.0f, 0, 0));
            }

        }
        
        public override void OnMouseStay()
        {
            UnityEngine.Debug.LogWarning("Oxygen Bar Mouse Stay");
        }
        
        public override void OnMouseExited()
        {
            UnityEngine.Debug.LogWarning("Oxygen Bar Mouse Exit");
            infoTextWrapper.StartLifeTime = true;
        }
    }
}
