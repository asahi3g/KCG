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

    public struct SquadBalckboard
    {
        public int[] Members; // List with ids of each member of the squad.
        public int[] Enemies; // List with ids of every know enemy location.
    }
}
