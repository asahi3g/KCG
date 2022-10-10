using System.Collections.Generic;
using Planet;
using AI.Sensor;

namespace AI
{
    public class AgentController
    {
        public int BehaviorTreeRoot;
        public BlackBoard BlackBoard;
        public List<SensorBase> Sensors;

        public void AttachSensors(SensorBase sensor)
        {
            Sensors.Add(sensor);
        }

        public void Update(ref PlanetState planet)
        {
            foreach (var sensor in Sensors)
            {
                sensor.Update(BlackBoard, ref planet);
            }
        }
    }
}
