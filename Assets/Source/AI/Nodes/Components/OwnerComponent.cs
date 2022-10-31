using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Node
{
    // Only add this when action is scheduled to be executed by the agent.
    [Node]
    public class OwnerComponent : IComponent
    {
        [EntityIndex]
        public int AgentID;
    }
}
