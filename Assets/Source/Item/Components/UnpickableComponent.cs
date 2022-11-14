using Entitas;

namespace Item
{
    [ItemParticle]
    public class Unpickable : IComponent
    {
        // Time in seconds being unpickable.
        public float Duration;
    }
}
