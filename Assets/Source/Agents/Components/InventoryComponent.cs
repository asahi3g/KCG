using Entitas;

namespace Agent
{
    [Agent]
    public class InventoryComponent : IComponent
    {
        public int InventoryID;

        public bool AutoPick;
    }
}
