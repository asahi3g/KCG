using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemySpawn : NodeBase
    {
        public override NodeType Type => NodeType.ToolActionEnemySpawn;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            GameState.Planet.AddAgentAsEnemy(new Vec2f(x, y));

            nodeEntity.nodeExecution.State =  NodeState.Success;
        }
    }
}
