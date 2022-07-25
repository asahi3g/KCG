using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Action
{
    /// <summary>
    /// This should be used for actions related to tools.
    /// </summary>
    [Action]
    public class ToolComponent : IComponent
    {
        [EntityIndex]
        public int ItemID;
    }
}
