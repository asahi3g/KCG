using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums;
using Mech;

namespace Item
{
    [ItemInventory]
    public class PotionComponent : IComponent
    {
        public PotionType potionType;
    }
}

