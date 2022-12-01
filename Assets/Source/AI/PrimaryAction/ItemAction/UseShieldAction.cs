using NodeSystem;
using Planet;
using Unity.Collections.LowLevel.Unsafe;

namespace Action
{
    public class UseShieldAction
    {
        // Action used by either player and AI.
        static public NodeState Action(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID); 
            ItemInventoryEntity itemEntity = agentEntity.GetItem();
            Item.FireWeaponProperties fireWeaponProperties = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            if (!agentEntity.agentPhysicsState.Invulnerable)
                agentEntity.agentPhysicsState.Invulnerable = true;
            else
                agentEntity.agentPhysicsState.Invulnerable = false;

            return NodeState.Success;
        }
    }
}
