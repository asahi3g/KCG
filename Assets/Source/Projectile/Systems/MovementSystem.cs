using Entitas;
using System.Collections.Generic;
using System.Collections;
using KMath;
using UnityEngine;
using Physics;
using Enums;
using Planet;

namespace Projectile
{
    public class MovementSystem
    {
        private void Update(ProjectileEntity entityP, float deltaTime)
        {
            if (entityP.isProjectileFirstFrame)
            {
                entityP.isProjectileFirstFrame = false;
                return;
            }

            ProjectileProperties projectileProperties = 
                GameState.ProjectileCreationApi.GetRef((int)entityP.projectileType.Type);

            PhysicsStateComponent physicsState = entityP.projectilePhysicsState;
            if (projectileProperties.Flags.HasFlag(ProjectileProperties.ProjFlags.AffectedByGravity))
                physicsState.Acceleration.Y -= Constants.Gravity;

            Vec2f dir = physicsState.Velocity.Normalized;
            if (projectileProperties.Flags.HasFlag(ProjectileProperties.ProjFlags.CanRamp) && physicsState.Velocity.Magnitude < projectileProperties.MaxVelocity)
                physicsState.Acceleration += dir * projectileProperties.RampAcceleration;

            if (projectileProperties.Flags.HasFlag(ProjectileProperties.ProjFlags.HasLinearDrag))
                physicsState.Acceleration += dir * projectileProperties.LinearDrag;

            Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            dir = newVelocity.Normalized;
            if (physicsState.Velocity.Magnitude > projectileProperties.MaxVelocity)
                newVelocity = dir * projectileProperties.MaxVelocity;

            Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = Vec2f.Zero;
        }

        public void Update(ref PlanetState planet)
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                Update(planet.ProjectileList.Get(i), deltaTime);
            }
        }
    }
}
