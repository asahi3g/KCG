using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.UIElements;
using Enums;
using UnityEngine.Networking.Types;
using Codice.CM.WorkspaceServer.DataStore;
using KMath;

namespace AI
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public int nodeID;
        public const int Width = 128;
        public const int Height = 80;
        public Port Input = null;
        public Port Output = null;
        public Action<NodeView> OnNodeSelected;
        public Vec2f Position { get; private set; }

        public NodeView(int nodeId, bool isEntryNode = false) : base("Assets/Source/AI/Editor/Resources/NodeEditorView.uxml") 
        {
            nodeID = nodeId;
            NodeSystem.Node node = GameState.NodeManager.Get(nodeId);
            CreatePorts(isEntryNode);
            SetupClasses(isEntryNode);
            if (isEntryNode) // Todo how to check if it's root in the new sistem.
                title = "Root";
            else
                title = GameState.NodeManager.GetName(nodeId);
            style.left = Position.X;
            style.top = Position.Y;
        }

        private void CreatePorts(bool isEntryNode)
        {
            NodeSystem.Node node = GameState.NodeManager.Get(nodeID);

            if (!isEntryNode)
            {
                Input = CreatePort(Direction.Input, Capacity.Single);
                inputContainer.Add(Input);
            }

            if (!IsLeafNode())
            {
                Output = CreatePort(Direction.Output, Capacity.Multi);
                outputContainer.Add(Output);
            }
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

        private void SetupClasses(bool isEntryNode)
        {
            NodeSystem.Node node = GameState.NodeManager.Get(nodeID);
            if (isEntryNode)
                AddToClassList("root");
            else
            { 
                switch (node.Type)
                {
                    case NodeSystem.NodeType.Sequence:
                        AddToClassList("composite");
                        break;
                    case NodeSystem.NodeType.Selector:
                        AddToClassList("composite");
                        break;
                    case NodeSystem.NodeType.Repeater:
                        AddToClassList("decroator");
                        break;
                    case NodeSystem.NodeType.Decorator:
                        AddToClassList("decroator");
                        break;
                    case NodeSystem.NodeType.Action:
                        AddToClassList("Action");
                        break;
                    case NodeSystem.NodeType.ActionSequence:
                        AddToClassList("Action");
                        break;
                }
            }
        }

        public void SetPos(Vec2f newPos)
        {
            base.SetPosition(new Rect(newPos.X, newPos.Y, Width, Height));
            Position = newPos;
        }


        // Node with no child.
        public bool IsLeafNode()
        {
            NodeSystem.Node node = GameState.NodeManager.Get(nodeID);
            return (node.Type == NodeSystem.NodeType.Action || node.Type == NodeSystem.NodeType.ActionSequence) ? true : false;

        }
    }
}
