using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Particle
{
    [Particle]
    public class EmitterIDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of ParticleEmitterList. It should never reuse values.
        public int ID;
        public int Index;
    }
}