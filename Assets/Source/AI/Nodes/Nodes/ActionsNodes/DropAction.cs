using Entitas;
using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class DropAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.DropAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
                return;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntity inventoryEntity = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            ref Inventory.InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(
                inventoryEntity.inventoryEntity.InventoryModelID);

            // Todo: start playing some animation
            if (inventoryModel.HasToolBar)
            {
                int selected = inventoryEntity.inventoryEntity.SelectedSlotID;


                ItemInventoryEntity itemInventory = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                    agentEntity.agentInventory.InventoryID, selected);
                if (itemInventory == null)
                {
                    nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                    return;
                }

                GameState.InventoryManager.RemoveItem(planet.EntitasContext, inventoryID, selected);

                // Create item particle from item inventory.
                Vec2f pos = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size / 2f;
                ItemParticleEntity itemParticle = GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, itemInventory, pos);
                itemParticle.itemPhysicsState.Velocity = new Vec2f(-8.0f, 8.0f);
                itemParticle.isItemUnpickable = true;

                nodeEntity.nodeExecution.State = Enums.NodeState.Running;
                return;
            }

            // ToolBar is non existent. 
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        // Action is active untill itemParticle becomes pickable again.
        // Todo: Create an unpickable system outside of here.
        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            NodeProperties nodeProperties = GameState.ActionCreationApi.Get(nodeEntity.nodeID.TypeID);

            float deltaTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (deltaTime < nodeProperties.Duration)
                return;

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            //if (nodeEntity.nodeExecution.State == Enums.NodeState.Success)
            //    ItemParticle.isItemUnpickable = false;

            base.OnExit(ref planet, nodeEntity);
        }
    }
}
