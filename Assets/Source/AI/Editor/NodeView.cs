using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.UIElements;
using KMath;
using Enums;
using UnityEngine.Networking.Types;
using static Codice.CM.WorkspaceServer.DataStore.WkTree.WriteWorkspaceTree;

namespace AI
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public NodeInfo Node;
        public const int Width = 128;
        public const int Height = 80;
        public Port Input = null;
        public Port Output = null;
        public Action<NodeView> OnNodeSelected;

        public NodeView(NodeInfo node) : base("Assets/Source/AI/Editor/Resources/NodeEditorView.uxml") 
        {
            Node = node;
            CreatePorts();
            SetupClasses();
            style.left = Node.pos.X;
            style.top = Node.pos.Y;
        }

        private void CreatePorts()
        {
            if (Node.index != 0)
            {
                Input = CreatePort(Direction.Input, Capacity.Single);
                inputContainer.Add(Input);
            }
            NodeGroup nodeGroup = AISystemState.GetNodeGroup(Node.type);

            if (nodeGroup == NodeGroup.CompositeNode)
                Output = CreatePort(Direction.Output, Capacity.Multi);
            else if (nodeGroup == NodeGroup.DecoratorNode)
                Output = CreatePort(Direction.Output, Capacity.Single);
            outputContainer.Add(Output);
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

        private void SetupClasses()
        { 
            if (Node.index == 0)
                AddToClassList("root");
            else if (AISystemState.GetNodeGroup(Node.type) == NodeGroup.CompositeNode)
                AddToClassList("composite");
            else if (AISystemState.GetNodeGroup(Node.type) == NodeGroup.DecoratorNode)
                AddToClassList("decorator");
            else
                AddToClassList("action");
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.pos.X = newPos.xMin;
            Node.pos.Y = newPos.yMin;
        }

        public void RemoveAll()
        {
            Node.children.Clear();
        }

        public void RemoveChild(NodeView nodeView)
        {
            Node.children.Remove(nodeView.Node.index);
        }

        public void AddChild(NodeView nodeView)
        {
            Node.children.Add(nodeView.Node.index);
        }
    }
}
