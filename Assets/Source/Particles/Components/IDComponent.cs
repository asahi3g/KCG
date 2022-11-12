using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;

namespace Particle
{
    [Particle]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of ParticleList. It should never reuse values.
        public Int64 ID;
        public int Index;
        public ParticleType ParticleType;
    }
}