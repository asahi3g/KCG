using Inventory;
using Planet;
using NodeSystem;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using NodeSystem.BehaviorTree;

namespace Action
{
    public class ShieldAction
    {
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);

            if (!agentEntity.hasAgentInventory)
                return NodeState.Failure;

            int inventoryID = agentEntity.agentInventory.InventoryID;
            EntityComponent inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID).inventoryEntity;
            ref InventoryModel inventoryModel = ref GameState.InventoryCreationApi.Get(inventory.InventoryModelID);

            if (inventoryModel.HasToolBar)
            {
                int selectedSlot = inventory.SelectedSlotID;
                ItemInventoryEntity itemEntity = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
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
