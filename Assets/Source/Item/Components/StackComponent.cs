using Entitas;

namespace Item
{
    [ItemInventory, ItemParticle]
    public class StackComponent : IComponent
    {
        // Number of Component in the stack.
        public int Count;
    }
}
