using Entitas;

namespace Mech
{
    // Planter Properties Component
    [Mech]
    public class PlanterComponent : IComponent
    {
        public bool GotPlant;
        public int  PlantMechID;
    }
}

