using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;

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

        public void ProcessMovement(VehicleContext vehicleContexts, Vec2f newSpeed)
        {
            // Get Vehicle Entites
            IGroup<VehicleEntity> entities =
            vehicleContexts.GetGroup(VehicleMatcher.VehiclePhysicsState2D);
            foreach (var vehicle in entities)
            {
                // Get position from component
                var position = vehicle.vehiclePhysicsState2D;
                position.TempPosition = position.Position;

                // Update the position
                vehicle.ReplaceVehiclePhysicsState2D(position.Position, position.TempPosition, position.Scale, position.TempScale,
                    newSpeed, vehicle.vehiclePhysicsState2D.angularMass, vehicle.vehiclePhysicsState2D.angularAcceleration,
                         vehicle.vehiclePhysicsState2D.centerOfGravity, vehicle.vehiclePhysicsState2D.centerOfRotation);

                // Add velocity to position
                position.Position += vehicle.vehiclePhysicsState2D.angularVelocity * Time.deltaTime;

                // Update the position
                vehicle.ReplaceVehiclePhysicsState2D(position.Position, position.TempPosition, position.Scale, position.TempScale,
                    vehicle.vehiclePhysicsState2D.angularVelocity, vehicle.vehiclePhysicsState2D.angularMass, vehicle.vehiclePhysicsState2D.angularAcceleration,
                         vehicle.vehiclePhysicsState2D.centerOfGravity, vehicle.vehiclePhysicsState2D.centerOfRotation);

                return;

            }
        }
    }
}

