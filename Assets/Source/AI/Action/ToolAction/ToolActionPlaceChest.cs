using UnityEngine;
using KMath;
using Enums;

namespace Action
{
    public class ToolActionPlaceChest
    {
        public void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            planet.AddMech(new Vec2f(x, y), Enums.MechType.Storage);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
