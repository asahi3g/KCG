using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Particle
{
    [Particle]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of ParticleList. It should never reuse values.
        public int ID;
        public int Index;
    }
}