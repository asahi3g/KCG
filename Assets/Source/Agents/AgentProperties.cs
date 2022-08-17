
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

        public int DropTableID;
        public int InventoryDropTableID;    // Item spawned inside corpse inventory.
                                            // (Note: Should we use items inside agent inventory while alive)

        // Enemy agent
        public EnemyBehaviour EnemyBehaviour;
        public float DetectionRadius;

        // Stats
        public float Health;
        public float AttackCooldown;

        public MovementProperties MovProperties;
    }

    public struct MovementProperties
    {
        public Enums.AgentMovementType MovType;
        public float MaxNumOfJumps;
        public float JumpHeight;
        public float DefaultSpeed;
    }
}