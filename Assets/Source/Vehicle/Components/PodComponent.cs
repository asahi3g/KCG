using Entitas;
using System.Collections.Generic;


namespace Vehicle
{
    [Vehicle]
    public class PodComponent : IComponent
    {
        public bool connectedToVehicle;
        public List<PodEntity> Pods;

        public KMath.Vec2f OffsetRight;
        public KMath.Vec2f OffsetLeft;
    }
}
