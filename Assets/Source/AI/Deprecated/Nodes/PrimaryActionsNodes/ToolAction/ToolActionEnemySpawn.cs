using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemySpawn : NodeBase
    {
        public override ItemUsageActionType  Type => ItemUsageActionType .ToolActionEnemySpawn;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            GameState.Planet.AddAgentAsEnemy(new Vec2f(x, y));
            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, nodeEntity.nodeOwner.AgentID, 0.5f);

            nodeEntity.nodeExecution.State =  NodeState.Success;
        }
    }
}
