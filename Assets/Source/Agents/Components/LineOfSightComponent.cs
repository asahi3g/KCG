using Entitas;
using KMath;

namespace Agents
{
    [Agent]
    public class LineOfSightComponent : IComponent
    {
        public CircleSector ConeSight;
    }
}
