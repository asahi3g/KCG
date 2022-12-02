using Entitas;
using KMath;
using Collisions;
using CollisionsTest;
using UnityEngine;
using System;
using Particle;
using Agent;

namespace Vehicle.Pod
{
    public sealed class AISystem
    {
        private AABox2D entityBoxBorders;
        private bool landed = false;
        private float elapsed = 0.0f;

        public void Update()
        {
            ref var planet = ref GameState.Planet;

            IGroup<PodEntity> pods = planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var pod in pods)
            {
                if(pod.hasVehiclePodStatus)
                {
                    if (pod.hasVehiclePodPhysicsState2D)
                    {
                        entityBoxBorders = new AABox2D(pod.vehiclePodPhysicsState2D.Position + new Vec2f(0, -1f), new Vec2f(0.5f, 50.0f));
                    
                        if (!IsPathEmpty(pod))
                        {
                            if(pod.vehiclePodPhysicsState2D.angularVelocity.Y == 0.0f)
                            {
                                if (!landed)
                                    CircleSmoke.SpawnExplosion(pod, 500, pod.vehiclePodPhysicsState2D.Position + Vec2f.Zero, new Vec2f(0, 0), new Vec2f(2.5f, 2.5f));

                                landed = true;
                                elapsed += Time.deltaTime;

                                pod.vehiclePodStatus.Exploded = true;

                                if(elapsed > 0.5f)
                                {
                                    if (elapsed > 2.0f)
                                    {
                                        var agentsInside = pod.vehiclePodStatus.AgentsInside;
                                        if (pod.hasVehiclePodStatus)
                                        {
                                            int count = agentsInside.Count;
                                            for (int j = 0; j <= count; j++)
                                            {
                                                AgentEntity agentEntity = agentsInside[j];
                                                
                                                if (pod.vehiclePodStatus.DefaultAgentCount > 0)
                                                {
                                                    agentEntity.agentAgent3DModel.SetIsActive(true);
                                                    agentEntity.isAgentAlive = true;

                                                    agentEntity.agentPhysicsState.Position = pod.vehiclePodPhysicsState2D.Position;

                                                    agentEntity.agentPhysicsState.Velocity.X += UnityEngine.Random.Range(5, 40);
                                                    agentEntity.agentPhysicsState.Velocity.Y += UnityEngine.Random.Range(10, 50);

                                                    pod.vehiclePodStatus.DefaultAgentCount--;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsPathEmpty(PodEntity podEntity)
        {
            // If is colliding bottom-top stop y movement
            if (entityBoxBorders.IsCollidingTop(GameState.Planet.TileMap, podEntity.vehiclePodPhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingBottom(podEntity.vehiclePodPhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingRight(GameState.Planet.TileMap, podEntity.vehiclePodPhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingLeft(podEntity.vehiclePodPhysicsState2D.angularVelocity))
            {
                return false;
            }

            return true;
        }
    }
}

