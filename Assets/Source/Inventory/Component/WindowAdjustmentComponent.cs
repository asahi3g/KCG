using Entitas;

namespace Inventory
{
    //  Allow dynamically changing default Position and size of inventory.
    [Inventory]
    public class WindowAdjustmentComponent : IComponent
    {
        public Window window;
    }
}
