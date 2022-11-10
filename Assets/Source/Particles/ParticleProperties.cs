//imports UnityEngine

using KMath;

namespace Particle
{

    public struct ParticleProperties
    {
        public int PropertiesId;
        public string Name;
        
        public float DecayRate;
        public Vec2f Acceleration;
        public float DeltaRotation;
        public float DeltaScale;

        // we can use a mix of sprites for the particles
        public int SpriteId;

        public bool HasAnimation;
        public Animation.AnimationType AnimationType;

        // the starting properties of the particles
        public Vec2f MinSize;
        public Vec2f MaxSize;
        public Vec2f StartingVelocity;
        public float StartingRotation;
        public float StartingScale;
        public float EndScale;
        public UnityEngine.Color StartingColor;
        public UnityEngine.Color EndColor;

        public Enums.ParticleColorUpdateMethod ColorUpdateMethod;
        public float AnimationSpeed;


        // Box Debris
        public bool IsCollidable;
        public bool Bounce;
        public Vec2f BounceFactor;

    }
}

