using Entitas;
using KMath;

namespace KGUI.Elements
{
    [UIElement]
    public class MultiplePositionComponent : IComponent
    {
        public Vec2f position1;
        public Vec2f position2;
    }
}
