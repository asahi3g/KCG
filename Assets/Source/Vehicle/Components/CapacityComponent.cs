using Entitas;
using System.Collections.Generic;


namespace Vehicle
{
    [Vehicle]
    public class CapacityComponent : IComponent
    {
        public List<AgentEntity> agentsInside;
    }
}
