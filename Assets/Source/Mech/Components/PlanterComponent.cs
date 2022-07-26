using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace Mech.Planter
{
    [Mech]
    public class PlanterComponent : IComponent
    {
        public bool GotSeed;
        public PlantType Plant;
        public float PlantGrowth;
        public float GrowthTarget;
        public float WaterLevel;
        public float MaxWaterLevel;
    }
}

