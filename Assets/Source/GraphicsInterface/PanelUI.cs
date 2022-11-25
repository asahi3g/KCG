//imports UnityEngine

using System.Collections.Generic;

namespace KGUI
{
    [UnityEngine.DefaultExecutionOrder (101)]
    public class PanelUI : UnityEngine.MonoBehaviour
    {
        public Dictionary<ElementEnums, ElementUI> ElementList = new Dictionary<ElementEnums, ElementUI>();
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
            
            //GameState.GUIManager.PanelList.Add(ID, this);
           // OnActivate();
        }

        public virtual void HandleClickEvent(ElementEnums elementID) { }
        
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
    }
}

