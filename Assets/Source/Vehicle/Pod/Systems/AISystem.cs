using Entitas;
using KMath;
using Collisions;

namespace Vehicle.Pod
{
    public sealed class AISystem
    {
        public void Update()
        {
            IGroup<PodEntity> pods = GameState.Planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
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

                        for(int i = 0; i < agentIds.Length; i++)
                        {
                            var agent = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(agentIds[i]);

                            if (pod.hasVehiclePodRadar)
                            {
                                if (!pod.vehiclePodRadar.Members.Contains(agent))
                                {
                                    if(agent.isAgentAlive)
                                        pod.vehiclePodRadar.Members.Add(agent);
                                    else
                                        pod.vehiclePodRadar.DeadMembers.Add(agent);
                                }

                                if (pod.vehiclePodRadar.Members.Contains(GameState.Planet.Player))
                                {
                                    var agents = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AgentID);
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

                            if (roadCheck.IsCollidingRight(GameState.Planet.TileMap, pod.vehiclePodPhysicsState2D.angularVelocity) ||
                                roadCheck.IsCollidingLeft(GameState.Planet.TileMap, pod.vehiclePodPhysicsState2D.angularVelocity))
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

