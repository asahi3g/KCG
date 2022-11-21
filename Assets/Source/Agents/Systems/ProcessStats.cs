namespace Agent
{
    public class ProcessStats
    {
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            ref AgentList agentList = ref planet.AgentList;

            for (int i = 0; i < agentList.Length; i++)
            {
                AgentEntity agentEntity = agentList.Get(i);
                
                if (agentEntity.agentStats.Health.GetValue() <= 0 && agentEntity.isAgentAlive)
                {
                    planet.KillAgent(i);
                    if (!agentEntity.hasAgentModel3D)
                        continue;
                    if (agentEntity.agentModel3D.Weapon == null)
                        continue;
                    UnityEngine.GameObject.Destroy(agentEntity.agentModel3D.Weapon);
                }
            }
        }
    }
}
