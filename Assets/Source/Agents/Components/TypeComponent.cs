using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Agent
{
    [Agent]
    public class TypeComponent : IComponent
    {
        [EntityIndex]
        public AgentType Type;
    }
}
