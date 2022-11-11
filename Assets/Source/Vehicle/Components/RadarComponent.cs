using Entitas;
using System.Collections.Generic;

namespace Vehicle
{
    [Vehicle]
    public class RadarComponent : IComponent
    {
        public List<PodEntity> podEntities;
    }
}
