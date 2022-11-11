//imports UnityEngine

using Entitas;

namespace Vehicle.Pod
{
    public sealed class MovementSystem
    {
        PodCreationApi PodCreationApi;

        // Constructor
        public MovementSystem(PodCreationApi podCreationApi)
        {
            PodCreationApi = podCreationApi;
        }

        public void UpdateEx()
        {
            // Get Vehicle Entites
            IGroup<PodEntity> entities =
                GameState.Planet.EntitasContext.pod.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var vehicle in entities)
            {

                PodProperties podProperties =
                        PodCreationApi.GetRef((int)vehicle.vehiclePodType.Type);

                // Process Gravity
                if (vehicle.vehiclePodPhysicsState2D.AffectedByGravity)
                    vehicle.vehiclePodPhysicsState2D.angularVelocity.Y += vehicle.vehiclePodPhysicsState2D.centerOfGravity *UnityEngine.Time.deltaTime;

                vehicle.vehiclePodPhysicsState2D.Position += vehicle.vehiclePodPhysicsState2D.angularVelocity * UnityEngine.Time.deltaTime;
            }
        }
    }
}

