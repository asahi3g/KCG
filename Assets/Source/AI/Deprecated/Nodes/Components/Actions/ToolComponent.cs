using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Node
{
    /// <summary>
    /// This should be used for actions related to tools.
    /// </summary>
    [Node]
    public class ToolComponent : IComponent
    {
        [EntityIndex]
        public int ItemID;
    }
}
