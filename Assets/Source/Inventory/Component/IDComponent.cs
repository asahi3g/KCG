using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Inventory
{
    // This is not the index of InventoryList. It should never reuse values.
    [Inventory]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int ID;
    }
}
