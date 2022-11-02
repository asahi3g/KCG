using Enums;
using System.Collections.Generic;

namespace AI.Sensor
{
    public struct SensorEntity
    {
        public SensorType Type;
        public List<int> EntriesID;

        public SensorEntity(SensorType type, List<int> entries)
        {
            Type = type;
            EntriesID = new List<int>(entries);
        }

        public SensorEntity(SensorType type)
        {
            Type = type;
            EntriesID = new List<int>();
        }
    }
}
