using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemySwordmanSpawn : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionEnemySwordmanSpawn; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            planet.AddAgent(new Vec2f(x, y), Enums.AgentType.EnemyInsect);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
