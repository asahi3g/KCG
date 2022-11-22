//imports UnityEngine

using KMath;
using Enums;

namespace Node.Action
{
    public class DropAction : NodeBase
    {
        public override ItemUsageActionType  Type => ItemUsageActionType .DropAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            ref Inventory.InventoryTemplateData inventoryModel = ref GameState.InventoryCreationApi.Get(
                inventoryEntity.inventoryInventory.InventoryModelID);

            // Todo: start playing some animation
            if (inventoryModel.HasToolBar)
            {
                int selected = inventoryEntity.inventoryInventory.SelectedSlotID;


                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(agentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                {
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    return;
                }

                GameState.InventoryManager.RemoveItem(inventoryID, selected);

                // Create item particle from item inventory.
                Vec2f pos = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticleEntity itemParticle = GameState.Planet.AddItemParticle(itemInventory, pos);
                itemParticle.itemPhysicsState.Velocity = new Vec2f(agentEntity.agentPhysicsState.FacingDirection * 8.0f, 8.0f);
                itemParticle.AddItemItemParticleAttributeUnpickable(0);
                nodeEntity.ReplaceNodeTool(itemParticle.itemID.ID);

                nodeEntity.nodeExecution.State = NodeState.Running;
                return;
            }

            // ToolBar is non existent. 
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }
    }
}
