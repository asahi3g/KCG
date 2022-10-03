using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;

namespace Pod
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
            podContexts.GetGroup(PodMatcher.PodPhysicsState2D);
            foreach (var vehicle in entities)
            {
                PodProperties podProperties =
                        PodCreationApi.GetRef((int)vehicle.podType.Type);

                // Process Gravity
                if (vehicle.podPhysicsState2D.AffectedByGravity)
                    vehicle.podPhysicsState2D.angularVelocity.Y += vehicle.podPhysicsState2D.centerOfGravity * Time.deltaTime;

                vehicle.podPhysicsState2D.Position += vehicle.podPhysicsState2D.angularVelocity * Time.deltaTime;
            }
        }
    }
}

