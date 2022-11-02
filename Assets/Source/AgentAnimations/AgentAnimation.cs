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
            return Animation != other.Animation ||
            FadeTime != other.FadeTime ||
            Speed != other.Speed ||
            StartTime != other.StartTime ||
            Looping != other.Looping ||
            MovementSpeedFactor != other.MovementSpeedFactor ||
            UseActionDurationForSpeed != other.UseActionDurationForSpeed;
        
        }


    }
}