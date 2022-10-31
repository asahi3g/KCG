using Entitas;

namespace Animation
{
    [Agent, Projectile]
    public class StateComponent : IComponent
    {
        public float AnimationSpeed;

        public Animation State;
    }
}