using KMath;
using UnityEngine;
using Planet;
using NodeSystem;
using Unity.Collections.LowLevel.Unsafe;
using NodeSystem.BehaviorTree;


namespace Action
{
    public class ChargeAction
    {
        static public NodeState OnEnter(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item == null)
            {
                return NodeState.Failure;
            }
            
            bool isChargable = item.hasItemFireWeaponCharge;
            if (isChargable)
            {
                return NodeState.Running;
            }

            return NodeState.Failure;
        }

        static public NodeState OnUpdate(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            if (item.itemFireWeaponCharge.ChargeRate < item.itemFireWeaponCharge.ChargeMax)
            {
                item.itemFireWeaponCharge.ChargeRate += item.itemFireWeaponCharge.ChargeRatio;
                return NodeState.Success;
            }

            return NodeState.Running;
        }

        static public NodeState OnSucess(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity item = agentEntity.GetItem();

            Item.FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);
            float tempCharge = item.itemFireWeaponCharge.ChargeRate;
            float difference = item.itemFireWeaponCharge.ChargeRate - tempCharge;
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
