using AI;
using Planet;
using NodeSystem;
using NodeSystem.BehaviorTree;

namespace Action
{
    public class SelectClosestTarget
    {
        static public NodeState Action(object ptr, int id)
        {
            ref BehaviorTreeState data = ref BehaviorTreeState.GetRef((ulong)ptr);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(data.BlackboardID);
            ref PlanetState planet = ref GameState.Planet;

            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
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
                return NodeState.Failure;
            }
            else
            {
                blackboard.ShootingTarget = target.agentPhysicsState.Position + target.physicsBox2DCollider.Size * 1f / 2f;
                return NodeState.Success;
            }
        }
    }
}
