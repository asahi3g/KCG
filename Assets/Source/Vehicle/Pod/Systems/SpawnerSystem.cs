using System.Collections.Generic;
using KMath;
using Enums;

namespace Vehicle.Pod
{
    public class SpawnerSystem
    {
        // Pod ID
        private static int UniqueID;
        PodCreationApi PodCreationApi;

        public SpawnerSystem(PodCreationApi podCreationApi)
        {
            PodCreationApi = podCreationApi;
        }

        public PodEntity Spawn(PodType podType, Vec2f position)
        {
            PodProperties podProperties =
                                    PodCreationApi.GetRef((int)podType);

            // Create Entity
            var entity = GameState.Planet.EntitasContext.pod.CreateEntity();

            // Add ID Component
            entity.AddVehiclePodID(UniqueID, -1);

            // Add Sprite Component
            entity.AddVehiclePodSprite2D(podProperties.SpriteId, podProperties.SpriteSize);

            // Add Physics State 2D Component
            entity.AddVehiclePodPhysicsState2D(position, position, podProperties.Scale, podProperties.Scale, podProperties.Rotation, podProperties.AngularVelocity, podProperties.AngularMass,
                podProperties.AngularAcceleration, podProperties.CenterOfGravity, podProperties.CenterOfRotation, podProperties.AffectedByGravity);

            // Add Physics Box Collider Component
            entity.AddPhysicsBox2DCollider(podProperties.CollisionSize, podProperties.CollisionOffset);

            entity.AddVehiclePodType(podType);

            PodState podState = PodState.None;
            entity.AddVehiclePodState(podState);

            List<AgentEntity> Members = new List<AgentEntity>();
            List<AgentEntity> DeadMembers = new List<AgentEntity>();
            entity.AddVehiclePodRadar(podProperties.RadarSize, Members, DeadMembers, Members.Count);

            entity.AddVehiclePodStatus(podProperties.PodValue, podProperties.Score, false);

            List<Vec2f> CoversPositions = new List<Vec2f>();
            List<Vec2f> FiringPositions = new List<Vec2f>();
            entity.AddVehiclePodCover(CoversPositions, FiringPositions);

            // Increase ID per object statically
            UniqueID++;

            // Return entity
            return entity;
        }
    }
}
