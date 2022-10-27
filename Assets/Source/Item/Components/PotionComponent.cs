using Entitas;
using Enums;

namespace Item
{
    [ItemInventory]
    public class PotionComponent : IComponent
    {
        public PotionType potionType;
    }
}

