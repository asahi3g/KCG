using Entitas;
using KMath;

namespace Agents
{
    // Have agent line of sight information: Direction, angle, etc.
    // Not used yet. It will be used by sensor and DeubgAgentSightCoinVisualizer
    [Agent]
    public class LineOfSightComponent : IComponent
    {
        public CircleSector ConeSight;
    }
}
