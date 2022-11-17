using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Item
{
    [ItemInventory, ItemParticle]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of ItemParticleList. It should never reuse values. It should never be changed.
        // Todo use one id instead of two: https://news.ycombinator.com/item?id=17995634
        public int              ID;
        public int              Index;
    }
}
