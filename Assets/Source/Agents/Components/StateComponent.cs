using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Diagnostics.Tracing;

namespace Agent
{
    [Agent]
    public class StateComponent : IComponent
    {
        public AgentState State;
    }
}