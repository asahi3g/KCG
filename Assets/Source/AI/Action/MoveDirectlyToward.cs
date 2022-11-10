using AI;
using Condition;
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
            public int EndConditionId; // Condition ID to end movement scene.
        }

        // Todo: Add non reachable check.
        static public NodeState Action(object ptr, int index)
        {
            // Get Data
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState data = ref NodesExecutionState.GetRef((ulong)ptr);
            int endConditionId = data.GetNodeData<int>(index);
            ref float acceptableRadius = ref data.GetNodeData<float>(index);

            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            var physicsState = agent.agentPhysicsState;
            ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agent.agentController.BlackboardID);
            int direction = Math.Sign(blackboard.AttackTarget.X - physicsState.Position.X);

            // Walk diagonal if there is an obstacle jump.
            TileID belowTile = planet.TileMap.GetFrontTileID((int)(physicsState.Position.X), (int)physicsState.Position.Y - 1);
            if (belowTile != TileID.Air)
            {
                ConditionManager.Condition condition = GameState.ConditionManager.Get(endConditionId);
                if (condition(ptr))
                    return NodeState.Success;

                physicsState.FacingDirection = direction;
                agent.SetAimTarget(new KMath.Vec2f(physicsState.Position.X + direction, agent.GetGunFiringPosition().Y));
                agent.Run(direction);

                // if next tile is solid jump.
                TileID frontTileIDX = planet.TileMap.GetFrontTileID((int)(physicsState.Position.X + direction), (int)physicsState.Position.Y);
                if (frontTileIDX != TileID.Air && agent.agentPhysicsState.MovementState != Enums.AgentMovementState.Jump)
                    agent.Jump();
            }

            return NodeState.Running;
        }
    }
}
