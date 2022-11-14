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

        public void Update()
        {
            ref var planet = ref GameState.Planet;

            IGroup<PodEntity> pods = planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var pod in pods)
            {
                if(pod.hasVehiclePodStatus)
                {
                    var agentsInside = pod.vehiclePodStatus.AgentsInside;

                    for (int i = 0; i < agentsInside.Count; i++)
                    {
                        
                    }
                    
                    if(pod.hasVehiclePodPhysicsState2D)
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

                                pod.vehiclePodStatus.RightPanel.X += 0.5f;
                                pod.vehiclePodStatus.RightPanel.Y += 0.1f;

                                pod.vehiclePodStatus.LeftPanel.X -= 0.5f;
                                pod.vehiclePodStatus.LeftPanel.Y -= 0.1f;

                                pod.vehiclePodStatus.BottomPanel.X -= 0.1f;
                                pod.vehiclePodStatus.BottomPanel.Y -= 0.5f;

                                pod.vehiclePodStatus.TopPanel.X += 0.1f;
                                pod.vehiclePodStatus.TopPanel.Y += 0.5f;


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

