using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;

namespace AI.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public NodeView(NodeEntity nodeEntity) : base("Assets/Source/AI/Editor/NodeEditorView.uxml") 
        {
            CreatePorts(nodeEntity);
            SetupClasses(nodeEntity);
            title = nodeEntity.nodeID.TypeID.ToString();
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
    }
}
