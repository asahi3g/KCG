using Item;
using Planet;
using NodeSystem;
using UnityEngine;
using NodeSystem.BehaviorTree;

namespace Action
{
    public class ReloadAction
    {
        static public NodeState OnEnter(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem(ref planet);

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
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem(ref planet);
            FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);

            float runningTime = Time.realtimeSinceStartup - data.GetTime(id);
            if (runningTime >= WeaponPropreties.ReloadTime)
            {
                if(item.hasItemFireWeaponClip)
                    item.itemFireWeaponClip.NumOfBullets = WeaponPropreties.ClipSize;

                return NodeState.Success;
            }
            return NodeState.Running;
        }

        static public NodeState OnSuccess(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem(ref planet);

            if (item.hasItemFireWeaponClip)
                Debug.Log("Weapon Reloaded." + item.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");

            return NodeState.Success;
        }

        static public NodeState OnFailure(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            Debug.Log("Fail to reload.");
            return NodeState.Failure;
        }
    }
}
