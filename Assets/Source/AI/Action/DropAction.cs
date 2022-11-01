using KMath;
using Planet;
using NodeSystem;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using NodeSystem.BehaviorTree;

namespace Action
{
    public class DropAction
    {
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);

            if (!agentEntity.hasAgentInventory)
                return NodeState.Failure;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                inventoryEntity.inventoryEntity.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selected = inventoryEntity.inventoryEntity.SelectedSlotID;

                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(
                    agentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                    return NodeState.Failure;

                GameState.InventoryManager.RemoveItem(inventoryID, selected);

                // Create item particle from item inventory.
                Vec2f pos = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticleEntity itemParticle = GameState.ItemSpawnSystem.SpawnItemParticle(itemInventory, pos);
                itemParticle.itemPhysicsState.Velocity = new Vec2f(agentEntity.agentPhysicsState.FacingDirection * 8.0f, 8.0f);
                itemParticle.isItemUnpickable = true;

                return NodeState.Success;
            }

            // ToolBar is non existent. 
            return NodeState.Failure;
        }

        // Todo: Create system that remove unpickable after two seconds?
        public void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            const float DURATION = 2.0f;

            float deltaTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (deltaTime < DURATION)
                return;

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemParticleEntity itemParticle = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            if (nodeEntity.nodeExecution.State == Enums.NodeState.Success)
                itemParticle.isItemUnpickable = false;
        }
    }
}
