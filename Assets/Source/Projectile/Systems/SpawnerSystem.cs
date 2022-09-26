using UnityEngine;
using KMath;

namespace Projectile
{
    public class SpawnerSystem
    {
        private static int UniqueID;

        public ProjectileEntity Spawn(ProjectileContext projectileContext, Vec2f position, Vec2f direction, 
            Enums.ProjectileType projectileType, bool isFirstHit = true)
        {
            ProjectileProperties projectileProperties = 
                                    GameState.ProjectileCreationApi.GetRef((int)projectileType);

            ProjectileEntity entity = projectileContext.CreateEntity();
            entity.isProjectileFirstFrame = true;
            entity.AddProjectileID(UniqueID++, -1);
            entity.AddProjectileStart(position, Time.realtimeSinceStartup);
            entity.AddProjectileLinearDrag(projectileProperties.LinearDrag, projectileProperties.LinearCutOff);
            entity.AddProjectileSprite2D(projectileProperties.SpriteId, projectileProperties.Size);
            entity.AddProjectilePhysicsState(
                newPosition: position,
                newPreviousPosition: position,
                newRotation: 0.0f,
                newVelocity: direction.Normalized * projectileProperties.StartVelocity,
                newAcceleration: Vec2f.Zero,
                newOnGrounded: false);
            
            entity.AddPhysicsBox2DCollider(projectileProperties.Size, Vec2f.Zero);
            entity.AddProjectileType(projectileType, Enums.ProjectileDrawType.Standard);

            if (projectileProperties.HasAnimation)
                entity.AddAnimationState(1.0f, new Animation.Animation{Type=(int)projectileProperties.AnimationType});

            if (isFirstHit)
                entity.isProjectileFirstHIt = true;

            return entity;
        }

        public ProjectileEntity Spawn(ProjectileContext projectileContext, Vec2f position, Vec2f direction,
            Enums.ProjectileType projectileType, int damage, bool isFirstHit = true)
        {
            ProjectileEntity entity = Spawn(projectileContext, position, direction, projectileType, isFirstHit);
            entity.AddProjectileDamage(damage);

            return entity;
        }
    }
}
