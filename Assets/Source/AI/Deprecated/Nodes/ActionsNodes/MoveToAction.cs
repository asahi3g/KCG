//import UnityEngine

using AI.Movement;
using KMath;
using System;
using Enums;

namespace Node.Action
{
    public class MoveToAction : NodeBase
    {
        public override NodeType Type => NodeType.MoveToAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
#if DEBUG
            float deltaTime = UnityEngine.Time.realtimeSinceStartup;
#endif
            const int MAX_FALL_HEIGHT = 6;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var characterMov = new CharacterMovement(agentEntity.agentPhysicsState.InitialJumpVelocity, 
                agentEntity.agentPhysicsState.Speed, MAX_FALL_HEIGHT, agentEntity.physicsBox2DCollider.Size);

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)agentEntity.agentID.Type);
            var movTo = nodeEntity.nodeMoveTo;
            movTo.path = GameState.PathFinding.getPath(planet.TileMap, agentEntity.agentPhysicsState.Position, movTo.GoalPosition, movProperties.MovType, characterMov);

#if DEBUG
            deltaTime = (UnityEngine.Time.realtimeSinceStartup - deltaTime) * 1000f; // get time and transform to ms.
            UnityEngine.Debug.Log("Found time in " + deltaTime.ToString() + "ms");
#endif

            if (movTo.path == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            movTo.pathLength = movTo.path.Length;
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        // Todo: Improve path following algorithm
        public override void OnUpdate(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var movTo = nodeEntity.nodeMoveTo;

            Vec2f targetPos = movTo.path[movTo.pathLength - 1];

#if DEBUG
            GameState.PathFindingDebugSystem.AddPath(ref movTo.path, movTo.pathLength);
#endif
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            Vec2f direction = targetPos - agentEntity.agentPhysicsState.Position;
            if (Math.Abs(direction.X) < 0.2)
            {
                movTo.reachedX = true;
            }
            if (Math.Abs(direction.Y) < 0.2f)
            {
                movTo.reachedY = true;
            }

            if (movTo.reachedX && movTo.reachedY)
            {
                if (--movTo.pathLength == 0)
                {
                    nodeEntity.nodeExecution.State = NodeState.Success;
                    return;
                }
                movTo.reachedX = false;
                movTo.reachedY = false;
            }

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)agentEntity.agentID.Type);

            if (movProperties.MovType == AgentMovementType.FlyingMovemnt)
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
                    planet.TileMap.GetFrontTileID((int)agentEntity.agentPhysicsState.Position.X, (int)agentEntity.agentPhysicsState.Position.Y - 1) == Enums.PlanetTileMap.TileID.Platform)
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