using KMath;
using UnityEngine;

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
    }
}

