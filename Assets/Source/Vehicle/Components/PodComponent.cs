using Entitas;
using System.Collections.Generic;


namespace Vehicle
{
    [Vehicle]
    public class PodComponent : IComponent
    {
        public bool connectedToVehicle;
        public List<PodEntity> Pods;
    }
}
