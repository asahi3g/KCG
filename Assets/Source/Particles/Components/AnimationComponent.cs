using Entitas;

namespace Particle
{
    [Particle]
    public class AnimationComponent : IComponent
    {
        public float AnimationSpeed;

        public Animation.Animation State;
    }
}