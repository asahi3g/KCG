using UnityEngine;
using KMath;
using Enums;

namespace Node.Action
{
    public class ToolActionEnemySpawn : NodeBase
    {
        public override ActionType  Type => ActionType .ToolActionEnemySpawn;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            GameState.Planet.AddAgent(new Vec2f(x, y), AgentType.InsectLarge, Agent.AgentFaction.InsectsEnemy);
            GameState.ActionCoolDownSystem.SetCoolDown(nodeEntity.nodeID.TypeID, nodeEntity.nodeOwner.AgentID, 0.5f);

            nodeEntity.nodeExecution.State =  NodeState.Success;
        }
    }
}
