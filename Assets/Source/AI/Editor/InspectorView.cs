using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AI
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        public InspectorView()
        {
        }

        public void OnGUI()
        { 
        }


        internal void UpdateSelection(NodeView nodeView)
        {
            Clear();
            AddChildrenAttributes(Contexts.sharedInstance.node.GetEntityWithNodeIDID(nodeView.NodeID), this);
        }

        void AddChildrenAttributes(NodeEntity nodeEntity, VisualElement visualElement)
        {
            VisualElement newVisualElement = AddAttributes(nodeEntity, visualElement);
            var nodes = nodeEntity.GetChildren(Contexts.sharedInstance.node);
            foreach (var node in nodes)
            {
                AddChildrenAttributes(nodeEntity, newVisualElement);
            }
        }

        VisualElement AddAttributes(NodeEntity nodeEntity, VisualElement visualElement)
        {
            Foldout foldout = new Foldout();
            foldout.text = nodeEntity.nodeID.TypeID.ToString();
            foldout.value = true;
            IntegerField integerField = new IntegerField("ID") { value = nodeEntity.nodeID.ID };
            integerField.isReadOnly = true;
            foldout.contentContainer.Add(integerField);

            TextField enumField = new TextField("Node Type");
            enumField.value = nodeEntity.nodeID.TypeID.ToString();
            enumField.isReadOnly = true;
            foldout.contentContainer.Add(enumField);
            visualElement.Add(foldout);
            return foldout;
        }
    }
}
