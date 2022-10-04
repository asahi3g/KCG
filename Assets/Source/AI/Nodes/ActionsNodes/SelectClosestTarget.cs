using AI;
using Enums;
using Planet;
using KMath;

namespace Node.Action
{
    public class SelectClosestTarget : NodeBase
    {
        public override NodeType Type { get { return NodeType.SelectClosestTarget; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            AgentEntity target = null;
            float dist = float.MaxValue;
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive)
                    continue;

                float newDist = (agent.agentPhysicsState.Position - entity.agentPhysicsState.Position).Magnitude;
                if (dist > newDist)
                {
                    target = entity;
                    dist = newDist;
                }
            }

            if (target == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
            }
            else
            {
                nodeEntity.nodeExecution.State = NodeState.Success;
                BlackBoard blackBoard = agent.agentController.Controller.BlackBoard;
                blackBoard.Set(nodeEntity.nodeBlackboardData.DataID, target.agentPhysicsState.Position + target.physicsBox2DCollider.Size * 1f / 2f);
            }
        }
    }
}
