using UnityEngine.UIElements;

namespace AI
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        public InspectorView()
        {

        }

        internal void UpdateSelection(NodeView nodeView)
        {
            NodeEntity nodeEntity = Contexts.sharedInstance.node.GetEntityWithNodeIDID(nodeView.NodeID);
        }
    }
}
