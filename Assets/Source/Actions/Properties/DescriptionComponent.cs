using Entitas;

namespace Action.Property
{
    /// <summary>
    /// Holds a brief description of what the action does.
    /// </summary>
    [ActionProperties]
    public class DescriptionComponent : IComponent
    {
        public string str;
    }
}
