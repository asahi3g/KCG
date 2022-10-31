using Entitas;

namespace Particle
{
    [Particle]
    public class StateComponent : IComponent
    {
        public float Health;
        public float DecayRate;

        public float DeltaRotation;
        public float DeltaScale;
    }
}