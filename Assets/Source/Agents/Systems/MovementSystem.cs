using Physics;
using System;
using KMath;
using UnityEngine;

namespace Agent
{
    public class MovementSystem
    {
        private void Update(PhysicsStateComponent physicsState, float deltaTime)
        {
            // Note(Joao) Increase gravity and initial velocity for smaller air time during jump. 

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

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;


            // maximum velocity in the game
            if (physicsState.Velocity.Y > Constants.MaxVelocityY)
            {
                physicsState.Velocity.Y = Constants.MaxVelocityY;
            }
            if (physicsState.Velocity.Y < -Constants.MaxVelocityY)
            {
                physicsState.Velocity.Y = -Constants.MaxVelocityY;
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

                var physicsState = entity.agentPhysicsState;

                Update(physicsState, deltaTime);
            }
        }
    }
}
