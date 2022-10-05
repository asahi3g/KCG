using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle.Pod
{
    [Pod]
    public class PatrolComponent : IComponent
    {
        public Vec2f PatrolStart;
        public Vec2f PatrolEnd;
    }
}