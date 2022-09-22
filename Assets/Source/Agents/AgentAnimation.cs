namespace Agent
{


    public struct AgentAnimation
    {
        public Engine3D.AnimationType Animation;
        public float FadeTime;
        public float Speed;
        public bool Looping;

        public float MovementSpeedFactor;
        public bool UseActionDurationForSpeed;
    }
}