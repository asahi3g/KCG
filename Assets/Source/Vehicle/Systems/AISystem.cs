//imports UnityEngine

using KMath;
using Enums;
using Particle;
using Collisions;
using System;
using System.Collections.Generic;

namespace Vehicle
{
    public sealed class AISystem
    {
        private VehicleCreationApi VehicleCreationApi;
        private VehicleEntity vehicle;
        private Vec2f particlePosition;
        private Vec2f movementSpeed;
        private AABox2D entityBoxBorders;

        bool reLaunchStart = false;
        float remaningReLaunch = 0.0f;

        // Constructor
        public AISystem(VehicleCreationApi vehicleCreationApi)
        {
            VehicleCreationApi = vehicleCreationApi;
        }

        // Initialize Method.

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

        public void Update()
        {
            if (vehicle == null || particlePosition == null || movementSpeed == null)
                return;

            // Scan planet and find open-sky area.
            // Spawn vehicle to open sky area found.

            ref var planet = ref GameState.Planet;
            if(vehicle != null)
            {
                if(vehicle.hasVehicleType)
                {
                    if (vehicle.vehicleType.Type == VehicleType.DropShip)
                    {
                        if (reLaunchStart)
                        {
                            remaningReLaunch += UnityEngine.Time.deltaTime;

                            if (remaningReLaunch > 8.0f)
                            {
                                vehicle.vehiclePhysicsState2D.angularVelocity.Y += 0.01f;

                                if (remaningReLaunch > 30.0f)
                                {
                                    planet.RemoveVehicle(vehicle.vehicleID.Index);
                                    return;
                                }
                            }
                            else if (remaningReLaunch > 5.0f)
                            {
                                vehicle.vehiclePhysicsState2D.angularVelocity = new Vec2f(0f, 4.0f);
                                vehicle.vehicleThruster.Jet = true;
                            }
                        }

                        if (!vehicle.vehicleHeightMap.OpenSky)
                        {
                            for (int i = 0; i < planet.TileMap.MapSize.X; i++)
                            {
                                var tile = planet.TileMap.GetTile(i, 30);
                                if (tile.FrontTileID == Enums.PlanetTileMap.TileID.Air)
                                {
                                    vehicle.vehicleHeightMap.OpenSky = true;
                                    vehicle.vehicleHeightMap.SpawnPosition = new Vec2f(i - 4, planet.TileMap.MapSize.Y - 3);

                                    vehicle.vehiclePhysicsState2D.Position = vehicle.vehicleHeightMap.SpawnPosition;
                                }
                            }
                        }

                        // Spew out smoke if jet/ignition is on.

                        if (vehicle.hasVehicleThruster)
                        {
                            if (vehicle.vehicleThruster.Jet)
                            {
                                CircleSmoke.Spawn(vehicle, 1, vehicle.vehiclePhysicsState2D.Position + particlePosition, new Vec2f(UnityEngine.Random.Range(-2f, 2f), -8.0f), new Vec2f(0.1f, 0.3f));
                            }
                        }

                        // Sky and Land check for scan path.
                        // Check if path is clear.
                        // Pop out all passengers in the vehicle after landed.
                        // Scan all pods near by and add to the array.

                        entityBoxBorders = new AABox2D(new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y) + vehicle.physicsBox2DCollider.Offset,
                            new Vec2f(1.0f, -1));

                        if (!GameState.VehicleAISystem.IsPathEmpty())
                        {
                            vehicle.vehiclePhysicsState2D.AffectedByGravity = false;

                            List<AgentEntity> agentsInside = vehicle.vehicleCapacity.agentsInside;
                            if (vehicle.hasVehicleCapacity)
                            {
                                int count = agentsInside.Count;
                                
                                for (int j = 0; j < count; j++)
                                {
                                    AgentEntity agentEntity = agentsInside[j];
                                    
                                    if (!agentEntity.agentModel3D.IsActive)
                                    {
                                        agentEntity.agentModel3D.SetIsActive(true);

                                        agentEntity.agentPhysicsState.Velocity.X += UnityEngine.Random.Range(30, 360);
                                        agentEntity.agentPhysicsState.Velocity.Y += UnityEngine.Random.Range(25, 360);

                                        agentEntity.isAgentAlive = true;
                                        agentEntity.agentPhysicsState.Position = new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y);
                                        vehicle.vehicleThruster.Jet = false;
                                    }
                                    else
                                    {
                                        reLaunchStart = true;
                                    }
                                }
                            }
                        }

                        var pods = planet.EntitasContext.pod.GetGroup(PodMatcher.AllOf(PodMatcher.VehiclePodID));
                        foreach (var pod in pods)
                        {
                            if (Vec2f.Distance(vehicle.vehiclePhysicsState2D.Position, pod.vehiclePodPhysicsState2D.Position) < 2.0f)
                            {
                                vehicle.vehicleRadar.podEntities.Add(pod);
                            }
                        }
                    }
                    else if (vehicle.vehicleType.Type == VehicleType.Jet)
                    {
                        vehicle.vehiclePhysicsState2D.angularVelocity += movementSpeed * UnityEngine.Time.deltaTime;

                        CircleSmoke.Spawn(vehicle, 1, vehicle.vehiclePhysicsState2D.Position + particlePosition, new Vec2f(UnityEngine.Random.Range(-2f, 2f), -4.0f), new Vec2f(0.1f, 0.3f));

                        entityBoxBorders = new AABox2D(new Vec2f(vehicle.vehiclePhysicsState2D.Position.X, vehicle.vehiclePhysicsState2D.Position.Y) + vehicle.physicsBox2DCollider.Offset,
                            new Vec2f(1.0f, 5));

                        if (GameState.VehicleAISystem.IsPathEmpty())
                        {
                            vehicle.vehiclePhysicsState2D.AffectedByGravity = false;
                            GameState.VehicleAISystem.RunAI(vehicle, new Vec2f(1.1f, -2.8f), new Vec2f(0f, 3.0f));
                        }
                        else
                        {
                            UnityEngine.Debug.Log("LANDING");
                            movementSpeed = new Vec2f(movementSpeed.X, -25f);

                            if (entityBoxBorders.IsCollidingBottom(vehicle.vehiclePhysicsState2D.angularVelocity))
                            {
                                vehicle.vehiclePhysicsState2D.AffectedByGravity = true;
                                GameState.VehicleAISystem.StopAI();
                            }
                        }
                    }
                }
            }
        }

        // Check if giving path is empty.
        // Definition of method.

        public bool IsPathEmpty()
        {
            if (vehicle == null || particlePosition == null || movementSpeed == null)
                return false;

            // If is colliding bottom-top stop y movement
            if (entityBoxBorders.IsCollidingTop(GameState.Planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingRight(GameState.Planet.TileMap, vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }
            else if (entityBoxBorders.IsCollidingLeft(vehicle.vehiclePhysicsState2D.angularVelocity))
            {
                return false;
            }

            return true;
        }
    }
}

