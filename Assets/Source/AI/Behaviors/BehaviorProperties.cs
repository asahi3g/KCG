using System.Collections.Generic;

namespace AI
{
    public struct BehaviorProperties
    {
        public Enums.BehaviorType TypeID;
        public string Name;
        public List<NodeInfo> Nodes;
        public int BlackBordID;
        public int SquadBlackBoardID;

        public AgentController InstatiateBehavior()
        {
            return null;
        }
    }
}
