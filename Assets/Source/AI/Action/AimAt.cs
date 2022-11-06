using NodeSystem;
using AI;
using Planet;

namespace Action
{
    public class AimAt
    {
        static public NodeState Action(object ptr, int id)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            agent.SetAimTarget(blackboard.AttackTarget);
            return NodeState.Success;
        }
    }
}
