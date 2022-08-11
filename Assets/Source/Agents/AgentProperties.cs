
using KMath;

namespace Agent
{
    public struct AgentProperties
    {
        public int PropertiesId;
        public string Name;
        public Vec2f SpriteSize;
        public int StartingAnimation;

        public Vec2f CollisionOffset;
        public Vec2f CollisionDimensions;


        // Enemy agent
        public EnemyBehaviour EnemyBehaviour;
        public float DetectionRadius;

        // Stats
        public float Health;
        public float AttackCooldown;
    }
}