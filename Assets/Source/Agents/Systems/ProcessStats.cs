using Entitas;
using Planet;

namespace Agent
{
    public class ProcessStats
    {
        public void Update(ref PlanetState planet)
        {
            ref AgentList agentList = ref planet.AgentList;

            for (int i = 0; i < agentList.Length; i++)
            {
                AgentEntity agentEntity = agentList.Get(i);
                
                if (agentEntity.agentStats.Health <= 0 && agentEntity.isAgentAlive)
                    planet.KillAgent(i);
            }
        }
    }
}
