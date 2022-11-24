//imports UnityEngine

using Enums;
using KMath;

namespace Node.Action
{
    public class MechPlacementAction : NodeBase
    {
        public override ItemUsageActionType  Type => ItemUsageActionType .MechPlacementAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
            int x = (int)worldPosition.X;
            int y = (int)worldPosition.Y;

            planet.AddMech(new Vec2f(x, y), itemEntity.itemMechPlacement.MechID);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        public override void OnExit(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(agentEntity.agentInventory.InventoryID, 
                    itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
            base.OnExit(nodeEntity);
        }
    }
}
