using Collisions;
using KMath;
using Planet;

namespace AI.Sensor
{
    public class Hearing : SensorBase
    {
        public override void RegisterInBlackBoard(BlackBoard blackboard)
        {
            VariableID = blackboard.Register(typeof(bool), "Can Hear Enemy");
        }
        public override void Update(BlackBoard blackboard, ref PlanetState planet)
        {
            const float MAX_DIST = 10f;
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(blackboard.OwnerAgentID);
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID)
                    continue;

                bool canHear = ((agent.agentPhysicsState.Position - entity.agentPhysicsState.Position).Magnitude < MAX_DIST);
                if (canHear)
                {
                    blackboard.Set(VariableID, true);
                    return;
                }
            }
            blackboard.Set(VariableID, false);
        }
    }
}
