using UnityEngine;
using Enums;
using KMath;

namespace Node.Action
{
    public class MechPlacementAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.MechPlacementAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            planet.AddMech(new Vec2f(x, y), itemEntity.itemMech.MechID);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, 
                    itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
            base.OnExit(ref planet, nodeEntity);
        }
    }
}
