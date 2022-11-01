using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Item
{
    [ItemInventory, ItemParticle]
    // This is not the index of the lists. It should never reuse values.
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int              ID;
    }
}
