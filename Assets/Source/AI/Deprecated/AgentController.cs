using AI.Sensor;
using Enums;

namespace AI
{
    public class AgentController
    {
        public BehaviorType behaviorType;
        public int BehaviorTreeRoot;
        public BlackBoard BlackBoard;
        public SensorEntity[] Sensors;

        public void Update(AgentEntity agent)
        {
            for (int i = 0; i < Sensors.Length; i++)
            {
                AISystemState.Sensors[(int)Sensors[i].Type].Update(agent, in Sensors[i], ref BlackBoard);
            }
        }
    }
}
