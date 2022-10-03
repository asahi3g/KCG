using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using KMath;
using Enums;

namespace Pod
{
    [Pod]
    public class StateComponent : IComponent
    {
        public PodState State;
    }
}

