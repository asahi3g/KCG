using Entitas;

namespace Mech
{
    // Makes item destructable.
    [Mech]
    public class DurabilityComponent : IComponent
    {
        public int Durability;
    }
}
