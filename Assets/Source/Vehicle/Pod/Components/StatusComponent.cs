using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle.Pod
{
    [Pod]
    public class StatusComponent : IComponent
    {
        public int PodValue;
        public int Score;
        public bool Freeze;
    }
}