using Enums;
using System;
using System.Collections.Generic;

namespace AI.Sensor
{
    public class SensorBase
    {
        protected Tuple<string, Type> CreateEntry(string name, Type type) => new Tuple<string, Type>(name, type);

        public virtual SensorType Type => SensorType.Error;

        public virtual List<Tuple<string, Type>> GetBlackboardEntries()
        {
            return null;
        }
        public virtual void Update(AgentEntity agent, in SensorEntity sensor, ref BlackBoard blackBoard) { }
    }
}