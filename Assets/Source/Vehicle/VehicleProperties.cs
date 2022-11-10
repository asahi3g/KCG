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

        public bool Jet;
        public Vec2f JetAngle;
        public JetSize JetSize;

        public int ThrusterSpriteId;
        public Vec2f ThrusterSpriteSize;

        public Vec2f Thruster1Position;
        public Vec2f Thruster2Position;

        public Vec2f PodOffsetRight;
        public Vec2f PodOffsetLeft;
    }
}

