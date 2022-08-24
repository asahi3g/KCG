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
        // Todo use one id instead of two: https://news.ycombinator.com/item?id=17995634
        public int ID;
        public int Index;
        public Enums.AgentType Type;
    }
}