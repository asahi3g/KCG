//imports UnityEngine

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

        public Enums.LootTableType DropTableID;
        public Enums.LootTableType InventoryDropTableID;    // Item spawned inside corpse inventory.
        // (Note: Should we use items inside agent inventory while alive)

        // Enemy agent
        public int BehaviorTreeRootID;
        public float DetectionRadius;

        // Stats
        public float Health;
        public float AttackCooldown;

        public MovementProperties MovProperties;
        public Enums.AgentAnimationType AnimationType;

        public Engine3D.ModelType ModelType;
        public Vec3f ModelScale;

        public UnityEngine.GameObject TrackStub;
    }

    public struct MovementProperties
    {
        public Enums.AgentMovementType MovType;
        public float MaxNumOfJumps;
        public float JumpHeight;
        public float DefaultSpeed;
    }
}