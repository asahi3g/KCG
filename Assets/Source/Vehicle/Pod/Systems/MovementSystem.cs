//imports UnityEngine

using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;

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

        public void UpdateEx(PodContext podContexts)
        {
            // Get Vehicle Entites
            IGroup<PodEntity> entities =
            podContexts.GetGroup(PodMatcher.VehiclePodPhysicsState2D);
            foreach (var vehicle in entities)
            {
                if(!vehicle.vehiclePodStatus.Freeze)
                {
                    PodProperties podProperties =
                            PodCreationApi.GetRef((int)vehicle.vehiclePodType.Type);

                    // Process Gravity
                    if (vehicle.vehiclePodPhysicsState2D.AffectedByGravity)
                        vehicle.vehiclePodPhysicsState2D.angularVelocity.Y += vehicle.vehiclePodPhysicsState2D.centerOfGravity * UnityEngine.Time.deltaTime;

                    vehicle.vehiclePodPhysicsState2D.Position += vehicle.vehiclePodPhysicsState2D.angularVelocity * UnityEngine.Time.deltaTime;
                }
            }
        }
    }
}

