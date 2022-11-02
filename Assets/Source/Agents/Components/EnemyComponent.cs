using Entitas;

namespace Agent
{
    // Mahdi's Temporary solution for simple AI.
    // It will be removed soon.
    [Agent]
    public class EnemyComponent : IComponent
    {
        public EnemyBehaviour Behaviour;
        public float DetectionRadius;
        public float EnemyCooldown;
    }
}
