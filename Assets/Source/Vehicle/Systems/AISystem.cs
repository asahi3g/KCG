using UnityEngine;
using Entitas;
using System.Collections;
using KMath;
using Projectile;
using Enums;
using UnityEngine.UIElements;
using Particle;
using static UnityEditor.PlayerSettings;
using System.Drawing;
using Collisions;
using Unity.Mathematics;

namespace Vehicle
{
    public sealed class AISystem
    {
        private VehicleCreationApi VehicleCreationApi;
        private VehicleEntity vehicle;
        private Vec2f particlePosition;
        private Vec2f movementSpeed;
        private AABox2D entityBoxBorders;

        // Constructor
        public AISystem(VehicleCreationApi vehicleCreationApi)
        {
            VehicleCreationApi = vehicleCreationApi;
        }

        public void Initialize(VehicleEntity _vehicle, Vec2f _particlePosition, Vec2f _movementSpeed)
        {
            vehicle = _vehicle;
            particlePosition = _particlePosition;
            movementSpeed = _movementSpeed;
        }

        public void RunAI(VehicleEntity _vehicle, Vec2f _particlePosition, Vec2f _movementSpeed)
        {
            vehicle = _vehicle;
            particlePosition = _particlePosition;
            movementSpeed = _movementSpeed;
        }

        public void StopAI()
        {
            if(vehicle != null)
            {
                vehicle.vehiclePhysicsState2D.angularVelocity = new Vec2f(0f, vehicle.vehiclePhysicsState2D.angularVelocity.Y);
                vehicle = null;
            }
        }

        public void Update(ref Planet.PlanetState planet)
        {
            if (vehicle == null || particlePosition == null || movementSpeed == null)
                return;

            if(vehicle.vehicleType.Type == VehicleType.DropShip)
            {
                CircleSmoke.Spawn(vehicle, 1, vehicle.vehiclePhysicsState2D.Position + particlePosition, new Vec2f(UnityEngine.Random.Range(-2f, 2f), -4.0f), new Vec2f(0.1f, 0.3f));

                entityBoxBorders = new AABox2D(new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y) + vehicle.physicsBox2DCollider.Offset,
                    new Vec2f(1.0f, -5));

                var skyCheck = new AABox2D(new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y) +                vehicle.physicsBox2DCollider.Offset,
                            new Vec2f(1.0f, 20));

                if(skyCheck.IsCollidingTop(planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
                {
                    return;
                }

                if (!GameState.VehicleAISystem.IsPathEmpty(ref planet))
                {
                    vehicle.vehiclePhysicsState2D.AffectedByGravity = false;

                    if (vehicle.hasVehicleCapacity)
                    {
                        var agentsInside = vehicle.vehicleCapacity.agentsInside;
                        for (int j = 0; j < agentsInside.Count; j++)
                        {
                            agentsInside[j].agentPhysicsState.Position = vehicle.vehiclePhysicsState2D.Position;
                            agentsInside[j].agentModel3D.GameObject.gameObject.SetActive(true);
                            agentsInside[j].isAgentAlive = true;
                        }
                    }

                    GameState.VehicleAISystem.StopAI();
                }

                var pods = planet.EntitasContext.pod.GetGroup(PodMatcher.AllOf(PodMatcher.VehiclePodID));
                foreach (var pod in pods)
                {
                    if(Vec2f.Distance(vehicle.vehiclePhysicsState2D.Position, pod.vehiclePodPhysicsState2D.Position) < 2.0f)
                    {
                        vehicle.vehicleRadar.podEntities.Add(pod);
                    }
                }
            }
            else if(vehicle.vehicleType.Type == VehicleType.Jet)
            {
                vehicle.vehiclePhysicsState2D.angularVelocity += movementSpeed * Time.deltaTime;

                CircleSmoke.Spawn(vehicle, 1, vehicle.vehiclePhysicsState2D.Position + particlePosition, new Vec2f(UnityEngine.Random.Range(-2f, 2f), -4.0f), new Vec2f(0.1f, 0.3f));

                entityBoxBorders = new AABox2D(new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y) + vehicle.physicsBox2DCollider.Offset,
                    new Vec2f(1.0f, 5));

                if (GameState.VehicleAISystem.IsPathEmpty(ref planet))
                {
                    vehicle.vehiclePhysicsState2D.AffectedByGravity = false;
                    GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));
                }
                else
                {
                    Debug.Log("LANDING");
                    movementSpeed = new Vec2f(movementSpeed.X, -25f);

                    if(entityBoxBorders.IsCollidingBottom(planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
                    {
                        vehicle.vehiclePhysicsState2D.AffectedByGravity = true;
                        GameState.VehicleAISystem.StopAI();
                    }
                }
            }
        }

        public bool IsPathEmpty(ref Planet.PlanetState planet)
        {
            if (vehicle == null || particlePosition == null || movementSpeed == null)
                return false;

            // If is colliding bottom-top stop y movement
            if (entityBoxBorders.IsCollidingTop(planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingRight(planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingLeft(planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }

            return true;
        }
    }
}

