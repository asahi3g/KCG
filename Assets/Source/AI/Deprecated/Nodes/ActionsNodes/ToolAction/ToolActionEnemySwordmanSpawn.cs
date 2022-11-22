using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemySwordmanSpawn : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.ToolActionEnemySwordmanSpawn;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            GameState.Planet.AddAgent(new Vec2f(x, y), AgentType.EnemyInsect, 1);

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
