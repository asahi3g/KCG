using Entitas;
using KMath;
using Collisions;
using CollisionsTest;

namespace Vehicle.Pod
{
    public sealed class AISystem
    {
        private AABox2D entityBoxBorders;

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
                        var groundCheck = -2.0f;
                        var roadCheck = new AABox2D(pod.vehiclePodPhysicsState2D.Position, new Vec2f(1.0f, groundCheck));
                    
                        if (roadCheck.IsCollidingBottom(pod.vehiclePodPhysicsState2D.angularVelocity))
                        {
                            pod.vehiclePodPhysicsState2D.angularVelocity = Vec2f.Zero;
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

