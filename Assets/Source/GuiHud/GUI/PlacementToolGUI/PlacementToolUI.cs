using UnityEngine;

namespace KGUI
{
    public class PlacementToolUI : UIPanel
    {
        [SerializeField] private BedrockElementUI bedrockElementUI;
        [SerializeField] private DirtElementUI dirtElementUI;
        [SerializeField] private PipeElementUI pipeElementUI;
        [SerializeField] private WireElementUI wireElementUI;

        public override void Init()
        {
            ID = UIPanelID.PlacementTool;
            
            UIElementList.Add(bedrockElementUI.ID, bedrockElementUI);
            UIElementList.Add(dirtElementUI.ID, dirtElementUI);
            UIElementList.Add(pipeElementUI.ID, pipeElementUI);
            UIElementList.Add(wireElementUI.ID, wireElementUI);
            
            base.Init();
        }
    }
}
