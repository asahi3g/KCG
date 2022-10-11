using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle.Pod
{
    [Pod]
    public class CoverComponent : IComponent
    {
        public List<Vec2f> CoverPositions;
        public List<Vec2f> FirePositions;
    }
}