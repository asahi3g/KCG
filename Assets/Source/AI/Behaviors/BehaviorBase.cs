using Enums;

namespace AI
{
    public class BehaviorBase
    {
        public virtual BehaviorType Type { get { return BehaviorType.Error; } }

        public virtual int BehaviorTreeGenerator()
        {
            int rootID = GameState.BehaviorTreeCreationAPI.CreateTree();
            GameState.BehaviorTreeCreationAPI.EndTree();
            return rootID;
        }
    }
}
