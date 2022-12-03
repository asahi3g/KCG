using KMath;
using System;
using UnityEngine;
using AI.SquadBehvaior;

namespace AI.Movement
{
    // Evaluate score of positions around AI.
    public class PositionScoreSystem
    {
        // Basic score Account for :
        //      1) Density of allies.
        //      2) (Todo)Cover value.
        // Todo: Diffirent values for different types of properties.
        public const int OccupiedScore = -10000;
        public const int DensityScore = 500; // Cost increase for increasing the agent's Density by one unit in the tile's region.

        // - Draw a circle of radius maximum-firing-range around the player
        // -Distribute points    along the circle evenly
        // - Find points around the circle that 1) are reachable 2) have a line of sight to the player
        // -Move nearby enemies to those points while firing on the player.
        public void Update(int squadID)
        {
            ref Planet.PlanetState planet = ref GameState.Planet;
            ref Squad squad = ref GameState.SquadManager.Get(squadID);
            // Todo: Make range an attribute.
            const int Range = 30;
            const int DensityRange = 8;

            // Todo Cache enemies in squad.cs and get choosen enemy position instead of player.
            if (planet.Player == null)
                return;
            Vec2f enemyPos = planet.Player.agentPhysicsState.Position;
            int x = (int)enemyPos.X;
            int y = (int)enemyPos.Y;

            // Todo use same logic as collision to get surface tiles.
            // Add positions and initialize score
            float pos = x - Range;
            for (int j = 0; pos < x + Range; j++)
            {
                squad.CombatPositions[j] = new Vec2f(pos, y);
                squad.PositionsScore[j] = 0;
                squad.PositionsLength = j;
                pos += 1.5f;
            }
            
            // Todo function to get all tiles in radius..
            // Initialize scores
            int[] agentIDs = Collisions.Collisions.BroadphaseAgentCircleTest(enemyPos, Range);
            for (int j = 0; j < squad.PositionsLength; j++)
            {
                int density = 0; // Number of agents inside density range.
                
                // If there is an agent in this tile drastically reduce score.
                foreach (int id in agentIDs)
                {
                    AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(id);
                    if (agent.agentID.SquadID != squadID)
                        continue;
                    // Use goal position not current position
                    Vec2f TargetPos = GameState.BlackboardManager.Get(agent.agentController.BlackboardID).MoveToTarget;
                    if (KMath.KMath.AlmostEquals(TargetPos.X, squad.CombatPositions[j].X, precision: 0.3f))
                    {
                        squad.PositionsScore[j] += OccupiedScore;
                    }
                    if (MathF.Abs(TargetPos.X - squad.CombatPositions[j].X) <= DensityRange)
                    {
                        density++;
                    }
                }

                // The position score decrease when higher than 3 and increase when smaller than this; 
                if (density > 3)
                {
                    int score = 500;
                    squad.PositionsScore[j] -= (density - 3) * score;
                }
                else 
                {
                    int score = 500;
                    squad.PositionsScore[j] += density * score; 
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
            if (agent.agentID.SquadID == -1)
            {
                Debug.Log("Agent with no squad.");
                return agent.agentPhysicsState.Position;
            }
            ref Squad squad = ref GameState.SquadManager.Get(agent.agentID.SquadID);

            int bestPosIndex = 0;
            int highestTotalScore = int.MinValue;

            for (int i = 0; i < squad.PositionsLength; i++)
            {
                int agentScore = 0;

                // Account for distance to position.
                Vec2f TargetPos = GameState.BlackboardManager.Get(agent.agentController.BlackboardID).MoveToTarget;
                if (KMath.KMath.AlmostEquals(TargetPos.X, squad.CombatPositions[i].X, precision: 0.1f))
                {
                    squad.PositionsScore[i] -= OccupiedScore;
                    agentScore += 4000;
                }
                else
                {
                    if (TargetPos == Vec2f.Zero)
                        TargetPos = agent.agentPhysicsState.Position;
                    float distance = Heuristics.ManhattanDistance(TargetPos, squad.CombatPositions[i]);
                    agentScore = -(int)distance * 200;
                }

                int totalScore = squad.PositionsScore[i] + agentScore;
                if (totalScore > highestTotalScore)
                {
                    bestPosIndex = i;
                    highestTotalScore = totalScore;
                }

            }

            squad.PositionsScore[bestPosIndex] += OccupiedScore;
            return squad.CombatPositions[bestPosIndex];
        }
    }
}
