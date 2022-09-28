using Planet;

namespace AI.Sensor
{
    public class EnemySensor : SensorBase
    {
        public override void RegisterInBlackBoard(BlackBoard blackboard)
        {
            VariableID = blackboard.Register(typeof(bool), "Enemies-alive");
        }
        public override void Update(BlackBoard blackboard, ref PlanetState planet)
        {
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(blackboard.OwnerAgentID);
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID || !entity.isAgentAlive)
                    continue;

                blackboard.Set(VariableID, true);
                return;
            }
            blackboard.Set(VariableID, false);
        }
    }
}
