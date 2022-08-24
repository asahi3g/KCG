using Entitas;
using KMath;
using System;
using UnityEngine;

namespace Action
{
    public class MoveAction : ActionBase
    {
        private ItemParticleEntity ItemParticle;
        Vec2f[] path;
        int pathLength;
        bool reachedX = false;
        bool reachedY = false;

        public MoveAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
#if DEBUG
            float deltaTime = Time.realtimeSinceStartup;
#endif
            const int MAX_FALL_HEIGHT = 6;

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)AgentEntity.agentID.Type);
            Vec2f goalPosition = ActionEntity.actionMoveTo.GoalPosition;

            path = GameState.PathFinding.getPath(planet.TileMap, AgentEntity.agentPhysicsState.Position, goalPosition, movProperties.MovType,
                new AI.Movement.CharacterMovement(AgentEntity.agentPhysicsState.InitialJumpVelocity, AgentEntity.agentPhysicsState.Speed,
                    MAX_FALL_HEIGHT, AgentEntity.physicsBox2DCollider.Size));

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

        // Todo: Improve path following algorithm
        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            Vec2f targetPos = path[pathLength - 1];

#if DEBUG
            GameState.PathFindingDebugSystem.AddPath(ref path, pathLength);
#endif

            Vec2f direction = targetPos - AgentEntity.agentPhysicsState.Position;
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
                    ActionEntity.actionExecution.State = Enums.ActionState.Success;
                    return;
                }
                reachedX = false;
                reachedY = false;
            }

            Agent.MovementProperties movProperties = GameState.AgentCreationApi.GetMovementProperties((int)AgentEntity.agentID.Type);

            if (movProperties.MovType == Enums.AgentMovementType.FlyingMovemnt)
            {
                direction.Normalize();
                AgentEntity.agentPhysicsState.Acceleration = direction * 2 * AgentEntity.agentPhysicsState.Speed / Physics.Constants.TimeToMax;
            }
            else
            {
                // Jumping is just an increase in velocity.
                const float THRESHOLD = 0.1f;
                if ((direction.Y > THRESHOLD) && AgentEntity.agentPhysicsState.OnGrounded)
                {
                    AgentEntity.agentPhysicsState.Velocity.Y = AgentEntity.agentPhysicsState.InitialJumpVelocity;
                }
                if (direction.Y < -THRESHOLD && AgentEntity.agentPhysicsState.OnGrounded &&
                    planet.TileMap.GetFrontTileID((int)AgentEntity.agentPhysicsState.Position.X, (int)AgentEntity.agentPhysicsState.Position.Y - 1) == Enums.Tile.TileID.Platform)
                {
                    AgentEntity.agentPhysicsState.Droping = true;
                }

                // Todo: deals with flying agents.
                direction.Y = 0;
                direction.Normalize();
                AgentEntity.agentPhysicsState.Acceleration.X = direction.X * 2 * AgentEntity.agentPhysicsState.Speed / Physics.Constants.TimeToMax;
            }
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