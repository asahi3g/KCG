using Enums;

namespace Node
{
    public class UseShieldAction : NodeBase
    {
        public override ActionType Type => ActionType.UseShieldAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            var planet = GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            var WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            if (!agentEntity.agentPhysicsState.Invulnerable)
                agentEntity.agentPhysicsState.Invulnerable = true;
            else
                agentEntity.agentPhysicsState.Invulnerable = false;
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
