using Entitas;
using UnityEngine;
using KMath;
using Enums;
using static UnityEditor.PlayerSettings;

namespace Node.Action
{
    public class DropAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.DropAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            if (!agentEntity.hasAgentInventory)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

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
                itemParticle.itemPhysicsState.Velocity = new Vec2f(agentEntity.agentPhysicsState.FacingDirection * 8.0f, 8.0f);
                itemParticle.isItemUnpickable = true;
                nodeEntity.ReplaceNodeTool(itemParticle.itemID.ID);

                nodeEntity.nodeExecution.State = Enums.NodeState.Running;
                return;
            }

            // ToolBar is non existent. 
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        // Action is active untill itemParticle becomes pickable again.
        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            const float DURATION = 2.0f;

            float deltaTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (deltaTime < DURATION)
                return;

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemParticleEntity itemParticle = planet.EntitasContext.itemParticle.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            if (nodeEntity.nodeExecution.State == Enums.NodeState.Success)
                itemParticle.isItemUnpickable = false;

            base.OnExit(ref planet, nodeEntity);
        }
    }
}
