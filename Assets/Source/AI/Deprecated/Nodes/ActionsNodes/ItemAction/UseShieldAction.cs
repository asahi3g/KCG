using Enums;

namespace Node
{
    public class UseShieldAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.UseShieldAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            if (!agentEntity.agentPhysicsState.Invulnerable)
                agentEntity.agentPhysicsState.Invulnerable = true;
            else
                agentEntity.agentPhysicsState.Invulnerable = false;
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
