using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace Mech.Planter
{
    // Planter Properties Component
    [Mech]
    public class PlanterComponent : IComponent
    {
        public bool GotSeed;
        public MechEntity Plant;
        public float PlantGrowth;
        public float GrowthTarget;
        public float WaterLevel;
        public float MaxWaterLevel;
        public int LightLevel;
    }
}

