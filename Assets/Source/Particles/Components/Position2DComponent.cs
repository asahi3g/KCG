using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using KMath;

namespace Particle
{
    [Particle]
    public class Position2DComponent : IComponent
    {
        public Vec2f Position;
        public Vec2f PreviousPosition;
        public Vec2f Acceleration;
        public Vec2f Velocity;
        public float Rotation;
    }
}