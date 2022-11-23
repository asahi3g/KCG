using Entitas;

namespace Item
{
    [ItemParticle]
    public class ItemParticleAttributeUnpickableComponent : IComponent
    {
        // Time in seconds being unpickable.
        public float Duration;
    }
}
