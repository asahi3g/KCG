using Entitas;

namespace Agent
{
    /// <summary>
    /// Mahdi's Temporary solution for simple AI.
    /// It will be removed soon.
    /// </summary>
    [Agent]
    public class EnemyComponent : IComponent
    {
        public EnemyBehaviour Behaviour;
        public float DetectionRadius;
        public float EnemyCooldown;
    }
}
