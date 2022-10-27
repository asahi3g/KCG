using System.Collections.Generic;
using System;
using Enums;

namespace AI.Sensor
{
    public class EnemySensor : SensorBase
    {
        public override SensorType Type => SensorType.EnemySensor;

        public override List<Tuple<string, Type>> GetBlackboardEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("AnyEnemiesAlive", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void Update(AgentEntity agent, in SensorEntity sensor, ref BlackBoard blackBoard)
        {
            ref var planet = ref GameState.Planet;
            int AnyEnemiesAlive = sensor.EntriesID[0];

            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive)
                    continue;

                blackBoard.Set(AnyEnemiesAlive, true);
                return;
            }
            blackBoard.Set(AnyEnemiesAlive, false);
        }
    }
}
