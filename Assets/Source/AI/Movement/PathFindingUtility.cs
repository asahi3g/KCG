using KMath;

namespace AI.Movement
{
    struct Node
    {
        public int id;  // Is  equal closedList index. 
        public int parentID;
        public Vec2i pos;

        public float pathCost;
        public float heuristicCost;
        public float totalCost;

        public int jumpValue;

        public bool IsCheaper(ref Node node)
        {
            return totalCost < node.totalCost ? true : false;
        }

        public void UpdateCost(Vec2i end)
        {
            heuristicCost = Heuristics.euclidean_distance(pos, end) * 100f;
            totalCost = heuristicCost + pathCost;
        }

        // Used by Array.indexOF
        public override bool Equals(object obj)
        {
            if (obj is Node)
                return pos == ((Node)obj).pos ? true : false;
            return false;
        }
    }

    internal struct PathAdjacency
    {
        public Vec2i dir;
        public int cost;
    }

    internal struct CharacterMovement
    {
        public float JumpMaxDistance;
        public float MaxJumpNumber;
        public float DownMaxDistance;
    }

    internal static class Heuristics
    {
        static public float euclidean_distance(Vec2i firstPos, Vec2i secondPos)
        {
            float x = firstPos.X - secondPos.X;
            float y = firstPos.Y - secondPos.Y;
            return System.MathF.Sqrt(x * x + y * y);
        }

        static public float manhattan_distance(Vec2i firstPos, Vec2i secondPos)
        {
            float x = firstPos.X - secondPos.X;
            float y = firstPos.Y - secondPos.Y;
            return System.MathF.Abs(x) + System.MathF.Abs(y);
        }
    }
}
