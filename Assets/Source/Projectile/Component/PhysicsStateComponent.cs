using System;
using UnityEngine;
using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class PhysicsStateComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;
        public float Rotation;

        public Vec2f Velocity;
        public Vec2f Acceleration; // Instantaneous, reset to zero at the end of the frame.

        public bool AffectedByGravity;

        [Range(-1.0f, 1.0f)]
        public Vec2f angularVelocity = Vec2f.Zero;

        public float angularMass = 1.0f;
        public float angularAcceleration = 3.0f;

        public float centerOfGravity = 0.0f;
        public Vec2f centerOfRotation = Vec2f.One;
    }
}

