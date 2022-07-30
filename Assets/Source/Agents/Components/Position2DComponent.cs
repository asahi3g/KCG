using Entitas;
using KMath;
using UnityEngine;

namespace Agent
{
    [Agent]
    public class Position2DComponent : IComponent
    {
        public Vec2f Value;
        public Vec2f PreviousValue;
        
        public static Vec2f operator +(Position2DComponent velocity, Vec2f other) => velocity.Value + other;
    }
}
