namespace Sensor
{
    public class SensorManager
    {
        Sensor[] Sensors = new Sensor[1024];
        public int length;

        public ref Sensor Get(int id) => ref Sensors[id];

        public int CreateSensor(SensorType type, int agentID, int ticks)
        {
            Sensors[length] = new Sensor(length, type, agentID, ticks);
            return length++;
        }
    }
}
