        
using Entitas;
using UnityEngine;
using KMath;

namespace FloatingText
{
    [FloatingText]
    public class MovementComponent : IComponent
    {
        public Vec2f Velocity;
        public Vec2f Position;
    }
}
