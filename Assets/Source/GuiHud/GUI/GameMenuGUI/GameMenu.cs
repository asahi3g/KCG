using UnityEngine;

namespace KGUI
{
    public class GameMenu : PanelUI
    {
        [SerializeField] private StartElementUI startButton;
        [SerializeField] private ResumeElementUI resumeButton;
        [SerializeField] private ControlsElementUI controlsButton;
        [SerializeField] private ExitElementUI exitButton;

        public override void Init()
        {
            ID = PanelEnums.GameMenu;
            
            UIElementList.Add(startButton.ID, startButton);
            UIElementList.Add(resumeButton.ID, resumeButton);
            UIElementList.Add(controlsButton.ID, controlsButton);
            UIElementList.Add(exitButton.ID, exitButton);
            
            base.Init();
        }
    }
}
