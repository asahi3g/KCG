using UnityEngine;
using System.Collections.Generic;
using Entitas;
using KMath;
using Projectile;
using Enums;

namespace Vehicle
{
    public class SpawnerSystem
    {
        // Projectile ID
        private static int UniqueID;
        VehicleCreationApi VehicleCreationApi;

        public SpawnerSystem(VehicleCreationApi vehicleCreationApi)
        {
            VehicleCreationApi = vehicleCreationApi;
        }

        public VehicleEntity Spawn(VehicleContext contexts, VehicleType vehicleType, Vec2f position)
        {
            VehicleProperties vehicleProperties =
                                    VehicleCreationApi.GetRef((int)vehicleType);

            // Create Entity
            var entity = contexts.CreateEntity();

            // Add ID Component
            entity.AddVehicleID(UniqueID, -1);

            // Add Sprite Component
            entity.AddVehicleSprite2D(vehicleProperties.SpriteId, vehicleProperties.SpriteSize);

            // Add Physics State 2D Component
            entity.AddVehiclePhysicsState2D(position, position, vehicleProperties.Scale, vehicleProperties.Scale, vehicleProperties.AngularVelocity, vehicleProperties.AngularMass,
                vehicleProperties.AngularAcceleration, vehicleProperties.CenterOfGravity, vehicleProperties.CenterOfRotation);

            // Add Physics Box Collider Component
            entity.AddPhysicsBox2DCollider(vehicleProperties.CollisionSize, Vec2f.Zero);

            entity.AddVehicleType(vehicleType);


            // Increase ID per object statically
            UniqueID++;

            // Return projectile entity
            return entity;
        }
    }
}
