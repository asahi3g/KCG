namespace Agent
{
    public class ProcessStats
    {
<<<<<<< HEAD
        public void Update(PlanetState planet)
=======
        public void Update()
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
        {
            ref var planet = ref GameState.Planet;
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
