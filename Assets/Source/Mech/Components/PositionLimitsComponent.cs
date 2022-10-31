using Entitas;

namespace Mech
{
    [Mech]
    public class PositionLimitsComponent : IComponent
    {
        public int XMin, XMax, YMin, YMax;
    }
}
