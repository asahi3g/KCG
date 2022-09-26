using AI.Movement;
using KMath;
using System;
using UnityEngine;
using Enums;

namespace Node.Action
{
    public class MoveAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.MoveAction; } }

        // Todo: Move this to a blackboard/whiteboard?
        Vec2f[] path;
        int pathLength;
        bool reachedX = false;
        bool reachedY = false;

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
#if DEBUG
            float deltaTime = Time.realtimeSinceStartup;
#endif
            const int MAX_FALL_HEIGHT = 6;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var characterMov = new CharacterMovement(agentEntity.agentPhysicsState.InitialJumpVelocity, 
                agentEntity.agentPhysicsState.Speed, MAX_FALL_HEIGHT, agentEntity.physicsBox2DCollider.Size);

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)agentEntity.agentID.Type);
            Vec2f goalPosition = nodeEntity.nodeMoveTo.GoalPosition;
            path = GameState.PathFinding.getPath(planet.TileMap, agentEntity.agentPhysicsState.Position, goalPosition, movProperties.MovType, characterMov);

#if DEBUG
            deltaTime = (Time.realtimeSinceStartup - deltaTime) * 1000f; // get time and transform to ms.
            Debug.Log("Found time in " + deltaTime.ToString() + "ms");
#endif

            if (path == null)
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            pathLength = path.Length;
            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
        }

        // Todo: Improve path following algorithm
        public override void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            Vec2f targetPos = path[pathLength - 1];

#if DEBUG
            GameState.PathFindingDebugSystem.AddPath(ref path, pathLength);
#endif
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            Vec2f direction = targetPos - agentEntity.agentPhysicsState.Position;
            if (Math.Abs(direction.X) < 0.2)
            {
                reachedX = true;
            }
            if (Math.Abs(direction.Y) < 0.2f)
            {
                reachedY = true;
            }

            if (reachedX && reachedY)
            {
                if (--pathLength == 0)
                {
                    nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                    return;
                }
                reachedX = false;
                reachedY = false;
            }

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)agentEntity.agentID.Type);

            if (movProperties.MovType == Enums.AgentMovementType.FlyingMovemnt)
            {
                direction.Normalize();
                agentEntity.agentPhysicsState.Acceleration = direction * 2 * agentEntity.agentPhysicsState.Speed / Physics.Constants.TimeToMax;
            }
            else
            {
                // Jumping is just an increase in velocity.
                const float THRESHOLD = 0.1f;
                if ((direction.Y > THRESHOLD) && agentEntity.agentPhysicsState.OnGrounded)
                {
                    agentEntity.agentPhysicsState.Velocity.Y = agentEntity.agentPhysicsState.InitialJumpVelocity;
                }
                if (direction.Y < -THRESHOLD && agentEntity.agentPhysicsState.OnGrounded &&
                    planet.TileMap.GetFrontTileID((int)agentEntity.agentPhysicsState.Position.X, (int)agentEntity.agentPhysicsState.Position.Y - 1) == Enums.Tile.TileID.Platform)
                {
                    agentEntity.agentPhysicsState.Droping = true;
                }

                // Todo: deals with flying agents.
                direction.Y = 0;
                direction.Normalize();
                agentEntity.agentPhysicsState.Acceleration.X = direction.X * 2 * agentEntity.agentPhysicsState.Speed / Physics.Constants.TimeToMax;
            }
        }
    }
}