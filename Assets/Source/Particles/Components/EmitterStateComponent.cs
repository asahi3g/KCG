using Entitas;

namespace Particle
{
    [Particle]
    public class EmitterStateComponent : IComponent
    {
        public ParticleType ParticleType;
        public ParticleEmitterType ParticleEmitterType;

        public float Duration;
        public float CurrentTime;
    }
}