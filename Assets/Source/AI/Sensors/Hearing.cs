using System.Collections.Generic;
using System;
using Enums;

namespace AI.Sensor
{
    public class Hearing : SensorBase
    {
        public override SensorType Type => SensorType.Hearing;

        public override List<Tuple<string, Type>> GetBlackboardEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("CanHear", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void Update(AgentEntity agent, in SensorEntity sensor, ref BlackBoard blackBoard)
        {
            int canHearID = sensor.EntriesID[0];

            const float MAX_DIST = 10f;
            for (int i = 0; i < GameState.Planet.AgentList.Length; i++)
            {
                AgentEntity entity = GameState.Planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID)
                    continue;

                bool canHear = ((agent.agentPhysicsState.Position - entity.agentPhysicsState.Position).Magnitude < MAX_DIST);
                if (canHear)
                {
                    blackBoard.Set(canHearID, true);
                    return;
                }
            }
            blackBoard.Set(canHearID, false);
        }
    }
}
