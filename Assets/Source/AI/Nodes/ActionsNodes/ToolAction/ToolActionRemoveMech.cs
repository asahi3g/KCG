using UnityEngine;
using Enums;

namespace Node
{
    public class ToolActionRemoveMech : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionRemoveMech;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            var mech = GameState.Planet.GetMechFromPosition(new KMath.Vec2f(x, y));

            if (mech != null)
            {
                GameState.Planet.Player.UseTool(0.2f);

                GameState.Planet.RemoveMech(mech.mechID.Index);
            }

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
