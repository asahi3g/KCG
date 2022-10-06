namespace AI
{
    public struct BehaviorProperties
    {
        public Enums.BehaviorType TypeID;
        public int RootID;  // Uses shared entitas context in properties.
        public int BlackBordID;
        public int SquadBlackBoardID;   

        public AgentController InstatiateBehavior()
        {
            return null;
        }
    }
}
