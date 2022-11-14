using Entitas;
using KMath;
using Collisions;
using CollisionsTest;
using UnityEngine;
using System;
using Particle;

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
                        entityBoxBorders = new AABox2D(pod.vehiclePodPhysicsState2D.Position + new Vec2f(0, -10f), new Vec2f(0.5f, 50.0f));
                    
                        if (!IsPathEmpty(pod))
                        {
                            pod.vehiclePodPhysicsState2D.angularVelocity.X = Mathf.Lerp(pod.vehiclePodPhysicsState2D.angularVelocity.X, 0,
                                1.5f * Time.deltaTime);
                            pod.vehiclePodPhysicsState2D.angularVelocity.Y = Mathf.Lerp(pod.vehiclePodPhysicsState2D.angularVelocity.Y, 0,
                              1.5f * Time.deltaTime);

                            if(pod.vehiclePodPhysicsState2D.angularVelocity.Y > - 0.1f)
                            {
                                if (!landed)
                                    CircleSmoke.SpawnExplosion(pod, 500, pod.vehiclePodPhysicsState2D.Position + Vec2f.Zero, new Vec2f(0, 0), new Vec2f(2.5f, 2.5f));

                                landed = true;
                                elapsed += Time.deltaTime;

                                pod.vehiclePodStatus.RightPanel.X += 0.5f;
                                pod.vehiclePodStatus.RightPanel.Y += 0.1f;

                                pod.vehiclePodStatus.LeftPanel.X -= 0.5f;
                                pod.vehiclePodStatus.LeftPanel.Y -= 0.1f;

                                pod.vehiclePodStatus.BottomPanel.X -= 0.1f;
                                pod.vehiclePodStatus.BottomPanel.Y -= 0.5f;

                                pod.vehiclePodStatus.TopPanel.X += 0.1f;
                                pod.vehiclePodStatus.TopPanel.Y += 0.5f;

                                if(elapsed > 2.0f)
                                {
                                    pod.vehiclePodPhysicsState2D.AffectedByGravity = true;
                                    if(elapsed > 7.0f)
                                    {
                                        var agentsInside = pod.vehiclePodStatus.AgentsInside;
                                        if (pod.hasVehiclePodStatus)
                                        {
                                            for (int j = 0; j < agentsInside.Count; j++)
                                            {
                                                if (!agentsInside[j].agentModel3D.GameObject.gameObject.activeSelf)
                                                {
                                                    agentsInside[j].agentModel3D.GameObject.gameObject.SetActive(true);

                                                    agentsInside[j].agentPhysicsState.Velocity.X += UnityEngine.Random.Range(30, 360);
                                                    agentsInside[j].agentPhysicsState.Velocity.Y += UnityEngine.Random.Range(25, 360);

                                                    agentsInside[j].isAgentAlive = true;
                                                    agentsInside[j].agentPhysicsState.Position = new Vec2f
                                                        (pod.vehiclePodPhysicsState2D.Position.X, pod.vehiclePodPhysicsState2D.Position.Y);
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

