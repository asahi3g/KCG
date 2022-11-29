//imports UnityEngine

using KMath;

namespace Agent
{
    public struct AgentPropertiesTemplate
    {
        public int PropertiesId;
        public string Name;
        public int StartingAnimation;

        public Vec2f CollisionOffset;
        public Vec2f CollisionDimensions;

        public int DropTableID;
        public int InventoryDropTableID;    // Item spawned inside corpse inventory.
        // (Note: Should we use items inside agent inventory while alive)

        // Enemy agent
        public int BehaviorTreeRootID;

        // Stats
        public int DefaultHealth;

        public BasicAttack Attack;
        public MovementProperties MovProperties;
        public Enums.AgentAnimationType AnimationType;

        public Engine3D.AgentModelType ModelType;
        public Vec3f ModelScale;

        public bool Stagger;
        public float StaggerAffectTime;
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
