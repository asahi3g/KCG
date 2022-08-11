using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Diagnostics.Tracing;

namespace Agent
{
    [Agent]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of AgentList. It should never reuse values. It should never be changed.
        public int ID;
        public int Index;
    }
}