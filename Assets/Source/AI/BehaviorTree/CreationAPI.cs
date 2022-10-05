using Enums;
using System.Collections.Generic;

namespace AI.BehaviorTree
{
    public class CreationAPI
    {
        class Node
        {
            public Node(Node p, NodeEntity entity)
            { 
                parent = p;
                nodeEntity = entity;
            }

            public Node parent;
            public NodeEntity nodeEntity;
        }

        int NextNodeID = 0;
        Node Current;

        public NodeEntity CreateBehaviorTreeNode(NodeType NodeTypeID)
        {
            NodeEntity nodeEntity = Contexts.sharedInstance.node.CreateEntity();
            nodeEntity.AddNodeID(NextNodeID++, NodeTypeID);
            switch(AISystemState.Nodes[(int)NodeTypeID].NodeGroup)
            {
                case NodeGroup.CompositeNode:
                    nodeEntity.AddNodeComposite(new List<int>(), 0);
                    break;
                case NodeGroup.DecoratorNode:
                    nodeEntity.AddNodesDecorator(-1);
                    break;
            }

            return nodeEntity;
        }

        private void AddToParent(NodeEntity child)
        {
            NodeEntity parent = Current.nodeEntity;
            switch (AISystemState.Nodes[(int)parent.nodeID.TypeID].NodeGroup)
            {
                case NodeGroup.CompositeNode:
                    parent.nodeComposite.Children.Add(child.nodeID.ID);
                    if (AISystemState.Nodes[(int)child.nodeID.TypeID].NodeGroup != NodeGroup.ActionNode)
                        Current = new Node(Current, child);
                    break;
                case NodeGroup.DecoratorNode:
                    parent.nodesDecorator.ChildID = child.nodeID.ID;
                    if (AISystemState.Nodes[(int)child.nodeID.TypeID].NodeGroup == NodeGroup.ActionNode)
                        Current = Current.parent;
                    else
                        Current = new Node(Current, child);
                    break;
                default:
                    Utils.Assert(false, "Error: You can't attach node to a leaf node.");
                    break;

            }

        }

        public int CreateTree()
        {
            NodeEntity root = CreateBehaviorTreeNode(NodeType.DecoratorNode);
            root.isNodeRoot = true;
            Current = new Node(null, root);
            return root.nodeID.ID;
        }

        public void AddChild(NodeType type)
        {
            NodeEntity newEntity = CreateBehaviorTreeNode(type);
            AddToParent(newEntity);
        }

        public void EndNode()
        {
            Current = Current.parent;
        }

        public void EndTree()
        {
            Current = null;
        }
    }
}
