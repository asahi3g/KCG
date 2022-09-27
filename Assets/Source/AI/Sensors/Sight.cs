using Collisions;
using KMath;
using Planet;

namespace AI.Sensor
{
    public class Sight : SensorBase
    {
        public override void RegisterInBlackBoard(BlackBoard blackboard)
        {
            VariableID = blackboard.Register(typeof(bool), "Has Enemy on Sight");
        }
        public override void Update(BlackBoard blackboard, ref PlanetState planet)
        {
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(blackboard.OwnerAgentID);
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity entity = planet.AgentList.Get(i);
                if (entity.agentID.ID == agent.agentID.ID)
                    continue;

                bool intersect = LineOfSight.CanSee(ref planet, agent.agentID.ID, entity.agentID.ID);
                if (intersect)
                {
                    blackboard.Set(VariableID, true);
                    return;
                }
            }
            blackboard.Set(VariableID, false);
        }
    }
}
