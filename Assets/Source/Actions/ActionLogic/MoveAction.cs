using Entitas;
using KMath;
using System.IO;
using UnityEngine;

namespace Action
{
    public class MoveAction : ActionBase
    {
        private ItemParticleEntity ItemParticle;
        Vec2f [] path;
        int pathLength;
        bool availableJump = true;

        public MoveAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
#if DEBUG
            float deltaTime = Time.realtimeSinceStartup;
#endif

            Vec2f goalPosition = ActionEntity.actionMoveTo.GoalPosition;
            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)AgentEntity.agentID.Type);
            path = GameState.PathFinding.getPath(ref planet.TileMap, AgentEntity.agentPhysicsState.Position, goalPosition, movProperties.MovType);

#if DEBUG
            deltaTime = (Time.realtimeSinceStartup - deltaTime) * 1000f; // get time and transform to ms.
            Debug.Log("Found time in " + deltaTime.ToString() + "ms");
#endif

            if (path == null)
            {
                ActionEntity.actionExecution.State = Enums.ActionState.Fail;
                return;
            }

            pathLength = path.Length;
            ActionEntity.actionExecution.State = Enums.ActionState.Running;
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            Vec2f targetPos = path[pathLength - 1];

            GameState.PathFindingDebugSystem.AddPath(ref path, pathLength);

            Vec2f direction = targetPos - AgentEntity.agentPhysicsState.Position;

            if (direction.Magnitude < 0.1f)
            {
                if (--pathLength == 0)
                {
                    ActionEntity.actionExecution.State = Enums.ActionState.Success;
                    return;
                }
            }

            // Jumping is just an increase in velocity.
            if (direction.Y > 0 && AgentEntity.agentPhysicsState.OnGrounded)
            {
                AgentEntity.agentPhysicsState.Velocity.Y = 14f;
            }

            // Todo: deals with flying agents.
            direction.Y = 0;
            direction.Normalize();
            AgentEntity.agentPhysicsState.Acceleration.X = direction.X * 2 * AgentEntity.agentPhysicsState.Speed / Physics.Constants.TimeToMax;
        }

        public override void OnExit(ref Planet.PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }
    // Factory Method
    public class MoveActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new MoveAction(entitasContext, actionID);
        }
    }
}