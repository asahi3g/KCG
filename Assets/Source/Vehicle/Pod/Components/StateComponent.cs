using Entitas;
using Enums;

namespace Vehicle.Pod
{
    [Pod]
    public class StateComponent : IComponent
    {
        public PodState State;
    }
}

