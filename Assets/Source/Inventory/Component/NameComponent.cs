using Entitas;

namespace Inventory
{
    // Todo: Improve this and make this more versatile.

    [Inventory]
    // Add title to inventory.
    public class NameComponent : IComponent
    {
        public string Name;
    }
}
