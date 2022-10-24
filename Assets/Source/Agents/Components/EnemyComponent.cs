using Entitas;

namespace Agent
{
    [Agent]
    public class EnemyComponent : IComponent
    {
        public EnemyBehaviour Behaviour;

        public float DetectionRadius;


        public float EnemyCooldown;
    }
}
