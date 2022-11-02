using Enums;
using Item;
using NodeSystem;
using Planet;

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
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

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
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive)
                    continue;

                return true;
            }
            return false;
        }

        public static void RegisterConditions()
        {
            GameState.ConditionManager.RegisterCondition("HasBulletInClip", HasBulletInClip);
            GameState.ConditionManager.RegisterCondition("HasEnemyAlive", HasEnemyAlive);
        }
    }
}
