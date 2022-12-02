namespace Agent
{
    public class ProcessState
    {
        public void Update()
        {
            const int MaximumMapBoundaries = 10;

            ref var planet = ref GameState.Planet;
            ref AgentList agentList = ref planet.AgentList;

            for (int i = 0; i < agentList.Length; i++)
            {
                AgentEntity agentEntity = agentList.Get(i);
                var physicsState = agentEntity.agentPhysicsState;

                if (physicsState.Position.X < -MaximumMapBoundaries || physicsState.Position.X >= planet.TileMap.MapSize.X + MaximumMapBoundaries ||
                physicsState.Position.Y < -MaximumMapBoundaries || physicsState.Position.Y >= planet.TileMap.MapSize.Y + MaximumMapBoundaries)
                {
                    agentEntity.agentStats.Health.SetAsMin();
                }
                
                if (agentEntity.agentStats.Health.GetValue() <= 0 && agentEntity.isAgentAlive)
                {
                    planet.KillAgent(i);
                    if (!agentEntity.hasAgentAgent3DModel)
                        continue;
                    if (agentEntity.agentAgent3DModel.Weapon == null)
                        continue;
                    UnityEngine.GameObject.Destroy(agentEntity.agentAgent3DModel.Weapon);
                }
            }
        }
    }
}
