using UnityEngine;
using System.Collections.Generic;
using Entitas;
using Enums;
using Enums.Tile;
using KMath;
using Unity.VisualScripting;
using Sprites;

namespace Projectile
{
    public class SpawnerSystem
    {
        // Projectile ID
        private static int UniqueID;
        ProjectileCreationApi ProjectileCreationApi;

        public SpawnerSystem(ProjectileCreationApi projectileCreationApi)
        {
            ProjectileCreationApi = projectileCreationApi;
        }

        public ProjectileEntity Spawn(ProjectileContext projectileContext, Vec2f position, Vec2f direction, 
                                Enums.ProjectileType projectileType, bool isFirstSolid)
        {
            ProjectileProperties projectileProperties = 
                                    ProjectileCreationApi.GetRef((int)projectileType);

            ProjectileEntity entity = projectileContext.CreateEntity();
            entity.AddProjectileID(UniqueID++, -1);
            entity.AddProjectileRamp(projectileProperties.canRamp, projectileProperties.StartVelocity, projectileProperties.StartVelocity, projectileProperties.rampTime);
            entity.AddProjectileLinearDrag(projectileProperties.linearDrag, projectileProperties.linearCutOff);
            entity.AddProjectileSprite2D(projectileProperties.SpriteId, projectileProperties.Size);
            entity.AddProjectilePhysicsState(
                newPosition: position,
                newPreviousPosition: position,
                newRotation: 0.0f,
                newVelocity: direction.Normalized * projectileProperties.Speed,
                newAcceleration: projectileProperties.Acceleration,
                newAffectedByGravity: projectileProperties.AffectedByGravity,
                newAngularVelocity: Vec2f.Zero,
                newAngularMass: 1.0f, 
                newAngularAcceleration: 1.0f,
                newCenterOfGravity: 0.5f,
                newCenterOfRotation: Vec2f.Zero);
            
            entity.AddPhysicsBox2DCollider(projectileProperties.Size, Vec2f.Zero);

            entity.AddProjectileCollider(isFirstSolid, true);
            entity.AddProjectileType(projectileType, Enums.ProjectileDrawType.Standard);

            if (projectileProperties.HasAnimation)
            {
                 entity.AddAnimationState(1.0f, new Animation.Animation{Type=(int)projectileProperties.AnimationType});
            }

            return entity;

        }
    }
}
