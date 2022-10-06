using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Vehicle.Pod
{
    [Pod]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of VehicleList. It should never reuse values.
        public int ID;
        public int Index;
    }
}