using System.Collections.Generic;
using UnityEngine;

namespace KGUI
{
    [DefaultExecutionOrder (101)]
    public class UIPanel : MonoBehaviour
    {
        public UIPanelID ID;
        public Dictionary<UIElementID, UIElement> UIElementList = new();

        private void Start()
        {
            Init();
        }
        
        public virtual void Update() { }

        public virtual void Init()
        {
            GameState.GUIManager.UIPanelList.Add(ID, this);
        }
        
        public virtual void OnDeactivate() { }
    }
}

