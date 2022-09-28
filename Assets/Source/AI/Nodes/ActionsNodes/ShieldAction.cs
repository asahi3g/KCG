using Inventory;
using Planet;
using Enums;

namespace Node.Action
{
    public class ShieldAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ShieldAction; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity) 
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (!agentEntity.hasAgentInventory)
                return;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            EntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                if(itemEntity.itemType.Type is Enums.ItemType.Sword or Enums.ItemType.StunBaton)
                {
                    agentEntity.agentPhysicsState.Invulnerable = !agentEntity.agentPhysicsState.Invulnerable;
                }

                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            base.OnExit(ref planet, nodeEntity);
        }
    }
}
