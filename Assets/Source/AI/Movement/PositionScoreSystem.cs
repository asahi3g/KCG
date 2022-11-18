

using KMath;

namespace AI.Movement
{
    // Evaluate score of positions around AI.
    public class PositionScoreSystem
    {
        public Vec2f[] Positions;

        // Basic score Account for :
        //      1) Density of allies.
        //      2) (Todo)Cover value.
        // Todo: Diffirent values for different types of properties.
        public int[] BasicScore;
        public int Length;

        public PositionScoreSystem()
        {
            Positions = new Vec2f[1024];
            BasicScore = new int[1024];
            Length = 0;
        }

        // - Draw a circle of radius maximum-firing-range around the player
        // -Distribute points along the circle evenly
        // - Find points around the circle that 1) are reachable 2) have a line of sight to the player
        // -Move nearby enemies to those points while firing on the player.
        public void Update()
        {
            // Todo: Make range an attribute.
            const float Range = 20.0f;
            // Todo Get enemy pos
            Vec2f enemyPos = new Vec2f();

            // 1) Get all tiles inside range? 
            // 2) how to eliminate more tiles at the beggining?
                // eliminate all in a range above player.
                // eliminate all in a range below player
                // only look in positions closer after dealing with the furthest ones? 
                // Get agent closer to the player. Get agent farthest. (Use position from closes to edge of the circle)
                // Or from edge to 8 tiles of edge.
                // If all position is taken do the same for next 8 tiles.
            // 2) Filter out all positions in the air. (Todo: how to deal with flying agents.)
            // 3) Filter out all positions that can't see the player.
            
            // For now consider map completely flat.
            // points two blocks of distance of each other.


            // How to calculate agent density?
            // get point
            // broadphase test around point.
            // what's the raidus of the test?
            // If density is less than 3. Score increases.


        }

        // Compare (basic score + agent specific score) of all positions and returns position with highest score.
        // Agent specific score:
        //      1) Distance from position.
        public Vec2f GetHighestScorePosition(AgentEntity agent)
        {
            int idealPosIndex = 0;
            int highestTotalScore = 0;

            for (int i = 0; i < Length; i++)
            {
                int agentScore = 0;
                // Account for distance to position.
                float distance = Heuristics.ManhattanDistance(agent.agentPhysicsState.Position, Positions[idealPosIndex]);
                agentScore = (int)distance * 100;

                int totalScore = BasicScore[idealPosIndex] + agentScore;
                if (totalScore > highestTotalScore)
                { 
                    idealPosIndex = i;
                    highestTotalScore = totalScore;
                }

            }

            return new Vec2f(0, 0);
        }
    }
}
