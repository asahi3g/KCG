using Entitas;
using Enums;

namespace Vehicle
{
    [Vehicle]
    public class TypeComponent : IComponent
    {
        public VehicleType Type;
        public bool HasAgent;
    }
}

