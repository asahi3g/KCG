using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Node
{
    // This should be used for actions related to tools.
    [Node]
    public class ToolComponent : IComponent
    {
        [EntityIndex]
        public int ItemID;
    }
}
