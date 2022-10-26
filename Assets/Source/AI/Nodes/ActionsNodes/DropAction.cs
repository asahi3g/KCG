//imports UnityEngine

using KMath;
using Enums;

namespace Node.Action
{
    public class DropAction : NodeBase
    {
        public override NodeType Type => NodeType.DropAction;
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
            ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                inventoryEntity.inventoryEntity.InventoryModelID);

            // Todo: start playing some animation
            if (inventoryModel.HasToolBar)
            {
                int selected = inventoryEntity.inventoryEntity.SelectedSlotID;


                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(GameState.Planet.EntitasContext,
                    agentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                {
                    nodeEntity.nodeExecution.State = NodeState.Fail;
                    return;
                }

                GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, inventoryID, selected);

                // Create item particle from item inventory.
                Vec2f pos = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticleEntity itemParticle = GameState.ItemSpawnSystem.SpawnItemParticle(GameState.Planet.EntitasContext, itemInventory, pos);
                itemParticle.itemPhysicsState.Velocity = new Vec2f(agentEntity.agentPhysicsState.FacingDirection * 8.0f, 8.0f);
                itemParticle.isItemUnpickable = true;
                nodeEntity.ReplaceNodeTool(itemParticle.itemID.ID);

                nodeEntity.nodeExecution.State = NodeState.Running;
                return;
            }

            // ToolBar is non existent. 
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }

        // Action is active untill itemParticle becomes pickable again.
        public override void OnUpdate(NodeEntity nodeEntity)
        {
            const float DURATION = 2.0f;

            float deltaTime = UnityEngine.Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (deltaTime < DURATION)
                return;

            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ItemParticleEntity itemParticle = GameState.Planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            if (nodeEntity.nodeExecution.State == NodeState.Success)
                itemParticle.isItemUnpickable = false;

            base.OnExit(nodeEntity);
        }
    }
}
