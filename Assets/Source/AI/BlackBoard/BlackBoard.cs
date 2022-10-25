using KMath;
using Planet;

namespace AI
{
    public struct Blackboard
    {
        public Vec2f ShootingTarget;
        public Vec2f MoveToTarget;
        public int ItemID; // ItemId to interact.
        public int MechID; // MechId to interact.
    }
}
