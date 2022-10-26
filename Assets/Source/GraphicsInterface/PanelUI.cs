//imports UnityEngine

using System.Collections.Generic;

namespace KGUI
{
    [UnityEngine.DefaultExecutionOrder (101)]
    public class PanelUI : UnityEngine.MonoBehaviour
    {
        public Dictionary<ElementEnums, ElementUI> UIElementList = new();
        
        public PanelEnums ID { get; protected set; }

        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            if(!GameState.GUIManager.PanelList.ContainsKey(ID))
                GameState.GUIManager.PanelList.Add(ID, this);
            OnActivate();
        }
        
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
    }
}

