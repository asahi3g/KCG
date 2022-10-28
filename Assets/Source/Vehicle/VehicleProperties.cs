using Enums;
using KMath;

namespace Vehicle
{

    public struct VehicleProperties
    {
        public int PropertiesId;
        public string Name;

        public int SpriteId;
        public Vec2f SpriteSize;

        public Vec2f CollisionSize;
        public Vec2f CollisionOffset;
        public Vec2f Scale;

        public float Rotation;

        public Vec2f AngularVelocity;

        public float AngularMass;
        public float AngularAcceleration;
        public float CenterOfGravity;

        public Vec2f CenterOfRotation;

        public bool AffectedByGravity;

        public int DefaultAgentCount;

        public bool Jet;
        public float JetAngle;
        public JetSize JetSize;

        public int ThrusterSpriteId;
        public Vec2f ThrusterSpriteSize;

        public Vec2f Thruster1Position;
        public Vec2f Thruster2Position;
    }
}

