using System;

namespace Agent
{
    public struct AgentAnimation
    {
        //TODO: AgentAnimationId
        public int AnimationId;

        public Engine3D.AnimationType Animation;
        public float FadeTime;
        public float Speed;
        public float StartTime;
        public bool Looping;

        public float MovementSpeedFactor;
        public bool UseActionDurationForSpeed;
        
        public bool Equals(AgentAnimation other)
        {
            return Animation == other.Animation && 
                   FadeTime.Equals(other.FadeTime) && 
                   Speed.Equals(other.Speed) &&
                   StartTime.Equals(other.StartTime) && 
                   Looping == other.Looping &&
                   MovementSpeedFactor.Equals(other.MovementSpeedFactor) &&
                   UseActionDurationForSpeed == other.UseActionDurationForSpeed;
        }

        public override bool Equals(object obj)
        {
            return obj is AgentAnimation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Animation, FadeTime, Speed, StartTime, Looping, MovementSpeedFactor, UseActionDurationForSpeed);
        }

        public bool AnimationsAreEqual(Engine3D.AnimationType animation)
        {
            return Animation == animation;
        }

        public bool AnimationsAreEqual(AgentAnimation other)
        {
            return Animation == other.Animation;
        }
    }
}