//imports UnityEngine

namespace KGUI
{
    public class GameMenu : PanelUI
    {
        [UnityEngine.SerializeField] private StartElementUI startButton;
        [UnityEngine.SerializeField] private ResumeElementUI resumeButton;
        [UnityEngine.SerializeField] private ControlsElementUI controlsButton;
        [UnityEngine.SerializeField] private ExitElementUI exitButton;

        public override void Init()
        {
            ID = PanelEnums.GameMenu;
            
            base.Init();
        }
    }
}
