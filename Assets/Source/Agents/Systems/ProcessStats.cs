namespace Agent
{
    public class ProcessStats
    {
        public void Update()
        {
            ref AgentList agentList = ref GameState.Planet.AgentList;

            for (int i = 0; i < agentList.Length; i++)
            {
                AgentEntity agentEntity = agentList.Get(i);
                
                if (agentEntity.agentStats.Health <= 0 && agentEntity.isAgentAlive)
                    GameState.Planet.KillAgent(i);
            }
        }
    }
}
