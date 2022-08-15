using Entitas;
using KMath;

namespace Inventory
{
    /// <summary>
    ///  Allow dynamically changing default Position and size of inventory.
    /// </summary>
 
    [Inventory]
    public class WindowAdjustmentComponent : IComponent
    {
        public Window window;
    }
}
