using Entitas;

namespace Projectile
{
    [Projectile]
    public class LinearDragComponent : IComponent
    {
        public float Drag;
        public float CutOff;
    }
}

