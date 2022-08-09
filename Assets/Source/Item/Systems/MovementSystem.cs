using System;
using Physics;
using KMath;
using UnityEngine;

namespace Item
{
    public class MovementSystem
    {
        private void Update(PhysicsStateComponent physicsState, float deltaTime)
        {
            float Gravity = Constants.Gravity;
            float MaxAcceleration = Constants.MaxAcceleration;
            
            // Maximum Y velocity
            float MaxVelocityY = Constants.MaxVelocityY;

            physicsState.Acceleration.Y -= Gravity;
         
            // Maximum acceleration in the game
            if (physicsState.Acceleration.Y <= -MaxAcceleration)
            {
                physicsState.Acceleration.Y = -MaxAcceleration;
            }

            if (physicsState.Acceleration.Y >= MaxAcceleration)
            {
                physicsState.Acceleration.Y = MaxAcceleration;
            }


            // Maximum velocity in the game
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
                // Ground friction
                newVelocity.X *= 0.9f;
            }
            else
            {
                // Air friction
                newVelocity.X *= 0.98f;
            }

            // Maximum velocity in the game
            if (newVelocity.Y > MaxVelocityY)
            {
                newVelocity.Y = MaxVelocityY;
            }
            if (newVelocity.Y < -MaxVelocityY)
            {
                newVelocity.Y = -MaxVelocityY;
            }


            Vec2f newphysicsStateition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newphysicsStateition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero;
        }

        public void Update(ItemParticleContext Context)
        {
            float deltaTime = Time.deltaTime;
            var EntitiesWithPhysicsState = Context.GetGroup(ItemParticleMatcher.ItemPhysicsState);
            foreach (var entity in EntitiesWithPhysicsState)
            {
                var physicsState = entity.itemPhysicsState;
                Update(physicsState, deltaTime);
            }
        }
    }
}
