using Item;
using Planet;
using NodeSystem;
using UnityEngine;
using BehaviorTree;

namespace Action
{
    public class ReloadAction
    {
        // Action used by either player and AI.
        static public NodeState OnEnter(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item != null)
            {
                if (item.hasItemFireWeaponClip)
                {
                    return NodeState.Running;
                }
            }
            return NodeState.Failure;
        }

        static public NodeState OnUpdate(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();
            FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);

            if (data.NodesExecutiondata[id].ExecutionTime >= fireWeaponProperties.ReloadTime)
            {
                if(item.hasItemFireWeaponClip)
                    item.itemFireWeaponClip.NumOfBullets = fireWeaponProperties.ClipSize;

                return NodeState.Success;
            }
            return NodeState.Running;
        }

        static public NodeState OnSuccess(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item.hasItemFireWeaponClip)
                Debug.Log("Weapon Reloaded." + item.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");

            return NodeState.Success;
        }

        static public NodeState OnFailure(object ptr, int id)
        {
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            Debug.Log("Fail to reload.");
            return NodeState.Failure;
        }
    }
}
