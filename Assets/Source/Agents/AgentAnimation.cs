namespace Agent
{


    public struct AgentAnimation
    {
        public Engine3D.AnimationType Animation;
        public float FadeTime;
        public float Speed;
        public float StartTime;
        public bool Looping;

        public float MovementSpeedFactor;
        public bool UseActionDurationForSpeed;


        public static bool operator !=(AgentAnimation a, AgentAnimation b) => 
            a.Animation != b.Animation ||
            a.FadeTime != b.FadeTime ||
            a.Speed != b.Speed ||
            a.StartTime != b.StartTime ||
            a.Looping != b.Looping ||
            a.MovementSpeedFactor != b.MovementSpeedFactor ||
            a.UseActionDurationForSpeed != b.UseActionDurationForSpeed;

        public static bool operator ==(AgentAnimation a, AgentAnimation b) => 
            a.Animation == b.Animation &&
            a.FadeTime == b.FadeTime &&
            a.Speed == b.Speed &&
            a.StartTime == b.StartTime &&
            a.Looping == b.Looping &&
            a.MovementSpeedFactor == b.MovementSpeedFactor &&
            a.UseActionDurationForSpeed == b.UseActionDurationForSpeed;
    }
}