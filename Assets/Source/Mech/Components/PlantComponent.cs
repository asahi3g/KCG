using Entitas;

namespace Mech
{
    [Mech]
    public class PlantComponent : IComponent
    {
        public float PlantGrowth;
        public float GrowthTarget;
        public float WaterLevel;
    }
}

