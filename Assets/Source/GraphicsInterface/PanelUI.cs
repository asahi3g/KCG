using System.Collections.Generic;
using UnityEngine;

namespace KGUI
{
    [DefaultExecutionOrder (101)]
    public class PanelUI : MonoBehaviour
    {
        public Dictionary<ElementEnums, ElementUI> ElementList = new();
        public PanelEnums ID { get; protected set; }

        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            var components = GetComponentsInChildren<ElementUI>();

            foreach (var element in components)
            {
                ElementList.Add(element.ID, element);
            }
            
            GameState.GUIManager.PanelList.Add(ID, this);
            OnActivate();
        }

        public virtual void HandleClickEvent(ElementEnums elementID) { }
        
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
    }
}

