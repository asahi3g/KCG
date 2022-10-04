using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;
using Particle;
using static UnityEditor.PlayerSettings;
using System.Drawing;
using Collisions;
using Unity.Mathematics;

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

                pod.vehiclePodRadar.AgentCount = pod.vehiclePodRadar.Agents.Count;

                pod.vehiclePodRadar.Agents.Clear();

                for(int i = 0; i < agentIds.Length; i++)
                {
                    if (pod.hasVehiclePodRadar)
                    {
                        if (!pod.vehiclePodRadar.Agents.Contains(planet.EntitasContext.agent.GetEntityWithAgentID(agentIds[i])))
                        {
                            pod.vehiclePodRadar.Agents.Add(planet.EntitasContext.agent.GetEntityWithAgentID(agentIds[i]));
                        }
                    }
                }
            }
        }
    }
}

