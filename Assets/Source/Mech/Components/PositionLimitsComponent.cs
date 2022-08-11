using Enums;
using KMath;
using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Mech
{
    [Mech]
    public class PositionLimitsComponent : IComponent
    {
        public int XMin, XMax, YMin, YMax;
    }
}
