//import UnityEngine

using Physics;
using System;
using ECSInput;
using Entitas;
using KMath;
using Enums;
using Unity.VisualScripting;

namespace Agent
{
    public class MovementSystem
    {
        private void UpdateFlying(AgentEntity entity, float deltaTime)
        {
            var physicsState = entity.agentPhysicsState;

            // maximum acceleration in the game
            if (physicsState.Acceleration.Magnitude >= Physics.Constants.MaxAcceleration)
            {
                physicsState.Acceleration = physicsState.Acceleration.Normalized * Physics.Constants.MaxAcceleration;
            }

            if (physicsState.AffectedByFriction)
            {
                Vec2f direction = physicsState.Velocity.Normalized;

                if (Math.Abs(physicsState.Velocity.Magnitude) >= physicsState.Speed)
                    physicsState.Acceleration += 2 * -direction * physicsState.Speed / Physics.Constants.TimeToMax;
                if (Math.Abs(physicsState.Velocity.Magnitude) >= 0.25f)
                    physicsState.Acceleration += -direction * physicsState.Speed / Physics.Constants.TimeToMax;
                else
                    physicsState.Velocity = Vec2f.Zero;
            }

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            // Todo: Use a vector to represent direction.
            if (entity.isAgentAlive && Math.Abs(newVelocity.X) > physicsState.Speed * 0.1f && physicsState.MovementState != AgentMovementState.Stagger)
            {
                if (newVelocity.X > 0)
                {
                    physicsState.MovingDirection = 1;
                }
                else if (newVelocity.X < 0)
                {
                    physicsState.MovingDirection = -1;
                }
            }

            // maximum velocity in the game
            if (newVelocity.Magnitude > Physics.Constants.MaxVelocityY)
                physicsState.Velocity = physicsState.Velocity.Normalized * Physics.Constants.MaxVelocityX;

            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero; // Reset acceleration.
        }

        private void UpdateLand(AgentEntity entity, float deltaTime, Planet.PlanetState planet)
        {

            
            // Note(Joao) Increase gravity and initial velocity for smaller air time during jump. 
            var physicsState = entity.agentPhysicsState;

            if (physicsState.AffectedByGravity && !physicsState.OnGrounded)
            {
                physicsState.Acceleration.Y -= Physics.Constants.Gravity;
            }

            

            // maximum acceleration in the game
            if (physicsState.Acceleration.Y <= -Physics.Constants.MaxAcceleration)
            {
                physicsState.Acceleration.Y = -Physics.Constants.MaxAcceleration;
            }

            if (physicsState.Acceleration.Y >= Physics.Constants.MaxAcceleration)
            {
                physicsState.Acceleration.Y = Physics.Constants.MaxAcceleration;
            }


            if (physicsState.AffectedByFriction)
            {
                if (physicsState.OnGrounded)
                {
                    int sign = Math.Sign(physicsState.Velocity.X);
                    // ground friction at max speed. -> Equal the running acceleration.
                    if (Math.Abs(physicsState.Velocity.X) >= physicsState.Speed)
                    {
                        physicsState.Acceleration.X -= 2 * sign * physicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    if (Math.Abs(physicsState.Velocity.X) >= 0.25f)
                    {
                        physicsState.Acceleration.X -= sign * physicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    else
                    {
                        physicsState.Velocity.X = 0;
                    }
                }
                else
                {
                    // For now air friction is equal ground friction.
                    // Air friction at max speed. -> Equal the running acceleration.
                    int sign = Math.Sign(physicsState.Velocity.X);

                    if (Math.Abs(physicsState.Velocity.X) >= physicsState.Speed)
                    {
                        physicsState.Acceleration.X -= 2 * sign * physicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    if (Math.Abs(physicsState.Velocity.X) >= 0.1f) // Equal half running acceleration.
                    {
                        physicsState.Acceleration.X -= sign * physicsState.Speed / Physics.Constants.TimeToMax;
                    }
                    else
                        physicsState.Velocity.X = 0;
                }
            }


            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;


            if (physicsState.LastMovingDirection == physicsState.MovingDirection)
            {
                physicsState.MovingDistance += displacement.Magnitude;
            }

            physicsState.LastMovingDirection = physicsState.MovingDirection;
            physicsState.LastMovementState = physicsState.MovementState;

            if (entity.isAgentAlive && Math.Abs(newVelocity.X) > physicsState.Speed * 0.1f && 
                physicsState.MovementState != AgentMovementState.Stagger)
            {
                if (newVelocity.X > 0)
                {
                    physicsState.MovingDirection = 1;
                }
                else if (newVelocity.X < 0)
                {
                    physicsState.MovingDirection = -1;
                }
                
            }



            // maximum velocity in the game
            if (newVelocity.Y > Physics.Constants.MaxVelocityY)
            {
                newVelocity.Y = Physics.Constants.MaxVelocityY;
            }
            if (newVelocity.Y < -Physics.Constants.MaxVelocityY)
            {
                newVelocity.Y = -Physics.Constants.MaxVelocityY;
            }


            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero; // Reset acceleration.
        }

        public void UpdateStagger(AgentEntity entity)
        {
            if(entity.agentStagger.Stagger)
            {
                entity.agentStagger.elapsed += UnityEngine.Time.deltaTime;

                if(entity.agentStagger.elapsed > entity.agentStagger.StaggerAffectTime)
                {
                    entity.UnStagger();
                }
            }
            else
            {
                entity.agentStagger.elapsed = 0.0f;
            }
        }

        public void Update()
        {
            UpdateFacingDirections();

            float deltaTime = UnityEngine.Time.deltaTime;
            var EntitiesWithVelocity = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
            foreach (var entity in EntitiesWithVelocity)
            {
                if (GameState.AgentCreationApi.GetMovementProperties((int)entity.agentID.Type).MovType != AgentMovementType.FlyingMovemnt || !entity.isAgentAlive)
                    UpdateLand(entity, deltaTime, GameState.Planet);
                else
                    UpdateFlying(entity, deltaTime);

                UpdateStagger(entity);
            }
        }


        private void UpdateFacingDirections()
        {
            IGroup<AgentEntity> agentEntities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer));
            Vec2f mouseWorldPosition = InputProcessSystem.GetCursorWorldPosition();

            foreach (AgentEntity agentEntity in agentEntities)
            {
                UpdateFacingDirection(agentEntity, mouseWorldPosition);
            }
        }
        
        public void UpdateFacingDirection(AgentEntity agentEntity, Vec2f mouseWorldPosition)
        {
            PhysicsStateComponent physicsStateComponent = agentEntity.agentPhysicsState;
                
            if (agentEntity.CanFaceMouseDirection())
            {
                if (mouseWorldPosition.X >= physicsStateComponent.Position.X) physicsStateComponent.FacingDirection = 1;
                else physicsStateComponent.FacingDirection = -1;
            }
            else physicsStateComponent.FacingDirection = physicsStateComponent.MovingDirection;
        }
    }
}
