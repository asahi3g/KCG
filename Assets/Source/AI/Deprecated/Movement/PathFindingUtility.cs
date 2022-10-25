using KMath;
using System;

namespace AI.Movement
{
    struct Node
    {
        public Int16 id;  // Is  equal closedList index. 
        public Int16 parentID;
        public Vec2i pos;

        public int pathCost;
        public int heuristicCost;
        public int totalCost;

        public Int16 jumpValue;
        public float horizontalMovemnt;   // if more than one can go to a horizontal direction in the air.

        public bool IsCheaper(ref Node node)
        {
            return totalCost < node.totalCost ? true : false;
        }

        public void UpdateCost(Vec2i end, Heuristics.distance heuristics)
        {
            heuristicCost = heuristics(pos, end) * 100;
            totalCost = heuristicCost + pathCost;
        }
    }

    internal struct PathAdjacency
    {
        public Vec2i dir;
        public int cost;
    }

    public struct CharacterMovement
    {
        public Int16 JumpMaxHeight;
        public Int16 FallMaxHeight;
        public int HorizontalRateMovement; // How much can move horizontal to each cell of height.
        public Vec2i Size;

        public CharacterMovement(float initialJumpVelocity, float speed, int maxFallHeight, Vec2f size)
        {
            float jumpHeight = Physics.PhysicsFormulas.JumpHeightFromVelocity(initialJumpVelocity);

            JumpMaxHeight = (Int16)jumpHeight;
            // Distance = V / T. -> Distance/jumpHeight
            HorizontalRateMovement = (int)(100 * (speed * initialJumpVelocity / Physics.Constants.Gravity) / jumpHeight); // Multiply by 100. So we can do comparisons as ints.
            FallMaxHeight = (Int16)(maxFallHeight + jumpHeight);
            Size = new Vec2i((int)MathF.Ceiling(size.X), (int)MathF.Ceiling(size.Y));
        }
    }

    internal static class Heuristics
    {
        public delegate int distance(Vec2i firstPos, Vec2i secondPos);

        static public int euclidean_distance(Vec2i firstPos, Vec2i secondPos)
        {
            float x = firstPos.X - secondPos.X;
            float y = firstPos.Y - secondPos.Y;
            return (int)(System.MathF.Sqrt(x * x + y * y) + 0.5f);
        }

        static public int manhattan_distance(Vec2i firstPos, Vec2i secondPos)
        {
            int x = firstPos.X - secondPos.X;
            int y = firstPos.Y - secondPos.Y;
            return System.Math.Abs(x) + System.Math.Abs(y);
        }

        // https://www.researchgate.net/publication/32117120_Efficient_path_finding_for_2D_games
        static public int overestimeted_distance(Vec2i firstPos, Vec2i secondPos)
        {
            return manhattan_distance(firstPos, secondPos) * 4;
        }
    }
}
