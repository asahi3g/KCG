using Entitas;
using KMath;

namespace Node
{
    /// <summary> This should be used by Goap. To request movement to movimentSystem. </summary>
    [Node]
    public class MoveToComponent : IComponent
    {
        public Vec2f GoalPosition;

        public Vec2f[] path;
        public int pathLength;
        public bool reachedX = false;
        public bool reachedY = false;
    }
}
