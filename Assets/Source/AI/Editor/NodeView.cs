using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.UIElements;

namespace AI
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Vector2 Postion;
        public int NodeID;
        public Action<NodeView> OnNodeSelected;
        public const int Width = 128;
        public const int Height = 80;

        public NodeView(NodeEntity nodeEntity, Vector2 pos) : base("Assets/Source/AI/Editor/Resources/NodeEditorView.uxml") 
        {
            CreatePorts(nodeEntity);
            SetupClasses(nodeEntity);
            NodeID = nodeEntity.nodeID.ID;
            title = nodeEntity.nodeID.TypeID.ToString();
            Postion = pos;
            style.left = Postion.x;
            style.top = Postion.y;
        }

        private void CreatePorts(NodeEntity nodeEntity)
        {
            if (!nodeEntity.isNodeRoot)
                inputContainer.Add(CreatePort(Direction.Input, Capacity.Single));
            if (nodeEntity.hasNodeComposite)
                outputContainer.Add(CreatePort(Direction.Output, Capacity.Multi));
            else if (nodeEntity.hasNodesDecorator)
                outputContainer.Add(CreatePort(Direction.Output, Capacity.Single));
        }

        private Port CreatePort(Direction dir, Capacity capacity)
        {
            Type type = typeof(int);
            Port port = InstantiatePort(Orientation.Vertical, dir, capacity, type);
            port.portName = "";
            port.name = "input-port";
            port.portColor = Color.gray;
            port.style.flexDirection = FlexDirection.Column;
            return port;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if (OnNodeSelected != null)
            {
                OnNodeSelected.Invoke(this);
            }
        }

        private void SetupClasses(NodeEntity nodeEntity)
        { 
            if (nodeEntity.isNodeRoot)
                AddToClassList("root");
            else if (nodeEntity.hasNodeComposite)
                AddToClassList("composite");
            else if (nodeEntity.hasNodesDecorator)
                AddToClassList("decorator");
            else
                AddToClassList("action");
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Postion.x = newPos.xMin;
            Postion.y = newPos.yMin;
        }

        public void RemoveAll(NodeView other)
        {
            NodeEntity node = Contexts.sharedInstance.node.GetEntityWithNodeIDID(NodeID);
            node.RemoveAllChildren();
        }

        public void RemoveChild(NodeView other)
        {
            NodeEntity node = Contexts.sharedInstance.node.GetEntityWithNodeIDID(NodeID);
            node.RemoveChild(other.NodeID);
        }

        public void AddChild(NodeView other)
        {
            NodeEntity node = Contexts.sharedInstance.node.GetEntityWithNodeIDID(NodeID);
            node.AddChild(other.NodeID);
        }
    }
}
