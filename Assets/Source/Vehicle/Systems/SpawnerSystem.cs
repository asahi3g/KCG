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

        public VehicleEntity Spawn(ref Planet.PlanetState planet, VehicleType vehicleType, Vec2f position)
        {
            VehicleProperties vehicleProperties =
                                    VehicleCreationApi.GetRef((int)vehicleType);

            // Create Entity
            var entity = planet.EntitasContext.vehicle.CreateEntity();

            // Add ID Component
            entity.AddVehicleID(UniqueID, -1);

            // Add Sprite Component
            entity.AddVehicleSprite2D(vehicleProperties.SpriteId, vehicleProperties.SpriteSize);

            // Add Physics State 2D Component
            entity.AddVehiclePhysicsState2D(position, position, vehicleProperties.Scale, vehicleProperties.Scale, vehicleProperties.Rotation, vehicleProperties.AngularVelocity, vehicleProperties.AngularMass,
                vehicleProperties.AngularAcceleration, vehicleProperties.CenterOfGravity, vehicleProperties.CenterOfRotation, vehicleProperties.AffectedByGravity);

            // Add Physics Box Collider Component
            entity.AddPhysicsBox2DCollider(vehicleProperties.CollisionSize, vehicleProperties.CollisionOffset);

            entity.AddVehicleType(vehicleType, false);

            List<PodEntity> pods = new List<PodEntity>();
            entity.AddVehicleRadar(pods);

            if(vehicleType == VehicleType.DropShip)
            {
                List<AgentEntity> agentsInside = new List<AgentEntity>();
                entity.AddVehicleCapacity(agentsInside);

                GameState.VehicleAISystem.Initialize(entity, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));
                GameState.VehicleAISystem.RunAI(entity, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));

                for(int i = 0; i < vehicleProperties.DefaultAgentCount; i++)
                {
                    var agent = planet.AddAgent(Vec2f.Zero, AgentType.EnemyInsect);
                    agent.agentModel3D.GameObject.gameObject.SetActive(false);
                    agent.isAgentAlive = false;

                    entity.vehicleCapacity.agentsInside.Add(agent);
                }
            }

            // Increase ID per object statically
            UniqueID++;

            // Return projectile entity
            return entity;
        }
    }
}
