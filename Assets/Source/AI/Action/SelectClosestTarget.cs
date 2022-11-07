using AI;
using Planet;
using NodeSystem;
using BehaviorTree;

namespace Action
{
    public class SelectClosestTarget
    {
        static public NodeState Action(object ptr, int id)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            AgentEntity target = null;
            float dist = float.MaxValue;
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive || agent.agentID.Faction == entity.agentID.Faction)
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
                return NodeState.Failure;
            }
            else
            {
                blackboard.AttackTarget = target.agentPhysicsState.Position + target.physicsBox2DCollider.Size * 1f / 2f;
                blackboard.AgentTargetID = target.agentID.ID;
                blackboard.UpdateTarget = true;
                return NodeState.Success;
            }
        }
    }
}
