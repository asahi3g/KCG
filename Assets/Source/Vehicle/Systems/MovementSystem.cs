using Entitas;

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

        public void UpdateEx()
        {
            // Get Vehicle Entites
            IGroup<VehicleEntity> entities =
                GameState.Planet.EntitasContext.vehicle.GetGroup(VehicleMatcher.VehiclePhysicsState2D);
            foreach (var vehicle in entities)
            {
                VehicleProperties vehicleProperties =
                        VehicleCreationApi.GetRef((int)vehicle.vehicleType.Type);

                var velocity = vehicle.vehiclePhysicsState2D.angularVelocity;

                // Process Gravity
                if(vehicle.vehiclePhysicsState2D.AffectedByGravity)
                    velocity.Y += vehicle.vehiclePhysicsState2D.centerOfGravity * UnityEngine.Time.deltaTime;

                if(vehicle.hasVehicleThruster)
                {
                    if(vehicle.vehicleThruster.Jet)
                        vehicle.vehiclePhysicsState2D.Position += vehicle.vehiclePhysicsState2D.angularVelocity * UnityEngine.Time.deltaTime;
                }
                else
                {
                    vehicle.vehiclePhysicsState2D.Position += vehicle.vehiclePhysicsState2D.angularVelocity * UnityEngine.Time.deltaTime;
                }

            }
        }
    }
}

