using UnityEngine;
using Enums;
using KMath;

namespace Action
{
    // Action used by either player and AI.
    public class MechPlacementAction
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            planet.AddMech(new Vec2f(x, y), itemEntity.itemMechPlacement.MechID);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(agentEntity.agentInventory.InventoryID, 
                    itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
        }
    }
}
