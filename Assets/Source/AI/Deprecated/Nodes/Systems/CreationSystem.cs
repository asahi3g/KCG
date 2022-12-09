//imports UntiyEngine

using Enums;
using KMath;
using AI;
using System.Collections.Generic;
using NodeSystem;

namespace Node
{
    public class CreationSystem
    {

        private static int ActionID;

        // Create action and schedule it. Later we will be able to create action without scheduling immediately.
        // If actions is in cool down returns -1. 
        public int CreateAction(ActionType ActionTypeID, int agentID)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateAction({nameof(ActionTypeID)}:{ActionTypeID}), {nameof(agentID)}:{agentID}");
            
            if (GameState.ActionCoolDownSystem.InCoolDown(ActionTypeID, agentID))
            {
                //UnityEngine.Debug.Log("Action " + ActionTypeID.ToString() + " in CoolDown");
                return -1;
            }

            NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.CreateEntity();
            nodeEntity.AddNodeID(ActionID, ActionTypeID);
            nodeEntity.AddNodeOwner(agentID);
            nodeEntity.AddNodeExecution(Enums.NodeState.Entry);
            nodeEntity.AddNodeTime(UnityEngine.Time.realtimeSinceStartup);

            return ActionID++;
        }

        public int CreateAction(ActionType ActionTypeID, int agentID, int itemID)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateAction({nameof(ActionTypeID)}:{ActionTypeID}), {nameof(agentID)}:{agentID}, {nameof(itemID)}:{itemID}");
            
            int nodeID = CreateAction(ActionTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeTool(itemID);
            }
            return nodeID;
        }

        public int CreateMovementAction(ActionType ActionTypeID, int agentID, Vec2f goalPosition)
        {
            //UnityEngine.Debug.Log($"{nameof(CreationSystem)}.CreateMovementAction({nameof(ActionTypeID)}:{ActionTypeID}), {nameof(agentID)}:{agentID}, {nameof(goalPosition)}:{goalPosition}");
            
            int nodeID = CreateAction(ActionTypeID, agentID);
            if (nodeID != -1)
            {
                NodeEntity nodeEntity = GameState.Planet.EntitasContext.node.GetEntityWithNodeIDID(nodeID);
                nodeEntity.AddNodeMoveTo(goalPosition, new Vec2f[1], 0, false, false);
            }
            return nodeID;
        }
    }
}
