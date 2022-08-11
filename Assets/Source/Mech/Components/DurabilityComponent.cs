using Entitas;

namespace Mech
{
    /// <summary>
    /// Makes item destructable.
    /// </summary>
    [Mech]
    public class DurabilityComponent : IComponent
    {
        public int Durability;
    }
}
