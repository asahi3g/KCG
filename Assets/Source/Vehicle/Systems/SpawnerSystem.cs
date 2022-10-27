using System.Collections.Generic;
using KMath;
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

<<<<<<< HEAD
        public VehicleEntity Spawn(Planet.PlanetState planet, VehicleType vehicleType, Vec2f position)
=======
        public VehicleEntity Spawn(VehicleType vehicleType, Vec2f position)
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
        {
            VehicleProperties vehicleProperties =
                                    VehicleCreationApi.GetRef((int)vehicleType);

            // Create Entity.
            // Add components to entity. (id, sprite, physics,
            //  collider, type, radar, thruster, height map, capacity).
            // Add default agents 


            ref var planet = ref GameState.Planet;
            var entity = planet.EntitasContext.vehicle.CreateEntity();

            entity.AddVehicleID(UniqueID, -1);

            entity.AddVehicleSprite2D(vehicleProperties.SpriteId, vehicleProperties.SpriteSize);

            entity.AddVehiclePhysicsState2D(position, position, vehicleProperties.Scale, vehicleProperties.Scale, vehicleProperties.Rotation, vehicleProperties.AngularVelocity, vehicleProperties.AngularMass,
                vehicleProperties.AngularAcceleration, vehicleProperties.CenterOfGravity, vehicleProperties.CenterOfRotation, vehicleProperties.AffectedByGravity);

            entity.AddPhysicsBox2DCollider(vehicleProperties.CollisionSize, vehicleProperties.CollisionOffset);

            entity.AddVehicleType(vehicleType, false);

            List<PodEntity> pods = new List<PodEntity>();
            entity.AddVehicleRadar(pods);

            entity.AddVehicleThruster(vehicleProperties.Jet, vehicleProperties.JetAngle, JetSize.None, true);

            entity.AddVehicleThrusterSprite2D(vehicleProperties.ThrusterSpriteId, vehicleProperties.ThrusterSpriteSize, 
                vehicleProperties.Thruster1Position, vehicleProperties.Thruster2Position);

            entity.AddVehicleHeightMap(false, Vec2f.Zero);

            if (vehicleType == VehicleType.DropShip)
            {
                List<AgentEntity> agentsInside = new List<AgentEntity>();
                entity.AddVehicleCapacity(agentsInside);

                GameState.VehicleAISystem.Initialize(entity, new Vec2f(1.1f, 0.0f), new Vec2f(0f, 3.0f));
                GameState.VehicleAISystem.RunAI(entity, new Vec2f(1.1f, 0.0f), new Vec2f(0f, 3.0f));

                for(int i = 0; i <= vehicleProperties.DefaultAgentCount; i++)
                {
                    var enemy = planet.AddAgent(Vec2f.Zero, AgentType.EnemyInsect);
                    enemy.agentModel3D.GameObject.gameObject.SetActive(false);
                    enemy.isAgentAlive = false;
                    enemy.agentAction.Action = Agent.AgentAlertState.Alert;

                    entity.vehicleCapacity.agentsInside.Add(enemy);
                }
            }

            // Increase ID per object statically
            UniqueID++;

            // Return projectile entity
            return entity;
        }
    }
}
