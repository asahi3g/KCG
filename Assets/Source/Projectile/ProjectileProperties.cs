using KMath;
using System;

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

        public int NumberOfTicks;
        public float BlastMagnitude;
        public float StartVelocity;
        public float RampAcceleration;
        public float MaxVelocity;

        public float DeltaRotation; // Degrees/seconds.

        public float LinearDrag;
        public float LinearCutOff;

        public float BounceValue;
        public float GravityScale;

        public float TimeToLive;

        public ProjFlags Flags;

        [Flags]
        public enum  ProjFlags : byte
        {
            CanRamp = 1 << 0,
            HasLinearDrag = 1 << 1,
            CanBounce = 1 << 2,
            AffectedByGravity = 1 << 3
        }
    }
}
