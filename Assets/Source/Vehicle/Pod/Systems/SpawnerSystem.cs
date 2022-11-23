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
            entity.AddVehiclePodStatus(Members, podProperties.DefaultAgentCount, true, true, true, true, false, false, false, false, false, Vec2f.Zero, Vec2f.Zero, Vec2f.Zero,
                Vec2f.Zero, podProperties.RightPanel, podProperties.LeftPanel, podProperties.TopPanel, podProperties.BottomPanel, podProperties.RightPanelWidth, podProperties.LeftPanelWidth,
                podProperties.TopPanelWidth, podProperties.BottomPanelWidth, podProperties.RightPanelHeight, podProperties.LeftPanelHeight, podProperties.TopPanelHeight, podProperties.BottomPanelHeight);

            for (int i = 0; i <= podProperties.DefaultAgentCount; i++)
            {
                var enemy = GameState.Planet.AddAgent(Vec2f.Zero, AgentType.EnemyMarine, 1);
                enemy.agentModel3D.GameObject.gameObject.SetActive(false);
                enemy.isAgentAlive = false;

                entity.vehiclePodStatus.AgentsInside.Add(enemy);

                enemy.agentPhysicsState.Position = entity.vehiclePodPhysicsState2D.Position;
            }

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
