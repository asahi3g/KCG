using Entitas;
using KMath;
using System.Collections.Generic;

namespace Vehicle.Pod
{
    [Pod]
    public class RadarComponent : IComponent
    {
        public Vec2f RadarSize;

        public List<AgentEntity> Members;

        public List<AgentEntity> DeadMembers;

        public int AgentCount;
    }
}