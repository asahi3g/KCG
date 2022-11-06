using UnityEngine.UIElements;

namespace Sensor
{
    public struct Sensor
    {
        public Sensor(int id, SensorType type, int agentID, int ticks)
        {
            ID = id;
            Type = type;
            OwnerID = agentID;
            Tick = ticks;
            TickSinceLast = 0;
        }

        public readonly int ID;
        public readonly SensorType Type;
        public readonly int OwnerID;        // Id of agent using sensor.
        public readonly int Tick;           // How many ticks it takes to update sensor.
        public int TickSinceLast;           // How many ticks since last update.
    }
}
