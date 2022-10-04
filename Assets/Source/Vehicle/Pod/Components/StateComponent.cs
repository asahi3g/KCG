using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using KMath;
using Enums;

namespace Vehicle.Pod
{
    [Pod]
    public class StateComponent : IComponent
    {
        public PodState State;
    }
}

