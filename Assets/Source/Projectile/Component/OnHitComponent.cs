using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class OnHitComponent : IComponent
    {
        // If tile is hit. AgentID = -1.
        public int AgentID;
        public float FirstHitTime;
        public Vec2f FistHitPos;

        public float LastHitTime;
        public Vec2f LastHitPos;

        public bool ParticleSpawned;
    }
}