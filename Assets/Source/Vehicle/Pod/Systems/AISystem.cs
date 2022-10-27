using Entitas;
using KMath;
using Collisions;

namespace Vehicle.Pod
{
    public sealed class AISystem
    {
<<<<<<< HEAD
        public void Update(Planet.PlanetState planet)
=======
        public void Update()
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
        {
            ref var planet = ref GameState.Planet;
            
            IGroup<PodEntity> pods = planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var pod in pods)
            {
                if(pod.hasVehiclePodStatus)
                {
                    if(!pod.vehiclePodStatus.Freeze)
                    {
                        var size = pod.vehiclePodRadar.RadarSize;

                        // Get all agents in collision box
                        // Size of radar size (AABox2D)

                        int[] agentIds = Collisions.Collisions.BroadphaseAgentBoxTest(new AABox2D(new Vec2f(pod.vehiclePodPhysicsState2D.Position.X - 4.0f, pod.vehiclePodPhysicsState2D.Position.Y - 2.0f), size));

                        pod.vehiclePodStatus.Freeze = agentIds.Length <= 0;

                        if(pod.hasVehiclePodRadar)
                        {
                            pod.vehiclePodRadar.AgentCount = pod.vehiclePodRadar.Members.Count;

                            pod.vehiclePodRadar.Members.Clear();
                        }

                        // Add all agents in bounds to members of radar.
                        // If Player was in radar, alert all enemies.
                        // If Player was not alive, add to dead members array.
                        // Roadcheck - Scan the drive path to see if it's clear.

                        foreach (var agentID in agentIds)
                        {
                            var agent = planet.EntitasContext.agent.GetEntityWithAgentID(agentID);

                            if (pod.hasVehiclePodRadar)
                            {
                                if (!pod.vehiclePodRadar.Members.Contains(agent))
                                {
                                    if(agent.isAgentAlive)
                                        pod.vehiclePodRadar.Members.Add(agent);
                                    else
                                        pod.vehiclePodRadar.DeadMembers.Add(agent);
                                }

                                if (pod.vehiclePodRadar.Members.Contains(planet.Player))
                                {
                                    var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AgentID);
                                    foreach (var entity in agents)
                                    {
                                        entity.agentAction.Action = Agent.AgentAlertState.Alert;
                                    }
                                }
                            }
                        }

                        if(pod.hasVehiclePodPhysicsState2D)
                        {
                            var roadCheckSizeX = 4.0f;
                            var roadCheck = new AABox2D(pod.vehiclePodPhysicsState2D.Position, new Vec2f(roadCheckSizeX, 1.0f));

                            if (roadCheck.IsCollidingRight(pod.vehiclePodPhysicsState2D.angularVelocity) ||
                                roadCheck.IsCollidingLeft(pod.vehiclePodPhysicsState2D.angularVelocity))
                            {
                                roadCheck = new AABox2D(pod.vehiclePodPhysicsState2D.Position, new Vec2f(-roadCheckSizeX, 1.0f));

                                pod.vehiclePodPhysicsState2D.angularVelocity = -pod.vehiclePodPhysicsState2D.angularVelocity;
                            }
                        }
                    }
                }
            }
        }
    }
}

