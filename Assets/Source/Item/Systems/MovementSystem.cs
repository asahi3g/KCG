//import UntiyEngine

using System;
using Physics;
using KMath;

namespace Item
{
    public class MovementSystem
    {
        private void Update(PhysicsStateComponent physicsState, float deltaTime)
        {
            const float Gravity = Constants.Gravity;
            const float MaxAcceleration = Constants.MaxAcceleration;
            
            const float MaxVelocityY = Constants.MaxVelocityY;

            physicsState.Acceleration.Y -= Gravity;
         
            if (physicsState.Acceleration.Y <= -MaxAcceleration)
                physicsState.Acceleration.Y = -MaxAcceleration;
            if (physicsState.Acceleration.Y >= MaxAcceleration)
                physicsState.Acceleration.Y = MaxAcceleration;

            if (physicsState.Velocity.Y > MaxVelocityY)
                physicsState.Velocity.Y = MaxVelocityY;
            if (physicsState.Velocity.Y < -MaxVelocityY)
                physicsState.Velocity.Y = -MaxVelocityY;
            

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            if (physicsState.OnGrounded)
                newVelocity.X *= 0.9f;  // Ground friction
            else
                newVelocity.X *= 0.98f; // Air friction

            if (newVelocity.Y > MaxVelocityY)
                newVelocity.Y = MaxVelocityY;
            if (newVelocity.Y < -MaxVelocityY)
                newVelocity.Y = -MaxVelocityY;

            Vec2f newphysicsStateition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newphysicsStateition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero;
        }

        public void Update(ItemParticleContext Context)
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            var EntitiesWithPhysicsState = Context.GetGroup(ItemParticleMatcher.ItemPhysicsState);
            foreach (var entity in EntitiesWithPhysicsState)
            {
                var physicsState = entity.itemPhysicsState;
                Update(physicsState, deltaTime);
            }
        }
    }
}
