using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;

namespace Node
{
    // This should be used by Goap. To request movement to movimentSystem.
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
