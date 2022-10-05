namespace AI
{
    public struct BehaviorProperties
    {
        public Enums.BehaviorType TypeID;
        public int RootID;  // Uses shared entitas context in properties.

        public AgentController InstatiateBehavior()
        {
            return null;
        }
    }
}
