using Entitas;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    [Vehicle]
    public class CapacityComponent : IComponent
    {
        public List<AgentEntity> agentsInside;
    }
}
