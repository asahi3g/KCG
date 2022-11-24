using KMath;

namespace AI
{
    public struct Blackboard
    {
        public bool UpdateTarget; // If true update target value.
        public int AgentTargetID;
        public Vec2f AttackTarget;
        public Vec2f MoveToTarget;
        public int ItemID; // ItemId to interact.
        public int MechID; // MechId to interact.
    }

    public struct SquadBalckboard
    {
        public int[] Enemies; // List with ids of every know enemy location.
    }
}
