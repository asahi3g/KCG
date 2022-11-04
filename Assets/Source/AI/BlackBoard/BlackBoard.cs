using KMath;

namespace AI
{
    public struct Blackboard
    {
        public Vec2f AttackTarget;
        public Vec2f MoveToTarget;
        public int ItemID; // ItemId to interact.
        public int MechID; // MechId to interact.
    }
}
