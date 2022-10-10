using Entitas;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    [Vehicle]
    public class HeightMapComponent : IComponent
    {
        public bool OpenSky;
        public Vec2f SpawnPosition;
    }
}
