using Enums;

namespace AI
{
    public class BehaviorBase
    {
        public virtual BehaviorType Type { get { return BehaviorType.Error; } }

        public virtual int BehaviorTreeGenerator()
        {
            return -1;
        }
    }
}
