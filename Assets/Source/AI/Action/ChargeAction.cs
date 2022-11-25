using UnityEngine;
using Planet;
using NodeSystem;
using Unity.Collections.LowLevel.Unsafe;
using BehaviorTree;


namespace Action
{
    public class ChargeAction
    {
        static public NodeState OnEnter(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item == null)
            {
                return NodeState.Failure;
            }
            
            bool isChargable = item.hasItemFireWeaponChargedWeapon;
            if (isChargable)
            {
                return NodeState.Running;
            }

            return NodeState.Failure;
        }

        static public NodeState OnUpdate(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item.itemFireWeaponChargedWeapon.ChargeRate < item.itemFireWeaponChargedWeapon.ChargeMax)
            {
                item.itemFireWeaponChargedWeapon.ChargeRate += item.itemFireWeaponChargedWeapon.ChargeRatio;
                return NodeState.Success;
            }

            return NodeState.Running;
        }

        static public NodeState OnSucess(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            Item.FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);
            float tempCharge = item.itemFireWeaponChargedWeapon.ChargeRate;
            float difference = item.itemFireWeaponChargedWeapon.ChargeRate - tempCharge;
            Debug.Log("Weapon Charged: " + difference.ToString());
            return NodeState.Success;
        }

        static public NodeState Onfailure(object objData, int id)
        {
            Debug.Log("Reload Failed.");
            return NodeState.Failure;
        }
    }
}
