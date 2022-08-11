using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace Mech
{
    // Planter Properties Component
    [Mech]
    public class PlanterComponent : IComponent
    {
        public bool GotSeed;
        public int PlantMechID;
        public float PlantGrowth;
        public float GrowthTarget;
        public float WaterLevel;
        public float MaxWaterLevel;
        public int LightLevel;
    }
}

