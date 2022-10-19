using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemyGunnerSpawn : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionEnemyGunnerSpawn; } }
        public override bool IsPlayerOnly { get { return true; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            planet.AddAgent(new Vec2f(x, y), Enums.AgentType.EnemyHeavy);

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
