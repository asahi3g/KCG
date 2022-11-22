using Inventory;
using Planet;
using NodeSystem;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using BehaviorTree;

namespace Action
{
    public class ShieldAction
    {
        // Ability doesn't need an item.
        // Action used by either player and AI.
        static public NodeState Action(object objData, int index)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);

            if (!agentEntity.hasAgentInventory)
                return NodeState.Failure;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            InventoryEntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryInventoryEntity;
            ref InventoryTemplateData InventoryEntityTemplate = ref GameState.InventoryCreationApi.Get(inventory.InventoryEntityTemplateID);

            if (InventoryEntityTemplate.HasToolBar)  
            {
                int selectedSlot = inventory.SelectedSlotID;
                ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(inventoryID, selectedSlot);
                if(itemEntity != null)
                {
                    if(itemEntity.itemType.Type is Enums.ItemType.Sword or Enums.ItemType.StunBaton)
                    {
                        if(agentEntity.hasAgentPhysicsState)
                            agentEntity.agentPhysicsState.Invulnerable = !agentEntity.agentPhysicsState.Invulnerable;
                    }
                }
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
    }
}
