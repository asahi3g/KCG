using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Node
{
    /// <summary> Only add this when action is scheduled to be executed by the agent. </summary>
    [Node]
    public class OwnerComponent : IComponent
    {
        [EntityIndex]
        public int AgentID;
    }
}
