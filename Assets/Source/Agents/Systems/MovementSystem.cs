using System;
using KMath;
using UnityEngine;

namespace Agent
{
    public class MovementSystem
    {
        private void Update(PhysicsStateComponent physicsState, float deltaTime)
        {
            float Gravity = 800.0f;
            float MaxAcceleration = 50.0f;
            // maximum Y velocity
            float MaxVelocityY = 12.0f;

            if (physicsState.AffectedByGravity)
            {
                physicsState.Acceleration.Y -= Gravity * deltaTime;
            }

            // maximum acceleration in the

            // maximum acceleration in the game
            if (physicsState.Acceleration.Y <= -MaxAcceleration)
            {
                physicsState.Acceleration.Y = -MaxAcceleration;
            }

            if (physicsState.Acceleration.Y >= MaxAcceleration)
            {
                physicsState.Acceleration.Y = MaxAcceleration;
            }


            // maximum velocity in the game
            if (physicsState.Velocity.Y > MaxVelocityY)
            {
                physicsState.Velocity.Y = MaxVelocityY;
            }
            if (physicsState.Velocity.Y < -MaxVelocityY)
            {
                physicsState.Velocity.Y = -MaxVelocityY;
            }

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            if (physicsState.OnGrounded)
            {
                // ground friction
                newVelocity.X *= 0.7f;
            }
            else
            {
                // air friction
                newVelocity.X *= 0.7f;
            }


            // maximum velocity in the game
            if (newVelocity.Y > MaxVelocityY)
            {
                newVelocity.Y = MaxVelocityY;
            }
            if (newVelocity.Y < -MaxVelocityY)
            {
                newVelocity.Y = -MaxVelocityY;
            }


            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
        }

        public void Update(AgentContext agentContext)
        {

            float deltaTime = Time.deltaTime;
            var EntitiesWithVelocity = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
            foreach (var entity in EntitiesWithVelocity)
            {

                var physicsState = entity.agentPhysicsState;

                Update(physicsState, deltaTime);
            }
        }
    }
}
