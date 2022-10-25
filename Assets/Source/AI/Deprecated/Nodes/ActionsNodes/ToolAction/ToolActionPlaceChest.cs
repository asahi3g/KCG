using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionPlaceChest : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionPlaceChest; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            planet.AddMech(new Vec2f(x, y), Enums.MechType.Storage);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
