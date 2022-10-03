using UnityEngine;
using System.Collections.Generic;
using Entitas;
using KMath;
using Projectile;
using Enums;

namespace Pod
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

        public PodEntity Spawn(PodContext contexts, PodType podType, Vec2f position)
        {
            PodProperties podProperties =
                                    PodCreationApi.GetRef((int)podType);

            // Create Entity
            var entity = contexts.CreateEntity();

            // Add ID Component
            entity.AddPodID(UniqueID, -1);

            // Add Sprite Component
            entity.AddPodSprite2D(podProperties.SpriteId, podProperties.SpriteSize);

            // Add Physics State 2D Component
            entity.AddPodPhysicsState2D(position, position, podProperties.Scale, podProperties.Scale, podProperties.Rotation, podProperties.AngularVelocity, podProperties.AngularMass,
                podProperties.AngularAcceleration, podProperties.CenterOfGravity, podProperties.CenterOfRotation, podProperties.AffectedByGravity);

            // Add Physics Box Collider Component
            entity.AddPhysicsBox2DCollider(podProperties.CollisionSize, podProperties.CollisionOffset);

            entity.AddPodType(podType);


            // Increase ID per object statically
            UniqueID++;

            // Return entity
            return entity;
        }
    }
}
