//imports UntiyEngine

using Enums;
using KMath;
using AI;
using System.Collections.Generic;

namespace Node
{
    public class CreationSystem
    {

        private static int ActionID;

        public int CreateBehaviorTreeNode(ItemUsageActionType ItemUsageActionTypeID, int agentID, int[] entiresID = null)
        {
            NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.CreateEntity();
            nodeEntity.AddNodeID(ActionID, ItemUsageActionTypeID);
            nodeEntity.AddNodeOwner(agentID);
            nodeEntity.AddNodeExecution(NodeState.Entry);
            nodeEntity.AddNodeTime(UnityEngine.Time.realtimeSinceStartup);
            if (entiresID != null)
                nodeEntity.AddNodeBlackboardData(entiresID);

            switch (AISystemState.Nodes[(int)ItemUsageActionTypeID].NodeGroup)
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

        // Create action and schedule it. Later we will be able to create action without scheduling immediately.
        // If actions is in cool down returns -1. 
        public int CreateAction(ItemUsageActionType ItemUsageActionTypeID, int agentID)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateAction({nameof(ItemUsageActionTypeID)}:{ItemUsageActionTypeID}), {nameof(agentID)}:{agentID}");
            
            if (GameState.ActionCoolDownSystem.InCoolDown(ItemUsageActionTypeID, agentID))
            {
                //UnityEngine.Debug.Log("Action " + ItemUsageActionTypeID.ToString() + " in CoolDown");
                return -1;
            }

            NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.CreateEntity();
            nodeEntity.AddNodeID(ActionID, ItemUsageActionTypeID);
            nodeEntity.AddNodeOwner(agentID);
            nodeEntity.AddNodeExecution(NodeState.Entry);
            nodeEntity.AddNodeTime(UnityEngine.Time.realtimeSinceStartup);

            return ActionID++;
        }

        public int CreateAction(ItemUsageActionType ItemUsageActionTypeID, int agentID, int itemID)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateAction({nameof(ItemUsageActionTypeID)}:{ItemUsageActionTypeID}), {nameof(agentID)}:{agentID}, {nameof(itemID)}:{itemID}");
            
            int nodeID = CreateAction(ItemUsageActionTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeTool(itemID);
            }
            return nodeID;
        }

        public int CreateMovementAction(ItemUsageActionType ItemUsageActionTypeID, int agentID, Vec2f goalPosition)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateMovementAction({nameof(ItemUsageActionTypeID)}:{ItemUsageActionTypeID}), {nameof(agentID)}:{agentID}, {nameof(goalPosition)}:{goalPosition}");
            
            int nodeID = CreateAction(ItemUsageActionTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeMoveTo(goalPosition, new Vec2f[1], 0, false, false);
            }
            return nodeID;
        }
    }
}
