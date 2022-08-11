using Entitas;

namespace Inventory
{
    // Todo: Improve this and make this more versatile.

    [Inventory]
    /// <summary>
    /// Add title to inventory.
    /// </summary>
    public class NameComponent : IComponent
    {
        public string Name;
    }
}
