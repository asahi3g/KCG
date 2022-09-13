using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using KMath;
using Enums;

namespace Vehicle
{
    [Vehicle]
    public class TypeComponent : IComponent
    {
        public VehicleType Type;
    }
}

