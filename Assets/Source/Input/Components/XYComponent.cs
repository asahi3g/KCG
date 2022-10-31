using Entitas;
using KMath;

namespace ECSInput
{
    [Agent]
    public class XYComponent : IComponent
    {
        public Vec2f Value;
        public bool Jump;
        public bool Dash;
    }
}

