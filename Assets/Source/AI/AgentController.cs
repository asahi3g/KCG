using System.Collections.Generic;
using Planet;
using AI.Sensor;

namespace AI
{
    public class AgentController
    {
        public int BehaviorTreeRoot;
        public BlackBoard BlackBoard;
        public SensorEntity[] Sensors;

        public void Update(AgentEntity agent, ref Planet.PlanetState planet)
        {
            for (int i = 0; i < Sensors.Length; i++)
            {
                AISystemState.Sensors[(int)Sensors[i].Type].Update(agent, in Sensors[i], ref BlackBoard, ref planet);
            }
        }
    }
}
