using UnityEngine;
using System.Collections.Generic;

using Enums;
using KMath;
using Entitas;
using Unity.VisualScripting;

namespace Action
{
    public class ActionCreationSystem
    {

        private static int ActionID;

        /// <summary>
        /// Create action and schedule it. Later we will be able to create action without scheduling immediately.
        /// If actions is in cool down returns -1. 
        /// </summary>
        public int CreateAction(Contexts entitasContext, ActionType actionTypeID, int agentID)
        {
            ActionProperties actionProperties = GameState.ActionCreationApi.Get(actionTypeID);

            if (GameState.ActionCoolDownSystem.InCoolDown(entitasContext, actionTypeID, agentID))
            {
                Debug.Log("Action " + actionProperties.Name + " in CoolDown");
                return -1;
            }

            ActionEntity actionEntity = entitasContext.action.CreateEntity();
            actionEntity.AddActionID(ActionID, actionTypeID);
            actionEntity.AddActionOwner(agentID);
            actionEntity.AddActionExecution(actionProperties.ActionFactory.CreateAction(entitasContext, ActionID), 
                ActionState.Entry);

            const float TIME_THRESHOLD = 0.05f;
            if (actionProperties.Duration > TIME_THRESHOLD)
            {
                actionEntity.AddActionTime(0f);
            }

            return ActionID++;
        }

        public int CreateAction(Contexts entitasContext, ActionType actionTypeID, int agentID, int itemID)
        {
            int actionID = CreateAction(entitasContext, actionTypeID, agentID);
            if (actionID != -1)
            {
                ActionEntity actionEntity = entitasContext.action.GetEntityWithActionIDID(actionID);
                actionEntity.AddActionTool(itemID);
            }
            return actionID;
        }

        public int CreateMovementAction(Contexts entitasContext, ActionType actionTypeID, int agentID, Vec2f goalPosition)
        {
            int actionID = CreateAction(entitasContext, actionTypeID, agentID);
            if (actionID != -1)
            {
                ActionEntity actionEntity = entitasContext.action.GetEntityWithActionIDID(actionID);
                actionEntity.AddActionMoveTo(goalPosition);
            }
            return actionID;
        }

        public int CreateTargetAction(Contexts entitasContext, ActionType actionTypeID, int agentID, Vec2f target, int itemID)
        {
            int actionID = CreateAction(entitasContext, actionTypeID, agentID);
            if (actionID != -1)
            {
                ActionEntity actionEntity = entitasContext.action.GetEntityWithActionIDID(actionID);
                actionEntity.AddActionTaget(-1, -1, target);
            }
            return actionID;
        }

        public int CreateTargetAction(Contexts entitasContext, ActionType actionTypeID, int agentID, int agentTargetID, int itemID)
        {
            int actionID = CreateAction(entitasContext, actionTypeID, agentID);
            if (actionID != -1)
            {
                ActionEntity actionEntity = entitasContext.action.GetEntityWithActionIDID(actionID);
                actionEntity.AddActionTool(itemID);
                actionEntity.AddActionTaget(agentTargetID, -1, Vec2f.Zero);
            }
            return actionID;
        }
    }
}
