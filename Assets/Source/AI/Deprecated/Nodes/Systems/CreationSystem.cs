using UnityEngine;
using Enums;
using KMath;
using AI;
using System.Collections.Generic;

namespace Node
{
    public class CreationSystem
    {

        private static int ActionID;

        public int CreateBehaviorTreeNode(Contexts entitasContext, NodeType NodeTypeID, int agentID, int[] entiresID = null)
        {
            NodeEntity nodeEntity = entitasContext.node.CreateEntity();
            nodeEntity.AddNodeID(ActionID, NodeTypeID);
            nodeEntity.AddNodeOwner(agentID);
            nodeEntity.AddNodeExecution(NodeState.Entry);
            nodeEntity.AddNodeTime(Time.realtimeSinceStartup);
            if (entiresID != null)
                nodeEntity.AddNodeBlackboardData(entiresID);

            switch (AISystemState.Nodes[(int)NodeTypeID].NodeGroup)
            {
                case NodeGroup.CompositeNode:
                    nodeEntity.AddNodeComposite(new List<int>(), 0);
                    break;
                case NodeGroup.DecoratorNode:
                    nodeEntity.AddNodeDecorator(-1);
                    break;
            }
            nodeEntity.isNodeBT = true;

            return ActionID++;
        }

        /// <summary>
        /// Create action and schedule it. Later we will be able to create action without scheduling immediately.
        /// If actions is in cool down returns -1. 
        /// </summary>
        public int CreateAction(Contexts entitasContext, NodeType NodeTypeID, int agentID)
        {
            if (GameState.ActionCoolDownSystem.InCoolDown(entitasContext, NodeTypeID, agentID))
            {
                Debug.Log("Action " + NodeTypeID.ToString() + " in CoolDown");
                return -1;
            }

            NodeEntity nodeEntity = entitasContext.node.CreateEntity();
            nodeEntity.AddNodeID(ActionID, NodeTypeID);
            nodeEntity.AddNodeOwner(agentID);
            nodeEntity.AddNodeExecution(NodeState.Entry);
            nodeEntity.AddNodeTime(Time.realtimeSinceStartup);

            return ActionID++;
        }

        public int CreateAction(Contexts entitasContext, NodeType NodeTypeID, int agentID, int itemID)
        {
            int nodeID = CreateAction(entitasContext, NodeTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = entitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeTool(itemID);
            }
            return nodeID;
        }

        public int CreateMovementAction(Contexts entitasContext, NodeType NodeTypeID, int agentID, Vec2f goalPosition)
        {
            int nodeID = CreateAction(entitasContext, NodeTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = entitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeMoveTo(goalPosition, new Vec2f[1], 0, false, false);
            }
            return nodeID;
        }
    }
}
