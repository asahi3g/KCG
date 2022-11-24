using AI;
using AI.Movement;
using KMath;
using NodeSystem;
using Planet;
using System;

namespace Action
{
    public class MoveToBestScorePos
    {
        static public NodeState Action(object ptr, int index)
        {
            // Get Data
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            var physicsState = agent.agentPhysicsState;
            
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            blackboard.MoveToTarget = GameState.MovementPositionScoreSystem.GetHighestScorePosition(agent);
            if (MathF.Abs(physicsState.Position.X - blackboard.MoveToTarget.X) < 0.25f)
            {
                return NodeState.Success;
            }


            int direction = Math.Sign(blackboard.MoveToTarget.X - physicsState.Position.X);
            // Walk diagonal if there is an obstacle jump.  

            physicsState.FacingDirection = direction;
            agent.SetAimTarget(new KMath.Vec2f(physicsState.Position.X + direction, agent.GetGunFiringPosition().Y));
            agent.Walk(direction);
            // Todo: Fix bug character get stuck in diagonals when running.(Bug is ralated to speed.)
            //agent.Run(direction);

            // If we can't walk to next tile jump.
            if (!PathFollowing.IsPathFree(new KMath.Vec2i((int)(physicsState.Position.X), (int)physicsState.Position.Y), direction)
                && agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Jump)
            {
                agent.Jump();
            }

            return NodeState.Running;
        }
    }
}
