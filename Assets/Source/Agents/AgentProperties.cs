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

        // Stats
        public float Health;

        public BasicAttack Attack;
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

    // Properties of unarmerd melee attack.
    public struct BasicAttack
    {
        public float Range;
        public int Demage;
        public float Windup;        // Time to damage.
        public float CoolDown;
    }
}