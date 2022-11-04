using KMath;
using AI;
using NodeSystem;
using Planet;
using System;

namespace Action
{
    // Movement action with no pathing.
    public class MoveDirectlyToward
    {
        struct Data
        {
            public float AcceptableRadius;
        }

        static public NodeState Action(object ptr, int index)
        {
            // Get Data
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            ref float acceptableRadius = ref data.GetNodeData<float>(index);

            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            int direction = Math.Sign(blackboard.MoveToTarget.X - agent.agentPhysicsState.Position.X);

            // Walk diagonal if there is an obstacle jump.
            float range = 10.0f; // Todo: get correct weapong range.
            if ((blackboard.AttackTarget - agent.agentPhysicsState.Position).Magnitude < range)
                return NodeState.Success;
            if (direction == 0)
                return NodeState.Failure;

            agent.agentPhysicsState.FacingDirection = direction;
            agent.Run(direction);
            return NodeState.Running;
        }
    }
}
