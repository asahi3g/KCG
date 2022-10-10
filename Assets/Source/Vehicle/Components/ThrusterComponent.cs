using Entitas;
using Enums;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    [Vehicle]
    public class ThrusterComponent : IComponent
    {
        public bool Jet;
        public float Angle;
        public JetSize JetSize;
        public bool isLaunched;
    }
}
