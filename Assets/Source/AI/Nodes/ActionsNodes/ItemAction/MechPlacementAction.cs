//imports UnityEngine

using Enums;
using KMath;

namespace Node.Action
{
    public class MechPlacementAction : NodeBase
    {
        public override NodeType Type => NodeType.MechPlacementAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            GameState.Planet.AddMech(new Vec2f(x, y), itemEntity.itemMech.MechID);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, agentEntity.agentInventory.InventoryID, 
                    itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
            base.OnExit(nodeEntity);
        }
    }
}
