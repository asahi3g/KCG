using Entitas;
using KMath;

namespace Agents
{
    /// <summary>
    /// Have agent line of sight information: Direction, angle, etc.
    /// Not used yet. It will be used by sensor and DeubgAgentSightCoinVisualizer
    /// </summary>
    [Agent]
    public class LineOfSightComponent : IComponent
    {
        public CircleSector ConeSight;
    }
}
