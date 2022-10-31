using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemyGunnerSpawn : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionEnemyGunnerSpawn;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            GameState.Planet.AddAgent(new Vec2f(x, y), AgentType.EnemyHeavy);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
