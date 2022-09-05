using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums;
using Mech;

namespace Item
{
    [ItemInventory]
    public class PotionCastDataComponent : IComponent
    {
        public PotionType potionType;
    }
}

