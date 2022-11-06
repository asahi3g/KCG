using Collisions;
using KMath;
using Planet;

namespace Sensor
{
    public class Sight
    {
        public static void Update(in Sensor sensor)
        {
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(sensor.OwnerID);

            // Todo: Get agent sight.
            ref CircleSector circleSector = ref agent.agentsLineOfSight.ConeSight;
            Vec2f pos = agent.agentPhysicsState.Position;
            pos += agent.physicsBox2DCollider.Size / 2.0f;
            circleSector.StartPos = pos;
            circleSector.Dir = new Vec2f(agent.agentPhysicsState.FacingDirection, 0.0f);

            // it's not use sight vision.
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity target = planet.AgentList.Get(i);
                if (agent.agentID.ID == target.agentID.ID)
                    continue;
                LineOfSight.CanSeeAlert(agent.agentID.ID, target.agentID.ID);
            }
        }
    }
}
