using UnityEngine;

namespace KGUI
{
    public class TestPanel : PanelUI
    {
        [SerializeField] private TestElement testElement;

        public override void Init()
        {
            ID = PanelEnums.Test;
            
            UIElementList.Add(testElement.ID, testElement);
            
            base.Init();
        }
    }
}

