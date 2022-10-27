using Entitas;

namespace Particle
{
    [Particle]
    public class Emitter2dPositionComponent : IComponent
    {
        public UnityEngine.Vector2 Position;
        public UnityEngine.Vector2 Acceleration;
        public UnityEngine.Vector2 Velocity;
    }
}