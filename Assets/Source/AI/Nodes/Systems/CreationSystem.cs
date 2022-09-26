using UnityEngine;
using System.Collections.Generic;

using Enums;
using KMath;
using Entitas;
using Unity.VisualScripting;

namespace Node
{
    public class CreationSystem
    {

        private static int ActionID;

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
            nodeEntity.AddNodeTime(0f);

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
                nodeEntity.AddNodeMoveTo(goalPosition);
            }
            return nodeID;
        }

        public int CreateTargetAction(Contexts entitasContext, NodeType NodeTypeID, int agentID, Vec2f target, int itemID)
        {
            int nodeID = CreateAction(entitasContext, NodeTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = entitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeTaget(-1, -1, target);
            }
            return nodeID;
        }

        public int CreateTargetAction(Contexts entitasContext, NodeType NodeTypeID, int agentID, int agentTargetID, int itemID)
        {
            int nodeID = CreateAction(entitasContext, NodeTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity actionEntity = entitasContext.node.GetEntityWithNodeIDID(nodeID);
                actionEntity.AddNodeTool(itemID);
                actionEntity.AddNodeTaget(agentTargetID, -1, Vec2f.Zero);
            }
            return nodeID;
        }
    }
}
