using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
using UnityEngine.UIElements;

namespace AI
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Vector2 position;
        public int NodeID;

        public NodeView(NodeEntity nodeEntity, Vector2 pos) : base("Assets/Source/AI/Editor/NodeEditorView.uxml") 
        {
            CreatePorts(nodeEntity);
            SetupClasses(nodeEntity);
            title = nodeEntity.nodeID.TypeID.ToString();
            position = pos;
            style.left = position.x;
            style.top = position.y;
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
            position.x = newPos.xMin;
            position.y = newPos.yMin;
        }
    }
}
