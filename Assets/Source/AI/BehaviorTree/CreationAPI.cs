using Enums;
using System.Collections.Generic;

namespace AI.BehaviorTree
{
    public class CreationAPI
    {
        Contexts EntitasContext;
        int AgentID;

        private void AddToParent(NodeEntity parent, int childID)
        {
            if (parent.hasNodesDecorator)
            {
                parent.nodesDecorator.ChildID = childID;
                return;
            }
            if (parent.hasNodeComposite)
            {
                parent.nodeComposite.Children.Add(childID);
                return;
            }

            Utils.Assert(true, "Error: You can't attach node to a leaf node.");
        }

        public NodeEntity CreateTree(Contexts entitasContext, int agentID)
        {
            EntitasContext = entitasContext;
            AgentID = agentID;
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(entitasContext, NodeType.DecoratorNode, agentID);
            newEntity.AddNodesDecorator(-1);
            return newEntity;
        }

        public NodeEntity AddDecoratorNode(NodeEntity nodeEntity)
        {
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext, 
                NodeType.DecoratorNode, AgentID);
            newEntity.AddNodesDecorator(-1);
            AddToParent(nodeEntity, newEntity.nodeID.ID);
            return newEntity;
        }

        public NodeEntity AddSequence(NodeEntity nodeEntity)
        {
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext,
                NodeType.SequenceNode, AgentID);
            newEntity.AddNodeComposite(new List<int>(), 0);
            AddToParent(nodeEntity, newEntity.nodeID.ID);
            return newEntity;
        }

        public NodeEntity AddSelector(NodeEntity nodeEntity)
        {
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext,
                NodeType.SelectorNode, AgentID);
            newEntity.AddNodeComposite(new List<int>(), 0);
            AddToParent(nodeEntity, newEntity.nodeID.ID);
            return newEntity;
        }

        public NodeEntity AddReapterNode(NodeEntity nodeEntity)
        {
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext,
                NodeType.RepeaterNode, AgentID);
            newEntity.AddNodesDecorator(-1);
            AddToParent(nodeEntity, newEntity.nodeID.ID);
            return newEntity;
        }

        // Todo: Single function for BTNodes
        //public NodeEntity AddBTNodes(NodeEntity nodeEntity, NodeType type)
        //{
        //    NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext,
        //        NodeType.RepeaterNode, AgentID);
        //    if (SystemState.Nodes[(int)].)
        //    newEntity.AddNodesDecorator(-1);
        //    AddToParent(nodeEntity, newEntity.nodeID.ID);
        //    return newEntity;
        //}

        /// <summary>Leafs </summary>
        public NodeEntity AddLeaf(NodeEntity nodeEntity, NodeType type)
        {
            NodeEntity newEntity = GameState.ActionCreationSystem.CreateBehaviorTreeNode(EntitasContext, type, AgentID);
            AddToParent(nodeEntity, newEntity.nodeID.ID);
            return newEntity;
        }
    }
}
