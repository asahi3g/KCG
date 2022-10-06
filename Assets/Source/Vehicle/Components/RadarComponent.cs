using Entitas;
using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    [Vehicle]
    public class RadarComponent : IComponent
    {
        public List<PodEntity> podEntities;
    }
}
