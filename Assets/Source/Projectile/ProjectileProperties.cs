
using KMath;
using UnityEngine;

namespace Projectile
{

    public struct ProjectileProperties
    {
        public int PropertiesId;
        public string Name;
        
        public int SpriteId;
        public Vec2f Size;
        public bool HasAnimation;
        public Animation.AnimationType AnimationType;

        public float Speed;
        public Vec2f Acceleration;
        public float DeltaRotation;

        public bool  CanRamp;
        public float StartVelocity;
        public float MaxVelocity;
        public float RampTime;

        public bool  HasLinearDrag;
        public float LinearDrag;
        public float LinearCutOff;

        public bool  Bounce;
        public float BounceValue;

        public bool  AffectedByGravity;
        public float GravityScale;

        public Enums.DragType DragType;
    }
}

