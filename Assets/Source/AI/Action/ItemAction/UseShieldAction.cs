using NodeSystem.BehaviorTree;
using Enums;
using Planet;
using Unity.Collections.LowLevel.Unsafe;

namespace Action
{
    public class UseShieldAction
    {
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID); 
            ItemInventoryEntity itemEntity = agentEntity.GetItem(ref planet);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            if (!agentEntity.agentPhysicsState.Invulnerable)
                agentEntity.agentPhysicsState.Invulnerable = true;
            else
                agentEntity.agentPhysicsState.Invulnerable = false;

            return NodeState.Success;
        }
    }
}
