using KMath;

namespace AI.SquadBehvaior
{
    public struct Squad
    {
        public int BlackboardID;
        public int[] MemberIDs; // Members with the ID of every member of the squad.
        public Vec2f[] CombatPositions; // Available positions AI can be during combat.
        public int[] PositionsScore;     // ScoreForEvery position in combatPositions.
        public int PositionsLength;      // Length of CombatPositions and PostiionScore
    }
}
