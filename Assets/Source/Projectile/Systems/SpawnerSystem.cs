using UnityEngine;
using KMath;

namespace Projectile
{
    public class SpawnerSystem
    {
        private static int UniqueID;

        public ProjectileEntity Spawn(Vec2f position, Vec2f direction, 
            Enums.ProjectileType projectileType, int agentOwnerId, bool isFirstHit = true)
        {
            ProjectileProperties projectileProperties = 
                                    GameState.ProjectileCreationApi.GetRef((int)projectileType);

            ProjectileEntity entity = GameState.Planet.EntitasContext.projectile.CreateEntity();
            entity.isProjectileFirstFrame = true;
            entity.AddProjectileID(UniqueID++, -1, agentOwnerId);
            entity.AddProjectileStart(position, Time.realtimeSinceStartup);
            entity.AddProjectileLinearDrag(projectileProperties.LinearDrag, projectileProperties.LinearCutOff);
            entity.AddProjectileSprite2D(projectileProperties.SpriteId, projectileProperties.Size);
            entity.AddProjectilePhysicsState(
                newPosition: position,
                newPreviousPosition: position,
                newRotation: 0.0f,
                newVelocity: (direction.Normalized + Random.Range(-0.05f
                , 0.05f)) * projectileProperties.StartVelocity,
                newAcceleration: Vec2f.Zero,
                newOnGrounded: false);
            
            entity.AddPhysicsBox2DCollider(Vec2f.Zero, Vec2f.Zero);
            entity.AddProjectileType(projectileType, Enums.ProjectileDrawType.Standard);

            if (projectileProperties.HasAnimation)
                entity.AddAnimationState(1.0f, new Animation.Animation{Type=(int)projectileProperties.AnimationType});

            if (isFirstHit)
                entity.isProjectileFirstHIt = true;

            return entity;
        }

        public ProjectileEntity Spawn(Vec2f position, Vec2f direction,
            Enums.ProjectileType projectileType, int damage, int agentOwnerId, bool isFirstHit = true)
        {
            ProjectileEntity entity = Spawn(position, direction, projectileType, agentOwnerId, isFirstHit);
            entity.AddProjectileDamage(damage);

            return entity;
        }
    }
}
