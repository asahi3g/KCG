using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionPlaceChest : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionPlaceChest;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            GameState.Planet.AddMech(new Vec2f(x, y), MechType.Storage);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
