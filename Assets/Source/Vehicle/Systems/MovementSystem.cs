using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;

namespace Vehicle
{
    public sealed class MovementSystem
    {
        VehicleCreationApi VehicleCreationApi;

        // Constructor
        public MovementSystem(VehicleCreationApi vehicleCreationApi)
        {
            VehicleCreationApi = vehicleCreationApi;
        }

        public void UpdateEx(VehicleContext vehicleContexts)
        {
            // Get Vehicle Entites
            IGroup<VehicleEntity> entities =
            vehicleContexts.GetGroup(VehicleMatcher.VehiclePhysicsState2D);
            foreach (var vehicle in entities)
            {
                VehicleProperties vehicleProperties =
                        VehicleCreationApi.GetRef((int)vehicle.vehicleType.Type);

                // Process Gravity
                if(vehicleProperties.AffectedByGravity)
                    vehicle.vehiclePhysicsState2D.Position.Y += vehicle.vehiclePhysicsState2D.centerOfGravity * Time.deltaTime;

                vehicle.vehiclePhysicsState2D.Position += vehicle.vehiclePhysicsState2D.angularVelocity * Time.deltaTime;
            }
        }
    }
}

