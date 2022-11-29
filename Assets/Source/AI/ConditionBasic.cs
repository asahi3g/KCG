using Enums;
using Item;
using NodeSystem;
using Planet;
using AI;
using UnityEngine;

namespace Condition
{
    // Basic conditions used by several behavior trees.
    public static class ConditionBasic
    {
        static bool HasBulletInClip(object ptr)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ItemInventoryEntity item = agent.GetItem();
            ItemProperties itemProperty = GameState.ItemCreationApi.GetItemProperties(item.itemType.Type);

            if (itemProperty.Group == ItemGroups.Gun)
            {
                if (item.hasItemFireWeaponClip)
                {
                    if (item.itemFireWeaponClip.NumOfBullets == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false; // Weapon not equipped.
        }

        static bool HasEnemyAlive(object ptr)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive || agent.agentID.Faction == entity.agentID.Faction)
                    continue;
                return true;
            }
            return false;
        }

        static bool IsInAttackRange(object ptr)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            
            float distance = (agent.GetGunFiringPosition() - blackboard.AttackTarget).Magnitude;
            float attackRange = 30.0f; // Todo: Create method to get attack range.
            return (distance < attackRange) ? true : false;
        }

        static bool NotInAttackRange(object ptr)
        {
            return !IsInAttackRange(ptr);
        }

        static bool InLineOfSight(object ptr)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);

            return agent.CanSee(blackboard.AgentTargetID);
        }

        public static bool CanSeeAndInRange(object ptr)
        {
            if (InLineOfSight(ptr) && IsInAttackRange(ptr))
                return true;
            return false;
        }

        public static bool ItIsOnTheNextTile(object ptr)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            AgentEntity target = planet.EntitasContext.agent.GetEntityWithAgentID(blackboard.AgentTargetID);

            if (Mathf.Abs(agent.agentPhysicsState.Position.X - target.agentPhysicsState.Position.X) <= 1.0f)
                return true;

            return false;
        }

        public static void RegisterConditions()
        {
            GameState.ConditionManager.RegisterCondition("HasBulletInClip", HasBulletInClip);
            GameState.ConditionManager.RegisterCondition("HasEnemyAlive", HasEnemyAlive);
            GameState.ConditionManager.RegisterCondition("IsInAttackRange", IsInAttackRange);
            GameState.ConditionManager.RegisterCondition("NotInAttackRange", NotInAttackRange);
            GameState.ConditionManager.RegisterCondition("InLineOfSight", InLineOfSight);
            GameState.ConditionManager.RegisterCondition("CanSeeAndInRange", CanSeeAndInRange);
            GameState.ConditionManager.RegisterCondition("ItIsOnTheNextTile", ItIsOnTheNextTile);
        }
    }
}
