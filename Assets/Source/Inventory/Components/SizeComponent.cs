using Entitas;

namespace Inventory
{
    /// <summary>
    /// Number of slots.
    /// </summary>
    [Inventory]
    public sealed class SizeComponent : IComponent
    {
        public int  Width;
        public int  Height;
    }
}
