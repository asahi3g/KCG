using UnityEngine;
using KMath;
using Enums;
using Item;

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
            AgentEntity marine = GameState.Planet.AddAgent(new Vec2f(x, y), AgentType.EnemyMarine, Agent.AgentFaction.MarineEnemy);
            marine.agentID.SquadID = 0;
            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, nodeEntity.nodeOwner.AgentID, 0.5f);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
