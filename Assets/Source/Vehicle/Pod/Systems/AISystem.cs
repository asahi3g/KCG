using Entitas;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;
using Particle;
using static UnityEditor.PlayerSettings;
using System.Drawing;
using Collisions;
using Unity.Mathematics;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Vehicle.Pod
{
    public sealed class AISystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            IGroup<PodEntity> pods =
                planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var pod in pods)
            {
                var size = pod.vehiclePodRadar.RadarSize;

                int[] agentIds = Collisions.Collisions.BroadphaseAgentBoxTest(planet,
                    new KMath.AABox2D(new Vec2f(pod.vehiclePodPhysicsState2D.Position.X - 4.0f, pod.vehiclePodPhysicsState2D.Position.Y - 2.0f), size));

                pod.vehiclePodRadar.AgentCount = pod.vehiclePodRadar.Members.Count;

                pod.vehiclePodRadar.Members.Clear();

                // Add all agents in bounds to members of radar.
                // If Player was in radar, alert all enemies.

                for(int i = 0; i < agentIds.Length; i++)
                {
                    var agent = planet.EntitasContext.agent.GetEntityWithAgentID(agentIds[i]);

                    if (pod.hasVehiclePodRadar)
                    {
                        if (!pod.vehiclePodRadar.Members.Contains(agent))
                        {
                            pod.vehiclePodRadar.Members.Add(agent);
                        }

                        if (pod.vehiclePodRadar.Members.Contains(planet.Player))
                        {
                            var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AgentID);
                            foreach (var entity in agents)
                            {
                                entity.agentAction.Action = Agent.AgentAction.Alert;
                            }
                        }
                    }
                }

                var roadCheckSizeX = 4.0f;
                var roadCheck = new KMath.AABox2D(pod.vehiclePodPhysicsState2D.Position, new Vec2f(roadCheckSizeX, 1.0f));

                if (roadCheck.IsCollidingRight(planet.TileMap, pod.vehiclePodPhysicsState2D.angularVelocity) ||
                    roadCheck.IsCollidingLeft(planet.TileMap, pod.vehiclePodPhysicsState2D.angularVelocity))
                {
                    roadCheck = new AABox2D(pod.vehiclePodPhysicsState2D.Position, new Vec2f(-roadCheckSizeX, 1.0f));

                    pod.vehiclePodPhysicsState2D.angularVelocity = -pod.vehiclePodPhysicsState2D.angularVelocity;
                }

            }
        }
    }
}

