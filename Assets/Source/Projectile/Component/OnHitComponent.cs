using Entitas;
using KMath;

namespace Projectile
{
    [Projectile]
    public class OnHitComponent : IComponent
    {
        /// <summary>
        /// If tile is hit. AgentID = -1.
        /// </summary>
        public int AgentID;
        public float HitTime;
        public Vec2f HitPos;
    }
}