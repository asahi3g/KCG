using AI;
using Enums.PlanetTileMap;
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
            var physicsState = agent.agentPhysicsState;
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            int direction = Math.Sign(blackboard.AttackTarget.X - physicsState.Position.X);

            // Walk diagonal if there is an obstacle jump.
            float range = 10.0f; // Todo: get correct weapong range.
            if ((blackboard.AttackTarget - physicsState.Position).Magnitude < range)
                return NodeState.Success;
            if (direction == 0)
                return NodeState.Failure;

            physicsState.FacingDirection = direction;
            agent.Run(direction);

            // if next tile is solid jump.
            TileID frontTileIDX = planet.TileMap.GetFrontTileID((int)(physicsState.Position.X + direction), (int)physicsState.Position.Y);
            if (frontTileIDX != TileID.Air && agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Jump)
                agent.Jump();

            return NodeState.Running;
        }
    }
}
