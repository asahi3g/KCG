using KMath;
using Planet;

namespace AI
{
    public class BlackboardManager
    {
        public Blackboard[] Blackboards = new Blackboard[1024];
        public int count = 0;
        
        public int CreateBlackboard()
        {
            Blackboards[count] = new Blackboard()
            {
                ShootingTarget = Vec2f.Zero,
                MoveToTarget = Vec2f.Zero,
                ItemID = -1,
                MechID = -1
            };
            return count++;
        }

        public ref Blackboard Get(int id)
        {
            return ref Blackboards[id];
        }
    }
}
