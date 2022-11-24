using KMath;
using System;

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
        // -Distribute points    along the circle evenly
        // - Find points around the circle that 1) are reachable 2) have a line of sight to the player
        // -Move nearby enemies to those points while firing on the player.
        public void Update(int squadID)
        {
            ref Planet.PlanetState planet = ref GameState.Planet;
            // Todo: Make range an attribute.
            const int Range = 20;
            const int DensityRange = 5;

            // Todo Get enemy pos
            Vec2f enemyPos = GameState.Planet.Player.agentPhysicsState.Position;
            int x = (int)enemyPos.X;
            int y = (int)enemyPos.Y;

            // Todo function to get all tiles in radius
            // For now do horizontal check.
            // Initialize scores
            for (int j = 0, i = x - Range; i < x + Range; i++, j++)
            {
                Positions[j] = new Vec2f(i, y);
                BasicScore[j] = 0;
                Length = j;

                int[] agentIDs = Collisions.Collisions.BroadphaseAgentCircleTest(Positions[j], DensityRange);
                int length = 0;
                // if there is an agent in this tile drastically reduce score.
                foreach (int id in agentIDs)
                {
                    AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(id);
                    if (agent.agentID.Faction != squadID)
                        continue;
                    length++;
                    const int score = 10000;
                    if ((int)agent.agentPhysicsState.Position.X == x)
                        BasicScore[j] -= score;
                }

                // The position score decrease when higher than 3 and increase when smaller than this; 
                if (agentIDs.Length >= 3)
                {
                    int score = 500;
                    BasicScore[j] -= agentIDs.Length * score;
                }
                else 
                {
                    int score = 500;
                    BasicScore[j] += agentIDs.Length * score;
                }

            }

            // Get squad info.

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
            int bestPosIndex = 0;
            int highestTotalScore = 0;

            for (int i = 0; i < Length; i++)
            {
                int agentScore = 0;
                // Account for distance to position.
                float distance = Heuristics.ManhattanDistance(agent.agentPhysicsState.Position, Positions[bestPosIndex]);
                agentScore = (int)distance * 100;

                int totalScore = BasicScore[bestPosIndex] + agentScore;
                if (totalScore > highestTotalScore)
                {
                    bestPosIndex = i;
                    highestTotalScore = totalScore;
                }

            }

            return Positions[bestPosIndex];
        }

        public void UpdateEx()
        {
            // Should iterate for every faction we only have enemy faction.
            const int enemyfaction = 1;
            Update(enemyfaction);
        }
    }
}
