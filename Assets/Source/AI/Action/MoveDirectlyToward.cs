using AI;
using AI.Movement;
using UnityEngine ;
using Enums.PlanetTileMap;
using NodeSystem;
using Planet;
using System;

namespace Action
{
    // Movement action with no pathing.
    // Action used only by AI.
    public class MoveDirectlyToward
    {
        struct Data
        {
            public int EndConditionId; // Condition ID to end movement scene.
        }

        // Todo: Add non reachable check.
        static public NodeState Action(object ptr, int index)
        {
            // Get Data
            PlanetState planet = GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            int endConditionId = data.GetNodeData<int>(index);
            ref float acceptableRadius = ref data.GetNodeData<float>(index);

            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            var physicsState = agent.agentPhysicsState;
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);

            // Walk diagonal if there is an obstacle jump.
            ConditionManager.Condition condition = GameState.ConditionManager.Get(endConditionId);
            if (condition(ptr))
                return NodeState.Success;

            int direction = Math.Sign(blackboard.AttackTarget.X - physicsState.Position.X);
            physicsState.FacingDirection = direction;
            agent.SetAimTarget(new KMath.Vec2f(physicsState.Position.X + direction, agent.GetGunFiringPosition().Y));
            agent.Walk(direction);
            // Todo: Fix bug character get stuck in diagonals when running.(Bug is ralated to speed.)
            //agent.Run(direction);
            
            // If we can't walk to next tile jump.
            if (!PathFollowing.IsPathFree(new KMath.Vec2i((int)(physicsState.Position.X), (int)physicsState.Position.Y), direction)
                && agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Jump)
            {

                // if next tile is solid jump.
                TileID frontTileIDX = planet.TileMap.GetFrontTileID((int)(physicsState.Position.X + direction), (int)physicsState.Position.Y);

                if (frontTileIDX != TileID.Air &&
                    agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Jump &&
                    agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Falling)
                {
                    agent.Jump();
                }
            }

            return NodeState.Running;
        }
    }
}
