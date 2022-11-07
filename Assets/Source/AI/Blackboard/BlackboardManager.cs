using KMath;

namespace AI
{
    public class BlackboardManager
    {
        public Blackboard[] Blackboards = new Blackboard[1024];
        public int Length = 0;
        
        public int CreateBlackboard()
        {
            Blackboards[Length] = new Blackboard()
            {
                AttackTarget = Vec2f.Zero,
                MoveToTarget = Vec2f.Zero,
                ItemID = -1,
                MechID = -1
            };
            return Length++;
        }

        public ref Blackboard Get(int id)
        {
            return ref Blackboards[id];
        }
    }
}
