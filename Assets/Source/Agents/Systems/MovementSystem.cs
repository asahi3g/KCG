using Physics;
using System;
using KMath;
using UnityEngine;
using Enums;

namespace Agent
{
    public class MovementSystem
    {
        private void UpdateFlying(AgentEntity entity, float deltaTime)
        {
            var physicsState = entity.agentPhysicsState;

            // maximum acceleration in the game
            if (physicsState.Acceleration.Magnitude >= Constants.MaxAcceleration)
            {
                physicsState.Acceleration = physicsState.Acceleration.Normalized * Constants.MaxAcceleration;
            }

            if (physicsState.AffectedByFriction)
            {
                Vec2f direction = physicsState.Velocity.Normalized;

                if (Math.Abs(physicsState.Velocity.Magnitude) >= physicsState.Speed)
                    physicsState.Acceleration += 2 * -direction * physicsState.Speed / Constants.TimeToMax;
                if (Math.Abs(physicsState.Velocity.Magnitude) >= 0.25f)
                    physicsState.Acceleration += -direction * physicsState.Speed / Constants.TimeToMax;
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
            if (newVelocity.Magnitude > Constants.MaxVelocityY)
                physicsState.Velocity = physicsState.Velocity.Normalized * Constants.MaxVelocityX;

            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero; // Reset acceleration.
        }

        private void UpdateLand(AgentEntity entity, float deltaTime)
        {
            // Note(Joao) Increase gravity and initial velocity for smaller air time during jump. 
            var physicsState = entity.agentPhysicsState;

            if (physicsState.AffectedByGravity && !physicsState.OnGrounded)
            {
                physicsState.Acceleration.Y -= Constants.Gravity;
            }

            // maximum acceleration in the game
            if (physicsState.Acceleration.Y <= -Constants.MaxAcceleration)
            {
                physicsState.Acceleration.Y = -Constants.MaxAcceleration;
            }

            if (physicsState.Acceleration.Y >= Constants.MaxAcceleration)
            {
                physicsState.Acceleration.Y = Constants.MaxAcceleration;
            }


            if (physicsState.AffectedByFriction)
            {
                if (physicsState.OnGrounded)
                {
                    int sign = Math.Sign(physicsState.Velocity.X);
                    // ground friction at max speed. -> Equal the running acceleration.
                    if (Math.Abs(physicsState.Velocity.X) >= physicsState.Speed)
                    {
                        physicsState.Acceleration.X -= 2 * sign * physicsState.Speed / Constants.TimeToMax;
                    }
                    if (Math.Abs(physicsState.Velocity.X) >= 0.25f)
                    {
                        physicsState.Acceleration.X -= sign * physicsState.Speed / Constants.TimeToMax;
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
                        physicsState.Acceleration.X -= 2 * sign * physicsState.Speed / Constants.TimeToMax;
                    }
                    if (Math.Abs(physicsState.Velocity.X) >= 0.1f) // Equal half running acceleration.
                    {
                        physicsState.Acceleration.X -= sign * physicsState.Speed / Constants.TimeToMax;
                    }
                    else
                        physicsState.Velocity.X = 0;
                }
            }

            /*float speed = 20.0f;

            physicsState.Velocity = new Vec2f();
            physicsState.Acceleration = new Vec2f();

            if (Input.GetKey(KeyCode.W))
            {
                physicsState.Velocity.Y += speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                physicsState.Velocity.Y -= speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                physicsState.Velocity.X += speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                physicsState.Velocity.X -= speed;
            }*/

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            if (entity.isAgentAlive && System.Math.Abs(newVelocity.X) > physicsState.Speed * 0.1f && 
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
            if (newVelocity.Y > Constants.MaxVelocityY)
            {
                newVelocity.Y = Constants.MaxVelocityY;
            }
            if (newVelocity.Y < -Constants.MaxVelocityY)
            {
                newVelocity.Y = -Constants.MaxVelocityY;
            }


            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero; // Reset acceleration.
        }

        public void Update(AgentContext agentContext)
        {

            float deltaTime = Time.deltaTime;
            var EntitiesWithVelocity = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
            foreach (var entity in EntitiesWithVelocity)
            {
                if (GameState.AgentCreationApi.GetMovementProperties((int)entity.agentID.Type).MovType != AgentMovementType.FlyingMovemnt || !entity.isAgentAlive)
                    UpdateLand(entity, deltaTime);
                else
                    UpdateFlying(entity, deltaTime);
            }
        }
    }
}
