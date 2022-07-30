using System;
using Physics;
using KMath;
using UnityEngine;

namespace  Item
{
    public class MovementSystem
    {
        private void Update(Position2DComponent pos, MovementComponent movable, float deltaTime)
        {
            float Gravity = 800.0f;
            float MaxAcceleration = 50.0f;
            // Maximum Y velocity
            float MaxVelocityY = 15.0f;

            movable.Acceleration.Y -= Gravity * deltaTime;
         
            // Maximum acceleration in the game
            if (movable.Acceleration.Y <= -MaxAcceleration)
            {
                movable.Acceleration.Y = -MaxAcceleration;
            }

            if (movable.Acceleration.Y >= MaxAcceleration)
            {
                movable.Acceleration.Y = MaxAcceleration;
            }


            // Maximum velocity in the game
            if (movable.Velocity.Y > MaxVelocityY)
            {
                movable.Velocity.Y = MaxVelocityY;
            }
            if (movable.Velocity.Y < -MaxVelocityY)
            {
                movable.Velocity.Y = -MaxVelocityY;
            }

            Vec2f displacement = 0.5f * movable.Acceleration * (deltaTime * deltaTime) + movable.Velocity * deltaTime;
            Vec2f newVelocity = movable.Acceleration * deltaTime + movable.Velocity;

            if (movable.OnGrounded)
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


            Vec2f newPosition = pos.Value + displacement;
            pos.PreviousValue = pos.Value;
            pos.Value = newPosition;

            movable.Velocity = newVelocity;
        }

        public void Update(ItemParticleContext Context)
        {
            float deltaTime = Time.deltaTime;
            var EntitiesWithVelocity = Context.GetGroup(ItemParticleMatcher.AllOf(
                ItemParticleMatcher.ItemMovement, ItemParticleMatcher.ItemPosition2D));
            foreach (var entity in EntitiesWithVelocity)
            {
                var pos = entity.itemPosition2D;
                var movable = entity.itemMovement;

                Update(pos, movable, deltaTime);
            }
        }
    }
}
