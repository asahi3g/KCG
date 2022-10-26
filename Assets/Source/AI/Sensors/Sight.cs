using Collisions;
using System.Collections.Generic;
using System;
using Enums;

namespace AI.Sensor
{
    public class Sight : SensorBase
    {
        public override SensorType Type => SensorType.Sight;

        public override List<Tuple<string, Type>> GetBlackboardEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("CanSee", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void Update(AgentEntity agent, in SensorEntity sensor, ref BlackBoard blackBoard)
        {
            int canSeedID = sensor.EntriesID[0];

            for (int i = 0; i < GameState.Planet.AgentList.Length; i++)
            {
                AgentEntity entity = GameState.Planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID)
                    continue;

                bool intersect = LineOfSight.CanSee(agent.agentID.ID, entity.agentID.ID);
                if (intersect)
                {
                    blackBoard.Set(canSeedID, true);
                    return;
                }
            }
            blackBoard.Set(canSeedID, false);
        }
    }
}
