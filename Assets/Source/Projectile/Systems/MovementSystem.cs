﻿//imports UnityEngine

using Physics;

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

            KMath.Vec2f dir = physicsState.Velocity.Normalized;
            if (projectileProperties.Flags.HasFlag(ProjectileProperties.ProjFlags.CanRamp) && physicsState.Velocity.Magnitude < projectileProperties.MaxVelocity)
                physicsState.Acceleration += dir * projectileProperties.RampAcceleration;

            if (projectileProperties.Flags.HasFlag(ProjectileProperties.ProjFlags.HasLinearDrag) && physicsState.Velocity.Magnitude > projectileProperties.LinearCutOff 
                && physicsState.Velocity.Magnitude > 0.005f)
                physicsState.Acceleration -= dir * projectileProperties.LinearDrag;

            KMath.Vec2f displacement = 0.5f * physicsState.Acceleration * (deltaTime * deltaTime) + physicsState.Velocity * deltaTime;
            KMath.Vec2f newVelocity = physicsState.Acceleration * deltaTime + physicsState.Velocity;

            dir = newVelocity.Normalized;
            if (physicsState.Velocity.Magnitude > projectileProperties.MaxVelocity)
                newVelocity = dir * projectileProperties.MaxVelocity;

            // Perform fast deceleration.
            if (physicsState.OnGrounded)
                newVelocity *= 0.8f;

            KMath.Vec2f newPosition = physicsState.Position + displacement;
            physicsState.PreviousPosition = physicsState.Position;
            physicsState.Position = newPosition;

            physicsState.Velocity = newVelocity;
            physicsState.Acceleration = KMath.Vec2f.Zero;
        }

        public void Update()
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            for (int i = 0; i < GameState.Planet.ProjectileList.Length; i++)
            {
                Update(GameState.Planet.ProjectileList.Get(i), deltaTime);
            }
        }
    }
}
