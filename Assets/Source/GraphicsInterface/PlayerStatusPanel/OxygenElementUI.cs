//imports UnityEngine

using KGUI.Elements;
using Utility;

namespace KGUI
{
    public class OxygenElementUI : ElementUI
    {
        [UnityEngine.SerializeField]private ProgressBar progressBar;
        
        private float oxygenAmount;
        private readonly TextWrapper infoTextWrapper = new TextWrapper();
        
        public override void Init()
        {
            base.Init();
            
            ID = ElementEnums.OxygenIndicatorPS;
            
            oxygenAmount = GameState.Planet.Player != null ? GameState.Planet.Player.agentStats.Oxygen.GetValue() : 0;

            icon.Init(13, 13,"Assets\\StreamingAssets\\UserInterface\\Icons\\hud_status_oxygen.png", Enums.AtlasType.Gui);
            progressBar.Init(GameState.GUIManager.ProgressBar, oxygenAmount, UnityEngine.UI.Image.FillMethod.Radial360);
        }

        public override void Update()
        {
            base.Update();
            var planet = GameState.Planet;
            oxygenAmount = planet.Player != null ? planet.Player.agentStats.Oxygen.GetValue() : 0;
            progressBar.Update(oxygenAmount);
            infoTextWrapper.Update();
        }

        public override void Draw()
        {
            icon.Draw();
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
