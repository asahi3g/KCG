using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;
using Particle;

namespace Vehicle
{
    public sealed class AISystem
    {
        private VehicleCreationApi VehicleCreationApi;
        private VehicleEntity vehicle;
        private Vec2f particlePosition;
        private Vec2f movementSpeed;

        // Constructor
        public AISystem(VehicleCreationApi vehicleCreationApi)
        {
            VehicleCreationApi = vehicleCreationApi;
        }

        public void RunAI(VehicleEntity _vehicle, Vec2f _particlePosition, Vec2f _movementSpeed)
        {
            vehicle = _vehicle;
            particlePosition = _particlePosition;
            movementSpeed = _movementSpeed;
        }

        public void Update()
        {
            if (vehicle == null || particlePosition == null || movementSpeed == null)
                return;

            vehicle.vehiclePhysicsState2D.angularVelocity += movementSpeed * Time.deltaTime;

            CircleSmoke.Spawn(1, particlePosition, new Vec2f(4.0f, 1.0f), new Vec2f(1.0f, 0.4f));
        }
    }
}

